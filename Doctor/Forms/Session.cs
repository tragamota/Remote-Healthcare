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

        public Session(User patient, Client client, string hashcode)
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
                        doctor = hashcode
                    },
                    patient = patient
                }
            });
        }

        private void run()
        {
            while (active)
            {
                JObject json = client.ReadMessage();
                BikeData data = (BikeData)json["bikeData"].ToObject(typeof(BikeData));
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

        public void SetPulse(String s) {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetPulse), new object[] { s });
                return;
            }
            lblPulse.Text = s; }

        public void SetRoundMin(String s) {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetRoundMin), new object[] { s });
                return;
            }
            lblRoundMin.Text = s; }

        public void SetSpeed(String s) {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetSpeed), new object[] { s });
                return;
            }
            lblSpeed.Text = s; }

        public void SetDistance(String s) {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetDistance), new object[] { s });
                return;
            }
            lblDistance.Text = s; }

        public void SetResistance(String s) {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetResistance), new object[] { s });
                return;
            }
            lblResistence.Text = s; }

        public void SetEnergy(String s) {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetEnergy), new object[] { s });
                return;
            }
            lblEnergy.Text = s; }

        public void SetTime(String s) {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetTime), new object[] { s });
                return;
            }
            lblTime.Text = s; }

        public void SetWatt(String s) {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetWatt), new object[] { s });
                return;
            }
            lblWatt.Text = s; }

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

            int resistance = (375 / 100) * (Resistance_Track_Bar.Value - (Resistance_Track_Bar.Value  % 5)) + 25;

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
            new Thread(() => run()).Start();
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
