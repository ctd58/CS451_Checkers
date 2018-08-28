using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServerApplication;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace ClientApplication
{
    public partial class Main_Menu_Form : Form
    {
        Point mouseDownPoint = Point.Empty;

        public Main_Menu_Form()
        {
            InitializeComponent();
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

            ipBox.Text = ServerIP.ToString();
        }

        // Exit 
        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Host_Button_Click(object sender, EventArgs e) {
            // Open Game form
            var frm = new Game_Form(true);
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void Join_Button_Click(object sender, EventArgs e) {
            var frm = new Game_Form(false, ipBox.Text);
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }
    }

}