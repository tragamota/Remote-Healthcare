﻿using System;
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
        List<Model> models;
        List<Route> routes;

        public ModelFollowRoute(Connector connector)
        {
            this.connector = connector;
            InitializeComponent();

            models = connector.GetModels();
            routes = connector.GetRoutes();

            foreach (Model model in models)
            {
                model_Box.Items.Add(model.modelName);
            }

            foreach (Route route in routes)
            {
                route_Box.Items.Add(route.routeName);
            }
        }

        private void Follow_Btn_Click(object sender, EventArgs e)
        {
            Route route = routes[route_Box.SelectedIndex];
            route.MakeModelFollowRoute(models[model_Box.SelectedIndex]);
            this.Hide();
        }
    }
}
