using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UserData;

namespace Server
{
    public class Client {
        private TcpClient client;
        private NetworkStream stream;
        private BikeSession session;
        public User User { get; }
        public Client Docter { get; }

        public Client(TcpClient client, IList<User> users) {
            this.client = client;
            stream = this.client.GetStream();

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

            run();
        }

        private void run() {
            while (client.Connected) {
                JObject message = readFromStream();
                if (message != null) {
                    processIncomingMessage(message);
                }
            }
        }

        private void processIncomingMessage(JObject obj) {
            switch ((string)obj["id"]) {
                case "update":
                    update((JObject) obj["data"]);
                    break;
                case "BikeData":
                    update((JObject)obj["data"]);
                    break;
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
            if(session != null) {
                BikeData dataConverted = data.ToObject<BikeData>();
                session.data.Add(dataConverted);
                if(Docter != null) {

                }
            }
        }

        private JObject readFromStream() {
            int numberOfBytesRead = 0;
            byte[] messageBytes = new byte[4];
            byte[] receiveBuffer;

            while (numberOfBytesRead < messageBytes.Length && client.Connected) {
                try {
                    numberOfBytesRead += stream.Read(messageBytes, numberOfBytesRead, messageBytes.Length - numberOfBytesRead);
                    Thread.Sleep(50);
                }
                catch (IOException e) {
                    Console.WriteLine(e.StackTrace);
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

        public string ReadMessage()
        {
            NetworkStream stream = client.GetStream();
            StringBuilder message = new StringBuilder();
            int numberOfBytesRead = 0;
            byte[] messageBytes = new byte[4];
            stream.Read(messageBytes, 0, messageBytes.Length);
            byte[] receiveBuffer = new byte[BitConverter.ToInt32(messageBytes, 0)];

            do
            {
                numberOfBytesRead = stream.Read(receiveBuffer, 0, receiveBuffer.Length);

                message.AppendFormat("{0}", Encoding.ASCII.GetString(receiveBuffer, 0, numberOfBytesRead));

            }
            while (message.Length < receiveBuffer.Length);

            string response = message.ToString();
            return response;
        }

        public void SendMessage(dynamic message)
        {
            string json = null;

            if (message is string)
            {
                json = message;
            }
            else
            {
                json = JsonConvert.SerializeObject(message);
            }

            try
            {
                byte[] buffer;
                byte[] prefixArray = BitConverter.GetBytes(json.Length);
                byte[] requestArray = Encoding.Default.GetBytes(json);

                buffer = new Byte[prefixArray.Length + json.Length];
                prefixArray.CopyTo(buffer, 0);
                requestArray.CopyTo(buffer, prefixArray.Length);
                stream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void SendMessage(string message)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer;
            byte[] prefixArray = BitConverter.GetBytes(message.Length);
            byte[] requestArray = Encoding.Default.GetBytes(message);

            buffer = new Byte[prefixArray.Length + message.Length];
            prefixArray.CopyTo(buffer, 0);
            requestArray.CopyTo(buffer, prefixArray.Length);
            stream.Write(buffer, 0, buffer.Length);
        }

        public void SendMessage(User user)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer;
            string json = JsonConvert.SerializeObject(user);

            byte[] prefixArray = BitConverter.GetBytes(json.Length);
            byte[] requestArray = Encoding.Default.GetBytes(json);

            buffer = new Byte[prefixArray.Length + json.Length];
            prefixArray.CopyTo(buffer, 0);
            requestArray.CopyTo(buffer, prefixArray.Length);
            stream.Write(buffer, 0, buffer.Length);
        }

        public void SendMessage(BikeData data)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer;
            string json = JsonConvert.SerializeObject(data);

            byte[] prefixArray = BitConverter.GetBytes(json.Length);
            byte[] requestArray = Encoding.Default.GetBytes(json);

            buffer = new Byte[prefixArray.Length + json.Length];
            prefixArray.CopyTo(buffer, 0);
            requestArray.CopyTo(buffer, prefixArray.Length);
            stream.Write(buffer, 0, buffer.Length);
        }

        private void closeStream() {
            stream.Dispose();
            client.Close();
        }
    }
}
