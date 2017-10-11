using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace VR
{

    class HUD
    {
        Connector connector;
        public string uuid;
        public string uuidMessage;
        public string cameraID;

        public HUD(Connector connector)
        {
            this.connector = connector;
            this.cameraID = connector.GetUUID("Bike");
           // Console.WriteLine("Camera ID: {0}", cameraID);

            connector.AddHUD(cameraID);
            connector.AddMassageScreen(cameraID);

            this.uuid = connector.GetUUID("HUDPanel");
            this.uuidMessage = connector.GetUUID("HUDMessage");

            //connector.DrawLines(uuid);

            double x = 1;
            double y = 2;
            double z = 3;
            

            
            //while(x < 1000 && y < 1000 && z < 1000)
            //{
                x++;
                y++;
                z++;
            SetText("lul lul lul lul lul lul lul lul lul");
            Update2(x, y, z,x + 23,y + 23,z + 23, DateTime.Now.ToString("mm:ss tt"), x + y + 34);

             //}
            
            JObject jObject = connector.GetScene();
            Console.WriteLine(jObject);

            Console.WriteLine("Camera ID: {0}", cameraID);

        }

        public void DrawMessage(string message)
        {
            connector.DrawText(uuidMessage, message, 100, 600);
        }


        public void DrawHeartRate(double rate)
        { 
            string heartrate = $"Heartrate: {rate}bpm";
            connector.DrawText(uuid, heartrate, 300, 80);
        }
        public void DrawHeartRate2(double rate)
        {
            string heartrate = $"{rate}";
            connector.DrawText(uuid, "puls", 620, 260);
            connector.DrawText(uuid, heartrate, 620, 300);
        }

        public void DrawSpeed(double sp)
        {
            string speed = $"Speed: {sp}km/H";
            connector.DrawText(uuid, speed, 300, 120);
        }
        public void DrawSpeed2(double sp)
        {
            string speed = $"{sp}";

            connector.DrawText(uuid, "km/H", 450, 40);
            connector.DrawText(uuid, speed, 450, 80);
        }

        public void DrawDistance(double dist)
        {
            string distance = $"Distance: {dist}km";
            connector.DrawText(uuid, distance, 300, 160);
        }

        public void DrawDistance2(double dist)
        {
            string distance = $"{dist}";
            connector.DrawText(uuid, "km", 620, 80);
            connector.DrawText(uuid, distance, 620, 120);
        }

        public void DrawRoundMin(double round)
        {
            string roundmin = $"RoundMin: {round}R/M";
            connector.DrawText(uuid, roundmin, 300, 200);
        }

        public void DrawRoundMin2(double round)
        {
            string roundmin = $"{round}";
            connector.DrawText(uuid, "RPM", 300, 80);
            connector.DrawText(uuid, roundmin, 300, 120);
        }

        public void DrawResistance(double res)
        {
            string resistance = $"Resistance: {res}Watt";
            connector.DrawText(uuid, resistance, 300, 240);
        }

        public void DrawResistance2(double res)
        {
            string resistance = $"{res}";
            connector.DrawText(uuid, "Power", 300, 260);
            connector.DrawText(uuid, resistance, 300, 300);
        }

        public void DrawEnergy(double en)
        {
            string energy = $"Energy: {en}K/J";
            connector.DrawText(uuid, energy, 300, 280);
        }

        public void DrawEnergy2(double en)
        {
            string energy = $"{en}";
            connector.DrawText(uuid, "Energy", 450, 300);
            connector.DrawText(uuid, energy, 450, 340);
        }

        public void DrawTime(String ti)
        {
            string time = $"Time: {ti}";
            connector.DrawText(uuid, time, 300, 40);
        }
        public void DrawTime2(String ti)
        {
            string time = $"{ti}";
            connector.DrawText(uuid, time, 450, 190);
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

            connector.SetClearColor(uuid);
            connector.SwapText(uuid);
        }

        public void Update2(double rate, double sp, double dist, double round, double res, double en, string ti, double wat)
        {
            connector.ClearPanel(uuid);

            DrawHeartRate2(rate);
            DrawDistance2(sp);
            DrawSpeed2(dist);
            DrawRoundMin2(round);
            DrawResistance2(res);
            DrawEnergy2(en);
            DrawTime2(ti);

            connector.SetClearColor(uuid);
            connector.SwapText(uuid);
        }

        public void SetText(String text) {
            connector.ClearPanel(uuidMessage);

            DrawMessage(text);
            StartTimer();
            connector.SetClearColor(uuidMessage);
            connector.SwapText(uuidMessage);
        }

        public void StartTimer() {

            Timer timer1 = new Timer(5000);
            timer1.Elapsed += new ElapsedEventHandler(ClearMassage);
            timer1.Start();
        }

        public void ClearMassage(Object sender, ElapsedEventArgs e)
        {
            connector.ClearPanel(uuidMessage);
            connector.SetClearColor(uuidMessage);
            connector.SwapText(uuidMessage);
        }  
    }
}
