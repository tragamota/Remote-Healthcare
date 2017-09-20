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
            this.SuspendLayout();
            // 
            // model_Box
            // 
            this.model_Box.FormattingEnabled = true;
            this.model_Box.Location = new System.Drawing.Point(12, 12);
            this.model_Box.Name = "model_Box";
            this.model_Box.Size = new System.Drawing.Size(120, 82);
            this.model_Box.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(138, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "X:";
            // 
            // Position_X_Txt
            // 
            this.Position_X_Txt.Location = new System.Drawing.Point(161, 12);
            this.Position_X_Txt.Name = "Position_X_Txt";
            this.Position_X_Txt.Size = new System.Drawing.Size(100, 20);
            this.Position_X_Txt.TabIndex = 7;
            // 
            // Position_Y_Txt
            // 
            this.Position_Y_Txt.Location = new System.Drawing.Point(161, 38);
            this.Position_Y_Txt.Name = "Position_Y_Txt";
            this.Position_Y_Txt.Size = new System.Drawing.Size(100, 20);
            this.Position_Y_Txt.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(138, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Y:";
            // 
            // Position_Z_Txt
            // 
            this.Position_Z_Txt.Location = new System.Drawing.Point(161, 64);
            this.Position_Z_Txt.Name = "Position_Z_Txt";
            this.Position_Z_Txt.Size = new System.Drawing.Size(100, 20);
            this.Position_Z_Txt.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(138, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Z:";
            // 
            // Add_Model_Btn
            // 
            this.Add_Model_Btn.Location = new System.Drawing.Point(12, 100);
            this.Add_Model_Btn.Name = "Add_Model_Btn";
            this.Add_Model_Btn.Size = new System.Drawing.Size(249, 23);
            this.Add_Model_Btn.TabIndex = 12;
            this.Add_Model_Btn.Text = "Add model";
            this.Add_Model_Btn.UseVisualStyleBackColor = true;
            this.Add_Model_Btn.Click += new System.EventHandler(this.Add_Model_Btn_Click);
            // 
            // AddStandardModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 135);
            this.Controls.Add(this.Add_Model_Btn);
            this.Controls.Add(this.Position_Z_Txt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Position_Y_Txt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Position_X_Txt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.model_Box);
            this.Name = "AddStandardModel";
            this.Text = "AddStandardModel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox model_Box;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Position_X_Txt;
        private System.Windows.Forms.TextBox Position_Y_Txt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Position_Z_Txt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Add_Model_Btn;
    }
}