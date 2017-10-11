using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VR {
    public class Terrain {
        private Connector connector;
        private string terrainName;
        private string diffuseFile;
        private string normalFile;
        private int minHeight;
        private int maxHeight;
        private int fadeDist;
        private int width;
        private int length;
        private int x;
        private int y;
        private int z;
        private string imagepath;
        private int[] heightValues;

        public Terrain(Connector connector, string terrainName, string diffuseFile, string normalFile, int minHeight, int maxHeight, int fadeDist, int width, int length, int x, int y, int z, int[] heightValues) {
            this.connector = connector;
            this.terrainName = terrainName;
            this.diffuseFile = diffuseFile;
            this.normalFile = normalFile;
            this.minHeight = minHeight;
            this.maxHeight = maxHeight;
            this.fadeDist = fadeDist;
            this.width = width;
            this.length = length;
            this.x = x;
            this.y = y;
            this.z = z;
            this.heightValues = heightValues;


            int[] measure = new int[2] { width, length };

            dynamic message = new {
                id = "tunnel/send",
                data = new {
                    dest = connector.tunnelID,
                    data = new {
                        id = "scene/terrain/add",
                        data = new {
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

        public Terrain(Connector connector, string terrainName, string diffuseFile, string normalFile, int minHeight, int maxHeight, int fadeDist, int width, int length, int x, int y, int z) {
            this.connector = connector;
            this.terrainName = terrainName;
            this.diffuseFile = diffuseFile;
            this.normalFile = normalFile;
            this.minHeight = minHeight;
            this.maxHeight = maxHeight;
            this.fadeDist = fadeDist;
            this.width = width;
            this.length = length;
            this.x = x;
            this.y = y;
            this.z = z;

            int[] heightValues = new int[width * length];
            for (int i = 0; i < heightValues.Length; i++) {
                heightValues[i] = 0;
            }
            int[] measure = new int[2] { width, length };

            dynamic message = new {
                id = "tunnel/send",
                data = new {
                    dest = connector.tunnelID,
                    data = new {
                        id = "scene/terrain/add",
                        data = new {
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

        public Terrain(Connector connector, string terrainName, string diffuseFile, string normalFile, int minHeight, int maxHeight, int fadeDist, int x, int y, int z, string imagepath) {
            this.connector = connector;
            this.terrainName = terrainName;
            this.diffuseFile = diffuseFile;
            this.normalFile = normalFile;
            this.minHeight = minHeight;
            this.maxHeight = maxHeight;
            this.fadeDist = fadeDist;
            this.x = x;
            this.y = y;
            this.z = z;
            this.imagepath = imagepath;
            
            Bitmap heightImage = (Bitmap)Image.FromFile(imagepath);
            double[] heightValues = new double[heightImage.Width * heightImage.Height];

            for (int i = 0; i < heightImage.Height; i++)
            {
                for (int j = 0; j < heightImage.Width; j++)
                {
                    int alpha = int.Parse(heightImage.GetPixel(j, i).A.ToString());
                    heightValues[(i * (heightImage.Width - 1)) + j] = alpha / 8.75;
                }
            }

            int[] measure = new int[2] { heightImage.Width, heightImage.Height };

            dynamic message = new {
                id = "tunnel/send",
                data = new {
                    dest = connector.tunnelID,
                    data = new {
                        id = "scene/terrain/add",
                        data = new {
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

        public void Reload()
        {
            if(this.heightValues != null)
            {
                int[] measure = new int[2] { width, length };

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
                Bitmap heightImage = (Bitmap)Image.FromFile(imagepath);
                double[] heightValues = new double[heightImage.Width * heightImage.Height];

                for (int i = 0; i < heightImage.Height; i++)
                {
                    for (int j = 0; j < heightImage.Width; j++)
                    {
                        int alpha = int.Parse(heightImage.GetPixel(j, i).A.ToString());
                        heightValues[(i * (heightImage.Width - 1)) + j] = alpha / 8.75;
                    }
                }

                int[] measure = new int[2] { heightImage.Width, heightImage.Height };

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
