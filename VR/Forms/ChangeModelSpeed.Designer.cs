namespace VR
{
    partial class ChangeModelSpeed
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
            this.label1 = new System.Windows.Forms.Label();
            this.Speed_Txt = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.model_Box = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Speed";
            // 
            // Speed_Txt
            // 
            this.Speed_Txt.Location = new System.Drawing.Point(55, 120);
            this.Speed_Txt.Name = "Speed_Txt";
            this.Speed_Txt.Size = new System.Drawing.Size(79, 20);
            this.Speed_Txt.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(25, 146);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Set speed";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Set_Speed_Btn);
            // 
            // model_Box
            // 
            this.model_Box.FormattingEnabled = true;
            this.model_Box.Location = new System.Drawing.Point(14, 12);
            this.model_Box.Name = "model_Box";
            this.model_Box.Size = new System.Drawing.Size(120, 95);
            this.model_Box.TabIndex = 4;
            // 
            // ChangeModelSpeed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(151, 182);
            this.Controls.Add(this.model_Box);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Speed_Txt);
            this.Controls.Add(this.label1);
            this.Name = "ChangeModelSpeed";
            this.Text = "ChangeModelSpeed";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Speed_Txt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox model_Box;
    }
}