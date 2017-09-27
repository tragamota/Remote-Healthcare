namespace VR {
    partial class ControlPanel {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.AddTree = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Delete_Model_Btn = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.Speed_Model_Btn = new System.Windows.Forms.Button();
            this.Add_Terrain_By_Picture_Btn = new System.Windows.Forms.Button();
            this.Add_Standard_Model_Btn = new System.Windows.Forms.Button();
            this.Delete_Ground_Btn = new System.Windows.Forms.Button();
            this.HUDButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddTree
            // 
            this.AddTree.Location = new System.Drawing.Point(12, 25);
            this.AddTree.Name = "AddTree";
            this.AddTree.Size = new System.Drawing.Size(100, 25);
            this.AddTree.TabIndex = 0;
            this.AddTree.Text = "Add model";
            this.AddTree.UseVisualStyleBackColor = true;
            this.AddTree.Click += new System.EventHandler(this.Add_Model_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 25);
            this.button1.TabIndex = 1;
            this.button1.Text = "Add terrain";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Add_Terrain_Click);
            // 
            // Delete_Model_Btn
            // 
            this.Delete_Model_Btn.Location = new System.Drawing.Point(181, 89);
            this.Delete_Model_Btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Delete_Model_Btn.Name = "Delete_Model_Btn";
            this.Delete_Model_Btn.Size = new System.Drawing.Size(115, 25);
            this.Delete_Model_Btn.TabIndex = 2;
            this.Delete_Model_Btn.Text = "Delete model";
            this.Delete_Model_Btn.UseVisualStyleBackColor = true;
            this.Delete_Model_Btn.Click += new System.EventHandler(this.Delete_Model_Btn_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(181, 56);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 25);
            this.button3.TabIndex = 3;
            this.button3.Text = "Delete terrain";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 89);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 25);
            this.button4.TabIndex = 4;
            this.button4.Text = "Add route";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Add_Route_Btn_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(181, 119);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(115, 25);
            this.button5.TabIndex = 5;
            this.button5.Text = "Delete route";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(60, 255);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(170, 25);
            this.button6.TabIndex = 6;
            this.button6.Text = "Make model follow route";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.Model_Follow_Route_Btn_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(60, 224);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(170, 25);
            this.button7.TabIndex = 7;
            this.button7.Text = "Add road to route";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.Add_Road_Btn_Click);
            // 
            // Speed_Model_Btn
            // 
            this.Speed_Model_Btn.Location = new System.Drawing.Point(60, 316);
            this.Speed_Model_Btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Speed_Model_Btn.Name = "Speed_Model_Btn";
            this.Speed_Model_Btn.Size = new System.Drawing.Size(170, 25);
            this.Speed_Model_Btn.TabIndex = 8;
            this.Speed_Model_Btn.Text = "Change speed of model";
            this.Speed_Model_Btn.UseVisualStyleBackColor = true;
            this.Speed_Model_Btn.Click += new System.EventHandler(this.Speed_Model_Btn_Click);
            // 
            // Add_Terrain_By_Picture_Btn
            // 
            this.Add_Terrain_By_Picture_Btn.Location = new System.Drawing.Point(60, 194);
            this.Add_Terrain_By_Picture_Btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Add_Terrain_By_Picture_Btn.Name = "Add_Terrain_By_Picture_Btn";
            this.Add_Terrain_By_Picture_Btn.Size = new System.Drawing.Size(170, 25);
            this.Add_Terrain_By_Picture_Btn.TabIndex = 9;
            this.Add_Terrain_By_Picture_Btn.Text = "Add terrain by picture";
            this.Add_Terrain_By_Picture_Btn.UseVisualStyleBackColor = true;
            this.Add_Terrain_By_Picture_Btn.Click += new System.EventHandler(this.Add_Terrain_By_Picture_Btn_Click);
            // 
            // Add_Standard_Model_Btn
            // 
            this.Add_Standard_Model_Btn.Location = new System.Drawing.Point(60, 165);
            this.Add_Standard_Model_Btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Add_Standard_Model_Btn.Name = "Add_Standard_Model_Btn";
            this.Add_Standard_Model_Btn.Size = new System.Drawing.Size(170, 25);
            this.Add_Standard_Model_Btn.TabIndex = 10;
            this.Add_Standard_Model_Btn.Text = "Add standard model";
            this.Add_Standard_Model_Btn.UseVisualStyleBackColor = true;
            this.Add_Standard_Model_Btn.Click += new System.EventHandler(this.Add_Standard_Model_Btn_Click);
            // 
            // Delete_Ground_Btn
            // 
            this.Delete_Ground_Btn.Location = new System.Drawing.Point(181, 25);
            this.Delete_Ground_Btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Delete_Ground_Btn.Name = "Delete_Ground_Btn";
            this.Delete_Ground_Btn.Size = new System.Drawing.Size(115, 25);
            this.Delete_Ground_Btn.TabIndex = 11;
            this.Delete_Ground_Btn.Text = "Delete groundplane";
            this.Delete_Ground_Btn.UseVisualStyleBackColor = true;
            this.Delete_Ground_Btn.Click += new System.EventHandler(this.Delete_Ground_Btn_Click);
            // 
            // HUDButton
            // 
            this.HUDButton.Location = new System.Drawing.Point(60, 286);
            this.HUDButton.Name = "HUDButton";
            this.HUDButton.Size = new System.Drawing.Size(170, 25);
            this.HUDButton.TabIndex = 8;
            this.HUDButton.Text = "Add HUD";
            this.HUDButton.UseVisualStyleBackColor = true;
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 357);
            this.Controls.Add(this.Delete_Ground_Btn);
            this.Controls.Add(this.Add_Standard_Model_Btn);
            this.Controls.Add(this.Add_Terrain_By_Picture_Btn);
            this.Controls.Add(this.Speed_Model_Btn);
            this.Controls.Add(this.HUDButton);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.Delete_Model_Btn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.AddTree);
            this.Name = "ControlPanel";
            this.Text = "ControlPanel";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AddTree;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Delete_Model_Btn;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button Speed_Model_Btn;
        private System.Windows.Forms.Button Add_Terrain_By_Picture_Btn;
        private System.Windows.Forms.Button Add_Standard_Model_Btn;
        private System.Windows.Forms.Button Delete_Ground_Btn;
        private System.Windows.Forms.Button HUDButton;
    }
}