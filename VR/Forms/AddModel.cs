using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VR {
    partial class AddModel : Form {
        private Connector connector;

        public AddModel(Connector connector) {
            this.connector = connector;
            InitializeComponent();
        }

        private void Add_Btn_Click(object sender, EventArgs e)
        {
            if (Name_Txt.Text.Length > 0 && X_Txt.Text.Length > 0 && Y_Txt.Text.Length > 0 && Z_Txt.Text.Length > 0 && Object_Name_Txt.Text.Length > 0)
            {
                connector.AddModel(Name_Txt.Text, Object_Name_Txt.Text, int.Parse(X_Txt.Text), int.Parse(Y_Txt.Text), int.Parse(Z_Txt.Text));
                this.Hide();
            }
            else
            {
                MessageBox.Show("Vul al de velden in");
            }
        }

        private void Browse_Btn_Click(object sender, EventArgs e) {
            OpenFileDialog browseFileDialog = new OpenFileDialog();
            browseFileDialog.Filter = "Obj (.obj)|*.obj;";
            browseFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            if (browseFileDialog.ShowDialog() == DialogResult.OK) {
                string path = Path.GetFullPath(browseFileDialog.FileName);
                Object_Name_Txt.Text = path;
            }
        }
    }
}
