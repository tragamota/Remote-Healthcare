namespace Doctor
{
    partial class Form1
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
            this.SuspendLayout();
            // 
            // Awaiting_Patients_Box
            // 
            this.Awaiting_Patients_Box.FormattingEnabled = true;
            this.Awaiting_Patients_Box.Location = new System.Drawing.Point(12, 12);
            this.Awaiting_Patients_Box.Name = "Awaiting_Patients_Box";
            this.Awaiting_Patients_Box.Size = new System.Drawing.Size(120, 95);
            this.Awaiting_Patients_Box.TabIndex = 0;
            // 
            // Connect_Btn
            // 
            this.Connect_Btn.Location = new System.Drawing.Point(138, 12);
            this.Connect_Btn.Name = "Connect_Btn";
            this.Connect_Btn.Size = new System.Drawing.Size(75, 95);
            this.Connect_Btn.TabIndex = 1;
            this.Connect_Btn.Text = "Connect";
            this.Connect_Btn.UseVisualStyleBackColor = true;
            this.Connect_Btn.Click += new System.EventHandler(this.Connect_Btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 118);
            this.Controls.Add(this.Connect_Btn);
            this.Controls.Add(this.Awaiting_Patients_Box);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox Awaiting_Patients_Box;
        private System.Windows.Forms.Button Connect_Btn;
    }
}

