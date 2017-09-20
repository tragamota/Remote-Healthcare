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
    partial class ChangeModelSpeed : Form {
        Connector connector;

        public ChangeModelSpeed(Connector connector) {
            InitializeComponent();
            this.connector = connector;

            List<Model> models = connector.GetModels();

            foreach (Model model in models) {
                model_Box.Items.Add(model);
            }
        }

        private void Set_Speed_Btn(object sender, EventArgs e) {
            if (model_Box.SelectedItem != null) {

                Model model = (Model)model_Box.SelectedItem;
                model.ChangeSpeed(double.Parse(Speed_Txt.Text));
                this.Hide();
            }
            else {
                MessageBox.Show("Kies een model");
            }
        }
    }
}
