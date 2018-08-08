using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SimpleTCP;

namespace CheckersServerHost {
    public partial class ServerHostForm : Form {

        public ServerHostForm() {
            InitializeComponent();
        }

        SimpleTcpServer server;

        private void TestHostForm_Load(object sender, EventArgs e) {

            server = new SimpleTcpServer();
            server.Delimiter = 0x13; // Makes enter the delimiter
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived;
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e) {

            ConsoleTextBox.Invoke((MethodInvoker)delegate () {

                ConsoleTextBox.Text += e.MessageString;
                e.ReplyLine("You said " + e.MessageString + "\r\n");
            });
        }



        private void HostButton_Click(object sender, EventArgs e) {

            ConsoleTextBox.Text += "Starting server ...\r\n";
            System.Net.IPAddress serverIP = System.Net.IPAddress.Parse(IPTextBox.Text);
            Int32 serverPort = Convert.ToInt32(PortTextBox.Text);

            server.Start(serverIP, serverPort);
            ConsoleTextBox.Text += "Server started\r\n";
        }

        private void EndHostButton_Click(object sender, EventArgs e) {

            if(server.IsStarted) {
                ConsoleTextBox.Text += "You are stopping the server, disconnecting clients ...\r\n";
                server.Stop();
            }
        }
    }
}
