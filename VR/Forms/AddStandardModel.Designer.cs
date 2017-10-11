namespace VR
{
    partial class AddStandardModel
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
            this.model_Box = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Position_X_Txt = new System.Windows.Forms.TextBox();
            this.Position_Y_Txt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Position_Z_Txt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Add_Model_Btn = new System.Windows.Forms.Button();
            this.Rotation_Z_Txt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Scale_Txt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // model_Box
            // 
            this.model_Box.FormattingEnabled = true;
            this.model_Box.ItemHeight = 16;
            this.model_Box.Location = new System.Drawing.Point(16, 15);
            this.model_Box.Margin = new System.Windows.Forms.Padding(4);
            this.model_Box.Name = "model_Box";
            this.model_Box.Size = new System.Drawing.Size(159, 164);
            this.model_Box.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(184, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "X:";
            // 
            // Position_X_Txt
            // 
            this.Position_X_Txt.Location = new System.Drawing.Point(215, 15);
            this.Position_X_Txt.Margin = new System.Windows.Forms.Padding(4);
            this.Position_X_Txt.Name = "Position_X_Txt";
            this.Position_X_Txt.Size = new System.Drawing.Size(132, 22);
            this.Position_X_Txt.TabIndex = 7;
            // 
            // Position_Y_Txt
            // 
            this.Position_Y_Txt.Location = new System.Drawing.Point(215, 47);
            this.Position_Y_Txt.Margin = new System.Windows.Forms.Padding(4);
            this.Position_Y_Txt.Name = "Position_Y_Txt";
            this.Position_Y_Txt.Size = new System.Drawing.Size(132, 22);
            this.Position_Y_Txt.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(184, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Y:";
            // 
            // Position_Z_Txt
            // 
            this.Position_Z_Txt.Location = new System.Drawing.Point(215, 79);
            this.Position_Z_Txt.Margin = new System.Windows.Forms.Padding(4);
            this.Position_Z_Txt.Name = "Position_Z_Txt";
            this.Position_Z_Txt.Size = new System.Drawing.Size(132, 22);
            this.Position_Z_Txt.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(184, 82);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Z:";
            // 
            // Add_Model_Btn
            // 
            this.Add_Model_Btn.Location = new System.Drawing.Point(16, 193);
            this.Add_Model_Btn.Margin = new System.Windows.Forms.Padding(4);
            this.Add_Model_Btn.Name = "Add_Model_Btn";
            this.Add_Model_Btn.Size = new System.Drawing.Size(332, 28);
            this.Add_Model_Btn.TabIndex = 12;
            this.Add_Model_Btn.Text = "Add model";
            this.Add_Model_Btn.UseVisualStyleBackColor = true;
            this.Add_Model_Btn.Click += new System.EventHandler(this.Add_Model_Btn_Click);
            // 
            // Rotation_Z_Txt
            // 
            this.Rotation_Z_Txt.Location = new System.Drawing.Point(256, 115);
            this.Rotation_Z_Txt.Margin = new System.Windows.Forms.Padding(4);
            this.Rotation_Z_Txt.Name = "Rotation_Z_Txt";
            this.Rotation_Z_Txt.Size = new System.Drawing.Size(91, 22);
            this.Rotation_Z_Txt.TabIndex = 13;
            this.Rotation_Z_Txt.TextChanged += new System.EventHandler(this.Rotation_Z_Txt_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(183, 115);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Rotation:";
            // 
            // Scale_Txt
            // 
            this.Scale_Txt.Location = new System.Drawing.Point(256, 157);
            this.Scale_Txt.Margin = new System.Windows.Forms.Padding(4);
            this.Scale_Txt.Name = "Scale_Txt";
            this.Scale_Txt.Size = new System.Drawing.Size(91, 22);
            this.Scale_Txt.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(183, 160);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 17);
            this.label5.TabIndex = 16;
            this.label5.Text = "Scale:";
            // 
            // AddStandardModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 234);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Scale_Txt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Rotation_Z_Txt);
            this.Controls.Add(this.Add_Model_Btn);
            this.Controls.Add(this.Position_Z_Txt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Position_Y_Txt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Position_X_Txt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.model_Box);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AddStandardModel";
            this.Text = "AddStandardModel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Position_X_Txt;
        private System.Windows.Forms.TextBox Position_Y_Txt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Position_Z_Txt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Add_Model_Btn;
        private System.Windows.Forms.TextBox Rotation_Z_Txt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Scale_Txt;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ListBox model_Box;
    }
}