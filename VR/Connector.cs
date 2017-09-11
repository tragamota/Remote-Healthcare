using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VR
{
    class Connector
    {
        private int port = 6666;
        private TcpClient tcp;
        private NetworkStream stream;
        private JObject jObject;

        public Connector()
        {
            tcp = new TcpClient();
            Connect("145.48.6.10", port);
        }

        private void Connect(string host, int port)
        {
            tcp.Connect(host, port);
            stream = tcp.GetStream();
        }

        public Dictionary<string, string> GetClients()
        {
            string json = @"{""id"" : ""session/list""}";
            SendMessage(json);

            Dictionary<string, string> hosts = new Dictionary<string, string>();

            jObject = ReadMessage();

            JArray array = (JArray)jObject.SelectToken("data");
            Console.WriteLine(array.ToString());

            foreach (JObject client in array)
            {
                string host = (string)client.SelectToken("clientinfo").SelectToken("host");
                string id = (string)client.SelectToken("id");
                hosts.Add(host, id);
            }

            return hosts;
        }

        public void SendMessage(string message)
        {
            byte[] prefixArray = BitConverter.GetBytes(message.Length);
            byte[] requestArray = Encoding.Default.GetBytes(message);

            byte[] buffer = new Byte[prefixArray.Length + message.Length];
            prefixArray.CopyTo(buffer, 0);
            requestArray.CopyTo(buffer, prefixArray.Length);
            stream.Write(buffer, 0, buffer.Length);
        }

        public JObject ReadMessage()
        {
            StringBuilder message = new StringBuilder();
            int numberOfBytesRead = 0;
            byte[] messageBytes = new byte[4];
            int length = stream.Read(messageBytes, 0, messageBytes.Length);
            byte[] receiveBuffer = new byte[tcp.ReceiveBufferSize - length];

            do
            {
                numberOfBytesRead = stream.Read(receiveBuffer, 0, receiveBuffer.Length);

                message.AppendFormat("{0}", Encoding.ASCII.GetString(receiveBuffer, 0, numberOfBytesRead));

            }
            while (stream.DataAvailable);


            string response = message.ToString();

            return JObject.Parse(response);
        }
    }
}
