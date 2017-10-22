using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserData;

namespace Server {
    class Server {
        private TcpListener socket;
        private List<User> users;
        private List<Client> connectedDoctors;
        private List<Client> connectedClients;
        private Thread loadUsers;
        private bool serverRunning;

        private object usersLock, connectedDoctorsLock, connectedClientsLock;

        public Server(string IPaddress, int portNumber) {
            serverRunning       = true;
            loadUsers           = null;
            users               = new List<User>();
            connectedClients    = new List<Client>();
            connectedDoctors    = new List<Client>();
            usersLock               = new object();
            connectedClientsLock    = new object();
            connectedDoctorsLock    = new object();

            IPAddress Ip;
            string usersPath = Directory.GetCurrentDirectory() + @"\Users.json";

            if (!IPAddress.TryParse(IPaddress, out Ip)) {
                Console.WriteLine("The given IpAddress was not valid....\nClosing the server");
                Environment.Exit(1);
            }

            try {
                socket = new TcpListener(IPAddress.Any, portNumber);
                if (File.Exists(usersPath)) {
                    loadUsers = new Thread(() => loadAllUsers(usersPath));
                    loadUsers.Start();
                }
                else
                {
                    string username = Encoding.Default.GetString(new SHA256Managed().ComputeHash(Encoding.Default.GetBytes("admin")));
                    users.Add(new User(username, username, "Root", UserType.Doctor));
                    username = Encoding.Default.GetString(new SHA256Managed().ComputeHash(Encoding.Default.GetBytes("test")));
                    users.Add(new User(username, username, "Patient", UserType.Client));
                    File.WriteAllText(usersPath, JsonConvert.SerializeObject(users));
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

            try {
                File.WriteAllText(usersPath, JsonConvert.SerializeObject(users));
            }
            catch(Exception e) {
                Console.WriteLine(e.Source);
            }
            Environment.Exit(0);
        }

        private void run() {
            socket.Start();
            Console.WriteLine("Server Started");
            while (serverRunning) {
                try {
                    TcpClient clientSocket = socket.AcceptTcpClient();
                    Console.WriteLine("Client Connected");
                    new Thread(() => sortClients(new Client(clientSocket, ref users, ref connectedClients, ref connectedDoctors, ref usersLock, ref connectedClientsLock, ref connectedDoctorsLock))).Start();
                }
                catch (SocketException e) {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        private void sortClients(Client client) {
            client.LoginThread.Join();
            if (client.User != null) {
                if (client.User.Type == UserType.Doctor) {
                    lock (connectedDoctorsLock) {
                        connectedDoctors.Add(client);
                    }
                }
                else if (client.User.Type == UserType.Client) {
                    lock (connectedClientsLock) {
                        connectedClients.Add(client);
                    }
                }
            }
            Console.WriteLine("connected Clients: {0}\t Connected Doctors: {1}", connectedClients.Count, connectedDoctors.Count);
        }

        private void loadAllUsers(string path) {
            try {
                JArray usersObj = (JArray)JsonConvert.DeserializeObject(File.ReadAllText(path));
                if (usersObj != null) {
                    foreach (JObject o in usersObj) {
                        User tempUser = (User)o.ToObject(typeof(UserData.User));
                        lock (usersLock) {
                            users.Add(tempUser);
                        }
                    }
                }
                //User patient = new User("zwen", "zwen", "Zwen van Erkelens", DoctorType.Client);
                //User doctor = new User("bram", "bram", "Bram Stoof", DoctorType.Doctor);

                //users.Add(patient);
                //users.Add(doctor);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        static void Main(string[] args) {
            new Server("127.0.0.1", 1337);
        }
    }
}
