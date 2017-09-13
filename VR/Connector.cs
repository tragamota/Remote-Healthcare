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
            Console.WriteLine(jObject);
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
            Console.WriteLine(jObject);
        }

        public void AddTerrain(int width, int length)
        {
            Random rdm = new Random();
            int[] heightValues = new int[width * length];
            for (int i = 0; i < heightValues.Length; i++)
            {
                heightValues[i] = rdm.Next(0,5);
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

            SendMessage(message);
            JObject jObject = ReadMessage();
            Console.WriteLine(jObject);
        }

        public JObject GetScene()
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = id,
                    data = new
                    {
                        id = "scene/get"
                    }
                }
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            Console.WriteLine(jObject);
            return jObject;
        }

        public void AddModel(string model)
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = id,
                    data = new
                    {
                        id = "scene/node/add",
                        data = new
                        {
                            name = "name",
                            parent = "guid",
                            components = new
                            {
                                transform = new
                                {
                                    position = new int[3] { 0, 0, 0 },
                                    scale = 1,
                                    rotation = new int[3] { 0, 0, 0 }
                                },
                                model = new
                                {
                                    file = "tree1.obj",
                                    cullbackfaces = true,
                                    animated = false,
                                    animation = "animationname"
                                },
                                terrain = new
                                {
                                    smoothnormals = true
                                },
                                panel = new
                                {
                                    size = new int[2] { 1, 1 },
                                    resolution = new int[2] { 512, 512 },
                                    background = new int[4] { 1, 1, 1, 1 }
                                },
                                water = new
                                {
                                    size = new int[2] {20, 20},
                                    resolution = 0.1
                                }
                            }
                        }
                    }
                }
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            Console.WriteLine(jObject);
        }

        public void AddRoute()
        {
            dynamic pos1 = new { pos = new int[3] { 0, 0, 0 } };
            dynamic pos2 = new { pos = new int[3] { 50, 0, 0 } };
            dynamic pos3 = new { pos = new int[3] { 50, 0, 50 } };
            dynamic pos4 = new { pos = new int[3] { 0, 0, 50 } };

            dynamic dir1 = new { dir = new int[3] { 5, 0, -5 } };
            dynamic dir2 = new { dir = new int[3] { 5, 0, 5 } };
            dynamic dir3 = new { dir = new int[3] { -5, 0, 5 } };
            dynamic dir4 = new { dir = new int[3] { -5, 0, -5 } };

            dynamic[] data = new dynamic[8]
            {
                pos1, dir1, pos2, dir2, pos3, dir3, pos4, dir4
            };

            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = id,
                    data = new
                    {
                        id = "route/add",
                        data = data
                    }
                }
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            Console.WriteLine(jObject);
        }

        public void UpdateTerrain()
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = id,
                    data = new
                    {
                        id = "scene/terrain/update",
                        data = new {}
                    }
                }
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            Console.WriteLine(jObject);
        }

        public void DeleteTerrain()
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = id,
                    data = new
                    {
                        id = "scene/terrain/delete",
                        data = new { }
                    }
                }
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            Console.WriteLine(jObject);
        }

        public void GetHeight()
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = id,
                    data = new
                    {
                        id = "scene/terrain/getheight",
                        data = new
                        {
                            position = (new double[2] {10.2, 4.4})
                        }
                    }
                }
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            Console.WriteLine(jObject);
        }

        public void DeleteLayer()
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = id,
                    data = new
                    {
                        id = "scene/node/dellayer",
                        data = new {}
                    }
                }
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            Console.WriteLine(jObject);
        }

        public void SetId(string id)
        {
            this.id = id;
        }
    }
}
