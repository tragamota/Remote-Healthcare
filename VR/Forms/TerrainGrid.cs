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
    partial class TerrainGrid : Form {
        private AddTerrain addTerrain;

        public TerrainGrid(AddTerrain addTerrain, int width, int length, int minHeight, int maxHeight) {
            this.addTerrain = addTerrain;
            InitializeComponent();

            int[] heights = new int[width * length];

            for (int i = 0; i < heights.Length; i++) {
                heights[i] = 0;
            }

            Terrain_Grid_Id.ColumnCount += width - 2;
            Terrain_Grid_Id.RowCount += length - 2;

            float cellWidth = float.Parse((this.Width / width).ToString());
            float cellHeight = float.Parse((this.Height / length).ToString());

            for (int x = 0; x < Terrain_Grid_Id.ColumnCount; x++) {
                Terrain_Grid_Id.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute));
                Terrain_Grid_Id.ColumnStyles[x].Width = cellWidth * 0.9F;
            }

            for (int y = 0; y < Terrain_Grid_Id.RowCount; y++) {
                Terrain_Grid_Id.RowStyles.Add(new RowStyle(SizeType.Absolute));
                Terrain_Grid_Id.RowStyles[y].Height = cellHeight * 0.9F;
            }

            for (int x = 0; x < Terrain_Grid_Id.RowCount; x++) {
                for (int y = 0; y < Terrain_Grid_Id.ColumnCount; y++) {
                    NumericUpDown label = new NumericUpDown();
                    label.Minimum = minHeight;
                    label.Maximum = maxHeight;
                    Terrain_Grid_Id.Controls.Add(label, y, x);
                }
            }

            Set_Height_Btn.Location = new Point(this.Width / 2 - Set_Height_Btn.Width / 2, Terrain_Grid_Id.Height + 10);
        }

        private void Set_Height_Btn_Click(object sender, EventArgs e) {
            int[] heights = new int[Terrain_Grid_Id.ColumnCount * Terrain_Grid_Id.RowCount];
            for (int y = 0; y < Terrain_Grid_Id.RowCount; y++) {
                for (int x = 0; x < Terrain_Grid_Id.ColumnCount; x++) {
                    heights[(y * Terrain_Grid_Id.ColumnCount) + x] = int.Parse(Terrain_Grid_Id.GetControlFromPosition(x, y).Text);
                }
            }
            addTerrain.heightValues = heights;
            this.Hide();
        }
    }
}
