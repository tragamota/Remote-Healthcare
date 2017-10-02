using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UserData;

namespace Doctor
{
    public partial class Dokter : Form
    {
        Client client;
        List<User> users;
        
        public Dokter(Client client)
        {
            InitializeComponent();
            this.client = client;
            client.SendMessage(new{
                id = "getPatients"
            });

            JObject data = client.ReadMessage();
            users = (List<User>)data["users"].ToObject(typeof(List<User>));

            foreach (User user in users)
            {
                if(user.Type == DoctorType.Client)
                    Awaiting_Patients_Box.Items.Add(user.FullName);
            }
        }

        private void Connect_Btn_Click(object sender, EventArgs e)
        {
            if(Awaiting_Patients_Box.SelectedItem != null)
            {
                this.Hide();
                Form session = new Session(users.Find(x => x.FullName.Equals(Awaiting_Patients_Box.SelectedItem)), client);
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
            client.SendMessage("bye");
            this.Hide();
        }

        private void Closing(object sender, FormClosingEventArgs e)
        {
            client.SendMessage("bye");
            this.Hide();
        }

        private void Send_Message_Btn_Click(object sender, EventArgs e)
        {

        }
    }
}
