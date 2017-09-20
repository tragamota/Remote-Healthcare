using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VR {
    class Model {
        private Connector connector;
        public string modelName;
        public string uuid;

        public Model(Connector connector, string modelname, string filePath, int x, int y, int z) {
            this.connector = connector;
            this.modelName = modelname;
            dynamic message = new {
                id = "tunnel/send",
                data = new {
                    dest = connector.tunnelID,
                    data = new {
                        id = "scene/node/add",
                        data = new {
                            name = modelname,
                            components = new {
                                transform = new {
                                    position = (new int[3] { x, y, z }),
                                    scale = 1,
                                    rotation = (new int[3] { 0, 0, 0 })
                                },
                                model = new {
                                    file = filePath,
                                    cullbackfaces = true,
                                    animated = false,
                                    animation = "animationname"
                                },
                                panel = new {
                                    size = (new int[2] { 1, 1 }),
                                    resolution = (new int[2] { 512, 512 }),
                                    background = (new int[4] { 1, 1, 1, 1 })
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

        public Model(Connector connector, string modelname, string uuid) {
            this.connector = connector;
            this.modelName = modelname;
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
    }
}
