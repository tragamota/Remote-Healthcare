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

        public Client(TcpClient client, IList<User> users) {
            this.client = client;
            stream = this.client.GetStream();

            string hash, username, password;
            bool valid, found;
            JObject userReceived = readFromStream();
            found = false;
            username = (string) userReceived["username"];
            password = (string) userReceived["password"];

            foreach(User user in users) {
                user.CheckLogin(username, password, out valid, out hash);
                if(valid) {
                    dynamic response = new {
                        access = true,
                        hashcode = hash
                    };
                    writeMessage(response);
                    break;
                }
            }

            if(!found) {
                dynamic response = new {
                    access = false
                };
                writeMessage(response);
                stream.Close();
                stream.Dispose();
                client.Close();
                client.Dispose();
            }

            run();
        }

        private void run() {
            while(client.Connected) {
                JObject message = readFromStream();
                //handel the incomming message.
            }
        }

        private JObject readFromStream() {
            int numberOfBytesRead = 0;
            byte[] messageBytes = new byte[4];
            byte[] receiveBuffer;

            while(numberOfBytesRead < messageBytes.Length && client.Connected) {
                try {
                    numberOfBytesRead += stream.Read(messageBytes, numberOfBytesRead, messageBytes.Length - numberOfBytesRead);
                    Thread.Sleep(50);
                }
                catch(IOException e) {
                    Console.WriteLine(e.StackTrace);
                }
            }

            numberOfBytesRead = 0;
            try {
                receiveBuffer = new byte[BitConverter.ToInt32(messageBytes, 0)];
            }
            catch(ArgumentException e) {
                Console.WriteLine(e.StackTrace);
                return null;
            }

            while(numberOfBytesRead < receiveBuffer.Length && client.Connected) {
                try {
                    numberOfBytesRead += stream.Read(receiveBuffer, numberOfBytesRead, receiveBuffer.Length - numberOfBytesRead);
                }
                catch(IOException e) {
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
            catch(IOException e) {
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
            catch(IOException e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void closeStream() {
            //write the closeStream message

            stream.Dispose();
            client.Close();
        }
    }
}
