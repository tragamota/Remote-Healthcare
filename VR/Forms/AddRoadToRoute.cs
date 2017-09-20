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
    partial class AddRoadToRoute : Form {
        private Connector connector;

        public AddRoadToRoute(Connector connector) {
            this.connector = connector;
            InitializeComponent();

            List<Route> routes = connector.GetRoutes();
            foreach (Route route in routes) {
                route_Box.Items.Add(route);
            }
        }

        private void Add_Road_Btn_Click(object sender, EventArgs e) {
            Route route = (Route)route_Box.SelectedItem;
            route.AddRoad();
            Hide();
        }
    }
}
