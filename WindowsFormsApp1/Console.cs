using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace Remote_Healtcare_Console
{
    partial class Console : Form
    {

        ComboBox combo;

        public Console()
        {
            InitializeComponent();
            combo = comPorts;
            combo.Items.Clear();
            combo.Items.Add("Simulator");
            String[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                combo.Items.Add(port);
            }
        }

        private void BStart_Click(object sender, EventArgs e)
        {
            combo.Focus();
            if (combo.SelectedText.Equals("Simulator")){
                //
            }
            else
            {
                String p = combo.SelectedItem.ToString();

                Bike bike = new Bike(p, this);

                Thread thread = new Thread(bike.Start);
                thread.Start();
            }
        }

        public void SetPulse(String s){ lblPulse.Text = s; }

        public void SetRoundMin(String s) { lblRoundMin.Text = s; }

        public void SetSpeed(String s) { lblSpeed.Text = s; }

        public void SetDistance(String s) { lblDistance.Text = s; }

        public void SetResistance(String s) { lblResistence.Text = s; }

        public void SetEnergy(String s) { lblEnergy.Text = s; }

        public void SetTime(String s) { lblTime.Text = s; }

        public void SetWatt(String s) { lblWatt.Text = s; }
    }
}
