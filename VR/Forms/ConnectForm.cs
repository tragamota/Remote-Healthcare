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
        private List<string> filePath;
        private Dictionary<string, string> keys;

        public ConnectForm() {
            InitializeComponent();
            connector = new Connector();
            filePath = new List<string>();
            keys = new Dictionary<string, string>();

            JArray array = connector.GetClients();

            foreach (JObject obj in array) {
                string hostInfo = obj["clientinfo"]["user"] + " - " + (string)obj["id"];
                listBox_id.Items.Add(hostInfo);
                keys.Add(hostInfo, (string)obj["id"]);
                string file = (string)obj["clientinfo"]["file"];
                string endOfFile = "\\NetworkEngine.exe";
                filePath.Add(file.Remove(file.Length - endOfFile.Length));
            }
        }

        public bool Connected()
        {
            return id != null;
        }

        private void Connect_Btn_Click(object sender, EventArgs e) {
            string key = keys[(string)listBox_id.SelectedItem];

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
            connector.SetFilePath(filePath[listBox_id.SelectedIndex]);

            this.Hide();
            ControlPanel panel = new ControlPanel(connector);
            panel.Closed += (s, args) => this.Close();
            panel.Show();
        }
    }
}
