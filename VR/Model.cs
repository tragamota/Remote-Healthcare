using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VR {
    [Serializable]
    public class Model {
        public Connector connector;
        public string modelname, filePath;
        public string uuid;
        public double x, y, z, s;
        public int zRotation;

        public Model(Connector connector, string modelname, string filePath, double x, double y, double z, double s, int zRotation) {
            this.connector = connector;
            this.modelname = modelname;
            this.filePath = filePath;
            this.x = x;
            this.y = y;
            this.z = z;
            this.s = s;
            this.zRotation = zRotation;
        }

        [JsonConstructor]
        public Model(Connector connector, string modelname, string uuid) {
            this.connector = connector;
            this.modelname = modelname;
            this.uuid = uuid;
        }

        public void ChangeSpeed(double speed) {
            {
                dynamic message = new {
                    id = "tunnel/send",
                    data = new {
                        dest = connector.tunnelID,
                        data = new {
                            id = "route/follow/speed",
                            data = new {
                                node = uuid,
                                speed = speed
                            }
                        }
                    }
                };

                connector.SendMessage(message);
                //JObject jObject = connector.ReadMessage();
                //Console.WriteLine(jObject);
            }
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
                        id = "scene/node/add",
                        data = new
                        {
                            name = modelname,
                            components = new
                            {
                                transform = new
                                {
                                    position = (new double[3] { x, y, z }),
                                    scale = s,
                                    rotation = (new int[3] { 0, zRotation, 0 })
                                },
                                model = new
                                {
                                    file = filePath,
                                    cullbackfaces = true,
                                    animated = false,
                                    animation = "animationname"
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

        public void Reload(Connector connector)
        {
            this.connector = connector;
            if(filePath != null)
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
                                        scale = s,
                                        rotation = (new int[3] { 0, zRotation, 0 })
                                    },
                                    model = new
                                    {
                                        file = filePath,
                                        cullbackfaces = true,
                                        animated = false,
                                        animation = "animationname"
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
            else
            {
                this.connector = connector;
                this.modelname = modelname;
                this.uuid = uuid;
            }
        }
    }
}
