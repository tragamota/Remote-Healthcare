using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VR {
    public class Connector {
        private TcpClient tcp;
        private NetworkStream stream;
        public string tunnelID;
        public Terrain terrain;
        public List<Model> Models { get; set; }
        public List<Route> Routes { get; set; }
        private string filePath;
        private double previousHeight;
        public HUD hud;

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

        public JArray GetClients() {
            dynamic message = new {
                id = "session/list"
            };
            SendMessage(message);

            JObject jObject = ReadMessage();
            JArray array = (JArray)jObject["data"];
            return array;
        }

        internal string GetFilePath()
        {
            return filePath;
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
            //StringBuilder message = new StringBuilder();
            //int numberOfBytesRead = 0;
            //byte[] messageBytes = new byte[4];
            //stream.Read(messageBytes, 0, messageBytes.Length);
            //byte[] receiveBuffer = new byte[BitConverter.ToInt32(messageBytes, 0)];

            //do {
            //    numberOfBytesRead = stream.Read(receiveBuffer, 0, receiveBuffer.Length);

            //    message.AppendFormat("{0}", Encoding.Default.GetString(receiveBuffer, 0, numberOfBytesRead));

            //}
            //while (message.Length < receiveBuffer.Length);

            //string response = message.ToString();

            //try {
            //    return JObject.Parse(response);
            //}catch(Exception e) {
            //    return new JObject();
            //}

            int numberOfBytesRead = 0;
            byte[] messageBytes = new byte[4];
            byte[] receiveBuffer;

            while (numberOfBytesRead < messageBytes.Length) {
                try {
                    numberOfBytesRead += stream.Read(messageBytes, numberOfBytesRead, messageBytes.Length - numberOfBytesRead);
                }
                catch (IOException e) {
                    Console.WriteLine(e.StackTrace);
                    return null;
                }
            }

            numberOfBytesRead = 0;
            try {
                int size = BitConverter.ToInt32(messageBytes, 0);
                Console.WriteLine("Size: " + size);
                receiveBuffer = new byte[size];
            }
            catch (ArgumentException e) {
                Console.WriteLine(e.StackTrace);
                return null;
            }

            while (numberOfBytesRead < receiveBuffer.Length) {
                try {
                    numberOfBytesRead += stream.Read(receiveBuffer, numberOfBytesRead, receiveBuffer.Length - numberOfBytesRead);
                }
                catch (IOException e) {
                    Console.WriteLine(e.StackTrace);
                    return null;
                }
            }

            try {
                return JObject.Parse(Encoding.Default.GetString(receiveBuffer));
            }
            catch (Exception) {
                return null;
            }
        }

        internal void SetFilePath(string filePath)
        {
            this.filePath = filePath;
        }

        public void SetBikeSpeed(double speed)
        {
            Model bike = Models.Find(x => x.modelname.Equals("bike"));
            bike.ChangeSpeed((speed / 5.0));
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
            string uuid = (string)jObject["data"]["data"]["data"].First["uuid"];
            return uuid;
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
            Console.WriteLine(jObject.ToString());

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
            LoadSceneModels();

            foreach (Model model in Models) {
                if (model.modelname.Equals(terrainName))
                    return model.uuid;
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
                            parent = parentID
                        }
                    }
                }
                
            };

            SendMessage(message);
            JObject jObject = ReadMessage();
            //Console.WriteLine(jObject);

        }

        public List<double> getPosition(string objectName)
        {
            List<double> list = new List<double>();
            JObject jObject = GetScene();
            JArray array = (JArray)jObject.SelectToken("data").SelectToken("data").SelectToken("data").SelectToken("children");

            //List<JToken> list = array.ToList();
            foreach (JObject ob in array)
            {
                if (((string)ob.SelectToken("name")).Equals(objectName))
                {
                    JArray positionArray = (JArray)ob.SelectToken("components").First.SelectToken("position");

                    list = positionArray.ToObject<List<double>>();                
                }
            }

            return list;
        }


        public double CalculateIncline(string objectName)
        {
            List<double> list = getPosition(objectName);

            double incline = 0;
            double difference = 0;

            double currentHeight = list[1];

            difference = (currentHeight - previousHeight)/5;

            incline = 400 * difference;

            previousHeight = currentHeight;

            
            return incline;
        }

        public double CalculateInclineNotExactly(string objectName)
        {
            List<double> list = getPosition(objectName);

            double incline = 0;
            double difference = 0;

            double currentHeight = list[1];

            difference = (currentHeight - previousHeight);

            if (difference < 0)
                throw new ArgumentOutOfRangeException();
            else if (difference >= 0 && difference <= 1)
                incline = 100;
            else if (difference > 1 && difference <= 2)
                incline = 150;
            else if (difference > 2 && difference <= 3)
                incline = 200;
            else if (difference > 3 && difference <= 4)
                incline = 300;
            else if (difference > 4 && difference <= 5)
                incline = 400;
            else if (difference > 5)
                incline = 400;

            previousHeight = currentHeight;

            return incline;
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
                            size = 50.0,
                            font = "Arial"
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
                                    background = (new int[4] { 1, 1, 1, 0 }),
                                    color = new[] { 1, 1, 1, 1 },
                                    font = "Cooper Zwart",
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

        public void AddMassageScreen(string uuid)
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
                                name = "HUDMessage",
                                parent = uuid,
                                components = new
                                {
                                    transform = new
                                    {
                                        position = (new double[3] { -1, 1.5, 0 }),
                                        scale = 1,
                                        rotation = (new int[3] { 0, 90, 0 })
                                    },
                                    panel = new
                                    {
                                        size = (new int[2] { 1, 1 }),
                                        resolution = (new int[2] { 1028, 1028 }),
                                        background = (new int[4] { 1, 1, 1, 0 }),
                                        color = new[] { 1, 1, 1, 1 },
                                        font = "Cooper Zwart",
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

        public void Save(string filen)
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = tunnelID,
                    data = new
                    {
                        id = "scene/save",
                        data = new
                        {
                            filename = filen,
                            overwrite = true
                        }
                    }
                }
            };
            SendMessage(message);
            JObject jObject = ReadMessage();
            Console.WriteLine(jObject);

        }
        public void Load(string filen)
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = tunnelID,
                    data = new
                    {
                        id = "scene/load",
                        data = new
                        {
                            filename = filen,

                        }
                    }
                }
            };
            SendMessage(message);
            JObject jObject = ReadMessage();
            Console.WriteLine(jObject);
        }

        public void LoadSceneModels() {
            Models = new List<Model>();
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

        public void AddModel(string modelName, string filePath, double x, double y, double z, double s, int zRotation) {
            double positionHeight = 0;
            if (zRotation == 99)
            {
                List<int> xValues = new List<int>();
                List<int> yValues = new List<int>();

                Random random = new Random();

                for (int ix = 0; ix < 21; ix++)
                {
                    xValues.Add(random.Next(-129, 129));
                }

                for (int iy = 0; iy < 21; iy++)
                {
                    yValues.Add(random.Next(-129, 129));
                }
                for (int i = 0; i < 20; i++)
                {
                    positionHeight = GetTerrainHeight(xValues[i], yValues[i]);
                    modelName = modelName + i.ToString();
                    Model m = new Model(this, modelName, filePath, xValues[i], positionHeight, yValues[i], s, zRotation);
                    m.Load();
                    Models.Add(m);

                }

            }
            else
            {
                positionHeight = GetTerrainHeight(x, z);
                Model m = new Model(this, modelName, filePath, x, positionHeight, z, s, zRotation);
                m.Load();
                Models.Add(m);
            }
        }

        public void AddTerrain(string terrainName, string diffuseFilePath, string normalFilePath, int minHeight, int maxHeight, int fadeDistance, int width, int length, int x, int y, int z, double[] heightValues) {
            this.terrain = new Terrain(this, terrainName, diffuseFilePath, normalFilePath, minHeight, maxHeight, fadeDistance, width, length, x, y, z, heightValues);
            terrain.Load();
        }

        public void AddTerrainByPicture(string terrainName, string diffuseFilePath, string normalFilePath, int minHeight, int maxHeight, int fadeDistance, int x, int y, int z, string imagepath) {
            this.terrain = new Terrain(this, terrainName, diffuseFilePath, normalFilePath, minHeight, maxHeight, fadeDistance, x, y, z, imagepath);
            terrain.Load();
        }

        public void AddTerrain(string terrainName, string diffuseFilePath, string normalFilePath, int minHeight, int maxHeight, int fadeDistance, int width, int length, int x, int y, int z) {
            this.terrain = new Terrain(this, terrainName, diffuseFilePath, normalFilePath, minHeight, maxHeight, fadeDistance, width, length, x, y, z);
            terrain.Load();
        }

        public void AddRoute(dynamic[] data, string routeName) {
            Route route = new Route(this, data, routeName);
            route.Load();
            Routes.Add(route);
        }


        public double GetTerrainHeight(double x, double z)
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
                            position = (new double[2] { x, z })
                        }
                    }
                }
            };

            SendMessage(message);
            JObject jObject = ReadMessage();

            JToken jToken = jObject["data"]["data"]["data"]["height"];
            string height = jToken.ToString();
            //height = height.Replace(',', '.');

            double heightValue = double.Parse(height);
            heightValue = Math.Round(heightValue, 1);

            Console.WriteLine(jObject);
            return heightValue;
        }

        public List<Model> GetModels() {
            return Models;
        }

        public List<Route> GetRoutes() {
            return Routes;
        }
        public void CameraNode() {
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
                            name = "Camera",
                            parent = GetUUID("Head"),
                            components = new
                            {
                                transform = new
                                {
                                    position = (new int[3] { 1, 1, 1 }),
                                    scale = 1,
                                    rotation = (new int[3] { 0, 0, 0 })
                                }
                            }
                        }
                    }
                }
            };

        SendMessage(message);
            JObject jObject = ReadMessage();
        //uuid = (string) jObject.SelectToken("data").SelectToken("data").SelectToken("data").SelectToken("uuid");
        Console.WriteLine(jObject);
        }

        public void SaveScene()
        {
            string path = "";

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog.Filter = "JSON (.json)|*.json;";
            saveFileDialog.FileName = "sessie.json";
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string obj = JsonConvert.SerializeObject(new
                {
                    id = "scene",
                    data = new
                    {
                        terrain = terrain,
                        models = Models,
                        routes = Routes
                    }
                }, Formatting.Indented, new JsonSerializerSettings {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
                File.WriteAllText(saveFileDialog.FileName, obj);
            }
        }

        public void LoadScene()
        {
            string path = "";

            OpenFileDialog browseFileDialog = new OpenFileDialog();
            browseFileDialog.Filter = "JSON (.json)|*.json;";
            browseFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            List<Model> models = new List<Model>();
            List<Route> routes = new List<Route>();

            if (browseFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = Path.GetFullPath(browseFileDialog.FileName);
                string json = File.ReadAllText(path);
                terrain = (Terrain)((JObject)JsonConvert.DeserializeObject(json))["data"]["terrain"].ToObject(typeof(Terrain));
                models = (List<Model>)((JObject)JsonConvert.DeserializeObject(json))["data"]["models"].ToObject(typeof(List<Model>));
                routes = (List<Route>)((JObject)JsonConvert.DeserializeObject(json))["data"]["routes"].ToObject(typeof(List<Route>));
            }

            terrain.Reload(this);

            List<Model> lakes = new List<Model>();
            this.Models = models;
            this.Routes = routes;

            //Model fakeHUD = null;

            foreach (Model model in models)
            {
                if(model.modelname.Equals("water"))
                {
                    Water water = new Water(this, "water", model.x, model.y, model.z);
                    water.Load();
                    lakes.Add(water);
                }else if(model.modelname == "hud")
				{
                    //fakeHUD = model;
					//Models.Add(new HUD(this));
				}
                else
                    model.Reload(this);

                //if (fakeHUD != null)
                    //Models.Remove(fakeHUD);
            }

            Models.RemoveAll(x => x.modelname.Equals("water"));
            foreach (Model model in lakes)
            {
                Models.Add(model);
            }

            foreach (Route route in Routes)
            {
                route.Reload(this);
            }
        }

        public void AddWater()
        {
            Water water = new Water(this, "water", 1, 5, 1);
            water.Load();
            Models.Add(water);
        }
    }
}
