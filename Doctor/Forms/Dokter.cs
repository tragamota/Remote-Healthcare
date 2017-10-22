using Doctor.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UserData;

namespace Doctor {
    public partial class Dokter : Form {
        private Client client;
        private List<User> users, connectedUsers;
        private string hashcode;

        private object connectedUsersLock;

        public Dokter(Client client, string hashcode) {
            InitializeComponent();
            this.client = client;
            this.hashcode = hashcode;
            connectedUsersLock = new object();
            client.SendMessage(new {
                id = "getPatients"
            });

            string data = client.ReadMessage();
            users = (List<User>)((JArray)JsonConvert.DeserializeObject(data)).ToObject(typeof(List<User>));

            Form searchForm = new Search(ref users, panel1, ref client);
            searchForm.TopLevel = false;
            this.panel1.Controls.Add(searchForm);
            searchForm.Show();

            Update();
        }

        private void Update() {
            Awaiting_Patients_Box.Items.Clear();
            
            client.SendMessage(new {
                id = "getconPatients"
            });

            string data = client.ReadMessage();
            List<User> users = JsonConvert.DeserializeObject<List<User>>(data);

            connectedUsers = users;
            updateListView();

            Awaiting_Patients_Box.MouseDoubleClick += openSessions;

            System.Timers.Timer timer = new System.Timers.Timer(2500);
            timer.AutoReset = true;
            timer.Elapsed += ((sender, e) => Timer_Elapsed(sender, e, timer));
            timer.Start();
        }

        private void openSessions(object sender, MouseEventArgs e) {
            lock (connectedUsersLock) {
                if (Awaiting_Patients_Box.SelectedIndices.Count > 0 && connectedUsers.Count > 0) {
                    foreach (int i in Awaiting_Patients_Box.SelectedIndices) {
                        new Session(connectedUsers[i], ref client, null).Show();
                    }
                }
            }
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e, System.Timers.Timer timer) {
            dynamic request = new {
                id = "getconPatients"
            };
            string data;
            lock (client.ReadAndWriteLock) {
                client.SendMessage(request);
                data = client.ReadMessage();
            }

            List<User> Users2 = (List<User>)((JArray)JsonConvert.DeserializeObject(data)).ToObject(typeof(List<User>));

            bool needUpdate = false;
            foreach (User u in Users2) {
                lock (connectedUsers) {
                    if (!connectedUsers.Contains(u)) {
                        connectedUsers.Add(u);
                        needUpdate = true;
                    }
                }
            }
            if (needUpdate) {
                updateListView();
            }
        }

        private void updateListView() {
            lock (connectedUsersLock) {
                if (Awaiting_Patients_Box.InvokeRequired) {
                    Awaiting_Patients_Box.Invoke((MethodInvoker)delegate {
                        Awaiting_Patients_Box.Items.Clear();
                    });
                }
                else {
                    Awaiting_Patients_Box.Items.Clear();
                }
            }
            if (connectedUsers.Count > 0) {
                lock (connectedUsersLock) {
                    foreach (User user in connectedUsers) {
                        if (Awaiting_Patients_Box.InvokeRequired) {
                            Awaiting_Patients_Box.Invoke((MethodInvoker)delegate {
                                Awaiting_Patients_Box.Items.Add(user.FullName);
                            });
                        }
                        else {
                            Awaiting_Patients_Box.Items.Add(user.FullName);
                        }
                    }
                }
            }
            else {
                lock (connectedUsersLock) {
                    if (Awaiting_Patients_Box.InvokeRequired) {

                        Awaiting_Patients_Box.Invoke((MethodInvoker)delegate {
                            Awaiting_Patients_Box.Items.Add("No pantients connected");
                        });
                    }
                    else {
                        Awaiting_Patients_Box.Items.Add("No pantients connected");
                    }
                }
            }
        }

        private void Log_Out_Btn_Click(object sender, EventArgs e) {
            dynamic message = new {
                id = "disconnect"
            };
            client.SendMessage(message);

            Hide();
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e) {
            new AddUser(ref client).Show();
        }

        private new void Closing(object sender, FormClosingEventArgs e) {
            dynamic message = new {
                id = "disconnect"
            };
            client.SendMessage(message);
            Close();
        }
    }
}
