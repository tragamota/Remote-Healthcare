using System;
using System.Windows.Forms;
using UserData;

namespace Doctor
{
    public partial class Dokter : Form
    {
        Client client;
        
        public Dokter(Client client)
        {
            InitializeComponent();
            this.client = client;
        }

        private void Connect_Btn_Click(object sender, EventArgs e)
        {
            if(Awaiting_Patients_Box.SelectedItem != null)
            {
                this.Hide();
                Form session = new Session((User)Awaiting_Patients_Box.SelectedItem, client);
                session.Closed += (s, args) => this.Close();
                session.Show();
            }
            else
            {
                MessageBox.Show("Selecteer een patiënt");
            }
        }
    }
}
