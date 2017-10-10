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
    partial class AddTerrainByPicture : Form {
        private Connector connector;

        public AddTerrainByPicture(Connector connector) {
            InitializeComponent();
            this.connector = connector;
        }

        private void Browse_Btn_Click(object sender, EventArgs e) {
            OpenFileDialog browseFileDialog = new OpenFileDialog();
            browseFileDialog.Filter = "JPG (.jpg)|*.jpg|JPEG (.jpeg)|*.jpeg|PNG (.png)|*.png;";
            browseFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            if (browseFileDialog.ShowDialog() == DialogResult.OK) {
                string path = Path.GetFullPath(browseFileDialog.FileName);
                Browse_Path_Lbl.Text = path;
            }
        }

        private void Add_Btn_Click(object sender, EventArgs e) {
            if (Terrain_Name_Txt.Text.Length > 0 &&
                X_Txt.Text.Length > 0 && Y_Txt.Text.Length > 0 && Z_Txt.Text.Length > 0 &&
                Diffuse_Texture_Lbl.Text.Length > 0 && Normal_Texture_Lbl.Text.Length > 0 && Browse_Path_Lbl.Text.Length > 0) {
                connector.AddTerrainByPicture(Terrain_Name_Txt.Text, Diffuse_Texture_Lbl.Text, Normal_Texture_Lbl.Text, int.Parse(Min_Height_Txt.Text), int.Parse(Max_Height_Txt.Text), int.Parse(Fade_Distance_Txt.Text), int.Parse(X_Txt.Text), int.Parse(Y_Txt.Text), int.Parse(Z_Txt.Text), Browse_Path_Lbl.Text);
                this.Hide();
            }
            else {
                MessageBox.Show("Vul al de velden in");
            }
        }

        private void Diffuse_Texture_Btn_Click(object sender, EventArgs e) {
            OpenFileDialog browseFileDialog = new OpenFileDialog();
            browseFileDialog.Filter = "JPG (.jpg)|*.jpg|JPEG (.jpeg)|*.jpeg|PNG (.png)|*.png;";
            browseFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            if (browseFileDialog.ShowDialog() == DialogResult.OK) {
                string path = Path.GetFullPath(browseFileDialog.FileName);
                Diffuse_Texture_Lbl.Text = path;
            }
        }

        private void Normal_Texture_Btn_Click(object sender, EventArgs e) {
            OpenFileDialog browseFileDialog = new OpenFileDialog();
            browseFileDialog.Filter = "JPG (.jpg)|*.jpg|JPEG (.jpeg)|*.jpeg|PNG (.png)|*.png;";
            browseFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            if (browseFileDialog.ShowDialog() == DialogResult.OK) {
                string path = Path.GetFullPath(browseFileDialog.FileName);
                Normal_Texture_Lbl.Text = path;
            }
        }

        private void Picture_Browse_Btn_Click(object sender, EventArgs e) {
            OpenFileDialog browseFileDialog = new OpenFileDialog();
            browseFileDialog.Filter = "JPG (.jpg)|*.jpg|JPEG (.jpeg)|*.jpeg|PNG (.png)|*.png;";
            browseFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            if (browseFileDialog.ShowDialog() == DialogResult.OK) {
                string path = Path.GetFullPath(browseFileDialog.FileName);
                Browse_Path_Lbl.Text = path;
            }
        }
    }
}
