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

            client.SendMessage(new
            {
                id = "setPatient",
                data = new
                {
                    doctor = new
                    {
                        id = "setDoctor",
                        doctor = client
                    },
                    patient = patient
                }
            });
        }

        private void run()
        {
            while (active)
            {
                BikeData data = (BikeData)client.ReadMessage().ToObject(typeof(BikeData));
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

        private void Scrolling(object sender, EventArgs e)
        {
            Temp_Resistance_Lbl.Text = Resistance_Track_Bar.Value + " %";
        }

        private void Stopped_Scrolling(object sender, MouseEventArgs e)
        {
            lblResistence.Text = Temp_Resistance_Lbl.Text;

            string resistance = ((375 / 100) * int.Parse(lblResistence.Text)).ToString();

            client.SendMessage(new
            {
                id = "committingChanges",
                data = new
                {
                    id = "setResistance",
                    data = new
                    {
                        resistance = resistance
                    }
                }
            });
        }

        private void Send_Message_Btn_Click(object sender, EventArgs e)
        {
            client.SendMessage(new
            {
                id = "committingChanges",
                data = new
                {
                    id = "chat",
                    data = new
                    {
                        message = Message_Txt_Box.Text
                    }
                }
            });
        }

        private void Start_Session_Btn_Click(object sender, EventArgs e)
        {
            client.SendMessage(new
            {
                id = "startRecording"
            });

            active = true;
            new Thread(run).Start();
        }

        private void Stop_Session_Btn_Click(object sender, EventArgs e)
        {
            client.SendMessage(new
            {
                id = "stopRecording"
            });
            active = false;
        }
    }
}
