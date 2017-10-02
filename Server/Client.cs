﻿using Newtonsoft.Json;
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

        private object sessionLock, connectedClientsLock, connectedDoctorsLock, usersLock;

        private List<Client> connectedClients, connectedDoctors;
        private List<User> users;
        public BikeSession session { get; set; }
        public User User { get; set; }
        public Client patient;
        public Client doctor;
  
        public Client(TcpClient client, ref List<User> users, ref List<Client> connectedClients, ref List<Client> connectedDoctors, ref object usersLock, ref object connectedClientsLock, ref object connectedDoctorsLock) {
            this.client = client;
            this.connectedClients = connectedClients;
            this.connectedDoctors = connectedDoctors;
            this.users = users;

            sessionLock = new object();
            this.usersLock = usersLock;
            this.connectedClientsLock = connectedClientsLock;
            this.connectedDoctorsLock = connectedDoctorsLock;

            stream = this.client.GetStream();
            LoginThread = new Thread(() => init(this.users));
            LoginThread.Start();
        }

        private void init(List<User> users) {
            string hash, username, password;
            bool valid, found;
            JObject userReceived = readFromStream();
            found = false;
            username = (string)userReceived["username"];
            password = (string)userReceived["password"];

            lock (usersLock) {
                foreach (User user in users) {
                    user.CheckLogin(username, password, out valid, out hash);
                    if (valid) {
                        this.User = user;
                        dynamic response = new {
                            access = true,
                            fullname = user.FullName,
                            doctortype = user.Type,
                            hashcode = hash
                        };
                        writeMessage(response);
                        found = true;
                        new Thread(() => run()).Start();
                        break;
                    }
                }
            }

            if (!found) {
                dynamic response = new {
                    access = false
                };
                writeMessage(response);
                closeStream();
                return;
            }
        }

        private void run() {
            while (client.Connected) {
                JObject message = readFromStream();
                if (message != null) {
                    processIncomingMessage(message);
                }
            }
            if (User.Type == DoctorType.Doctor) {
                lock (connectedDoctorsLock) {
                    connectedDoctors.Remove(this);
                }
            }
            else {
                lock (connectedClientsLock) {
                    connectedClients.Remove(this);
                }
            }
            closeStream();
            Console.WriteLine(connectedClients.Count + "\t" + connectedDoctors.Count);
        }

        private void processIncomingMessage(JObject obj) {
            switch ((string)obj["id"]) {
                case "update":
                    update((JObject)obj["data"]);
                    break;
                case "committingChanges":
                    JObject data = (JObject)obj["data"];
                    //patient.writeMessage(data);
                    break;
                case "getPatients":
                    writeMessage(new
                    {
                        users = users
                    });
                    break;
                case "setPatient":
                    User patientUser = (User)obj["data"]["patient"].ToObject(typeof(User));
                    SetPatient(patientUser);
                    patient.writeMessage(obj["data"]["doctor"]);
                    break;
                case "setDoctor":
                    string hashcode = (string)obj["doctor"];
                    SetDoctor(hashcode);
                    break;
                case "startRecording":
                    patient.writeMessage(new
                    {
                        id = "start"
                    });
                    break;
                case "stopRecording":
                    if (patient != null)
                        patient.writeMessage(obj);
                    else
                        StopRecording();
                    break;
                case "sendData":
                    if(session == null)
                        StartRecording();
                    session.AddBikeData((BikeData)obj["data"]["bikeData"].ToObject(typeof(BikeData)));
                    doctor.writeMessage(obj["data"]);
                    break;
                case "add":
                    new Thread(() => addUser((JObject)obj["data"])).Start();
                    break;
                case "delete":
                    new Thread(() => deleteUser((JObject)obj["data"])).Start();
                    break;
                case "power":
                    new Thread(() => setPower((JObject)obj["data"])).Start();
                    break;
                case "manual":
                    new Thread(() => setManual((JObject)obj["data"])).Start();
                    break;
                case "disconnect":
                    closeStream();
                    break;
            }
        }

        private void sendAllClients() {
            writeMessage(users);
        }

        private void setManual(JObject jObject) {
            //send set manual power
        }

        private void setPower(JObject obj) {
            if (User.Type == DoctorType.Doctor) {
                Client Tempuser = null;
                string hashcode = (string)obj["hashcode"];
                int power = (int)obj["power"];

                lock (connectedClientsLock) {
                    foreach (Client user in connectedClients) {
                        if (user.User.Hashcode == hashcode) {
                            Tempuser = user;
                            break;
                        }
                    }
                }

                if (Tempuser != null) {
                    dynamic setPower = new {
                        id = "power",
                        data = new {
                            power = power
                        }
                    };
                    dynamic ok = new {
                        status = "ok"
                    };
                    Tempuser.writeMessage(setPower);
                    writeMessage(ok);
                }
                else {
                    dynamic failed = new {
                        status = "User not found"
                    };
                    writeMessage(failed);
                }
            }
            else {
                dynamic failed = new {
                    status = "no permission"
                };
                writeMessage(failed);
            }
        }

        public void StartRecording()
        {
            if (User.Type == DoctorType.Client)
            {
                session = new BikeSession(User.Hashcode);
                //new Thread(() => SendBikeData()).Start();
            }
        }

        public void SendBikeData()
        {
            while(session != null)
            {
                writeMessage(session.GetLatestBikeData());
            }
        }

        public void StopRecording() {
            if (User.Type == DoctorType.Client) {
                lock (sessionLock) {
                    session.SaveSessionToFile();
                    session = null;
                }
            }
        }

        private void update(JObject data) {
            if (User.Type == DoctorType.Client) {
                lock (sessionLock) {
                    if (session != null) {
                        BikeData dataConverted = data.ToObject<BikeData>();
                        session.data.Add(dataConverted);
                    }
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
                lock (usersLock) {
                    foreach (User user in users) {
                        if (user == this.User) {
                            continue;
                        }
                        else {
                            if (user.Username == tempUser.Username) {
                                exists = true;
                                break;
                            }
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
            if (User.Type == DoctorType.Doctor) {
                string hashcode = (string)data["hashcode"];
                bool deleted = false;

                lock (usersLock) {
                    foreach (User user in users) {
                        if (user.Hashcode == hashcode) {
                            users.Remove(user);
                            //write response user deleted
                            deleted = true;
                            break;
                        }
                    }
                }

                if (!deleted) {
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

            try {
                return JObject.Parse(Encoding.Default.GetString(receiveBuffer));
            }
            catch (Exception e) {
                return null;
            }
        }

        public void writeMessage(dynamic message) {
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

        public void SetPatient(User user)
        {
            foreach (Client patientClient in connectedClients)
            {
                byte[] bytes = Encoding.Default.GetBytes(patientClient.User.Hashcode);
                string patientString = Encoding.Unicode.GetString(bytes);
                bytes = Encoding.Default.GetBytes(user.Hashcode);
                string userString = Encoding.Unicode.GetString(bytes);

                if (patientString.Equals(userString))
                    patient = patientClient;
            }
        }

        private void SetDoctor(string hashcode)
        {
            foreach (Client doctorClient in connectedDoctors)
            {
                byte[] bytes = Encoding.Default.GetBytes(doctorClient.User.Hashcode);
                string doctorString = Encoding.Unicode.GetString(bytes);
                bytes = Encoding.Default.GetBytes(hashcode);
                string userString = Encoding.Unicode.GetString(bytes);

                if (doctorString.Equals(userString))
                    doctor = doctorClient;
            }
        }
    }
}
