namespace Doctor {
    partial class Dokter {
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
            this.Awaiting_Patients_Box = new System.Windows.Forms.ListBox();
            this.Message_Txt_Box = new System.Windows.Forms.TextBox();
            this.Send_Message_Btn = new System.Windows.Forms.Button();
            this.Log_Out_Btn = new System.Windows.Forms.Button();
            this.AddUser = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Awaiting_Patients_Box
            // 
            this.Awaiting_Patients_Box.FormattingEnabled = true;
            this.Awaiting_Patients_Box.Location = new System.Drawing.Point(26, 42);
            this.Awaiting_Patients_Box.Name = "Awaiting_Patients_Box";
            this.Awaiting_Patients_Box.Size = new System.Drawing.Size(173, 355);
            this.Awaiting_Patients_Box.TabIndex = 0;
            // 
            // Message_Txt_Box
            // 
            this.Message_Txt_Box.Location = new System.Drawing.Point(215, 319);
            this.Message_Txt_Box.Multiline = true;
            this.Message_Txt_Box.Name = "Message_Txt_Box";
            this.Message_Txt_Box.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Message_Txt_Box.Size = new System.Drawing.Size(337, 78);
            this.Message_Txt_Box.TabIndex = 2;
            // 
            // Send_Message_Btn
            // 
            this.Send_Message_Btn.Location = new System.Drawing.Point(558, 319);
            this.Send_Message_Btn.Name = "Send_Message_Btn";
            this.Send_Message_Btn.Size = new System.Drawing.Size(61, 78);
            this.Send_Message_Btn.TabIndex = 3;
            this.Send_Message_Btn.Text = "Send";
            this.Send_Message_Btn.UseVisualStyleBackColor = true;
            // 
            // Log_Out_Btn
            // 
            this.Log_Out_Btn.Location = new System.Drawing.Point(542, 12);
            this.Log_Out_Btn.Name = "Log_Out_Btn";
            this.Log_Out_Btn.Size = new System.Drawing.Size(77, 36);
            this.Log_Out_Btn.TabIndex = 4;
            this.Log_Out_Btn.Text = "Log out";
            this.Log_Out_Btn.UseVisualStyleBackColor = true;
            this.Log_Out_Btn.Click += new System.EventHandler(this.Log_Out_Btn_Click);
            // 
            // AddUser
            // 
            this.AddUser.Location = new System.Drawing.Point(411, 208);
            this.AddUser.Name = "AddUser";
            this.AddUser.Size = new System.Drawing.Size(75, 23);
            this.AddUser.TabIndex = 7;
            this.AddUser.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(215, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 252);
            this.panel1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Connected patients";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(215, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Add User";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Dokter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 409);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.AddUser);
            this.Controls.Add(this.Log_Out_Btn);
            this.Controls.Add(this.Send_Message_Btn);
            this.Controls.Add(this.Message_Txt_Box);
            this.Controls.Add(this.Awaiting_Patients_Box);
            this.Name = "Dokter";
            this.Text = "6";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox Awaiting_Patients_Box;
        private System.Windows.Forms.TextBox Message_Txt_Box;
        private System.Windows.Forms.Button Send_Message_Btn;
        private System.Windows.Forms.Button Log_Out_Btn;
        private System.Windows.Forms.Button AddUser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}

