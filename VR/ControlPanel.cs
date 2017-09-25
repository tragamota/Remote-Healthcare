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
    partial class ControlPanel : Form
    {
        Connector connector;

        public ControlPanel(Connector connector)
        {
            this.connector = connector;
            InitializeComponent();
        }

        private void Add_Model_Click(object sender, EventArgs e)
        {
            AddModel model = new AddModel(connector);
            model.Show();
        }

        private void Add_Terrain_Click(object sender, EventArgs e)
        {
            AddTerrain addTerrain = new AddTerrain(connector);
            addTerrain.Show();
        }

        private void Add_Route_Btn_Click(object sender, EventArgs e)
        {
            connector.AddRoute();
        }

        private void Model_Follow_Route_Btn_Click(object sender, EventArgs e)
        {
            ModelFollowRoute followRoute = new ModelFollowRoute(connector);
            followRoute.Show();
        }

        private void Add_Road_Btn_Click(object sender, EventArgs e)
        {
            AddRoadToRoute addRoad = new AddRoadToRoute(connector);
            addRoad.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string uuid = connector.GetUUID("HUDPanel");
            string cameraID = connector.GetUUID("Camera");
            connector.UpdateNode(uuid, cameraID);
        }

        private void HUDButton_Click(object sender, EventArgs e)
        {
           HUD hud = new HUD(connector);
        }
    }
}
