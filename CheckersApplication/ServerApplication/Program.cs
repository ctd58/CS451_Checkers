using System;

namespace ServerApplication
{
    public class Program
    {
        // 1. START IN MAIN
        static void Main()
        {


            Server server = new Server();  // 2. GO TO SETUPSERVER
            Console.ReadLine(); // When we press enter close everything
            server.CloseAllSockets();
        }
    }
}
