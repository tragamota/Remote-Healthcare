using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UserData;
using VR;
using System.Threading;

namespace Remote_Healtcare_Console
{
    public partial class Console : Form
    {
        private Kettler bike;
        private ComboBox combo;
        public string path;
        public ISet<BikeData> data;
        private Client client;
        public ConnectForm connectForm;
        private object hudlock;

        public Console(Client client)
        {
            InitializeComponent();
            hudlock = new object();
            this.client = client;
            combo = comPorts;
            combo.Items.Clear();
            combo.Items.Add("Simulator");
            combo.Items.AddRange(SerialPort.GetPortNames());
        }

        private void BStart_Click(object sender, EventArgs e)
        {
            combo.Focus();
            if (combo.SelectedItem.Equals("Simulator")){

                OpenFileDialog browseFileDialog = new OpenFileDialog();
                browseFileDialog.Filter = "JSON (.json)|*.json;";
                browseFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

                if (browseFileDialog.ShowDialog() == DialogResult.OK)
                {
                    path = Path.GetFullPath(browseFileDialog.FileName);
                    string json = File.ReadAllText(path);
                    JArray openedData = (JArray)JsonConvert.DeserializeObject(json);

                    data = (ISet<BikeData>)openedData.ToObject(typeof(ISet<BikeData>));
                }

                bike = new BikeSimulator(this);
                bike.Start();
            }
            else
            {
                connectForm = new ConnectForm(ref hudlock);
                connectForm.Show();

                //new Thread(() => test()).Start();

                bike = new Bike(combo.SelectedItem.ToString(), this, client);
                bike.Start();
            }
        }

        private void test()
        {
            while (!connectForm.Connected()) { }

        }

        protected override void OnFormClosed(FormClosedEventArgs e) {
            Environment.Exit(0);
            base.OnFormClosed(e);
        }

        public void SetPulse(String s){ lblPulse.Text = s; }

        public void SetRoundMin(String s) { lblRoundMin.Text = s; }

        public void SetSpeed(String s)
        {
            lblSpeed.Text = s;
            string speed = s.Replace(",", ".");
            connectForm.connector.SetBikeSpeed(Math.Round(double.Parse(speed)));
        }

        public void SetDistance(String s) { lblDistance.Text = s; }

        public void SetResistance(String s) { lblResistence.Text = s; }

        public void SetEnergy(String s) { lblEnergy.Text = s; }

        public void SetTime(String s) { lblTime.Text = s; }

        public void SetWatt(String s) { lblWatt.Text = s; }

        public void AddMessage(String value)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(AddMessage), new object[] { value });
                return;
            }
            Chat_Box.Text = Chat_Box.Text + value + "\r\n";
            connectForm.connector.hud.SetText(value);
        }

        private void Closing(object sender, FormClosingEventArgs e)
        {
            client.SendMessage("bye");
        }

        public void SetDisplay(double rate, double sp, double dist, double round, double res, double en, string ti, double wat)
        {
            lock (hudlock)
            {
                connectForm.connector.hud.Update2(rate, sp, dist, round, res, en, ti, wat);
            }
        }
    }
}
