using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Connector connector = new Connector();
            foreach (string id in connector.GetClients().Keys)
            {
                listBox_id.Items.Add(id);
            }
        }

        private void Connect_Btn_Click(object sender, EventArgs e)
        {
            
        }
    }
}
