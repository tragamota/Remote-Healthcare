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
        private Client client;
        public Search(ref List<User> users, Panel panel, ref Client client) {
            InitializeComponent();
            this.panel = panel;
            this.client = client;
            this.allUsers = users;
            textBox1.KeyDown += TextBox1_KeyDown;
            dataGridView1.MouseDoubleClick += DataGridView1_MouseDoubleClick;
            dataGridView1.DataSource = new BindingList<User>(allUsers);
        }

        private void DataGridView1_MouseDoubleClick(object sender, MouseEventArgs e) {
            if(dataGridView1.CurrentCell.RowIndex >= 0) {
                this.Hide();
                Form userForm = new UserInfo((User) dataGridView1.CurrentRow.DataBoundItem, this, ref client, panel);
                userForm.TopLevel = false;
                panel.Controls.Add(userForm);
                userForm.Show();
            }
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
    }
}
