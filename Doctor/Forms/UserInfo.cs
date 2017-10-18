using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserData;

namespace Doctor.Forms {
    public partial class UserInfo : Form {
        private Form searchForm;
        private User user;
        private Client client;
        private Panel panel;
        public UserInfo(User user, Form searchForm, ref Client client, Panel panel) {
            InitializeComponent();
            this.user = user;
            this.searchForm = searchForm;
            this.client = client;
            this.panel = panel;

            patientName.Text = user.FullName;

            dynamic request = new {
                id = "oldsession",
                data = new {
                    hashcode = user.Hashcode
                }
            };

            JObject sessionList;
            lock(client.ReadAndWriteLock) {
                client.SendMessage(request);
                sessionList = JObject.Parse(client.ReadMessage());
            }

            if ((string)sessionList["status"] == "alloldfiles") {
                string[] files = new string[sessionList["data"].Count()];
                int i = 0;
                foreach(JObject o in (JArray)sessionList["data"]) {
                    files[i] = (string) o;
                    i++;
                }
            }
            else {
                listView1.Items.Add("No oldsession");
            }

            textBox1.KeyDown += TextBox1_KeyDown;
            textBox2.KeyDown += TextBox2_KeyDown;
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                if (textBox2.Text.Count() >= 5) {
                    string userHash = Encoding.Default.GetString((new SHA256Managed().ComputeHash(Encoding.Default.GetBytes(textBox2.Text))));
                    dynamic request = new {
                        id = "changepass",
                        data = new {
                            password = userHash,
                            hashcode = user.Hashcode
                        }
                    };

                    JObject response;
                    lock (client.ReadAndWriteLock) {
                        client.SendMessage(request);
                        response = JObject.Parse(client.ReadMessage());
                    }

                    if ((string)response["status"] == "ok") {
                        MessageBox.Show("Username changed");
                    }
                    else {
                        MessageBox.Show("User not found");
                    }
                }
                else {
                    MessageBox.Show("Username is to short");
                }
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                if (textBox1.Text.Count() >= 3) {
                    string userHash = Encoding.Default.GetString((new SHA256Managed().ComputeHash(Encoding.Default.GetBytes(textBox1.Text))));
                    dynamic request = new {
                        id = "changeuser",
                        data = new {
                            username = userHash,
                            hashcode = user.Hashcode
                        }
                    };

                    JObject response;
                    lock (client.ReadAndWriteLock) {
                        client.SendMessage(request);
                        response = JObject.Parse(client.ReadMessage());
                    }

                    if ((string)response["status"] == "ok") {
                        MessageBox.Show("Username changed");
                    }
                    else {
                        MessageBox.Show("User not found");
                    }
                }
                else {
                    MessageBox.Show("Username is to short");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            panel.Controls.RemoveAt(1);
            searchForm.Show();
            this.Close();
            this.Dispose();
        }
    }
}
