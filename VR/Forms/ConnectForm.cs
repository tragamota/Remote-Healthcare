using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VR {
    public partial class ConnectForm : Form {
        public Connector connector;
        private string id;

        public ConnectForm() {
            InitializeComponent();
            connector = new Connector();
            foreach (string id in connector.GetClients().Keys) {
                listBox_id.Items.Add(id);
            }
        }

        public bool Connected()
        {
            return id != null;
        }

        private void Connect_Btn_Click(object sender, EventArgs e) {
            string key = connector.GetClients()[listBox_id.SelectedItem.ToString()];

            dynamic message = new {
                id = "tunnel/create",
                data = new {
                    session = key,
                    key = ""
                }
            };

            connector.SendMessage(message);
            JObject jObject = connector.ReadMessage();
            id = (string)jObject.SelectToken("data").SelectToken("id");
            connector.SetId(id);

            this.Hide();
            ControlPanel panel = new ControlPanel(connector);
            panel.Closed += (s, args) => this.Close();
            panel.Show();
        }
    }
}
