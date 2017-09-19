namespace VR
{
    partial class AddModel
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
            this.Name_Txt = new System.Windows.Forms.TextBox();
            this.X_Txt = new System.Windows.Forms.TextBox();
            this.Y_Txt = new System.Windows.Forms.TextBox();
            this.Z_Txt = new System.Windows.Forms.TextBox();
            this.Browse_Btn = new System.Windows.Forms.Button();
            this.Add_Btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Object_Name_Txt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Name_Txt
            // 
            this.Name_Txt.Location = new System.Drawing.Point(60, 12);
            this.Name_Txt.Name = "Name_Txt";
            this.Name_Txt.Size = new System.Drawing.Size(100, 20);
            this.Name_Txt.TabIndex = 0;
            // 
            // X_Txt
            // 
            this.X_Txt.Location = new System.Drawing.Point(60, 38);
            this.X_Txt.Name = "X_Txt";
            this.X_Txt.Size = new System.Drawing.Size(100, 20);
            this.X_Txt.TabIndex = 1;
            // 
            // Y_Txt
            // 
            this.Y_Txt.Location = new System.Drawing.Point(60, 64);
            this.Y_Txt.Name = "Y_Txt";
            this.Y_Txt.Size = new System.Drawing.Size(100, 20);
            this.Y_Txt.TabIndex = 2;
            // 
            // Z_Txt
            // 
            this.Z_Txt.Location = new System.Drawing.Point(60, 90);
            this.Z_Txt.Name = "Z_Txt";
            this.Z_Txt.Size = new System.Drawing.Size(100, 20);
            this.Z_Txt.TabIndex = 3;
            // 
            // Browse_Btn
            // 
            this.Browse_Btn.Location = new System.Drawing.Point(60, 116);
            this.Browse_Btn.Name = "Browse_Btn";
            this.Browse_Btn.Size = new System.Drawing.Size(100, 23);
            this.Browse_Btn.TabIndex = 4;
            this.Browse_Btn.Text = "Browse";
            this.Browse_Btn.UseVisualStyleBackColor = true;
            this.Browse_Btn.Click += new System.EventHandler(this.Browse_Btn_Click);
            // 
            // Add_Btn
            // 
            this.Add_Btn.Location = new System.Drawing.Point(29, 157);
            this.Add_Btn.Name = "Add_Btn";
            this.Add_Btn.Size = new System.Drawing.Size(100, 23);
            this.Add_Btn.TabIndex = 5;
            this.Add_Btn.Text = "Add";
            this.Add_Btn.UseVisualStyleBackColor = true;
            this.Add_Btn.Click += new System.EventHandler(this.Add_Btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "X:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Y:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Z:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Object:";
            // 
            // Object_Name_Txt
            // 
            this.Object_Name_Txt.AutoSize = true;
            this.Object_Name_Txt.Location = new System.Drawing.Point(166, 121);
            this.Object_Name_Txt.MaximumSize = new System.Drawing.Size(130, 0);
            this.Object_Name_Txt.MinimumSize = new System.Drawing.Size(130, 0);
            this.Object_Name_Txt.Name = "Object_Name_Txt";
            this.Object_Name_Txt.Size = new System.Drawing.Size(130, 13);
            this.Object_Name_Txt.TabIndex = 11;
            // 
            // AddModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 186);
            this.Controls.Add(this.Object_Name_Txt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Add_Btn);
            this.Controls.Add(this.Browse_Btn);
            this.Controls.Add(this.Z_Txt);
            this.Controls.Add(this.Y_Txt);
            this.Controls.Add(this.X_Txt);
            this.Controls.Add(this.Name_Txt);
            this.Name = "AddModel";
            this.Text = "AddModel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Name_Txt;
        private System.Windows.Forms.TextBox X_Txt;
        private System.Windows.Forms.TextBox Y_Txt;
        private System.Windows.Forms.TextBox Z_Txt;
        private System.Windows.Forms.Button Browse_Btn;
        private System.Windows.Forms.Button Add_Btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Object_Name_Txt;
    }
}