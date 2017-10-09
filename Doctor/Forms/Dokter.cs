using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using UserData;

namespace Doctor
{
    public partial class Dokter : Form
    {
        Client client;
        List<User> users;
        string hashcode;
        
        public Dokter(Client client, string hashcode)
        {
            InitializeComponent();
            this.client = client;
            this.hashcode = hashcode;
            client.SendMessage(new{
                id = "getconPatients"
            });

            string data = client.ReadMessage();
            users = (List<User>)((JArray)JsonConvert.DeserializeObject(data)).ToObject(typeof(List<User>));

            foreach (User user in users)
            {
                if(user.Type == UserType.Client)
                    Awaiting_Patients_Box.Items.Add(user);
            }
        }

        private void Connect_Btn_Click(object sender, EventArgs e)
        {
            if(Awaiting_Patients_Box.SelectedItem != null)
            {
                Form session = new Session((User)Awaiting_Patients_Box.SelectedItem, client, hashcode);
                session.Show();
            }
            else
            {
                MessageBox.Show("Selecteer een patiënt");
            }
        }

        private void Log_Out_Btn_Click(object sender, EventArgs e)
        {
            dynamic message = new {
                id = "disconnect"
            };
            client.SendMessage(message);

            Hide();
            Environment.Exit(0);
        }

        private void Closing(object sender, FormClosingEventArgs e)
        {
            client.SendMessage("bye");
            this.Hide();
        }

        private void Send_Message_Btn_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e) {
            string username = Encoding.Default.GetString(new SHA256Managed().ComputeHash(Encoding.Default.GetBytes(textBox1.Text)));
            string password = Encoding.Default.GetString(new SHA256Managed().ComputeHash(Encoding.Default.GetBytes(textBox2.Text)));
            dynamic newUser = new {
                id = "add",
                data = new {
                    username = username,
                    password = password,
                    fullname = "Ian van de Poll",
                    type = UserType.Client
                }
            };
            client.SendMessage(newUser);
            //Console.WriteLine(client.ReadMessage());
        }

        private void Patient_Selected(object sender, EventArgs e)
        {
            Old_Sessions_Box.Items.Clear();

            client.SendMessage(new
            {
                id = "oldsessions"
            });

            string data = client.ReadMessage();
            string[] files = (string[])((JObject)JsonConvert.DeserializeObject(data))["data"].ToObject(typeof(string[]));
            foreach (string file in files)
            {
                Old_Sessions_Box.Items.Add(Path.GetFileName(file));
            }
        }
    }
}
