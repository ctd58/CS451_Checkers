using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApplication
{

    class Program
    {

        //1. Start In Main
        static void Main()
        {
            //string folder = Environment.CurrentDirectory;
            //System.Diagnostics.Process.Start(folder + @"\ServerApplication.exe");

            Application.Run(new Main_Menu_Form());

            //Console.Title = "Client";
            //Client client = new Client();
            //client.ConnectToServer(); //2. START TRYING TO CONNECT TO SERVER
            
            //client.Exit();

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
        }

    }
}
