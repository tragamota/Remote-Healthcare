using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Remote_Healtcare_Console
{
    public partial class Login : Form
    {
        Dictionary<string, string> users;

        public Login()
        {
            InitializeComponent();
        }

        private void BLog_in_Click(object sender, EventArgs e)
        {
            login();
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
                BLog_in_Click(null, null);
        }

        private void login() {
            if (txtUsername.Text.Length > 0 && txtPassword.Text.Length > 0) {
                this.Hide();
                Form Form1 = new Console();
                Form1.Closed += (s, args) => this.Close();
                Form1.Show();
            }
        }
    }
}
