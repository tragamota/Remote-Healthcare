using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UserData;

namespace Doctor
{
    public partial class Login : Form
    {
        Dictionary<string, string> users;
        Client client;

        public Login()
        {
            InitializeComponent();
        }

        private void BLog_in_Click(object sender, EventArgs e)
        {
            client = new Client();
            login();
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
                BLog_in_Click(null, null);
        }

        private void login() {
            if (txtUsername.Text.Length > 0 && txtPassword.Text.Length > 0)
            {
                dynamic user = new
                {
                    username = txtUsername.Text,
                    password = txtPassword.Text
                };
                client.SendMessage(user);

                JObject jObject = client.ReadMessage();
                string result = (string)jObject.GetValue("access");
                if (result.Equals("True"))
                {
                    this.Hide();
                    Form Form1 = new Dokter(client, (string)jObject["hashcode"]);
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
