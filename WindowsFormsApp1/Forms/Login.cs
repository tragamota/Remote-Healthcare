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
using UserData;

namespace Remote_Healtcare_Console
{
    public partial class Login : Form
    {
        Client client;

        public Login()
        {
            InitializeComponent();
            client = new Client();
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
                User user = new User(txtUsername.Text, txtPassword.Text, null);
                client.SendMessage(user);

                JObject jObject = JObject.Parse(client.ReadMessage());
                string result = (string)jObject.GetValue("access");
                if (result.Equals("approved")) {
                    this.Hide();
                    Form Form1 = new Console(client);
                    Form1.Closed += (s, args) => this.Close();
                    Form1.Show();
                }
                else
                {
                    MessageBox.Show("Ingevulde gegevens zijn onjuist");
                }
            }
            else
            {
                MessageBox.Show("Vul de velden in");
            }
        }
    }
}
