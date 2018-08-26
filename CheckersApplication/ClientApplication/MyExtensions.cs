using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ExtensionMethods {
    public static class MyExtensions {
        public static void WriteLine(this TextBox txtBox, string value) {
            if (txtBox.Text.Length == 0)
                txtBox.Text = value;
            else
                txtBox.AppendText("\r\n" + value);
        }
    }
}
