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
        private IList<User> users;
        private Thread loadUsers;
        private bool serverRunning;

        public ServerBetter(string IPaddress, int portNumber) {
            loadUsers = null;
            users = new List<User>();

            IPAddress Ip;
            string usersPath = Directory.GetCurrentDirectory() + @"Users.json";

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

            loadUsers.Join();
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
                    TcpClient client = socket.AcceptTcpClient();
                    new Thread(() => new Client(client, users)).Start();
                }
                catch (SocketException e) {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        private void loadAllUsers(string path) {
            try {
                JArray usersObj = (JArray)JsonConvert.DeserializeObject(File.ReadAllText(path));
                users = (List<User>)usersObj.ToObject(typeof(List<User>));
            }
            catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        static void Main(string[] args) {
            new ServerBetter("172.0.0.1", 1337);
        }
    }
}
