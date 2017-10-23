namespace Remote_Healtcare_Console.Forms
{
    partial class AutoVR
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
            this.listBox_id = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBox_id
            // 
            this.listBox_id.FormattingEnabled = true;
            this.listBox_id.Location = new System.Drawing.Point(12, 12);
            this.listBox_id.Name = "listBox_id";
            this.listBox_id.Size = new System.Drawing.Size(170, 108);
            this.listBox_id.TabIndex = 1;
            this.listBox_id.DoubleClick += new System.EventHandler(this.ListBox_Double_Click);
            // 
            // AutoVR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(197, 133);
            this.Controls.Add(this.listBox_id);
            this.Name = "AutoVR";
            this.Text = "AutoVR";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_id;
    }
}