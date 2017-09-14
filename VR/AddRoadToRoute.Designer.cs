namespace VR
{
    partial class AddRoadToRoute
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
            this.route_Box = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // route_Box
            // 
            this.route_Box.FormattingEnabled = true;
            this.route_Box.Location = new System.Drawing.Point(12, 12);
            this.route_Box.Name = "route_Box";
            this.route_Box.Size = new System.Drawing.Size(120, 95);
            this.route_Box.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 113);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Add road";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Add_Road_Btn_Click);
            // 
            // AddRoadToRoute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(150, 146);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.route_Box);
            this.Name = "AddRoadToRoute";
            this.Text = "AddRoadToRoute";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox route_Box;
        private System.Windows.Forms.Button button1;
    }
}