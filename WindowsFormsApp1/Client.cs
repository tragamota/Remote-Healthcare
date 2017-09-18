using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Remote_Healtcare_Console;
using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Remote_Healtcare_Console
{
    public class Client
    {
        TcpClient client;

        public Client()
        {
            client = new TcpClient("127.0.0.1", 1330);
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

        public void SendMessage(string message)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer;
            if (message == "hoi")
            {
                string json = JsonConvert.SerializeObject(new BikeData());

                byte[] prefixArray = BitConverter.GetBytes(json.Length);
                byte[] requestArray = Encoding.Default.GetBytes(json);

                buffer = new Byte[prefixArray.Length + json.Length];
                prefixArray.CopyTo(buffer, 0);
                requestArray.CopyTo(buffer, prefixArray.Length);
            }
            else
            {
                byte[] prefixArray = BitConverter.GetBytes(message.Length);
                byte[] requestArray = Encoding.Default.GetBytes(message);

                buffer = new Byte[prefixArray.Length + message.Length];
                prefixArray.CopyTo(buffer, 0);
                requestArray.CopyTo(buffer, prefixArray.Length);
            }
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
    }
}
