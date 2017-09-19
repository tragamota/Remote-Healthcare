using System;
using System.Collections.Generic;
using System.Text;

namespace UserData
{
    class User
    {
        private string username { get { return username; } set { setUsername; } }
        private string password { get { return password; } set { setPassword; } }
        private string fullName { get; set; }

        public User user(string username, string password, string fullName)
        {
            this.username = username;
            this.password = password;
            this.fullName = fullName;
        }

        //this returns a boolean, if the username was accepted, true gets returned, otherwise there was an error or it was denied.
        public bool setUsername(List<User>users, string newUsername)
        {
            bool isTaken = false;
            foreach(User u in users)
            {
                if(u == newUsername)
                {
                    isTaken = true;
                }
            }
            if(isTaken)
            {
                return false;
            }
            else
            {
                username = newUsername;
                return true;
            }
        }

        public bool setPassword(string newPassword)
        {
            if(this.password == newPassword && newPassword < 3)
            {
                return false;
            }
            else
            {
                password = newPassword;
                return true;
            }
        }
    }
}
