using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Windows.Forms;
using ExtensionMethods;

namespace ClientApplication
{
     public class Client
    {
         private static readonly Socket ClientSocket = new Socket
(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private const int PORT = 100;
        private string ipAddress;
        private const int BUFFER_SIZE = 2048;
        private static readonly byte[] buffer = new byte[BUFFER_SIZE];

        private ClientCheckersGame currentGame;

        //private ClientCheckersGame currentGame;

        // Game Form Elements
        private TextBox output;
        private TextBox turnText;
        private Game_Form gameForm;

        public Client(Game_Form game) {
            currentGame = new ClientCheckersGame(game);
            gameForm = game;
            //output = game.GetOutputBox();
            turnText = game.GetTurnBox();
            //gameForm.SetOutputBox("Client Succesfully Created...");
        }

        public Client(Game_Form game, string ip)
        {
            currentGame = new ClientCheckersGame(game);
            gameForm = game;
            //output = game.GetOutputBox();
            turnText = game.GetTurnBox();
            ipAddress = ip;
            //gameForm.SetOutputBox("Client Succesfully Created...");
        }

        public GameBoard GetBoard() {
            return currentGame.GetGameBoard();
        }

        public void ConnectToServer()
        {
            int attempts = 0;

            while (!ClientSocket.Connected)
            {
                try
                {
                    attempts++;
                    //gameForm.SetOutputBox("Connection attempt: " + attempts);
                    // Change IPAddress.Loopback to a remote IP to connect to a remote host.
                    //gameForm.SetOutputBox("Enter IP Address");
                    //ipAddress = output.ReadLine();
                    //ipAddress = "10.250.123.157";
                    //Would put a User input here to get IP address
                    //IPAddress ServerIP = IPAddress.Parse(ipAddress);
                    //output.WriteLine(ServerIP);
                    IPAddress ServerIP = null;
                    IPHostEntry host;
                    host = Dns.GetHostEntry(Dns.GetHostName());

                    foreach (IPAddress ip in host.AddressList)
                    {
                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ServerIP = ip;
                            break;
                        }
                    }

                    if (ipAddress != null)
                        ServerIP = IPAddress.Parse(ipAddress);

                    ClientSocket.Connect(ServerIP, PORT);
                }
                catch (SocketException)
                {
                    //gameForm.SetOutputBox("");
                }
            }

            //gameForm.SetOutputBox("");
            //gameForm.SetOutputBox("Connected");
            //RequestLoop();
            ReceiveResponse();
        }

        /// <summary>
        /// Close socket and exit program.
        /// </summary>
        public void Exit()
        {
            SendString("exit"); // Tell the server we are exiting
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
            Environment.Exit(0);
        }

        /// <summary>
        /// Sends a string to the server with ASCII encoding.
        /// </summary>
        private static void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        public void ReceiveResponse()
        {
            //get message received
            var buffer = new byte[2048];
            SendMessage(MessageIdentifiers.ReadyUpdate); //4. KEEP SENDING REQUESTS
                                                         //int receivedReady = ClientSocket.Receive(buffer, SocketFlags.None);
            //gameForm.SetOutputBox("Receiving");
            int received = ClientSocket.Receive(buffer, SocketFlags.None);
            if (received == 0) return;
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            //string text = Encoding.ASCII.GetString(data);
            //output.WriteLine(text);

            InterpretMessage(data);
        }

        public void InterpretMessage(byte[] message)
        {
            //get the 1st byte
            byte[] firstByte = new byte[1];
            Array.Copy(message, 0, firstByte, 0, firstByte.Length);
            string identifier = Encoding.ASCII.GetString(firstByte);
            //output.WriteLine(identifier);

            //put the rest of the bytes in messageBytes
            byte[] messageBytes = new byte[message.Length - 1];
            Array.Copy(message, 1, messageBytes, 0, messageBytes.Length);

            if (identifier == MessageIdentifiers.OnePlayerConnected.ToString("d"))
            {
                string text = Encoding.ASCII.GetString(messageBytes);
                //gameForm.SetOutputBox(text);
            }
            else if (identifier == MessageIdentifiers.TwoPlayersConnected.ToString("d"))
            {
                Sclass1 deserializedClass;

                IFormatter formatter = new BinaryFormatter();
                using (MemoryStream stream = new MemoryStream(messageBytes))
                {
                    formatter.Binder = new PreMergeToMergedDeserializationBinder();
                    deserializedClass = (Sclass1)formatter.Deserialize(stream);
                }

                //gameForm.SetOutputBox(deserializedClass.GetMessage());
                currentGame.SetPlayerID(deserializedClass.GetPlayer());
                //gameForm.SetOutputBox(currentGame.GetPlayerID().ToString());
            }
            else if (identifier == MessageIdentifiers.GameUpdate.ToString("d"))
            {
                IFormatter formatter = new BinaryFormatter();
                using (MemoryStream stream = new MemoryStream(messageBytes))
                {
                    formatter.Binder = new PreMergeToMergedDeserializationBinder();
                    GameBoard temp = (GameBoard)formatter.Deserialize(stream);
                    currentGame.UpdateBoard(temp);
                    gameForm.UpdateBoard(temp);
                }
                if (currentGame.IsMyTurn())
                {
                    //gameForm.SetOutputBox("Is my turn");
                    gameForm.SetTurnBox("");
                    gameForm.SetTurnBox("Your Turn");

                    //Get and Send PlayerMove
                    gameForm.EnableInputs();
                    SendMessage(MessageIdentifiers.GameUpdate);
                }
                else
                {
                    gameForm.DisableInputs();
                    //gameForm.SetOutputBox("Is not my turn");
                    gameForm.SetTurnBox("");
                    gameForm.SetTurnBox("Not Your Turn");
                    //gameForm.SetOutputBox("Not Your Turn");
                }
            }
            else if (identifier == MessageIdentifiers.RetryGameUpdate.ToString("d"))
            {
                //gameForm.SetOutputBox("Invalid Move, Try Again");
                gameForm.SetTurnBox("");
                gameForm.SetTurnBox("Retry Move");
                gameForm.RestartMove();
                //do stuff to update the gameboard and try again
                SendMessage(MessageIdentifiers.GameUpdate);
            }
            else if(identifier == MessageIdentifiers.GameOver.ToString("d"))
            {
                IFormatter formatter = new BinaryFormatter();
                using (MemoryStream stream = new MemoryStream(messageBytes))
                {
                    formatter.Binder = new PreMergeToMergedDeserializationBinder();
                    GameBoard temp = (GameBoard)formatter.Deserialize(stream);
                    currentGame.UpdateBoard(temp);
                    gameForm.UpdateBoard(temp);
                }
                GameStatus status = currentGame.GetGameStatus();
                gameForm.SetTurnBox("");
                
                if (GameStatus.Player1Wins == status)
                    gameForm.SetTurnBox("Player 1 Wins !!!");
                else if (GameStatus.Player2Wins == status)
                    gameForm.SetTurnBox("Player 2 Wins !!!");
                else if (GameStatus.Draw == status)
                    gameForm.SetTurnBox("It's A Draw !!!");
                //gameForm.SetOutputBox(text);
                //switch forms and close sockets;
                return;
            }
            ReceiveResponse();
        }

        public void SendMessage(MessageIdentifiers id)
        {
            byte[] appended;
            byte[] identifier;
            byte[] data;
            switch (id)
            {
                case MessageIdentifiers.ReadyUpdate:
                    appended = Encoding.ASCII.GetBytes("Ready");

                    identifier = Encoding.ASCII.GetBytes(MessageIdentifiers.ReadyUpdate.ToString("d"));
                    data = Combine(identifier, appended);
                    ClientSocket.Send(data);
                    break;
                case MessageIdentifiers.GameUpdate:
          
                    while (gameForm.waitingToSubmit == false) { }
                    PlayerMove pm = gameForm.SubmitPlayerMove();

                    IFormatter formatter = new BinaryFormatter();
                    using (MemoryStream stream = new MemoryStream())
                    {
                        formatter.Serialize(stream, pm);
                        appended = stream.ToArray();
                    }
                    //byte[] appended = Encoding.ASCII.GetBytes("test");

                    identifier = Encoding.ASCII.GetBytes(MessageIdentifiers.GameUpdate.ToString("d"));
                    data = Combine(identifier, appended);

                    var buffer = new byte[2048];

                    ClientSocket.Send(data);

                    //gameForm.SetOutputBox("Sent Game Update");

                    //ReceiveResponse();
                    break;
                default:
                    break;
            }
        }

        private void RequestLoop()
        {
            //gameForm.SetOutputBox(@"<Type ""exit"" to properly disconnect client>");

            while (true)
            {
                ReceiveResponse(); //5. AND RECEIVING REQUESTS
            }
        }

        public int GetPlayerId()
        {
            return currentGame.GetPlayerID();
        }

        //Used to combine an identifier byte with a "message" byte[]
        public static byte[] Combine(byte[] first, byte[] second)
        {
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