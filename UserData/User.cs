using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace UserData {
    public enum UserType { Client, Doctor, None }
    public class User {
        private string username { get; set; }
        private string password { get; set; }
        public string FullName { get; set; }
        public string Hashcode { get; }
        public UserType Type { get; set; }

        [Newtonsoft.Json.JsonConstructor]
        public User(string username, string password, string fullName, string hashcode, UserType type) {
            this.username = username;
            this.password = password;
            this.FullName = fullName;
            this.Hashcode = hashcode;
            this.Type = type;
        }

        public User(string username, string password, string fullName) {
            this.username = username;
            this.password = password;
            this.FullName = fullName;
            this.Type = UserType.Client;
            Hashcode = Encoding.Default.GetString(new SHA256Managed().ComputeHash(Encoding.Default.GetBytes(DateTime.UtcNow.Ticks.ToString() + username)));
        }

        public User(string username, string password, string fullName, UserType type) {
            this.username = username;
            this.password = password;
            this.FullName = fullName;
            this.Type = type;
            Hashcode = Encoding.Default.GetString(new SHA256Managed().ComputeHash(Encoding.Default.GetBytes(DateTime.UtcNow.Ticks.ToString() + username)));
        }

        public string Password {
             get { return password; }
        }

        public string Username {
            get { return username; }
        }

        public void SetUsername(string newUsername) {
            username = newUsername;
        }

        public void SetPassword(string newPassword) {
            password = newPassword;
        }

        public void CheckLogin(string username, string password, out bool valid, out string hashcode) {
            if (this.username == username && this.password == password) {
                valid = true;
                hashcode = this.Hashcode;
            }
            else {
                valid = false;
                hashcode = null;
            }
        }
    }
}
