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

namespace CheckersPlayerClient {
    public partial class PlayerClientForm : Form {

        public PlayerClientForm() {
            InitializeComponent();
        }

        SimpleTcpClient client;

        private void PlayerClientForm_Load(object sender, EventArgs e) {
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e) {

            ConsoleTextBox.Invoke((MethodInvoker)delegate () {

                ConsoleTextBox.Text += e.MessageString;
            });
        }



        private void ConnectClientButton_Click(object sender, EventArgs e) {

            ConnectClientButton.Enabled = false;
            ConsoleTextBox.Text += "Connecting to server ...\r\n";

            Int32 serverPort = Convert.ToInt32(PortTextBox.Text);
            client.Connect(IPTextBox.Text, serverPort);

            ConsoleTextBox.Text += "Connected to server\r\n";
        }

        private void SendMessageButton_Click(object sender, EventArgs e) {

            client.WriteLineAndGetReply(MessageTextBox.Text, TimeSpan.FromSeconds(3));
        }

    }
}
