using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using UserData;

namespace Doctor {
    public partial class Login : Form {
        private Client client;
        private User Credentials;

        public Login() {
            InitializeComponent();
        }

        private void BLog_in_Click(object sender, EventArgs e) {
            login();
        }

        private void KeyPressed(object sender, KeyEventArgs e) {
            if (e.KeyData == Keys.Enter) {
                BLog_in_Click(null, null);
            }
        }

        private void login() {
            client = new Client();
            if (txtUsername.Text.Length > 0 && txtPassword.Text.Length > 0) {
                dynamic user = new {
                    username = Encoding.Default.GetString(new SHA256Managed().ComputeHash(Encoding.Default.GetBytes(txtUsername.Text))),
                    password = Encoding.Default.GetString(new SHA256Managed().ComputeHash(Encoding.Default.GetBytes(txtPassword.Text)))
                };
               
                try {
                    client.SendMessage(user);
                }
                catch(Exception e) {
                    Console.WriteLine(e.StackTrace);
                    this.client = new Client();
                    client.SendMessage(user);
                }

                JObject jObject = (JObject)JsonConvert.DeserializeObject(client.ReadMessage());
                bool result = (bool)jObject["access"];
               
                if (result == true) {
                    UserType type = (UserType)((int)jObject["doctortype"]);
                    if (type == UserType.Doctor) {
                        Credentials = new User(user.username, user.password, (string)jObject["fullname"], (string)jObject["hashcode"], UserType.Client);
                    }
                    else {
                        MessageBox.Show("This is not a Doctor account, please use the Client Application");
                        dynamic disconnect = new {
                            id = "disconnect"
                        };
                        client.SendMessage(disconnect);
                        client.client.Close();
                        return;
                    }

                    Hide();
                    Form Form1 = new Dokter(client, Credentials.Hashcode);
                    Form1.Closed += (s, args) => this.Close();
                    Form1.Show();
                }
                else {
                    MessageBox.Show("Uw gebruikersnaam en wachtwoord komen niet overeen");
                }
            }
            else {
                if (txtUsername.Text == "") {
                    MessageBox.Show("Vul het gebruikersnaam veld in");
                }
                else if(txtPassword.Text == "") {
                    MessageBox.Show("Vul het Wachtwoord veld in");
                }
                else {
                    MessageBox.Show("Vul de velden in");
                }
            }
        }
    }
}
