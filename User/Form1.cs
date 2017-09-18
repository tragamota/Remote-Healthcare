using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace User
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Add_Btn_Click(object sender, EventArgs e)
        {
            List<User> users = new List<User>();

            string path = Directory.GetCurrentDirectory() + @"\users.json";

            OpenFileDialog browseFileDialog = new OpenFileDialog();
            browseFileDialog.Filter = "JSON (.json)|*.json;";
            browseFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            if (browseFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = Path.GetFullPath(browseFileDialog.FileName);
                string jsonFile = File.ReadAllText(path);
                JArray openedData = (JArray)JsonConvert.DeserializeObject(jsonFile);

                users = (List<User>)openedData.ToObject(typeof(List<User>));
            }

            if (Username_Txt.Text.Length > 0 && Password_Txt.Text.Length > 0)
            {
                User user = new User(Username_Txt.Text, Password_Txt.Text);
                users.Add(user);
            }

            string json = JsonConvert.SerializeObject(users);
            File.WriteAllText(path, json);
        }
    }
}
