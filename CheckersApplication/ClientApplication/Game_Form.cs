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

        public TextBox GetOutputBox() {
            return tbConsole;
        }

        public TextBox GetTurnBox() {
            return tbTurn;
        }

        private void RunServer() {
            Process.Start(Application.StartupPath.ToString() + @"\ServerApplication.exe");
        }

        private void RunClient() {
            Client client = new Client(this);
            client.ConnectToServer(); //2. START TRYING TO CONNECT TO SERVER
        }

        private void Game_Form_Shown(object sender, EventArgs e) {
            if (host) {
                Task serverTask = Task.Factory.StartNew(() => RunServer());
                Task clientTask = Task.Factory.StartNew(() => RunClient());
            } else {
                Task clientTask = Task.Factory.StartNew(() => RunClient());
            }
        }
    }
}
