using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VR
{
    public class Connector
    {
        private int port = 6666;
        private TcpClient tcp;
        private NetworkStream stream;
        private JObject jObject;
        private string tunnelID;

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
            while (message.Length < receiveBuffer.Length);

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
                    dest = tunnelID,
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
                    dest = tunnelID,
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
                heightValues[i] = 0;
            }
            int[] measure = new int[2] { width, length };

            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = tunnelID,
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
                    dest = tunnelID,
                    data = new
                    {
                        id = "scene/get"
                    }
                }
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            return jObject;
        }

        public void AddLayer(string terrainName, string diffuseFile, string normalFile, int minHeight, int maxHeight, int fadeDist)
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = tunnelID,
                    data = new
                    {
                        id = "scene/node/addlayer",
                        data = new
                        {
                            id = GetUUID(terrainName),
                            diffuse = diffuseFile,
                            normal = normalFile,
                            minHeight = minHeight,
                            maxHeight = maxHeight,
                            fadeDist = fadeDist
                        }
                    }
                }
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            Console.WriteLine(jObject);
        }

        public void AddModel(string modelname, string path, int x, int y, int z)
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = tunnelID,
                    data = new
                    {
                        id = "scene/node/add",
                        data = new
                        {
                            name = modelname,
                            components = new
                            {
                                transform = new
                                {
                                    position = (new int[3] { x, y, z }),
                                    scale = 1,
                                    rotation = (new int[3] { 0, 0, 0 })
                                },
                                model = new
                                {
                                    file = path,
                                    cullbackfaces = true,
                                    animated = false,
                                    animation = "animationname"
                                },
                                panel = new
                                {
                                    size = (new int[2] { 1, 1 }),
                                    resolution = (new int[2] { 512, 512 }),
                                    background = (new int[4] { 1, 1, 1, 1 })
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

        public string AddRoute()
        {
            dynamic pos1 = new { pos = (new int[3] { 0, 0, 0 }), dir = (new int[3] { 5, 0, -5 }) };
            dynamic pos2 = new { pos = (new int[3] { 50, 0, 0 }), dir = (new int[3] { 5, 0, 5 }) };
            dynamic pos3 = new { pos = (new int[3] { 50, 0, 50 }), dir = (new int[3] { -5, 0, 5 }) };
            dynamic pos4 = new { pos = (new int[3] { 0, 0, 50 }), dir = (new int[3] { -5, 0, -5 }) };

            dynamic[] data = new dynamic[4]
            {
                pos1, pos2, pos3, pos4
            };

            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = tunnelID,
                    data = new
                    {
                        id = "route/add",
                        data = new
                        {
                            nodes = data
                        }
                    }
                }
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            string uuid = (string)jObject.SelectToken("data").SelectToken("data").SelectToken("data").SelectToken("uuid");
            Console.WriteLine(jObject);
            return uuid;
        }

        public void UpdateTerrain()
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = tunnelID,
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
                    dest = tunnelID,
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
                    dest = tunnelID,
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
                    dest = tunnelID,
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

        public void ResetScene()
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = tunnelID,
                    data = new
                    {
                        id = "scene/reset",
                        data = new { }
                    }
                }
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            Console.WriteLine(jObject);
        }

        public void AddTerrainNode(string terrainName, int x, int y, int z)
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = tunnelID,
                    data = new
                    {
                        id = "scene/node/add",
                        data = new
                        {
                            name = terrainName,
                            components = new
                            {
                                transform = new
                                {
                                    position = (new int[3] { x, y, z }),
                                    scale = 1,
                                    rotation = (new int[3] { 0, 0, 0 })
                                },
                                terrain = new
                                {
                                    smoothnormals = true
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

        public void MakeTreeFollowRoute()
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = tunnelID,
                    data = new
                    {
                        id = "route/follow",
                        data = new
                        {
                            route = AddRoute(),
                            node = GetUUID("tree"),
                            speed = 1.0,
                            offset = 0.0,
                            rotate = "XZ",
                            followHeight = true,
                            rotateOffset = (new int[] { 0, 0, 0 }),
                            positionOffset = (new int[] { 0, 0, 0 })
                        }
                    }
                }
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            Console.WriteLine(jObject);
        }

        public string GetUUID(string name)
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = tunnelID,
                    data = new
                    {
                        id = "scene/node/find",
                        data = new
                        {
                            name = name
                        }
                    }
                }
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            string uuid = (string)jObject.SelectToken("data").SelectToken("data").SelectToken("data").First.SelectToken("uuid");
            return uuid;
        }

        public void SetId(string id)
        {
            this.tunnelID = id;
        }

        public static int[] GetImageHeightArray(string imagepath, int length, int width)
        {
            Bitmap image = (Bitmap)Image.FromFile(imagepath);
            int[] result = new int[(length * width)];
            
            for (int y = 0; y <= length-1; y++)
            {
                for (int x = 0; x <= width-1; x++)
                {   
                    if ((image.GetPixel(x, y).R < 0x21) || (image.GetPixel(x, y).G < 0x21) || (image.GetPixel(x, y).B < 0x21))
                    {
                        result[((y * width) + x)] = 10;
                    }
                }

            }
            return result;
        }
    }
}
