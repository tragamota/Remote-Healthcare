﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VR {
    [Serializable]
    public class Terrain {
        private Connector connector;
        public string terrainName;
        public string diffuseFile;
        public string normalFile;
        public int minHeight;
        public int maxHeight;
        public int fadeDist;
        public int width;
        public int length;
        public int x;
        public int y;
        public int z;
        public string imagepath;
        public double[] heightValues;
        public double[] measure;

        public Terrain(Connector connector, string terrainName, string diffuseFile, string normalFile, int minHeight, int maxHeight, int fadeDist, int width, int length, int x, int y, int z, double[] heightValues) {
            this.connector = connector;
            this.terrainName = terrainName;

            SetFiles(diffuseFile, normalFile);

            this.minHeight = minHeight;
            this.maxHeight = maxHeight;
            this.fadeDist = fadeDist;
            this.width = width;
            this.length = length;
            this.x = x;
            this.y = y;
            this.z = z;
            this.heightValues = heightValues;
            this.measure = new double[2] { width, length };
        }

        public Terrain(Connector connector, string terrainName, string diffuseFile, string normalFile, int minHeight, int maxHeight, int fadeDist, int width, int length, int x, int y, int z) {
            this.connector = connector;
            this.terrainName = terrainName;

            SetFiles(diffuseFile, normalFile);

            this.minHeight = minHeight;
            this.maxHeight = maxHeight;
            this.fadeDist = fadeDist;
            this.width = width;
            this.length = length;
            this.x = x;
            this.y = y;
            this.z = z;

            heightValues = new double[width * length];
            for (int i = 0; i < heightValues.Length; i++) {
                heightValues[i] = 0;
            }
        }

        [JsonConstructor]
        public Terrain(Connector connector, string terrainName, string diffuseFile, string normalFile, int minHeight, int maxHeight, int fadeDist, int x, int y, int z, string imagepath) {
            this.connector = connector;
            this.terrainName = terrainName;

            SetFiles(diffuseFile, normalFile, imagepath);

            this.minHeight = minHeight;
            this.maxHeight = maxHeight;
            this.fadeDist = fadeDist;
            this.x = x;
            this.y = y;
            this.z = z;
            
            Bitmap heightImage = (Bitmap)Image.FromFile(this.imagepath);
            heightValues = new double[heightImage.Width * heightImage.Height];

            for (int i = 0; i < heightImage.Height; i++)
            {
                for (int j = 0; j < heightImage.Width; j++)
                {
                    int alpha = int.Parse(heightImage.GetPixel(j, i).A.ToString());
                    heightValues[(i * (heightImage.Width - 1)) + j] = alpha / 8.75;
                }
            }

            measure = new double[2] { heightImage.Width, heightImage.Height };
        }

        private void SetFiles(string diffuseFile, string normalFile, string imagepath)
        {
            string[] allFiles = Directory.GetFiles(connector.GetFilePath() + "\\data\\NetworkEngine", diffuseFile, SearchOption.AllDirectories);
            this.diffuseFile = allFiles[0];

            allFiles = Directory.GetFiles(connector.GetFilePath() + "\\data\\NetworkEngine", normalFile, SearchOption.AllDirectories);
            this.normalFile = allFiles[0];

            allFiles = Directory.GetFiles(connector.GetFilePath() + "\\data\\NetworkEngine", imagepath, SearchOption.AllDirectories);
            this.imagepath = allFiles[0];
        }

        private void SetFiles(string diffuseFile, string normalFile)
        {
            string[] allFiles = Directory.GetFiles(connector.GetFilePath());
            foreach (string file in allFiles)
            {
                string tempFile = file.Remove(file.Length - diffuseFile.Length);
                if (tempFile.Equals(diffuseFile))
                    this.diffuseFile = file;

                tempFile = file.Remove(file.Length - normalFile.Length);
                if (tempFile.Equals(normalFile))
                    this.normalFile = file;
            }
        }

        public void AddLayer() {
            dynamic message = new {
                id = "tunnel/send",
                data = new {
                    dest = connector.tunnelID,
                    data = new {
                        id = "scene/node/addlayer",
                        data = new {
                            id = connector.GetTerrainUUID(terrainName),
                            diffuse = diffuseFile,
                            normal = normalFile,
                            minHeight = minHeight,
                            maxHeight = maxHeight,
                            fadeDist = fadeDist
                        }
                    }
                }
            };

            connector.SendMessage(message);
            JObject jObject = connector.ReadMessage();
            Console.WriteLine(jObject);
        }

        public void AddNode() {
            dynamic message = new {
                id = "tunnel/send",
                data = new {
                    dest = connector.tunnelID,
                    data = new {
                        id = "scene/node/add",
                        data = new {
                            name = terrainName,
                            components = new {
                                transform = new {
                                    position = (new int[3] { x, y, z }),
                                    scale = 1,
                                    rotation = (new int[3] { 0, 0, 0 })
                                },
                                terrain = new {
                                    smoothnormals = true
                                }
                            }
                        }
                    }
                }
            };

            connector.SendMessage(message);
            JObject jObject = connector.ReadMessage();
            //Console.WriteLine(jObject);
        }

        public void DeleteTerrain() {
            dynamic message = new {
                id = "tunnel/send",
                data = new {
                    dest = connector.tunnelID,
                    data = new {
                        id = "scene/terrain/delete",
                        data = new { }
                    }
                }
            };

            connector.SendMessage(message);
            JObject jObject = connector.ReadMessage();
            //Console.WriteLine(jObject);
        }

        public void UpdateTerrain() {
            dynamic message = new {
                id = "tunnel/send",
                data = new {
                    dest = connector.tunnelID,
                    data = new {
                        id = "scene/terrain/update",
                        data = new { }
                    }
                }
            };

            connector.SendMessage(message);
            JObject jObject = connector.ReadMessage();
            //Console.WriteLine(jObject);
        }

        public void DeleteLayer() {
            dynamic message = new {
                id = "tunnel/send",
                data = new {
                    dest = connector.tunnelID,
                    data = new {
                        id = "scene/node/dellayer",
                        data = new { }
                    }
                }
            };

            connector.SendMessage(message);
            JObject jObject = connector.ReadMessage();
            //Console.WriteLine(jObject);
        }

        public void Load()
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = connector.tunnelID,
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

            connector.SendMessage(message);
            JObject jObject = connector.ReadMessage();
            //Console.WriteLine(jObject);

            AddNode();
            AddLayer();
        }

        public void Reload(Connector connector)
        {
            this.connector = connector;
            if(this.heightValues != null)
            {
                dynamic message = new
                {
                    id = "tunnel/send",
                    data = new
                    {
                        dest = connector.tunnelID,
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

                connector.SendMessage(message);
                JObject jObject = connector.ReadMessage();
                //Console.WriteLine(jObject);

                AddNode();
                AddLayer();
            }else if(imagepath != null)
            {
                dynamic message = new
                {
                    id = "tunnel/send",
                    data = new
                    {
                        dest = connector.tunnelID,
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

                connector.SendMessage(message);
                JObject jObject = connector.ReadMessage();
                //Console.WriteLine(jObject);

                AddNode();
                AddLayer();
            }
            else
            {
                dynamic message = new
                {
                    id = "tunnel/send",
                    data = new
                    {
                        dest = connector.tunnelID,
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

                connector.SendMessage(message);
                JObject jObject = connector.ReadMessage();
                //Console.WriteLine(jObject);

                AddNode();
                AddLayer();
            }
        }
    }
}
