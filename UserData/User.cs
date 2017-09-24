using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace UserData {
    public class User {
        private string username;
        private string password;
        public string fullName { get; set; }
        public string hashcode { get; }

        public User(string username, string password, string fullName) {
            this.username = username;
            this.password = password;
            this.fullName = fullName;
            hashcode = Encoding.UTF8.GetString(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(DateTime.UtcNow.Ticks.ToString())));
        }

        //this returns a boolean, if the username was accepted, true gets returned, otherwise there was an error or it was denied.
        public bool setUsername(List<User> users, string newUsername) {
            bool isTaken = false;
            foreach (User u in users) {
                if (u.username == newUsername) {
                    isTaken = true;
                }
            }
            if (isTaken) {
                return false;
            }
            else {
                username = newUsername;
                return true;
            }
        }

        public bool setPassword(string newPassword) {
            if (password == newPassword || newPassword.Length < 3) {
                return false;
            }
            else {
                password = newPassword;
                return true;
            }
        }

        public void CheckLogin(string username, string password, out bool valid, out string hashcode) {
            if (this.username == username && this.password == password) {
                valid = true;
                hashcode = this.hashcode;
            }
            else {
                valid = false;
                hashcode = null;
            }
        }
    }
}
