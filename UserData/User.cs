using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace UserData {
    public enum DoctorType { Client, Doctor, None }
    public class User {
        private string username { get; set; }
        private string password { get; set; }
        public string FullName { get; set; }
        public string Hashcode { get; }
        public DoctorType Type { get; set; }

        [Newtonsoft.Json.JsonConstructor]
        public User(string username, string password, string fullName, string hashcode, DoctorType type) {
            this.username = username;
            this.password = password;
            this.FullName = fullName;
            this.Hashcode = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(hashcode));
            this.Type = type;
        }

        public User(string username, string password, string fullName) {
            this.username = username;
            this.password = password;
            this.FullName = fullName;
            this.Type = DoctorType.Client;
            Hashcode = Encoding.UTF8.GetString(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(DateTime.UtcNow.Ticks.ToString())));
        }

        public User(string username, string password, string fullName, DoctorType type) {
            this.username = username;
            this.password = password;
            this.FullName = fullName;
            this.Type = type;
            Hashcode = Encoding.UTF8.GetString(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(DateTime.UtcNow.Ticks.ToString())));
        }

        public string Password {
             get { return password; }
        }

        public string Username {
            get { return username; }
        }

        //this returns a boolean, if the username was accepted, true gets returned, otherwise there was an error or it was denied.
        public bool SetUsername(List<User> users, string newUsername) {
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

        public bool SetPassword(string newPassword) {
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
                hashcode = this.Hashcode;
            }
            else {
                valid = false;
                hashcode = null;
            }
        }
    }
}
