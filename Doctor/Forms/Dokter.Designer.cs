namespace Doctor
{
    partial class Dokter
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
            this.Awaiting_Patients_Box = new System.Windows.Forms.ListBox();
            this.Connect_Btn = new System.Windows.Forms.Button();
            this.Message_Txt_Box = new System.Windows.Forms.TextBox();
            this.Send_Message_Btn = new System.Windows.Forms.Button();
            this.Log_Out_Btn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Old_Sessions_Box = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // Awaiting_Patients_Box
            // 
            this.Awaiting_Patients_Box.FormattingEnabled = true;
            this.Awaiting_Patients_Box.Location = new System.Drawing.Point(12, 12);
            this.Awaiting_Patients_Box.Name = "Awaiting_Patients_Box";
            this.Awaiting_Patients_Box.Size = new System.Drawing.Size(172, 303);
            this.Awaiting_Patients_Box.TabIndex = 0;
            this.Awaiting_Patients_Box.SelectedIndexChanged += new System.EventHandler(this.Patient_Selected);
            // 
            // Connect_Btn
            // 
            this.Connect_Btn.Location = new System.Drawing.Point(12, 323);
            this.Connect_Btn.Name = "Connect_Btn";
            this.Connect_Btn.Size = new System.Drawing.Size(75, 36);
            this.Connect_Btn.TabIndex = 1;
            this.Connect_Btn.Text = "Start session";
            this.Connect_Btn.UseVisualStyleBackColor = true;
            this.Connect_Btn.Click += new System.EventHandler(this.Connect_Btn_Click);
            // 
            // Message_Txt_Box
            // 
            this.Message_Txt_Box.Location = new System.Drawing.Point(361, 281);
            this.Message_Txt_Box.Multiline = true;
            this.Message_Txt_Box.Name = "Message_Txt_Box";
            this.Message_Txt_Box.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Message_Txt_Box.Size = new System.Drawing.Size(337, 78);
            this.Message_Txt_Box.TabIndex = 2;
            // 
            // Send_Message_Btn
            // 
            this.Send_Message_Btn.Location = new System.Drawing.Point(704, 281);
            this.Send_Message_Btn.Name = "Send_Message_Btn";
            this.Send_Message_Btn.Size = new System.Drawing.Size(61, 78);
            this.Send_Message_Btn.TabIndex = 3;
            this.Send_Message_Btn.Text = "Send";
            this.Send_Message_Btn.UseVisualStyleBackColor = true;
            this.Send_Message_Btn.Click += new System.EventHandler(this.Send_Message_Btn_Click);
            // 
            // Log_Out_Btn
            // 
            this.Log_Out_Btn.Location = new System.Drawing.Point(93, 323);
            this.Log_Out_Btn.Name = "Log_Out_Btn";
            this.Log_Out_Btn.Size = new System.Drawing.Size(77, 36);
            this.Log_Out_Btn.TabIndex = 4;
            this.Log_Out_Btn.Text = "Log out";
            this.Log_Out_Btn.UseVisualStyleBackColor = true;
            this.Log_Out_Btn.Click += new System.EventHandler(this.Log_Out_Btn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(361, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(214, 20);
            this.textBox1.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(361, 50);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(214, 20);
            this.textBox2.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(591, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Old_Sessions_Box
            // 
            this.Old_Sessions_Box.FormattingEnabled = true;
            this.Old_Sessions_Box.Location = new System.Drawing.Point(190, 12);
            this.Old_Sessions_Box.Name = "Old_Sessions_Box";
            this.Old_Sessions_Box.Size = new System.Drawing.Size(165, 303);
            this.Old_Sessions_Box.TabIndex = 8;
            // 
            // Dokter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 371);
            this.Controls.Add(this.Old_Sessions_Box);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Log_Out_Btn);
            this.Controls.Add(this.Send_Message_Btn);
            this.Controls.Add(this.Message_Txt_Box);
            this.Controls.Add(this.Connect_Btn);
            this.Controls.Add(this.Awaiting_Patients_Box);
            this.Name = "Dokter";
            this.Text = "Dokter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox Awaiting_Patients_Box;
        private System.Windows.Forms.Button Connect_Btn;
        private System.Windows.Forms.TextBox Message_Txt_Box;
        private System.Windows.Forms.Button Send_Message_Btn;
        private System.Windows.Forms.Button Log_Out_Btn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox Old_Sessions_Box;
    }
}

