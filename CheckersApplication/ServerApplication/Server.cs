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
        private static readonly List<Socket> clientSockets = new List<Socket>();
        private const int BUFFER_SIZE = 2048;
        private const int PORT = 100;
        private static readonly byte[] buffer = new byte[BUFFER_SIZE];

        private bool Client1Ready = false;
        private bool Client2Ready = false;
        private int currentPlayer = 0;
        private int otherPlayer = 1;

        public int test = 0;

        private ServerCheckersGame currentGame = new ServerCheckersGame();
        #endregion

        #region Constructors
        public Server() {

            Console.Title = "Server";
            SetupServer();
        }
        #endregion

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
            foreach (Socket socket in clientSockets) {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            serverSocket.Close();
        }

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

            clientSockets.Add(socket); // if client connects, add it to a list 

            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket); // start waiting to recieve messages from client
            while (!Client1Ready) {

            }
            Client1Ready = false;

            byte[] appended = Encoding.ASCII.GetBytes("Waiting For Opponent");

            byte[] identifier = Encoding.ASCII.GetBytes(MessageIdentifiers.OnePlayerConnected.ToString("d"));
            byte[] data3 = Combine(identifier, appended);
            socket.Send(data3);
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

            clientSockets.Add(socket); // if client connects, add it to a list 

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
            byte[] data3 = Combine(identifier, appended);

            while (!Client2Ready) {

            }
            Client2Ready = false;

            socket.Send(data3);

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
            data3 = Combine(identifier, appended);
            //------------------

            while (!Client1Ready) {

            }
            Client1Ready = false;


            clientSockets[0].Send(data3);

            //serverSocket.BeginAccept(WaitForClient2, null);// maybe this would tell client, this game is full

            StartingGame();

        }

        public void StartingGame() {
            Console.WriteLine("Starting Game");
            while (!Client1Ready || !Client2Ready) {

            }
            Console.WriteLine("Game Started");
            GameLoop();
        }

        public void GameLoop() {
            byte[] appended = Encoding.ASCII.GetBytes("Not Your Turn");
            byte[] identifier = Encoding.ASCII.GetBytes(MessageIdentifiers.StartingGame.ToString("d"));
            byte[] data = Combine(identifier, appended);
            data = Combine(identifier, appended);

            byte[] appended2 = Encoding.ASCII.GetBytes("Your Turn");
            byte[] identifier2 = Encoding.ASCII.GetBytes(MessageIdentifiers.StartingGame.ToString("d"));
            byte[] data2 = Combine(identifier2, appended2);
            data2 = Combine(identifier2, appended2);

            clientSockets[currentPlayer].Send(data);
            Client1Ready = false;
            //clientSockets[currentPlayer].BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, clientSockets[currentPlayer]); // start waiting to recieve messages from client

            clientSockets[otherPlayer].Send(data2);
            Client2Ready = false;
            //clientSockets[otherPlayer].BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, clientSockets[otherPlayer]); // start waiting to recieve messages from client

            while (!Client1Ready || !Client2Ready) {

            }
            var temp = currentPlayer;
            currentPlayer = otherPlayer;
            otherPlayer = temp;
            GameLoop();
        }

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
                clientSockets.Remove(current);
                return;
            }

            if (current == clientSockets[0]) {
                Client1Ready = true;
                Console.WriteLine("Player 1 ready");


            }
            else if (current == clientSockets[1]) {
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
            //if (text.ToLower() == "get time") // Client requested time
            //{
            //    Console.WriteLine("Text is a get time request");
            //    byte[] data = Encoding.ASCII.GetBytes(DateTime.Now.ToLongTimeString());
            //    current.Send(data);
            //    Console.WriteLine("Time sent to client");
            //}
            //else if (text.ToLower() == "exit") // Client wants to exit gracefully
            //{
            //    // Always Shutdown before closing
            //    current.Shutdown(SocketShutdown.Both);
            //    current.Close();
            //    clientSockets.Remove(current);
            //    Console.WriteLine("Client disconnected");
            //    return;
            //}

        }
    }
}
