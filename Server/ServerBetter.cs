using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserData;

namespace Server {
    class ServerBetter {
        private TcpListener socket;
        private List<User> users;
        private List<Client> connectedDoctors;
        private List<Client> connectedClients;
        private Thread loadUsers;
        private bool serverRunning;

        public ServerBetter(string IPaddress, int portNumber) {
            serverRunning = true;
            loadUsers = null;
            users = new List<User>();
            connectedClients = new List<Client>();
            connectedDoctors = new List<Client>();

            IPAddress Ip;
            string usersPath = Directory.GetCurrentDirectory() + @"\Users.json";

            if (!IPAddress.TryParse(IPaddress, out Ip)) {
                Console.WriteLine("The given IpAddress was not valid....\nClosing the server");
                Environment.Exit(1);
            }

            Console.WriteLine();

            try {
                socket = new TcpListener(Ip, portNumber);
                if (File.Exists(usersPath)) {
                    loadUsers = new Thread(() => loadAllUsers(usersPath));
                    loadUsers.Start();
                }
                else {
                    File.Create(usersPath);
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }

            if(loadUsers != null) {
                loadUsers.Join();
            }

            new Thread(run).Start();

            while (serverRunning) {
                if (Console.ReadLine().ToLower() == "stop") {
                    Console.WriteLine("Stopping Server....");
                    try {
                        serverRunning = false;
                        socket.Stop();
                    }
                    catch (SocketException e) {
                        Console.WriteLine(e.StackTrace);
                    }
                }
                else {
                    Console.WriteLine("Unknown command");
                }
            }
        }

        private void run() {
            socket.Start();
            while (serverRunning) {
                try {
                    TcpClient clientSocket = socket.AcceptTcpClient();
                    Console.WriteLine("Client Connected");
                    new Thread(() => sortClients(new Client(clientSocket, users, ref connectedClients, ref connectedDoctors))).Start();
                }
                catch (SocketException e) {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        private void sortClients(Client client) {
            client.loginThread.Join();
            if (client != null) {
                if (client.User.Type == User.DoctorType.Doctor) {
                    connectedDoctors.Add(client);
                }
                else if (client.User.Type == User.DoctorType.Client) {
                    connectedClients.Add(client);
                }
            }
            Console.WriteLine(connectedClients.Count);
        }

        private void loadAllUsers(string path) {
            try {
                JArray usersObj = (JArray)JsonConvert.DeserializeObject(File.ReadAllText(path));
                if (usersObj == null) {
                    foreach (JObject o in usersObj) {
                        User tempUser = (User)o.ToObject(typeof(UserData.User)); 
                        users.Add(tempUser);
                    }
                }
                User patient = new User("zwen", "zwen", "Zwen van Erkelens", User.DoctorType.Client);
                User doctor = new User("bram", "bram", "Bram Stoof", User.DoctorType.Doctor);

                users.Add(patient);
                users.Add(doctor);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        static void Main(string[] args) {
            new ServerBetter("127.0.0.1", 1337);
        }
    }
}
