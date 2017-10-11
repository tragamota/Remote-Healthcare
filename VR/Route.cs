using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VR
{
    public class Route
    {
        public string routeID;
        public Connector connector;
        public string routeName;
        private dynamic[] data;

        public Route(Connector connector, dynamic[] data, string routeName)
        {
            this.connector = connector;
            this.routeName = routeName;
            this.data = data;
        }

        public void MakeModelFollowRoute(Model model)
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = connector.tunnelID,
                    data = new
                    {
                        id = "route/follow",
                        data = new
                        {
                            route = routeID,
                            node = model.uuid,
                            speed = 0.0,
                            offset = 0.0,
                            rotate = "XZ",
                            followHeight = true,
                            rotateOffset = (new int[] { 0, 0, 0 }),
                            positionOffset = (new int[] { 0, 0, 0 })
                        }
                    }
                }
            };

            connector.SendMessage(message);
            JObject jObject = connector.ReadMessage();
            //Console.WriteLine(jObject);
        }

        public void AddRoad()
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = connector.tunnelID,
                    data = new
                    {
                        id = "scene/road/add",
                        data = new
                        {
                            route = routeID,
                            heightoffset = 0.01
                        }
                    }
                }
            };

            connector.SendMessage(message);
            JObject jObject = connector.ReadMessage();
            //Console.WriteLine(jObject);
        }

        public void Reload(Connector connector)
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
                        id = "route/add",
                        data = new
                        {
                            nodes = data
                        }
                    }
                }
            };

            connector.SendMessage(message);
            JObject jObject = connector.ReadMessage();
            routeID = (string)jObject.SelectToken("data").SelectToken("data").SelectToken("data").SelectToken("uuid");
            //Console.WriteLine(jObject);
        }

        internal void Load()
        {
            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = connector.tunnelID,
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

            connector.SendMessage(message);
            JObject jObject = connector.ReadMessage();
            routeID = (string)jObject.SelectToken("data").SelectToken("data").SelectToken("data").SelectToken("uuid");
            //Console.WriteLine(jObject);
        }
    }
}
