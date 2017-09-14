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
    partial class ModelFollowRoute : Form
    {
        Connector connector;

        public ModelFollowRoute(Connector connector)
        {
            this.connector = connector;
            InitializeComponent();

            List<Model> models = connector.GetModels();
            List<Route> routes = connector.GetRoutes();

            foreach (Model model in models)
            {
                model_Box.Items.Add(model);
            }

            foreach (Route route in routes)
            {
                route_Box.Items.Add(route);
            }
        }

        private void Follow_Btn_Click(object sender, EventArgs e)
        {
            Route route = (Route)route_Box.SelectedItem;
            route.MakeModelFollowRoute((Model)model_Box.SelectedItem);
            this.Hide();
        }
    }
}
