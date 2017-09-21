using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VR
{
    
    class HUD
    {
        Connector connector;
        public string uuid;
        public string cameraID;

        public HUD(Connector connector)
        {
            this.connector = connector;
            this.cameraID = connector.GetUUID("Camera");
            Console.WriteLine("Camera ID: {0}", cameraID);

            connector.AddHUD(cameraID);

            this.uuid = connector.GetUUID("HUDPanel");

            connector.DrawText(uuid, "Test", 50, 40);


            drawHeartRate("Heartrate: 100bpm");
            drawDistance("Distance: 69km");
            drawSpeed("Speed: 137km/h");

            connector.SwapText(uuid);

            JObject jObject = connector.GetScene();
            Console.WriteLine(jObject);

            Console.WriteLine("Camera ID: {0}", cameraID);

        }

        public void drawHeartRate(string text)
        {
            connector.DrawText(uuid, text, 50, 80);
            //connector.UpdateNode(uuid, cameraID);
        }

        public void drawSpeed(string text)
        {
            connector.DrawText(uuid, text, 50, 120);
        }

        public void drawDistance(string text)
        {
            connector.DrawText(uuid, text, 50, 160);
        }
    }
}
