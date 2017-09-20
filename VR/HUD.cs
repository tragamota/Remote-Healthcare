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

            connector.AddHUD(cameraID);

            this.uuid = connector.GetUUID("HUDPanel");

            connector.DrawText(uuid, "Test hier", 50, 40);
          
            connector.SwapText(uuid);

            drawHeartRate("Heartrate: 100bpm");
        }

        public void drawHeartRate(string text)
        {
            connector.DrawText(uuid, text, 50, 60);
            connector.UpdateNode(uuid, cameraID);

        }
    }
}
