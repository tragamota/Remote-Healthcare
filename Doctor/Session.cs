using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using System.Windows.Forms;
using UserData;
using Server;

namespace Doctor
{
    public partial class Session : Form
    {
        User patient;
        Client client;
        bool active;

        public Session(User patient, Client client)
        {
            InitializeComponent();
            this.patient = patient;
            this.client = client;
        }

        private void Start_Session_Btn_Click(object sender, EventArgs e)
        {
            active = true;
            new Thread(run).Start();
        }

        private void run()
        {
            BikeSession session = new BikeSession(patient.Hashcode);

            while (active)
            {
                session.SaveSessionToFile();

                string json = client.ReadMessage();
                BikeData data = (BikeData)JObject.Parse(json).ToObject(typeof(BikeData));
                SetPulse(data.Pulse.ToString());
                SetRoundMin(data.Rpm.ToString());
                SetSpeed(data.Speed.ToString());
                SetDistance(data.Distance.ToString());
                SetResistance(data.Resistance.ToString());
                SetEnergy(data.Energy.ToString());
                SetTime(data.Time.ToString());
                SetWatt(data.Power.ToString());
            }
        }

        public void SetPulse(String s) { lblPulse.Text = s; }

        public void SetRoundMin(String s) { lblRoundMin.Text = s; }

        public void SetSpeed(String s) { lblSpeed.Text = s; }

        public void SetDistance(String s) { lblDistance.Text = s; }

        public void SetResistance(String s) { lblResistence.Text = s; }

        public void SetEnergy(String s) { lblEnergy.Text = s; }

        public void SetTime(String s) { lblTime.Text = s; }

        public void SetWatt(String s) { lblWatt.Text = s; }

        private void Closing(object sender, FormClosingEventArgs e)
        {
            active = false;
            client.SendMessage("bye");
        }
    }
}
