using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

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
        private string playerID;

        //private ClientCheckersGame currentGame;

        public Client() { }

        public void ConnectToServer()
        {
            int attempts = 0;

            while (!ClientSocket.Connected)
            {
                try
                {
                    attempts++;
                    Console.WriteLine("Connection attempt: " + attempts);
                    // Change IPAddress.Loopback to a remote IP to connect to a remote host.
                    Console.WriteLine("Enter IP Address");
                    //ipAddress = Console.ReadLine();
                    ipAddress = "192.168.1.6";
                    //Would put a User input here to get IP address
                    //IPAddress ServerIP = IPAddress.Parse(ipAddress);
                    //Console.WriteLine(ServerIP);
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
                    ClientSocket.Connect(ServerIP, PORT);
                }
                catch (SocketException)
                {
                    Console.Clear();
                }
            }

            Console.Clear();
            Console.WriteLine("Connected");
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

        private void ReceiveResponse()
        {
            //get message received
            var buffer = new byte[2048];
            SendMessage(MessageIdentifiers.ReadyUpdate); //4. KEEP SENDING REQUESTS
                           //int receivedReady = ClientSocket.Receive(buffer, SocketFlags.None);
            Console.Write("Receiving \n");
            int received = ClientSocket.Receive(buffer, SocketFlags.None);
            if (received == 0) return;
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            //string text = Encoding.ASCII.GetString(data);
            //Console.WriteLine(text);

            InterpretMessage(data);
            ReceiveResponse();
        }

        public void InterpretMessage(byte[] message)
        {
            //get the 1st byte
            byte[] firstByte = new byte[1];
            Array.Copy(message, 0, firstByte, 0, firstByte.Length);
            string identifier = Encoding.ASCII.GetString(firstByte);
            //Console.WriteLine(identifier);

            //put the rest of the bytes in messageBytes
            byte[] messageBytes = new byte[message.Length - 1];
            Array.Copy(message, 1, messageBytes, 0, messageBytes.Length);

            if (identifier == MessageIdentifiers.OnePlayerConnected.ToString("d"))
            {
                string text = Encoding.ASCII.GetString(messageBytes);
                Console.WriteLine(text);
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

                Console.WriteLine(deserializedClass.GetMessage());
                playerID = deserializedClass.GetPlayer();
                Console.WriteLine(playerID);
            }
            else if (identifier == MessageIdentifiers.GameUpdate.ToString("d"))
            {
                string text = Encoding.ASCII.GetString(messageBytes);
                if (text == "Your Turn")
                {
                    Console.WriteLine(text);
                    //Get and Send PlayerMove
                    SendMessage(MessageIdentifiers.GameUpdate);
                }
                else
                {
                    Console.WriteLine(text);
                }
            }
            else if (identifier == MessageIdentifiers.RetryGameUpdate.ToString("d"))
            {
                Console.WriteLine("Invalid Move, Try Again");
                //do stuff to update the gameboard and try again
                SendMessage(MessageIdentifiers.GameUpdate);
            }
            //get the rest of the bytes to string, except the first
        }

        private void SendMessage(MessageIdentifiers id)
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
                    string request = Console.ReadLine();

                    PlayerMove pm = new PlayerMove();
                    CKPoint ck1 = new CKPoint(1, 1);
                    CKPoint ck2 = new CKPoint(2, 2);

                    if (request == "cheat")
                    {
                        pm.BuildMove(ck1);
                    }
                    else
                    {
                        //set appended to player move
                        pm.BuildMove(ck1);
                        pm.BuildMove(ck2);
                    }

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

                    Console.WriteLine("Sent Game Update");

                    ReceiveResponse();
                    break;
                default:
                    break;
            }
        }

        private void RequestLoop()
        {
            Console.WriteLine(@"<Type ""exit"" to properly disconnect client>");

            while (true)
            {
                ReceiveResponse(); //5. AND RECEIVING REQUESTS
            }
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