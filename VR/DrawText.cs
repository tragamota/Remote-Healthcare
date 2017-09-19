using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VR
{
    class DrawText
    {
        Connector connector;
        public string textString;
        public string uuid;
        

        public DrawText(Connector connector, string panelName, string inputText)
        {
            this.connector = connector;
            this.textString = inputText;
            this.uuid = connector.GetUUID("bike");
            connector.ClearPanel(uuid);

            //connector.SetClearColor(uuid);

            dynamic message = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = connector.tunnelID,
                    data = new
                    {
                        id = "scene/panel/drawtext",
                        data = new
                        {
                            id  = uuid,
		                    text = textString,
		                    position = new [] { 50,40 }
                        }
                    }
                }
            };

            connector.SendMessage(message);
            JObject jObject = connector.ReadMessage();
            Console.WriteLine(jObject);

            connector.SwapText(uuid);
           

        }
    }
}
