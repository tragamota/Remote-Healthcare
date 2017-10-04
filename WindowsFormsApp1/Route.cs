using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remote_Healtcare_Console
{
    class Route
    {
        public string routeID;
        public Connector connector;
        public string routeName;

        public Route(Connector connector, dynamic[] data, string routeName)
        {
            this.connector = connector;
            this.routeName = routeName;

            //dynamic pos1 = new { pos = (new int[3] { 0, 0, 0 }), dir = (new int[3] { 5, 0, -5 }) };
            //dynamic pos2 = new { pos = (new int[3] { 50, 0, 0 }), dir = (new int[3] { 5, 0, 5 }) };
            //dynamic pos3 = new { pos = (new int[3] { 50, 0, 50 }), dir = (new int[3] { -5, 0, 5 }) };
            //dynamic pos4 = new { pos = (new int[3] { 0, 0, 50 }), dir = (new int[3] { -5, 0, -5 }) };

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
    }
}
