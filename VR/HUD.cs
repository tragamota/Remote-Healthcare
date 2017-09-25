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
            this.cameraID = connector.GetUUID("Head");
            Console.WriteLine("Camera ID: {0}", cameraID);

            connector.AddHUD(cameraID);

            this.uuid = connector.GetUUID("HUDPanel");

            //connector.DrawLines(uuid);

            double x = 1;
            double y = 2;
            double z = 3;
            
            while(x < 1000 && y < 1000 && z < 1000)
            {
                x++;
                y++;
                z++;
                Update(x, y, z,23,23,23, DateTime.Now.ToString("h:mm:ss tt"), 34);

            }
            
            JObject jObject = connector.GetScene();
            Console.WriteLine(jObject);

            Console.WriteLine("Camera ID: {0}", cameraID);

        }

        public void DrawHeartRate(double rate)
        { 
            string heartrate = $"Heartrate: {rate}bpm";
            connector.DrawText(uuid, heartrate, 300, 80);
        }

        public void DrawSpeed(double sp)
        {
            string speed = $"Speed: {sp}km/H";
            connector.DrawText(uuid, speed, 300, 120);
        }

        public void DrawDistance(double dist)
        {
            string distance = $"Distance: {dist}km";
            connector.DrawText(uuid, distance, 300, 160);
        }

        public void DrawRoundMin(double round)
        {
            string roundmin = $"RoundMin: {round}R/MIN";
            connector.DrawText(uuid, roundmin, 300, 200);
        }

        public void DrawResistance(double res)
        {
            string resistance = $"Resistance: {res}Watt";
            connector.DrawText(uuid, resistance, 300, 240);
        }

        public void DrawEnergy(double en)
        {
            string energy = $"Energy: {en}K/J";
            connector.DrawText(uuid, energy, 300, 280);
        }

        public void DrawTime(String ti)
        {
            string time = $"Time: {ti}";
            connector.DrawText(uuid, time, 300, 40);
        }

        public void DrawWatt(double wat)
        {
            string watt = $"Watt: {wat}Watt";
            connector.DrawText(uuid, watt, 300, 320);
        }

        public void Update(double rate, double sp, double dist,double round, double res, double en, string ti, double wat)
        {
            connector.ClearPanel(uuid);

            DrawHeartRate(rate);
            DrawDistance(sp);
            DrawSpeed(dist);
            DrawRoundMin(round);
            DrawResistance(res);
            DrawEnergy(en);
            DrawTime(ti);
            DrawWatt(wat);

            connector.SwapText(uuid);
        }

    }
}
