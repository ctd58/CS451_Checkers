﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApplication {
    public partial class Game_Form : Form {
        public Game_Form() {
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
