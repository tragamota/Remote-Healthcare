using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserData;

namespace Server {
    class Client {
        private TcpClient client;
        private NetworkStream stream;
        public Thread LoginThread { get; }

        private List<Client> connectedClients, connectedDoctors;
        private List<User> users;
        public BikeSession session { get; set; }
        public User User { get; set; }
  
        public Client(TcpClient client, ref List<User> users, ref List<Client> connectedClients, ref List<Client> connectedDoctors) {
            this.client = client;
            this.connectedClients = connectedClients;
            this.connectedDoctors = connectedDoctors;
            this.users = users;

            stream = this.client.GetStream();
            LoginThread = new Thread(() => init(this.users));
            LoginThread.Start();
        }

        private void init( List<User> users) {
            string hash, username, password;
            bool valid, found;
            JObject userReceived = readFromStream();
            found = false;
            username = (string)userReceived["username"];
            password = (string)userReceived["password"];

            foreach (User user in users) {
                user.CheckLogin(username, password, out valid, out hash);
                if (valid) {
                    this.User = user;
                    dynamic response = new {
                        access = true,
                        hashcode = hash
                    };
                    writeMessage(response);
                    found = true;
                    break;
                }
            }

            if (!found) {
                dynamic response = new {
                    access = false
                };
                writeMessage(response);
                closeStream();
            }

            new Thread(() => run()).Start();
        }

        private void run() {
            while (client.Connected) {
                JObject message = readFromStream();
                if (message != null) {
                    processIncomingMessage(message);
                }
            }
            if(User.Type == DoctorType.Doctor) {
                connectedDoctors.Remove(this);
            }
            else {
                connectedClients.Remove(this);
            }
            closeStream();
            Console.WriteLine(connectedClients.Count + "\t" + connectedDoctors.Count);
        }

        private void processIncomingMessage(JObject obj) {
            switch ((string)obj["id"]) {
                case "update":
                    update((JObject) obj["data"]);
                    break;
                case "add":
                    new Thread(() => addUser((JObject)obj["data"])).Start();
                    break;
                case "delete":
                    new Thread(() => deleteUser((JObject)obj["data"])).Start();
                    break;
                case "power":
                    new Thread(() => setPower((JObject) obj["data"])).Start();
                    break;
                case "manual":
                    new Thread(() => setManual((JObject)obj["data"])).Start();
                    break;
            }
        }

        private void setManual(JObject jObject) {
            //send set manual power
        }

        private void setPower(JObject obj) {
            if(User.Type == DoctorType.Doctor) {
                Client Tempuser = null;
                string hashcode = (string)obj["hashcode"];
                int power = (int) obj["power"];

                foreach(Client user in connectedClients) {
                    if(user.User.Hashcode == hashcode) {
                        Tempuser = user;
                        break;
                    }
                }

                if(Tempuser != null) {
                    Tempuser.writeMessage("iets");
                }
                else {
                    //write response to doctor back
                }
            }
            else {
                //write no permission
            }
        }

        public void StartRecording() {
            session = new BikeSession(User.Hashcode);
        }

        public void StopRecording() {
            session.SaveSessionToFile();
            session = null;
        }

        private void update(JObject data) {
            if (User.Type == DoctorType.Client) {
                if (session != null) {
                    BikeData dataConverted = data.ToObject<BikeData>();
                    session.data.Add(dataConverted);
                }
            }
        }

        private void addUser(JObject data) {
            if (User.Type == DoctorType.Doctor) {
                string username = (string)data["username"];
                string password = (string)data["password"];
                string fullName = (string)data["fullname"];
                int clientType = (int)data["type"];
                User tempUser = new User(username, password, fullName, (DoctorType)clientType);

                bool exists = false;
                foreach(User user in users) {
                    if(user == this.User) {
                        continue;
                    }
                    else {
                        if(user.Username == tempUser.Username) {
                            exists = true;
                            break;
                        }
                    }
                }
                if (!exists) {
                    users.Add(new User(username, password, fullName, (DoctorType)clientType));
                    //write response
                }
                else {
                    //write response
                }
            }
            else {
                //write not permission
            }
        }

        private void deleteUser(JObject data) {
            if(User.Type == DoctorType.Doctor) {
                string username = (string)data["username"];
                bool deleted = false;

                foreach (User user in users) {
                    if(user.Username == username) {
                        users.Remove(user);
                        //write response user deleted
                        deleted = true;
                        break;
                    }
                }

                if(!deleted) {
                    //write user not found
                }                

            }
            else {
                //write no permission
            }
        }

        private JObject readFromStream() {
            int numberOfBytesRead = 0;
            byte[] messageBytes = new byte[4];
            byte[] receiveBuffer;

            while (numberOfBytesRead < messageBytes.Length && client.Connected) {
                try {
                    numberOfBytesRead += stream.Read(messageBytes, numberOfBytesRead, messageBytes.Length - numberOfBytesRead);
                    Thread.Yield();
                }
                catch (IOException e) {
                    Console.WriteLine(e.StackTrace);
                    return null;
                }
            }

            numberOfBytesRead = 0;
            try {
                receiveBuffer = new byte[BitConverter.ToInt32(messageBytes, 0)];
            }
            catch (ArgumentException e) {
                Console.WriteLine(e.StackTrace);
                return null;
            }

            while (numberOfBytesRead < receiveBuffer.Length && client.Connected) {
                try {
                    numberOfBytesRead += stream.Read(receiveBuffer, numberOfBytesRead, receiveBuffer.Length - numberOfBytesRead);
                }
                catch (IOException e) {
                    Console.WriteLine(e.StackTrace);
                    return null;
                }
            }

            return JObject.Parse(Encoding.UTF8.GetString(receiveBuffer));
        }

        private void writeMessage(dynamic message) {
            string json;
            try {
                json = JsonConvert.SerializeObject(message);
            }
            catch (IOException e) {
                Console.WriteLine(e.StackTrace);
                return;
            }

            byte[] prefixArray = BitConverter.GetBytes(json.Length);
            byte[] requestArray = Encoding.Default.GetBytes(json);

            byte[] buffer = new Byte[prefixArray.Length + json.Length];
            prefixArray.CopyTo(buffer, 0);
            requestArray.CopyTo(buffer, prefixArray.Length);
            try {
                stream.Write(buffer, 0, buffer.Length);
            }
            catch (IOException e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void closeStream() {
            stream.Close();
            client.Close();
            stream.Dispose();
            client.Dispose();
        }
    }
}
