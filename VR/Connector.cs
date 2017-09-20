using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VR {
    class Connector {
        private TcpClient tcp;
        private NetworkStream stream;
        private Terrain terrain;
        public string tunnelID;
        public List<Model> Models { get; set; }
        public List<Route> Routes { get; set; }

        public Connector() {
            try {
                tcp = new TcpClient("145.48.6.10", 6666);
                stream = tcp.GetStream();
            }
            catch (IOException e) {
                Console.WriteLine(e.StackTrace);
            }
            Models = new List<Model>();
            Routes = new List<Route>();
        }

        public Dictionary<string, string> GetClients() {
            dynamic message = new {
                id = "session/list"
            };
            SendMessage(message);

            Dictionary<string, string> hosts = new Dictionary<string, string>();

            JObject jObject = ReadMessage();

            JArray array = (JArray)jObject.SelectToken("data");

            foreach (JObject client in array) {
                string host = (string)client.SelectToken("clientinfo").SelectToken("host");
                string id = (string)client.SelectToken("id");
                hosts.Add(host + " - " + id, id);
            }

            return hosts;
        }

        public void SendMessage(dynamic message) {
            string json = JsonConvert.SerializeObject(message);

            byte[] prefixArray = BitConverter.GetBytes(json.Length);
            byte[] requestArray = Encoding.Default.GetBytes(json);

            byte[] buffer = new Byte[prefixArray.Length + json.Length];
            prefixArray.CopyTo(buffer, 0);
            requestArray.CopyTo(buffer, prefixArray.Length);
            stream.Write(buffer, 0, buffer.Length);
        }

        public JObject ReadMessage() {
            StringBuilder message = new StringBuilder();
            int numberOfBytesRead = 0;
            byte[] messageBytes = new byte[4];
            stream.Read(messageBytes, 0, messageBytes.Length);
            byte[] receiveBuffer = new byte[BitConverter.ToInt32(messageBytes, 0)];

            do {
                numberOfBytesRead = stream.Read(receiveBuffer, 0, receiveBuffer.Length);

                message.AppendFormat("{0}", Encoding.ASCII.GetString(receiveBuffer, 0, numberOfBytesRead));

            }
            while (message.Length < receiveBuffer.Length);

            string response = message.ToString();
            return JObject.Parse(response);
        }

        public void ChangeScene(string change) {
            dynamic message = new {
                id = "tunnel/send",
                data = new {
                    dest = tunnelID,
                    data = new {
                        id = change,
                        data = new { }
                    }
                }
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            //Console.WriteLine(jObject);
        }

        public void SetTime(double hour) {
            dynamic message = new {
                id = "tunnel/send",
                data = new {
                    dest = tunnelID,
                    data = new {
                        id = "scene/skybox/settime",
                        data = new {
                            time = hour
                        }
                    }
                }
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            //Console.WriteLine(jObject);
        }

        public void DeleteGroundplane() {
            JObject jObject = GetScene();
            JArray array = (JArray)jObject.SelectToken("data").SelectToken("data").SelectToken("data").SelectToken("children");
            List<JToken> list = array.ToList();
            foreach (JToken token in list) {
                if (((string)token.SelectToken("name")).Equals("GroundPlane"))
                    DeleteNode((string)token.SelectToken("uuid"));
            }
        }

        public void DeleteNode(string nodeID) {
            dynamic message = new {
                id = "tunnel/send",
                data = new {
                    dest = tunnelID,
                    data = new {
                        id = "scene/node/delete",
                        data = new {
                            id = nodeID
                        }
                    }
                }
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            //Console.WriteLine(jObject);

            foreach (Model model in Models) {
                if (model.uuid.Equals(nodeID)) {
                    Models.Remove(model);
                    break;
                }
            }
        }

        public JObject GetScene() {
            dynamic message = new {
                id = "tunnel/send",
                data = new {
                    dest = tunnelID,
                    data = new {
                        id = "scene/get"
                    }
                }
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            Console.WriteLine(jObject);
            return jObject;
        }

        public string GetTerrainUUID(string terrainName) {
            JObject jObject = GetScene();
            JArray array = (JArray)jObject.SelectToken("data").SelectToken("data").SelectToken("data").SelectToken("children");
            List<JToken> list = array.ToList();
            foreach (JToken token in list) {
                if (((string)token.SelectToken("name")).Equals(terrainName))
                    return (string)token.SelectToken("uuid");
            }

            return null;
        }

        public void ResetScene() {
            dynamic message = new {
                id = "tunnel/send",
                data = new {
                    dest = tunnelID,
                    data = new {
                        id = "scene/reset",
                        data = new { }
                    }
                }
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            //Console.WriteLine(jObject);
        }

        public void LoadSceneModels() {
            JObject jObject = GetScene();
            JArray array = (JArray)jObject.SelectToken("data").SelectToken("data").SelectToken("data").SelectToken("children");
            List<JToken> list = array.ToList();
            foreach (JToken token in list) {
                Models.Add(new Model(this, (string)token.SelectToken("name"), (string)token.SelectToken("uuid")));
            }
        }

        public void SetId(string id) {
            this.tunnelID = id;
        }

        public void AddModel(string modelName, string filePath, int x, int y, int z) {
            Models.Add(new Model(this, modelName, filePath, x, y, z));
        }

        public void AddTerrain(string terrainName, string diffuseFilePath, string normalFilePath, int minHeight, int maxHeight, int fadeDistance, int width, int length, int x, int y, int z, int[] heightValues) {
            terrain = new Terrain(this, terrainName, diffuseFilePath, normalFilePath, minHeight, maxHeight, fadeDistance, width, length, x, y, z, heightValues);
        }

        public void AddTerrainByPicture(string terrainName, string diffuseFilePath, string normalFilePath, int minHeight, int maxHeight, int fadeDistance, int width, int length, int x, int y, int z, string imagepath) {
            terrain = new Terrain(this, terrainName, diffuseFilePath, normalFilePath, minHeight, maxHeight, fadeDistance, width, length, x, y, z, imagepath);
        }

        public void AddTerrain(string terrainName, string diffuseFilePath, string normalFilePath, int minHeight, int maxHeight, int fadeDistance, int width, int length, int x, int y, int z) {
            terrain = new Terrain(this, terrainName, diffuseFilePath, normalFilePath, minHeight, maxHeight, fadeDistance, width, length, x, y, z);
        }

        public void AddRoute(dynamic[] data, string routeName) {
            Routes.Add(new Route(this, data, routeName));
        }

        public List<Model> GetModels() {
            return Models;
        }

        public List<Route> GetRoutes() {
            return Routes;
        }
    }
}
