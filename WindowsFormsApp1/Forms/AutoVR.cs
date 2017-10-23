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
using VR;

namespace Remote_Healtcare_Console.Forms
{
    public partial class AutoVR : Form
    {
        public Connector connector;
        private string id;
        private List<string> filePath;
        private Dictionary<string, string> keys;
        private object hudlock;

        public AutoVR(ref object hudlock)
        {
            InitializeComponent();
            connector = new Connector();
            filePath = new List<string>();
            keys = new Dictionary<string, string>();
            this.hudlock = hudlock;
            JArray array = connector.GetClients();

            foreach (JObject obj in array)
            {
                string hostInfo = obj["clientinfo"]["user"] + " - " + (string)obj["id"];
                listBox_id.Items.Add(hostInfo);
                keys.Add(hostInfo, (string)obj["id"]);
                string file = (string)obj["clientinfo"]["file"];
                string endOfFile = "\\NetworkEngine.exe";
                filePath.Add(file.Remove(file.Length - endOfFile.Length));
            }
        }

        private void ListBox_Double_Click(object sender, EventArgs e)
        {
            string key = keys[(string)listBox_id.SelectedItem];

            dynamic message = new
            {
                id = "tunnel/create",
                data = new
                {
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
            ControlPanel panel = new ControlPanel(connector, ref hudlock);
            //panel.Closed += (s, args) => this.Close();
            //panel.Show();

            panel.Load_Btn_Click(null, null);

            List<Route> routes = connector.GetRoutes();
            foreach (Route route in routes)
            {
                route.AddRoad();
            }

            ModelFollowRoute followRoute = new ModelFollowRoute(connector);
            followRoute.Show();

            new HUD(ref connector, ref hudlock);

            connector.UpdateNode(connector.GetUUID("Camera"), connector.GetUUID("bike"));

            connector.DeleteGroundplane();
        }
    }
}
