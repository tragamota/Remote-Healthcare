namespace VR
{
    partial class TerrainGrid
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Terrain_Grid_Id = new System.Windows.Forms.TableLayoutPanel();
            this.Set_Height_Btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Terrain_Grid_Id
            // 
            this.Terrain_Grid_Id.AutoSize = true;
            this.Terrain_Grid_Id.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Terrain_Grid_Id.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.Terrain_Grid_Id.ColumnCount = 2;
            this.Terrain_Grid_Id.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 139F));
            this.Terrain_Grid_Id.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 548F));
            this.Terrain_Grid_Id.Dock = System.Windows.Forms.DockStyle.Top;
            this.Terrain_Grid_Id.Location = new System.Drawing.Point(0, 0);
            this.Terrain_Grid_Id.Name = "Terrain_Grid_Id";
            this.Terrain_Grid_Id.RowCount = 2;
            this.Terrain_Grid_Id.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 129F));
            this.Terrain_Grid_Id.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.Terrain_Grid_Id.Size = new System.Drawing.Size(684, 142);
            this.Terrain_Grid_Id.TabIndex = 1;
            // 
            // Set_Height_Btn
            // 
            this.Set_Height_Btn.Location = new System.Drawing.Point(270, 526);
            this.Set_Height_Btn.Name = "Set_Height_Btn";
            this.Set_Height_Btn.Size = new System.Drawing.Size(75, 23);
            this.Set_Height_Btn.TabIndex = 2;
            this.Set_Height_Btn.Text = "Set heights";
            this.Set_Height_Btn.UseVisualStyleBackColor = true;
            this.Set_Height_Btn.Click += new System.EventHandler(this.Set_Height_Btn_Click);
            // 
            // TerrainGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 561);
            this.Controls.Add(this.Set_Height_Btn);
            this.Controls.Add(this.Terrain_Grid_Id);
            this.Name = "TerrainGrid";
            this.Text = "TerrainGrid";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel Terrain_Grid_Id;
        private System.Windows.Forms.Button Set_Height_Btn;
    }
}