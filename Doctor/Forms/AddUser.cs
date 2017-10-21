using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using UserData;

namespace Doctor.Forms {
    public partial class AddUser : Form {
        private Client client;
        public AddUser(ref Client client) {
            InitializeComponent();
            this.client = client;
            comboBox1.Items.Add("Patient");
            comboBox1.Items.Add("Doctor");
            comboBox1.SelectedIndex = 0;
            button1.Click += createUser;
        }

        private void createUser(object sender, EventArgs e) {
            if (textBox1.Text.Count() >= 3 && textBox2.Text.Count() >= 5 && textBox3.Text.Count() >= 4) {
                sendUser();
            }
            else if(textBox1.Text.Count() < 3 && textBox2.Text.Count() < 5 && textBox3.Text.Count() < 4) {
                MessageBox.Show("Username, Password en Name zijn te kort");
            }
            else if (textBox1.Text.Count() < 3 && textBox2.Text.Count() < 5) {
                MessageBox.Show("Username en Password zijn te kort");
            }
            else if(textBox1.Text.Count() < 3) {
                MessageBox.Show("Username is te kort");
            }
            else if(textBox2.Text.Count() < 5) {
                MessageBox.Show("Password is te kort");
            }
            else if (textBox3.Text.Count() < 4) {
                MessageBox.Show("Name is te kort");
            }
        }

        private void sendUser() {
            SHA256Managed SHA = new SHA256Managed();
            Encoding enc = Encoding.Default;
            string username = enc.GetString(SHA.ComputeHash(enc.GetBytes(textBox1.Text)));
            string password = enc.GetString(SHA.ComputeHash(enc.GetBytes(textBox2.Text)));
            dynamic user = new {
                id = "add",
                data = new {
                    username = username,
                    password = password,
                    fullname = textBox3.Text,
                    type = (UserType) comboBox1.SelectedIndex
                }
            };
            JObject response;
            lock(client.ReadAndWriteLock) {
                client.SendMessage(user);
                response = JObject.Parse(client.ReadMessage());
            }

            if((string) response["status"] == "ok") {
                MessageBox.Show("User is toegevoegd aan de server");
                Close();
                Dispose();
            }
            else {
                MessageBox.Show("User bestaat al, gebruik een andere gebruikersnaam");
            }
        }
    }
}
