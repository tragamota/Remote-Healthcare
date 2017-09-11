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

namespace VR
{
    public partial class Form1 : Form
    {

        private Connector connector;
        private string id;

        public Form1()
        {
            InitializeComponent();
            connector = new Connector();
            foreach (string id in connector.GetClients().Keys)
            {
                listBox_id.Items.Add(id);
            }
        }

        private void Connect_Btn_Click(object sender, EventArgs e)
        {
            string key = @"""" + connector.GetClients()[listBox_id.SelectedItem.ToString()] + @"""";
            string message = @"{""id"" : ""tunnel/create"",""data"" : {""session"" : " + key + @",""key"" : """"}}";

            connector.SendMessage(message);
            JObject jObject = connector.ReadMessage();
            id = (string)jObject.SelectToken("data").SelectToken("id");

            connector.ChangeScene(id, "scene/reset");
        }
    }
}
