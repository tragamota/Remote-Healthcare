using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VR {
    partial class DeleteModel : Form {
        private Connector connector;
        private List<Model> models;
        private List<Route> routes;

        public DeleteModel(Connector connector) {
            InitializeComponent();
            this.connector = connector;

            models = connector.GetModels();
            routes = connector.GetRoutes();

            foreach (Model model in models)
                model_Box.Items.Add(model.modelName);
        }

        private void Delete_Model_Btn_Click(object sender, EventArgs e) {
            if (model_Box.SelectedItem != null)
                connector.DeleteNode(models[model_Box.SelectedIndex].uuid);

            models = connector.GetModels();
            model_Box.Items.Clear();

            foreach (Model model in models)
                model_Box.Items.Add(model.modelName);
        }
    }
}
