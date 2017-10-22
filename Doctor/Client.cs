using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UserData;

namespace Doctor
{
    public class Client {
        public TcpClient client { get; set; }
        private NetworkStream stream;
        public object ReadAndWriteLock { get; }

        public Client() {
            try {
                client = new TcpClient("localhost", 1337);
                stream = client.GetStream();
            }
            catch (SocketException e) {
                Console.WriteLine(e.StackTrace);
            }
            ReadAndWriteLock = new object();
        }

        public string ReadMessage() {
            StringBuilder message = new StringBuilder();

            int numberOfBytesRead = 0;
            byte[] messageBytes = new byte[4];
            byte[] receiveBuffer;

            try {
                while (numberOfBytesRead < messageBytes.Length) {
                    numberOfBytesRead += stream.Read(messageBytes, numberOfBytesRead, messageBytes.Length - numberOfBytesRead);
                }
                receiveBuffer = new byte[BitConverter.ToInt32(messageBytes, 0)];
                do {
                    numberOfBytesRead = stream.Read(receiveBuffer, 0, receiveBuffer.Length);

                    message.AppendFormat("{0}", Encoding.Default.GetString(receiveBuffer, 0, numberOfBytesRead));
                }
                while (message.Length < receiveBuffer.Length);
            }
            catch (Exception e) {
                Console.WriteLine(e.StackTrace);
                return null;
            }

            return message.ToString();
        }

        public void SendMessage(dynamic message) {
            string json = null;

            if (message is string) {
                json = message;
            }
            else {
                json = JsonConvert.SerializeObject(message);
            }

            try {
                byte[] buffer;
                byte[] prefixArray = BitConverter.GetBytes(json.Length);
                byte[] requestArray = Encoding.Default.GetBytes(json);

                buffer = new Byte[prefixArray.Length + json.Length];
                prefixArray.CopyTo(buffer, 0);
                requestArray.CopyTo(buffer, prefixArray.Length);
                stream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}