using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using ServerApplication;
using System.Diagnostics;

namespace ClientApplication {
    public partial class Game_Form : Form {
        bool host;

        public Game_Form(bool host) {
            InitializeComponent();
            this.host = host;
        }

        private void Exit_Click(object sender, EventArgs e) {
            this.Close();
        }
/*
        [DllImport("kernel32")]
        static extern int AllocConsole();
        void RunServer() {
            // Open server console
            AllocConsole();
            Console.Title = "Server";
            Server server = new Server();  // 2. GO TO SETUPSERVER
            Console.ReadLine(); // When we press enter close everything
            server.CloseAllSockets();
        }
*/

        void RunServer() {
            Process.Start(Application.StartupPath.ToString() + @"\ServerApplication.exe");
        }

        [DllImport("kernel32")]
        static extern IntPtr AllocConsole();
        void RunClient() {
            // Open client console
            AllocConsole();
            Console.Title = "Client";
            TextWriter writer = new TextBoxConsole(tbConsole);
            Console.SetOut(writer);
            Client client = new Client();
            client.ConnectToServer(); //2. START TRYING TO CONNECT TO SERVER
        }

        private void Game_Form_Shown(object sender, EventArgs e) {
            if (host) {
                RunServer();
                Thread.Sleep(500);
                RunClient();
            } else {
                RunClient();
            }
        }
    }
}
