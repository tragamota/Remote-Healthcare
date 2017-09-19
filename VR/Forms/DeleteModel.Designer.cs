namespace VR
{
    partial class DeleteModel
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
            this.Delete_Model_Btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // model_Box
            // 
            this.model_Box.FormattingEnabled = true;
            this.model_Box.Location = new System.Drawing.Point(12, 12);
            this.model_Box.Name = "model_Box";
            this.model_Box.Size = new System.Drawing.Size(120, 82);
            this.model_Box.TabIndex = 6;
            // 
            // Delete_Model_Btn
            // 
            this.Delete_Model_Btn.Location = new System.Drawing.Point(36, 100);
            this.Delete_Model_Btn.Name = "Delete_Model_Btn";
            this.Delete_Model_Btn.Size = new System.Drawing.Size(75, 23);
            this.Delete_Model_Btn.TabIndex = 7;
            this.Delete_Model_Btn.Text = "Delete";
            this.Delete_Model_Btn.UseVisualStyleBackColor = true;
            this.Delete_Model_Btn.Click += new System.EventHandler(this.Delete_Model_Btn_Click);
            // 
            // DeleteModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(152, 133);
            this.Controls.Add(this.Delete_Model_Btn);
            this.Controls.Add(this.model_Box);
            this.Name = "DeleteModel";
            this.Text = "DeleteModel";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox model_Box;
        private System.Windows.Forms.Button Delete_Model_Btn;
    }
}