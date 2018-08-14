using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

namespace MultiClient
{

    class Program
    {

        //1. Start In Main
        static void Main()
        {
            Console.Title = "Client";
            SocketStuff client = new SocketStuff();
            client.ConnectToServer(); //2. START TRYING TO CONNECT TO SERVER
            //client.RequestLoop(); //3. ONCE YOU CONNECT, START THE REQUEST LOOP
            client.Exit();
        }

        public class SocketStuff
        {
            private static readonly Socket ClientSocket = new Socket
    (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            private const int PORT = 100;
            private bool waiting = false;

            public SocketStuff() { }

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
                        string ip = Console.ReadLine();
                        //Would put a User input here to get IP address
                        IPAddress ServerIP = IPAddress.Parse(ip);
                        Console.WriteLine(ServerIP);
                        ClientSocket.Connect(ServerIP, PORT);
                    }
                    catch (SocketException)
                    {
                        Console.Clear();
                    }
                }

                Console.Clear();
                Console.WriteLine("Connected");
                RequestLoop();
            }

            private void RequestLoop()
            {
                Console.WriteLine(@"<Type ""exit"" to properly disconnect client>");

                while (true)
                {
                        ReceiveResponse(); //5. AND RECEIVING REQUESTS
                }
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

            private void SendRequest()
            {
                //Console.Write("Send a request: ");
                //string request = Console.ReadLine();
                SendString("Ready");

            }

            /// <summary>
            /// Sends a string to the server with ASCII encoding.
            /// </summary>
            private static void SendString(string text)
            {
                byte[] buffer = Encoding.ASCII.GetBytes(text);
                ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
                Console.Write("Receiving \n");
            }

            private void ReceiveResponse()
            {
                //get message received
                var buffer = new byte[2048];
                SendRequest(); //4. KEEP SENDING REQUESTS
                //int receivedReady = ClientSocket.Receive(buffer, SocketFlags.None);
                int received = ClientSocket.Receive(buffer, SocketFlags.None);
                if (received == 0) return;
                var data = new byte[received];
                Array.Copy(buffer, data, received);
                //string text = Encoding.ASCII.GetString(data);
                //Console.WriteLine(text);

                //get the 1st byte
                byte[] firstByte = new byte[1];
                Array.Copy(data, 0, firstByte, 0, firstByte.Length);
                string identifier = Encoding.ASCII.GetString(firstByte);
                //Console.WriteLine(identifier);

                //put the rest of the bytes in messageBytes
                byte[] messageBytes = new byte[data.Length - 1];
                Array.Copy(data, 1, messageBytes, 0, messageBytes.Length);

                if (identifier == "0")
                {
                    string text = Encoding.ASCII.GetString(messageBytes);
                    Console.WriteLine(text);
                }
                else if (identifier == "1")
                {
                    Sclass1 deserializedClass;

                    IFormatter formatter = new BinaryFormatter();
                    using (MemoryStream stream = new MemoryStream(messageBytes))
                    {
                        formatter.Binder = new PreMergeToMergedDeserializationBinder();
                        deserializedClass = (Sclass1)formatter.Deserialize(stream);
                    }

                    Console.WriteLine(deserializedClass.GetMessage());
                    Console.WriteLine(deserializedClass.GetPlayer());
                }
                else if(identifier == "2")
                {
                    string text = Encoding.ASCII.GetString(messageBytes);
                    if(text == "Your Turn")
                    {
                        Console.WriteLine(text);
                        string request = Console.ReadLine();
                        SendString(request);
                        test();
                    }
                    else
                    {
                        Console.WriteLine(text);
                    }
                }
                //get the rest of the bytes to string, except the first
                waiting = false;
            }

            public void test()
            {
                var buffer = new byte[2048];
                int receivedthree = ClientSocket.Receive(buffer, SocketFlags.None);
            }
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
