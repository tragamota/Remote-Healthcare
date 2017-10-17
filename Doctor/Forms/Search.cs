using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserData;

namespace Doctor.Forms {
    public partial class Search : Form {
        private List<User> allUsers;
        private Panel panel;
        public Search(List<User> users, Panel panel) {
            InitializeComponent();
            this.panel = panel;
            this.allUsers = users;
            textBox1.KeyDown += TextBox1_KeyDown;
            dataGridView1.MouseDoubleClick += DataGridView1_MouseDoubleClick;
        }

        private void DataGridView1_MouseDoubleClick(object sender, MouseEventArgs e) {
            
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                List<User> filteredUsers = new List<User>();
                foreach (User u in allUsers) {
                    if (u.FullName.ToLower() == textBox1.Text.ToLower()) {
                        filteredUsers.Add(u);
                    }
                }
                if (filteredUsers.Count == 0) {
                    MessageBox.Show("Nothing found with this name");
                    dataGridView1.DataSource = new BindingList<User>(allUsers);
                }
                else {
                    dataGridView1.DataSource = new BindingList<User>(filteredUsers);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e) {

        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            //do nothing
        }
    }
}
