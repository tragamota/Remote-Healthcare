﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VR
{
    class Route
    {
        string routeID;
        Connector connector;

        public Route(Connector connector)
        {
            this.connector = connector;

            dynamic pos1 = new { pos = (new int[3] { 0, 0, 0 }), dir = (new int[3] { 5, 0, -5 }) };
            dynamic pos2 = new { pos = (new int[3] { 50, 0, 0 }), dir = (new int[3] { 5, 0, 5 }) };
            dynamic pos3 = new { pos = (new int[3] { 50, 0, 50 }), dir = (new int[3] { -5, 0, 5 }) };
            dynamic pos4 = new { pos = (new int[3] { 0, 0, 50 }), dir = (new int[3] { -5, 0, -5 }) };

            dynamic[] data = new dynamic[4]
            {
                pos1, pos2, pos3, pos4
            };

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
                            node = connector.GetUUID(model.nodeID),
                            speed = 1.0,
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