using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ServerApplication {
    public class Server {
        #region Attributes
        private static readonly Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private Socket player1Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private Socket player2Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private const int BUFFER_SIZE = 2048;
        private static readonly byte[] buffer = new byte[BUFFER_SIZE];
        private const int PORT = 100;

        private bool Client1Ready = false;
        private bool Client2Ready = false;
        private int currentPlayer = 1;
        private int otherPlayer = 2;

        private ServerCheckersGame currentGame;
        #endregion

        #region Constructors
        //Initialize and call SetupServer
        public Server() {

            Console.Title = "Server";
            SetupServer();
        }
        #endregion

        //Initialize server socket and async call to WaitForClient1
        public void SetupServer() {
            Console.WriteLine("Setting up server...");

            IPAddress ServerIP = null;
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList) {
                if (ip.AddressFamily == AddressFamily.InterNetwork) {
                    ServerIP = ip;
                    break;
                }
            }
            serverSocket.Bind(new IPEndPoint(ServerIP, PORT));
            Console.WriteLine(ServerIP);
            serverSocket.Listen(0);
            serverSocket.BeginAccept(WaitForClient1, null); //3. WAIT FOR PLAYER ONE, ASYNCHRONOUSLY
            Console.WriteLine("Server setup complete");
        }

        /// <summary>
        /// Close all connected client (we do not need to shutdown the server socket as its connections
        /// are already closed with the clients).
        /// </summary>
        public void CloseAllSockets() {
            player1Socket.Shutdown(SocketShutdown.Both);
            player1Socket.Close();
            player2Socket.Shutdown(SocketShutdown.Both);
            player2Socket.Close();

            serverSocket.Close();
        }

        //Setup Socket for client1, make sure its listening, and start async wait for client 2
        private void WaitForClient1(IAsyncResult AR) {
            Socket socket;

            try {
                socket = serverSocket.EndAccept(AR);
            }
            catch (ObjectDisposedException) // I cannot seem to avoid this (on exit when properly closing sockets)
            {
                return;
            }

            Console.WriteLine("1st Client Connected");

            player1Socket = socket; // if client connects, add it to a list 

            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket); // start waiting to recieve messages from client
            while (!Client1Ready) {

            }
            Client1Ready = false;

            byte[] appended = Encoding.ASCII.GetBytes("Waiting For Opponent");

            byte[] identifier = Encoding.ASCII.GetBytes(MessageIdentifiers.OnePlayerConnected.ToString("d"));
            byte[] data = Combine(identifier, appended);
            socket.Send(data);
            //------------------

            serverSocket.BeginAccept(WaitForClient2, null);//4. START WAITING FOR A SECOND PLAYER

        }

        private void WaitForClient2(IAsyncResult AR) {
            Socket socket;

            try {
                socket = serverSocket.EndAccept(AR);
            }
            catch (ObjectDisposedException) // I cannot seem to avoid this (on exit when properly closing sockets)
            {
                return;
            }

            Console.WriteLine("2nd Client Connected, Starting Game");

            player2Socket = socket; // if client connects, add it to a list 

            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket); // start waiting to recieve messages from client

            //------------------
            // Initialize our test class and set the message to Starting Game for player 2, serialize and send it to the client who just joined
            //------------------
            Sclass1 serializeMe = new Sclass1();
            serializeMe.SetMessage("Two Players Connected");
            serializeMe.SetPlayer("Player2");
            Console.WriteLine(serializeMe.GetMessage());
            byte[] appended;
            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream()) {
                formatter.Serialize(stream, serializeMe);
                appended = stream.ToArray();
            }
            //byte[] appended = Encoding.ASCII.GetBytes("test");

            byte[] identifier = Encoding.ASCII.GetBytes(MessageIdentifiers.TwoPlayersConnected.ToString("d"));
            byte[] data = Combine(identifier, appended);

            while (!Client2Ready) {

            }
            Client2Ready = false;

            socket.Send(data);

            //------------------

            //------------------
            //This one goes to the first client, letting them know the game is starting, and that they are player 1
            //------------------
            serializeMe = new Sclass1();
            serializeMe.SetMessage("Two Players Connected");
            serializeMe.SetPlayer("Player1");
            formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream()) {
                formatter.Serialize(stream, serializeMe);
                appended = stream.ToArray();
            }
            //byte[] appended = Encoding.ASCII.GetBytes("test");

            identifier = Encoding.ASCII.GetBytes(MessageIdentifiers.TwoPlayersConnected.ToString("d"));
            data = Combine(identifier, appended);
            //------------------

            while (!Client1Ready) {

            }
            Client1Ready = false;


            player1Socket.Send(data);

            //serverSocket.BeginAccept(WaitForClient2, null);// maybe this would tell client, this game is full

            StartingGame();

        }

        public void StartingGame() {
            currentGame = new ServerCheckersGame();
            Console.WriteLine("Starting Game");
            while (!Client1Ready || !Client2Ready) {

            }
            Console.WriteLine("Game Started");
            GameLoop();
        }

        public void GameLoop() {
            byte[] appendedCP = Encoding.ASCII.GetBytes("Your Turn");
            byte[] identifierCP = Encoding.ASCII.GetBytes(MessageIdentifiers.StartingGame.ToString("d"));
            byte[] CurrentPlayerData = Combine(identifierCP, appendedCP);
            CurrentPlayerData = Combine(identifierCP, appendedCP);

            byte[] appendedOP = Encoding.ASCII.GetBytes("Not Your Turn");
            byte[] identifierOP = Encoding.ASCII.GetBytes(MessageIdentifiers.StartingGame.ToString("d"));
            byte[] OtherPlayerData = Combine(identifierOP, appendedOP);
            OtherPlayerData = Combine(identifierOP, appendedOP);

            if(currentPlayer == 1)
            {
                player1Socket.Send(CurrentPlayerData);
                Client1Ready = false;
            }
            else
            {
                player2Socket.Send(CurrentPlayerData);
                Client2Ready = false;
            }
            if(otherPlayer == 1)
            {
                player1Socket.Send(OtherPlayerData);
                Client1Ready = false;
            }
            else
            {
                player2Socket.Send(OtherPlayerData);
                Client2Ready = false;
            }

            while (!Client1Ready || !Client2Ready) {

            }
            var temp = currentPlayer;
            currentPlayer = otherPlayer;
            otherPlayer = temp;
            GameLoop();
        }

        //Used to combine an identifier byte with a "message" byte[]
        public static byte[] Combine(byte[] first, byte[] second) {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }

        private void ReceiveCallback(IAsyncResult AR) {
            Socket current = (Socket)AR.AsyncState;

            int received;

            try {
                received = current.EndReceive(AR);
            }
            catch (SocketException) {
                Console.WriteLine("Client forcefully disconnected");
                // Don't shutdown because the socket may be disposed and its disconnected anyway.
                current.Close();
                return;
            }

            if (current == player1Socket) {
                Client1Ready = true;
                Console.WriteLine("Player 1 ready");


            }
            else if (current == player2Socket) {
                Client2Ready = true;
                Console.WriteLine("Player 2 ready");
            }

            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);

            //Console.WriteLine("Text is an invalid request");
            byte[] data = Encoding.ASCII.GetBytes("Server Ready");
            //current.Send(data);
            //Console.WriteLine("Warning Sent");

            if (text == "Ready") {
                current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
            }
            else {
                Console.WriteLine("Received Text: " + text);
                current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
            }
        }
    }
}
