using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using System.Windows.Forms;
using UserData;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doctor {
    public partial class Session : Form {
        private User patient;
        private Client client;

        private List<BikeData> sessionAllData;
        private List<BikeData> graphhistory;
        private List<int> pulsehistory;
        private List<int> roundhistory;
        private List<double> speedhistory;
        private List<int> distancehistory;
        private List<int> resistancehistory;
        private List<int> energyhistory;
        private List<int> generatedhistory;

        private object sessionLock = new object();

        private Thread UpdateThread;
        private Thread GraphThread;

        public Session(User patient, ref Client client, string SessionDate) {
            InitializeComponent();
            this.patient = patient;
            this.client = client;

            if (sessionDate != null) {
                DateTime daytime = DateTime.Now;
                sessionDate.Text = daytime.Day + "-" + daytime.Month + "-" + daytime.Year +"\t" + daytime.Hour + ":" + daytime.Minute;
                Start_Session_Btn.Enabled = true;
                Stop_Session_Btn.Enabled = false;

                //dynamic request = new {
                //    id = "reqSession"
                //};

                //lock (client.ReadAndWriteLock) {
                //    client.SendMessage(request);
                //    JObject obj = JObject.Parse(client.ReadMessage());
                //    if ((string) obj["id"] == "sessiondata") {
                //        List<BikeData> datas = JsonConvert.DeserializeObject<List<BikeData>>((string)obj["data"]);
                //        sessionAllData.AddRange(datas);
                //        foreach (BikeData data in datas) {
                //            AddToGraphHistory(data);
                //        }
                //    }
                //}

                sessionAllData = new List<BikeData>();

                UpdateThread = new Thread(run);
            }
            else {
                sessionDate.Text = SessionDate;
                Stop_Session_Btn.Enabled = false;
                Start_Session_Btn.Enabled = false;

                dynamic request = new {
                    id = "oldsession",
                    data = new {
                        hashcode = patient.Hashcode,
                        file = SessionDate
                    }
                };

                lock (client.ReadAndWriteLock) {
                    client.SendMessage(request);
                    JObject obj = JObject.Parse(client.ReadMessage());
                    if ((string)obj["id"] == "sessiondata") {
                        List<BikeData> datas = JsonConvert.DeserializeObject<List<BikeData>>((string)obj["data"]);
                        sessionAllData.AddRange(datas);
                        foreach (BikeData data in datas) {
                            AddToGraphHistory(data);
                        }
                    }
                }
            }
        }

        private void run() {
            pulsehistory = new List<int>();
            speedhistory = new List<double>();
            roundhistory = new List<int>();
            distancehistory = new List<int>();
            resistancehistory = new List<int>();
            energyhistory = new List<int>();
            generatedhistory = new List<int>();

            while (true) {
                JObject obj;
                lock (client.ReadAndWriteLock) {
                    client.SendMessage(new {
                        id = "latestData",
                        data = new {
                            hashcode = patient.Hashcode
                        }
                    });
                    string message = client.ReadMessage();
                    obj = JObject.Parse(message);
                }

                switch ((string)obj["id"]) {
                    case "clientDisconnected":
                        MessageBox.Show($"{patient.FullName} disconnected");
                        this.Hide();
                        UpdateThread.Abort();
                        break;
                    case "bikeDisconnected":
                        MessageBox.Show($"Fiets van {patient.FullName} disconnected");
                        Start_Session_Btn.Enabled = false;
                        Stop_Session_Btn.Enabled = false;
                        break;
                    case "latestdata":
                        List<BikeData> latest = JsonConvert.DeserializeObject<List<BikeData>>((string)obj["data"]);
                        updateAll(latest[latest.Count - 1]);

                        foreach (BikeData data in latest) {
                            if (!sessionAllData.Contains(data)) {
                                sessionAllData.Add(data);
                                AddToGraphHistory(data);
                            }
                        }
                        break;
                }
                Thread.Sleep(750);
            }
        }

        private void updateAll(BikeData data) {
            SetPulse(data.Pulse.ToString());
            SetRoundMin(data.Rpm.ToString());
            SetSpeed(data.Speed.ToString());
            SetDistance(data.Distance.ToString());
            SetResistance(data.Resistance.ToString());
            SetEnergy(data.Energy.ToString());
            SetTime(data.Time.ToString());
            SetWatt(data.Power.ToString());
            Application.DoEvents();
        }

        public void SetPulse(String s) {
            if (InvokeRequired) {
                this.BeginInvoke(new Action<string>(SetPulse), new object[] { s });
                return;
            }
            lblPulse.Text = s;
            lblPulse.Invalidate();
            lblPulse.Update();
            lblPulse.Refresh();
        }

        public void SetRoundMin(String s) {
            if (InvokeRequired) {
                this.BeginInvoke(new Action<string>(SetRoundMin), new object[] { s });
                return;
            }
            lblRoundMin.Text = s;
            lblRoundMin.Invalidate();
            lblRoundMin.Update();
            lblRoundMin.Refresh();
        }

        public void SetSpeed(String s) {
            if (InvokeRequired) {
                this.BeginInvoke(new Action<string>(SetSpeed), new object[] { s });
                return;
            }
            lblSpeed.Text = s;
            lblSpeed.Invalidate();
            lblSpeed.Update();
            lblSpeed.Refresh();
        }

        public void SetDistance(String s) {
            if (InvokeRequired) {
                this.BeginInvoke(new Action<string>(SetDistance), new object[] { s });
                return;
            }
            lblDistance.Text = s;
            lblDistance.Invalidate();
            lblDistance.Update();
            lblDistance.Refresh();
        }

        public void SetResistance(String s) {
            if (InvokeRequired) {
                this.BeginInvoke(new Action<string>(SetResistance), new object[] { s });
                return;
            }
            lblResistence.Text = s;
            lblResistence.Invalidate();
            lblResistence.Update();
            lblResistence.Refresh();

            if (!s.Equals("0")) {
                Resistance_Track_Bar.Value = (int.Parse(s) - 25) * 100 / 375;
                Scrolling(null, null);
            }
        }

        public void SetEnergy(String s) {
            if (InvokeRequired) {
                this.BeginInvoke(new Action<string>(SetEnergy), new object[] { s });
                return;
            }
            lblEnergy.Text = s;
            lblEnergy.Invalidate();
            lblEnergy.Update();
            lblEnergy.Refresh();
        }

        public void SetTime(String s) {
            if (InvokeRequired) {
                this.BeginInvoke(new Action<string>(SetTime), new object[] { s });
                return;
            }
            lblTime.Text = s;
            lblTime.Invalidate();
            lblTime.Update();
            lblTime.Refresh();
        }

        public void SetWatt(String s) {
            if (InvokeRequired) {
                this.BeginInvoke(new Action<string>(SetWatt), new object[] { s });
                return;
            }
            lblWatt.Text = s;
            lblWatt.Invalidate();
            lblWatt.Update();
            lblWatt.Refresh();
        }

        private void Closing(object sender, FormClosingEventArgs e) {
            Stop_Session_Btn_Click(null, null);
            client.SendMessage("bye");
        }

        private void Scrolling(object sender, EventArgs e) {
            Temp_Resistance_Lbl.Text = Resistance_Track_Bar.Value + " %";
            Temp_Resistance_Lbl.Invalidate();
            Temp_Resistance_Lbl.Update();
            Temp_Resistance_Lbl.Refresh();
        }

        private void Stopped_Scrolling(object sender, MouseEventArgs e) {
            lblResistence.Text = Resistance_Track_Bar.Value.ToString();

            int resistance = Resistance_Track_Bar.Value * 375 / 100 + 25;

            client.SendMessage(new {
                id = "setResistance",
                data = new {
                    resistance = resistance,
                    hashcode = patient.Hashcode
                }
            });
        }

        private void Send_Message_Btn_Click(object sender, EventArgs e) {
            client.SendMessage(new {
                id = "chat",
                data = new {
                    message = Message_Txt_Box.Text,
                    hashcode = patient.Hashcode
                }
            });
        }

        private void Start_Session_Btn_Click(object sender, EventArgs e) {
            Start_Session_Btn.Enabled = false;
            Stop_Session_Btn.Enabled = true;

            client.SendMessage(new {
                id = "startrecording",
                data = new {
                    hashcode = patient.Hashcode
                }
            });

            UpdateThread.Start();
        }

        private void Stop_Session_Btn_Click(object sender, EventArgs e) {
            Start_Session_Btn.Enabled = true;
            Stop_Session_Btn.Enabled = false;

            client.SendMessage(new {
                id = "stoprecording",
                data = new {
                    hashcode = patient.Hashcode
                }
            });

            UpdateThread.Abort();
        }


        private void AddToGraphHistory(BikeData bike) {
            lock (sessionLock) {
                pulsehistory.Add(bike.Pulse);
                roundhistory.Add(bike.Rpm);
                speedhistory.Add(bike.Speed);
                distancehistory.Add(bike.Distance);
                resistancehistory.Add(bike.Resistance);
                energyhistory.Add(bike.Energy);
                generatedhistory.Add(bike.Power);
            }

            if (grafiek.IsHandleCreated) {
                this.Invoke((MethodInvoker)delegate {
                    updateGrafiek();
                });
            }
            else {
                Console.WriteLine("ERROR Grafiek niet aangemaakt");
            }
        }

        private void updateGrafiek() {
            lock (sessionLock) {
                foreach (var Serie in grafiek.Series) {
                    Serie.Points.Clear();
                }
            }

            lock (sessionLock) {
                foreach (int hartslag in pulsehistory) {
                    grafiek.Series.FindByName("Hartslag").Points.AddY(hartslag);
                }
            }

            lock (sessionLock) {
                foreach (int round in roundhistory) {
                    grafiek.Series.FindByName("RPM").Points.AddY(round);
                }
            }

            lock (sessionLock) {
                foreach (int speed in speedhistory) {
                    grafiek.Series.FindByName("Snelheid").Points.AddY(speed);
                }
            }

            lock (sessionLock) {
                foreach (int distance in distancehistory) {
                    grafiek.Series.FindByName("Afstand").Points.AddY(distance);
                }
            }

            lock (sessionLock) {
                foreach (int weerstand in resistancehistory) {
                    grafiek.Series.FindByName("Weerstand").Points.AddY((weerstand - 25) * 100 / 375);
                }
            }
        }

        private void emergencyStop(object sender, EventArgs e) {
            client.SendMessage(new {
                id = "setResistance",
                data = new {
                    resistance = 25,
                    hashcode = patient.Hashcode,
                }
            });

            client.SendMessage(new {
                id = "stoprecording"
            });

            UpdateThread.Abort();
        }
    }
}