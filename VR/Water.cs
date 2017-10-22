using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VR
{
    class Water : Model
    {
        private Connector connector;

        public Water(Connector connector, string modelname, double x, double y, double z) : base()
        {
            this.connector = connector;
            this.modelname = modelname;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override void Load()
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = connector.tunnelID,
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
                                    position = (new double[3] { x, y, z }),
                                    scale = 1,
                                    rotation = (new int[3] { 0, 0, 0 })
                                },
                                water = new
                                {
                                    size = (new int[2] { 256, 256 }),
                                    resolution = 0.9
                                }
                            }
                        }
                    }
                }
            };

            connector.SendMessage(message);
            JObject jObject = connector.ReadMessage();
            uuid = (string)jObject.SelectToken("data").SelectToken("data").SelectToken("data").SelectToken("uuid");
            //Console.WriteLine(jObject);
        }

        public override void Reload(Connector connector)
        {
            this.connector = connector;
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = connector.tunnelID,
                    data = new
                    {
                        id = "scene/node/add",
                        data = new
                        {
                            name = "water",
                            components = new
                            {
                                transform = new
                                {
                                    position = (new double[3] { x, y, z }),
                                    scale = 1,
                                    rotation = (new int[3] { 0, 0, 0 })
                                },
                                water = new
                                {
                                    size = (new int[2] { 256, 256 }),
                                    resolution = 0.9
                                }
                            }
                        }
                    }
                }
            };

            connector.SendMessage(message);
            JObject jObject = connector.ReadMessage();
            uuid = (string)jObject.SelectToken("data").SelectToken("data").SelectToken("data").SelectToken("uuid");
            //Console.WriteLine(jObject);
        }
    }
}
