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
                id = "getPatients"
            });

            string data = client.ReadMessage();
            users = JsonConvert.DeserializeObject<List<User>>(data);

            foreach (User user in users)
            {
                if(user.Type == DoctorType.Client)
                    Awaiting_Patients_Box.Items.Add(user);
            }
        }

        private void Connect_Btn_Click(object sender, EventArgs e)
        {
            if(Awaiting_Patients_Box.SelectedItem != null)
            {
                this.Hide();
                Form session = new Session((User)Awaiting_Patients_Box.SelectedItem, client, hashcode);
                session.Closed += (s, args) => this.Close();
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
    }
}
