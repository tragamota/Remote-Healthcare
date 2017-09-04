using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BLog_in_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text.Length > 0 && txtPassword.Text.Length > 0)
            {
                this.Hide();
                Form Form1 = new Console();
                Form1.Closed += (s, args) => this.Close();
                Form1.Show();

            }
        }
    }
}
