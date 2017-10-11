using Newtonsoft.Json.Linq;
using System;
using System.Windows.Forms;

namespace VR {
    public partial class ControlPanel : Form {
        private Connector connector;

        public ControlPanel(Connector connector) {
            this.connector = connector;
            InitializeComponent();
            connector.LoadSceneModels();
        }

        private void Add_Model_Click(object sender, EventArgs e) {
            AddModel model = new AddModel(connector);
            model.Show();
        }

        private void Add_Terrain_Click(object sender, EventArgs e) {
            AddTerrain addTerrain = new AddTerrain(connector);
            addTerrain.Show();
        }

        private void Add_Route_Btn_Click(object sender, EventArgs e) {
            AddRoute addRoute = new AddRoute(connector);
            addRoute.Show();
        }

        private void Model_Follow_Route_Btn_Click(object sender, EventArgs e) {
            ModelFollowRoute followRoute = new ModelFollowRoute(connector);
            followRoute.Show();
        }

        private void Add_Road_Btn_Click(object sender, EventArgs e) {
            AddRoadToRoute addRoad = new AddRoadToRoute(connector);
            addRoad.Show();
        }

        private void Speed_Model_Btn_Click(object sender, EventArgs e) {
            ChangeModelSpeed modelSpeed = new ChangeModelSpeed(connector);
            modelSpeed.Show();
        }

        private void Add_Terrain_By_Picture_Btn_Click(object sender, EventArgs e) {
            AddTerrainByPicture addTerrain = new AddTerrainByPicture(connector);
            addTerrain.Show();
        }

        private void Add_Standard_Model_Btn_Click(object sender, EventArgs e) {
            AddStandardModel addModel = new AddStandardModel(connector);
            addModel.Show();
        }

        private void Delete_Ground_Btn_Click(object sender, EventArgs e) {
            connector.DeleteGroundplane();
        }

        private void Delete_Model_Btn_Click(object sender, EventArgs e) {
            DeleteModel deleteModel = new DeleteModel(connector);
            deleteModel.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //connector.Moveto(connector.GetUUID("Camera"));
            connector.UpdateNode(connector.GetUUID("Camera"),connector.GetUUID("Bike"));
            //connector.Moveto(connector.GetUUID("Head"));
            //connector.DeleteNode(connector.GetUUID("Camera"));
            //connector.CameraNode();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Console.WriteLine(connector.GetScene().ToString());
        }

        private void Save_Btn_Click(object sender, EventArgs e)
        {
            //connector.Save("3A_Test");
            connector.SaveScene();
        }

        private void Load_Btn_Click(object sender, EventArgs e)
        {
            //connector.Load("3A_Test");
            connector.LoadScene();
        }

        private void buttonWater_Click(object sender, EventArgs e)
        {
            connector.AddWater();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(cameraCheck.Checked)
                connector.UpdateNode(connector.GetUUID("Camera"), connector.GetUUID("Bike"));
            else
                connector.UpdateNode(connector.GetUUID("Camera"), connector.GetUUID("GroundPlane"));

        }
    }
}
