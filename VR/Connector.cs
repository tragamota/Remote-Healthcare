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
                string host = (string)client.SelectToken("clientinfo").SelectToken("user");
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

        public string GetUUID(string modelName) {
            var command = new {
                id = "tunnel/send",
                data = new {
                    id = "scene/node/find",
                    data = new {
                        name = modelName
                    }
                }
            };
            SendMessage(command);
            return (string) ReadMessage()["data"]["uuid"];
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

        public void UpdateNode(string uuid, string parentID)
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = tunnelID,
                    data = new
                    {
                        id = "scene/node/update",
                        data = new
                        {
                            id = uuid,
                            parent = parentID,
                            transform = new
                            {
                                scale = 1.0
                            }
                        }
                    }
                }
                
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            //Console.WriteLine(jObject);

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
        public void ClearPanel(string uuid)
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = tunnelID,
                    data = new
                    {
                        id = "scene/panel/clear",
                        data = new
                        {
                            id = uuid
                        }
                    }
                }
            };
            SendMessage(message);
            JObject jObject = ReadMessage();
            Console.WriteLine(jObject);

        }

        public void SwapText(string uuid)
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = tunnelID,
                    data = new
                    {
                        id = "scene/panel/swap",
                        data = new
                        {
                            id = uuid
                        }
                    }
                }
            };
            SendMessage(message);
            JObject jObject = ReadMessage();
            Console.WriteLine(jObject);

        }

        /* 
         * uuid: uuid van de node van het panel waarop getekend moet worden
         * text: invoertext
         * x: positie op panel x-waarde(moet hoger dan 0 zijn)
         * y: positie op panel y-waarde(moet hoger dan 0 zijn)
         */
        public void DrawText(string uuid, string text, int x, int y)
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = tunnelID,
                    data = new
                    {
                        id = "scene/panel/drawtext",
                        data = new
                        {
                            id = uuid,
                            text = text,
                            position = new[] { x, y },
                            size = 60.0
                        }
                    }
                }
            };
            SendMessage(message);
            JObject jObject = ReadMessage();
            Console.WriteLine(jObject);

        }

        public void SetClearColor(string uuid)
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = tunnelID,
                    data = new
                    {
                        id = "scene/panel/setclearcolor",
                        data = new
                        {
                            id = uuid,
                            color = (new int[4] { 0, 0, 0, 0})
                        }
                    }
                }
            };
            SendMessage(message);
            JObject jObject = ReadMessage();
            Console.WriteLine(jObject);

        }

        // x1,y1, x2,y2, r,g,b,a
        public void DrawLines(string uuid)
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = tunnelID,
                    data = new
                    {
                        id = "scene/panel/drawlines",
                        data = new
                        {
                            id = uuid,
                            width = 1,
                            lines = (new int[16] { 280, 40, 280, 180, 0, 0, 0, 1,
                                                   280, 40, 600, 40, 0, 0, 0, 1}),
                        }
                    }
                }
            };
            SendMessage(message);
            JObject jObject = ReadMessage();
            Console.WriteLine(jObject);
    
        }

        /*
         * position (x, y, z) 
         * x= naar je toe
         * y=
         * z= naar je toe
         * 
         */
        public void AddHUD(string uuid)
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
                            name = "HUDPanel",
                            parent = uuid,
                            components = new
                            {
                                transform = new
                                {
                                    position = (new double[3] { -0.3, 0.9, 0 }),
                                    scale = 1,
                                    rotation = (new int[3] { 0, 90, 50 })
                                },
                                panel = new
                                {
                                    size = (new int[2] { 1, 1 }),
                                    resolution = (new int[2] { 1028, 1028 }),
                                    background = (new int[4] { 1 ,1 ,1 , 0})
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
