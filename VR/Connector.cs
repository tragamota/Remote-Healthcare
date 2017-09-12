using Newtonsoft.Json;
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
        private string id;

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
            dynamic message = new
            {
                id = "session/list"
            };
            SendMessage(message);

            Dictionary<string, string> hosts = new Dictionary<string, string>();

            jObject = ReadMessage();

            JArray array = (JArray)jObject.SelectToken("data");

            foreach (JObject client in array)
            {
                string host = (string)client.SelectToken("clientinfo").SelectToken("host");
                string id = (string)client.SelectToken("id");
                hosts.Add(host + " - " + id, id);
            }

            return hosts;
        }

        public void SendMessage(dynamic message)
        {
            string json = JsonConvert.SerializeObject(message);

            byte[] prefixArray = BitConverter.GetBytes(json.Length);
            byte[] requestArray = Encoding.Default.GetBytes(json);

            byte[] buffer = new Byte[prefixArray.Length + json.Length];
            prefixArray.CopyTo(buffer, 0);
            requestArray.CopyTo(buffer, prefixArray.Length);
            stream.Write(buffer, 0, buffer.Length);
        }

        public JObject ReadMessage()
        {
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
            while (stream.DataAvailable);

            string response = message.ToString();

            return JObject.Parse(response);
        }

        public void ChangeScene(string change)
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = id,
                    data = new
                    {
                        id = change,
                        data = new {}
                    }
                }
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            //Console.WriteLine(jObject);
        }

        public void SetTime(double hour)
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = id,
                    data = new
                    {
                        id = "scene/skybox/settime",
                        data = new
                        {
                            time = hour
                        }
                    }
                }
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            //Console.WriteLine(jObject);
        }

        public void AddFlatTerrain(int width, int length)
        {
            int[] heightValues = new int[width * length];
            for (int i = 0; i < heightValues.Length; i++)
            {
                heightValues[i] = 0;
            }
            int[] measure = new int[2] { width, length };

            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = id,
                    data = new
                    {
                        id = "scene/terrain/add",
                        data = new
                        {
                            size = measure,
                            heights = heightValues
                        }
                    }
                }
            };

            //SendMessage(message);
            //JObject jObject = ReadMessage();
            Console.WriteLine(JsonConvert.SerializeObject(message));
        }

        public void SetId(string id)
        {
            this.id = id;
        }
    }
}
