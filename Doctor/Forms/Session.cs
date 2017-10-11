using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using System.Windows.Forms;
using UserData;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doctor
{
    public partial class Session : Form
    {
        User patient;
        Client client;

        List<BikeData> graphhistory;
        List<int> pulsehistory;
        List<int> roundhistory;
        List<double> speedhistory;
        List<int> distancehistory;
        List<int> resistancehistory;
        List<int> energyhistory;
        List<int> generatedhistory;

        object sessionLock = new object();

        Thread UpdateThread;
        Thread GraphThread;

        public Session(User patient, Client client, string hashcode)
        {
            InitializeComponent();
            this.patient = patient;
            this.client = client;

            Stop_Session_Btn.Enabled = false;

            client.SendMessage(new
            {
                id = "setpatient",
                data = new
                {
                    doctor = new
                    {
                        id = "setdoctor",
                        doctor = hashcode
                    },
                    patient = patient
                }
            });

            UpdateThread = new Thread(run);
        }

        private void run()
        {
            pulsehistory = new List<int>();
            speedhistory = new List<double>();
            roundhistory = new List<int>();
            distancehistory = new List<int>();
            resistancehistory = new List<int>();
            energyhistory = new List<int>();
            generatedhistory = new List<int>();
            
            while (true)
            {
                client.SendMessage(new
                {
                    id = "reqSession",
                    data = new
                    {
                        hashcode = patient.Hashcode
                    }
                });

                JObject obj = (JObject)JsonConvert.DeserializeObject(client.ReadMessage());
                switch ((string)obj["id"])
                {
                    case "clientDisconnected":
                        MessageBox.Show($"{patient.FullName} disconnected");
                        this.Hide();
                        break;
                    case "bikeDisconnected":
                        MessageBox.Show($"Fiets van {patient.FullName} disconnected");
                        Start_Session_Btn.Enabled = false;
                        Stop_Session_Btn.Enabled = false;
                        break;
                    case "bikeData":
                        BikeData data = (BikeData)(obj["bikeData"]).ToObject(typeof(BikeData));
                        SetPulse(data.Pulse.ToString());
                        SetRoundMin(data.Rpm.ToString());
                        SetSpeed(data.Speed.ToString());
                        SetDistance(data.Distance.ToString());
                        SetResistance(data.Resistance.ToString());
                        SetEnergy(data.Energy.ToString());
                        SetTime(data.Time.ToString());
                        SetWatt(data.Power.ToString());
                        Application.DoEvents();

                        AddToGraphHistory(data);
                        break;
                }
            }
        }


        public void SetPulse(String s)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetPulse), new object[] { s });
                return;
            }
            lblPulse.Text = s;
            lblPulse.Invalidate();
            lblPulse.Update();
            lblPulse.Refresh();
        }

        public void SetRoundMin(String s)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetRoundMin), new object[] { s });
                return;
            }
            lblRoundMin.Text = s;
            lblRoundMin.Invalidate();
            lblRoundMin.Update();
            lblRoundMin.Refresh();
        }

        public void SetSpeed(String s)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetSpeed), new object[] { s });
                return;
            }
            lblSpeed.Text = s;
            lblSpeed.Invalidate();
            lblSpeed.Update();
            lblSpeed.Refresh();
        }

        public void SetDistance(String s)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetDistance), new object[] { s });
                return;
            }
            lblDistance.Text = s;
            lblDistance.Invalidate();
            lblDistance.Update();
            lblDistance.Refresh();
        }

        public void SetResistance(String s)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetResistance), new object[] { s });
                return;
            }
            lblResistence.Text = s;
            lblResistence.Invalidate();
            lblResistence.Update();
            lblResistence.Refresh();

            if (!s.Equals("0"))
            {
                Resistance_Track_Bar.Value = (int.Parse(s) - 25) * 100 / 375;
                Scrolling(null, null);
            }
        }

        public void SetEnergy(String s)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetEnergy), new object[] { s });
                return;
            }
            lblEnergy.Text = s;
            lblEnergy.Invalidate();
            lblEnergy.Update();
            lblEnergy.Refresh();
        }

        public void SetTime(String s)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetTime), new object[] { s });
                return;
            }
            lblTime.Text = s;
            lblTime.Invalidate();
            lblTime.Update();
            lblTime.Refresh();
        }

        public void SetWatt(String s)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetWatt), new object[] { s });
                return;
            }
            lblWatt.Text = s;
            lblWatt.Invalidate();
            lblWatt.Update();
            lblWatt.Refresh();
        }

        private void Closing(object sender, FormClosingEventArgs e)
        {
            Stop_Session_Btn_Click(null, null);
            client.SendMessage("bye");
        }

        private void Scrolling(object sender, EventArgs e)
        {
            Temp_Resistance_Lbl.Text = Resistance_Track_Bar.Value + " %";
            Temp_Resistance_Lbl.Invalidate();
            Temp_Resistance_Lbl.Update();
            Temp_Resistance_Lbl.Refresh();
        }

        private void Stopped_Scrolling(object sender, MouseEventArgs e)
        {
            lblResistence.Text = Resistance_Track_Bar.Value.ToString();

            int resistance = Resistance_Track_Bar.Value * 375 / 100 + 25;

            client.SendMessage(new
            {
                id = "committingChanges",
                data = new
                {
                    hashcode = patient.Hashcode,
                    data = new
                    {
                        id = "setResistance",
                        data = new
                        {
                            resistance = resistance
                        }
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
            Start_Session_Btn.Enabled = false;
            Stop_Session_Btn.Enabled = true;

            client.SendMessage(new
            {
                id = "startrecording",
                data = new
                {
                    hashcode = patient.Hashcode
                }
            });
            
            UpdateThread.Start();
        }

        private void Stop_Session_Btn_Click(object sender, EventArgs e)
        {
            Start_Session_Btn.Enabled = true;
            Stop_Session_Btn.Enabled = false;

            client.SendMessage(new
            {
                id = "stoprecording"
            });

            UpdateThread.Abort();
        }


        private void AddToGraphHistory(BikeData bike)
        {
            lock (sessionLock)
            {
                pulsehistory.Add(bike.Pulse);
                roundhistory.Add(bike.Rpm);
                speedhistory.Add(bike.Speed);
                distancehistory.Add(bike.Distance);
                resistancehistory.Add(bike.Resistance);
                energyhistory.Add(bike.Energy);
                generatedhistory.Add(bike.Power);
            }
            
            if (grafiek.IsHandleCreated)
            {
                this.Invoke(
                    (MethodInvoker)delegate
                        {
                            UpdateGrafiek();
                        }
                     );
            }
            else
            {
                Console.WriteLine("ERROR Grafiek niet aangemaakt");
            }
        }

        private void UpdateGrafiek()
        {
            lock (sessionLock)
            {
                foreach (var Serie in grafiek.Series)
                {
                    Serie.Points.Clear();
                }
            }

            lock (sessionLock)
            {
                foreach (int hartslag in pulsehistory)
                {
                    grafiek.Series.FindByName("Hartslag").Points.AddY(hartslag);
                }
            }

            lock (sessionLock)
            {
                foreach (int round in roundhistory)
                {
                    grafiek.Series.FindByName("RPM").Points.AddY(round);
                }
            }

            lock (sessionLock)
            {
                foreach (int speed in speedhistory)
                {
                    grafiek.Series.FindByName("Snelheid").Points.AddY(speed);
                }
            }

            lock (sessionLock)
            {
                foreach (int distance in distancehistory)
                {
                    grafiek.Series.FindByName("Afstand").Points.AddY(distance);
                }
            }

            lock (sessionLock)
            {
                foreach (int weerstand in resistancehistory)
                {
                    grafiek.Series.FindByName("Weerstand").Points.AddY((weerstand - 25) * 100 / 375);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client.SendMessage(new
            {
                id = "committingChanges",
                data = new
                {
                    hashcode = patient.Hashcode,
                    data = new
                    {
                        id = "setResistance",
                        data = new
                        {
                            resistance = 25
                        }
                    }
                }
            });

            client.SendMessage(new
            {
                id = "stoprecording"
            });

            UpdateThread.Abort();


        }
    }   

}

