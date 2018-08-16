using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace MultiServer
{

    
    public class Program
    {
        // 1. START IN MAIN
        static void Main()
        {
            Server server = new Server();
            Console.Title = "Server";
            GameBoard gameBoard = new GameBoard();
            server.SetupServer(); // 2. GO TO SETUPSERVER
            Console.ReadLine(); // When we press enter close everything
            server.CloseAllSockets();
        }
    }
}
