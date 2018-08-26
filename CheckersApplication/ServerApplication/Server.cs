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
using System.Reflection;

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
        private bool AppliedPlayerMove = false;
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

        private void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;

            int received;

            try
            {
                received = current.EndReceive(AR);
            }
            catch (SocketException)
            {
                Console.WriteLine("Client forcefully disconnected");
                // Don't shutdown because the socket may be disposed and its disconnected anyway.
                current.Close();
                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);

            //Console.WriteLine("Text is an invalid request");
            //byte[] data = Encoding.ASCII.GetBytes("Server Ready");
            //current.Send(data);
            //Console.WriteLine("Warning Sent");

            InterpretMessage(recBuf, current);

            current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
        }

        public bool tempWinCheck = false;

        public void InterpretMessage(byte[] message, Socket current)
        {
            //get the firstbyte
            //Identifier is the string value
            byte[] firstByte = new byte[1];
            Array.Copy(message, 0, firstByte, 0, firstByte.Length);
            string identifier = Encoding.ASCII.GetString(firstByte);

            //put the rest of the bytes in messageBytes
            byte[] messageBytes = new byte[message.Length - 1];
            Array.Copy(message, 1, messageBytes, 0, messageBytes.Length);
            string text = Encoding.ASCII.GetString(message); //remove text after we change all messages to use first byte as identifier

            if (identifier == MessageIdentifiers.ReadyUpdate.ToString("d"))
            {
                if (current == player1Socket)
                {
                    Client1Ready = true;
                    Console.WriteLine("Player 1 ready");


                }
                else if (current == player2Socket)
                {
                    Client2Ready = true;
                    Console.WriteLine("Player 2 ready");
                }
            }
            else if (identifier == MessageIdentifiers.GameUpdate.ToString("d"))
            {
                PlayerMove deserializedPM;

                IFormatter formatter = new BinaryFormatter();
                using (MemoryStream stream = new MemoryStream(messageBytes))
                {
                    formatter.Binder = new PreMergeToMergedDeserializationBinder();
                    deserializedPM = (PlayerMove)formatter.Deserialize(stream);
                }
                Console.WriteLine("PlayerMove: From(" + deserializedPM.GetPlayerMove()[0].GetRow().ToString() + " , " + deserializedPM.GetPlayerMove()[0].GetColumn().ToString() + ")");
                Console.WriteLine("PlayerMove: To(" + deserializedPM.GetPlayerMove()[1].GetRow().ToString() + " , " + deserializedPM.GetPlayerMove()[1].GetColumn().ToString() + ")");
                //if move was invalid
                currentGame.SetCurrentPlayerMove(deserializedPM);
                if (currentGame.ApplyMove())
                {
                    Console.WriteLine("Move was valid");
                    AppliedPlayerMove = true;
                }
                else
                {
                    Console.WriteLine("Move was Invalid");
                    SendMessage(MessageIdentifiers.RetryGameUpdate, null);
                }
                currentGame.GetGameBoard().PrintBoard();

            }
            else
            {
                Console.WriteLine("Received Text: " + text);
            }

        }

        public void SendMessage(MessageIdentifiers id, Socket socket)
        {
            byte[] appended;
            byte[] identifier;
            byte[] data;
            switch (id){
                case MessageIdentifiers.OnePlayerConnected:
                    appended = Encoding.ASCII.GetBytes("Waiting For Opponent");

                    identifier = Encoding.ASCII.GetBytes(MessageIdentifiers.OnePlayerConnected.ToString("d"));
                    data = Combine(identifier, appended);
                    socket.Send(data);
                    break;
                case MessageIdentifiers.TwoPlayersConnected:
                    //------------------
                    // Initialize our test class and set the message to Starting Game for player 2, serialize and send it to the client who just joined
                    //------------------
                    Sclass1 serializeMe = new Sclass1();
                    serializeMe.SetMessage("Two Players Connected");
                    serializeMe.SetPlayer(2);
                    Console.WriteLine(serializeMe.GetMessage());
                    IFormatter formatter = new BinaryFormatter();
                    using (MemoryStream stream = new MemoryStream())
                    {
                        formatter.Serialize(stream, serializeMe);
                        appended = stream.ToArray();
                    }
                    //byte[] appended = Encoding.ASCII.GetBytes("test");

                    identifier = Encoding.ASCII.GetBytes(MessageIdentifiers.TwoPlayersConnected.ToString("d"));
                    data = Combine(identifier, appended);

                    while (!Client2Ready)
                    {

                    }
                    Client2Ready = false;

                    socket.Send(data);

                    //------------------
                    //This one goes to the first client, letting them know the game is starting, and that they are player 1
                    //------------------
                    serializeMe = new Sclass1();
                    serializeMe.SetMessage("Two Players Connected");
                    serializeMe.SetPlayer(1);
                    formatter = new BinaryFormatter();
                    using (MemoryStream stream = new MemoryStream())
                    {
                        formatter.Serialize(stream, serializeMe);
                        appended = stream.ToArray();
                    }
                    //byte[] appended = Encoding.ASCII.GetBytes("test");

                    identifier = Encoding.ASCII.GetBytes(MessageIdentifiers.TwoPlayersConnected.ToString("d"));
                    data = Combine(identifier, appended);

                    while (!Client1Ready)
                    {

                    }
                    Client1Ready = false;

                    player1Socket.Send(data);

                    break;
                case MessageIdentifiers.GameUpdate:
                    GameBoard serializeGameBoard = currentGame.GetGameBoard();
                    formatter = new BinaryFormatter();
                    using (MemoryStream stream = new MemoryStream())
                    {
                        formatter.Serialize(stream, serializeGameBoard);
                        appended = stream.ToArray();
                    }

                    identifier = Encoding.ASCII.GetBytes(MessageIdentifiers.GameUpdate.ToString("d"));
                    byte[] GameBoardData = Combine(identifier, appended);

                    player1Socket.Send(GameBoardData);
                    Client1Ready = false;
                    player2Socket.Send(GameBoardData);
                    Client2Ready = false;
                    break;
                case MessageIdentifiers.RetryGameUpdate:
                    //just send the gameboard again
                    appended = Encoding.ASCII.GetBytes("Unupdated Gameboard");

                    identifier = Encoding.ASCII.GetBytes(MessageIdentifiers.RetryGameUpdate.ToString("d"));
                    data = Combine(identifier, appended);
                    if (currentPlayer == 1)
                    {
                        player1Socket.Send(data);
                        Client1Ready = false;
                    }
                    else {
                        player2Socket.Send(data);
                        Client2Ready = false;
                    }
                    break;
                case MessageIdentifiers.GameOver:
                    //Set the game over message
                    serializeGameBoard = currentGame.GetGameBoard();
                    formatter = new BinaryFormatter();
                    using (MemoryStream stream = new MemoryStream())
                    {
                        formatter.Serialize(stream, serializeGameBoard);
                        appended = stream.ToArray();
                    }

                    identifier = Encoding.ASCII.GetBytes(MessageIdentifiers.GameOver.ToString("d"));
                    GameBoardData = Combine(identifier, appended);

                    player1Socket.Send(GameBoardData);
                    Client1Ready = false;
                    player2Socket.Send(GameBoardData);
                    Client2Ready = false;
                    //do stuff to shut down server
                    break;
                default:
                    break;
            }
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

            SendMessage(MessageIdentifiers.OnePlayerConnected, socket);

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

            SendMessage(MessageIdentifiers.TwoPlayersConnected, socket);

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
            Console.WriteLine("Next Turn");

            //check for win first, other wise send to get a move from a player
            if (currentGame.GetGameStatus() != GameStatus.InProgress)
            {
                SendMessage(MessageIdentifiers.GameOver, null);
            }
            else
            {
                SendMessage(MessageIdentifiers.GameUpdate, null);
            }
            while (!Client1Ready || !Client2Ready || !AppliedPlayerMove)
            {
                
            }
            AppliedPlayerMove = false;

            var temp = currentPlayer;
            currentPlayer = otherPlayer;
            otherPlayer = temp;
            currentGame.SwitchTurns();
            GameLoop();
        }

        //Used to combine an identifier byte with a "message" byte[]
        public static byte[] Combine(byte[] first, byte[] second) {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }

        
    }
}

sealed class PreMergeToMergedDeserializationBinder : SerializationBinder
{
    public override Type BindToType(string assemblyName, string typeName)
    {
        Type typeToDeserialize = null;

        // For each assemblyName/typeName that you want to deserialize to
        // a different type, set typeToDeserialize to the desired type.
        String exeAssembly = Assembly.GetExecutingAssembly().FullName;


        // The following line of code returns the type.
        typeToDeserialize = Type.GetType(String.Format("{0}, {1}",
            typeName, exeAssembly));

        return typeToDeserialize;
    }
}
