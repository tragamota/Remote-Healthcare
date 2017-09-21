using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VR
{
    class Model
    {
        Connector connector;
        public string nodeID;

        public Model(Connector connector, string modelname, string filePath, int x, int y, int z)
        {
            this.connector = connector;
            this.nodeID = modelname;

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
                                    position = (new int[3] { x, y, z }),
                                    scale = 1,
                                    rotation = (new int[3] { 0, 0, 0 })
                                },
                                model = new
                                {
                                    file = filePath,
                                    cullbackfaces = true,
                                    animated = false,
                                    animation = "animationname"
                                },
                                //panel = new
                                //{
                                //    size = (new int[2] { 1, 1 }),
                                //    resolution = (new int[2] { 512, 512 }),
                                //    background = (new int[4] { 1, 1, 1, 0 })
                                //}
                            }
                        }
                    }
                }
            };

            connector.SendMessage(message);
            JObject jObject = connector.ReadMessage();
            Console.WriteLine(jObject);
        }

        
    }
}
