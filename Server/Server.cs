//using System;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;
//using Newtonsoft.Json.Linq;
//using Remote_Healtcare_Console;
//using System.Collections.Generic;
//using System.IO;
//using Newtonsoft.Json;
//using System.Globalization;
//using UserData;

//namespace ClientServer {
//    class Server {
//        public static List<User> users;

//        static void Main(string[] args) {
//            IPAddress localhost;

//            bool ipIsOk = IPAddress.TryParse("127.0.0.1", out localhost);
//            if (!ipIsOk) { Console.WriteLine("ip adres kan niet geparsed worden."); Environment.Exit(1); }

//            TcpListener listener = new TcpListener(IPAddress.Any, 1330);
//            listener.Start();

//            string path = Directory.GetCurrentDirectory() + @"\users.json";
//            string jsonFile = File.ReadAllText(path);
//            JArray openedData = (JArray)JsonConvert.DeserializeObject(jsonFile);

//            users = (List<User>)openedData.ToObject(typeof(List<User>));

//            while (true) {
//                Console.WriteLine(@"
//                      ==============================================
//                        Server started at {0}
//                        Waiting for connection
//                      =============================================="
//                , DateTime.Now);

//                TcpClient client = listener.AcceptTcpClient();


//                Thread thread = new Thread(HandleClientThread);
//                thread.Start(client);
//            }
//        }

//        static void HandleClientThread(object obj) {
//            List<BikeData> data = new List<BikeData>();
//            User declaredUser = new User("test", "test", "test");
//            TcpClient client = obj as TcpClient;

//            bool userDeclared = false;
//            while (!userDeclared) {
//                string received = ReadMessage(client);
//                User user = (User)JObject.Parse(received).ToObject(typeof(User));

//                foreach (User userOfList in users) {
//                    if (user.username == userOfList.username && user.password == userOfList.password) {
//                        declaredUser = user;
//                        userDeclared = true;
//                        dynamic response = new {
//                            access = "approved"
//                        };
//                        SendMessage(client, response);
//                    }
//                    else {
//                        dynamic response = new {
//                            access = "denied"
//                        };
//                        SendMessage(client, response);
//                    }
//                }
//            }

//            DateTime date = DateTime.Now;
//            Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");
//            string path = Directory.GetCurrentDirectory() + @"\" + declaredUser.username + "_" + date.ToShortDateString() + ".json";

//            bool done = false;
//            while (!done) {
//                string received = ReadMessage(client);

//                if (received.Equals("bye")) {
//                    done = true;
//                    SendMessage(client, "BYE");
//                }
//                else {
//                    data.Add((BikeData)JObject.Parse(received).ToObject(typeof(BikeData)));
//                    Console.WriteLine("Received: {0}", received);
//                    SendMessage(client, "OK");

//                    string json = JsonConvert.SerializeObject(data);
//                    File.WriteAllText(path, json);
//                }

//            }

//            client.Close();
//            Console.WriteLine("Connection closed");
//        }

//        public static string ReadMessage(TcpClient client) {
//            NetworkStream stream = client.GetStream();
//            StringBuilder message = new StringBuilder();
//            int numberOfBytesRead = 0;
//            byte[] messageBytes = new byte[4];
//            stream.Read(messageBytes, 0, messageBytes.Length);
//            byte[] receiveBuffer = new byte[BitConverter.ToInt32(messageBytes, 0)];

//            do {
//                numberOfBytesRead = stream.Read(receiveBuffer, 0, receiveBuffer.Length);

//                message.AppendFormat("{0}", Encoding.ASCII.GetString(receiveBuffer, 0, numberOfBytesRead));

//            }
//            while (message.Length < receiveBuffer.Length);

//            string response = message.ToString();
//            return response;
//        }

//        public static void SendMessage(TcpClient client, string message) {
//            byte[] bytes = Encoding.UTF8.GetBytes(message);
//            client.GetStream().Write(bytes, 0, bytes.Length);
//        }

//        public static void SendMessage(TcpClient client, dynamic message) {
//            NetworkStream stream = client.GetStream();
//            string json = JsonConvert.SerializeObject(message);

//            byte[] prefixArray = BitConverter.GetBytes(json.Length);
//            byte[] requestArray = Encoding.Default.GetBytes(json);

//            byte[] buffer = new Byte[prefixArray.Length + json.Length];
//            prefixArray.CopyTo(buffer, 0);
//            requestArray.CopyTo(buffer, prefixArray.Length);
//            stream.Write(buffer, 0, buffer.Length);
//        }
//    }
//}