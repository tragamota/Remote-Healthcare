using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Remote_Healtcare_Console;

namespace Remote_Healtcare_Console {
    class Bike : Kettler {
        private bool start;
        private SerialCommunicator serialCommunicator;
        private Connector connector;
        private Client client;
        private Thread BikeThread;
        private bool autoCalculateResistance;
        private bool autoCalculateResistanceNotExactly;

        public Bike(string port, Console console, Client client) : base(console) {
            this.client = client;
            start = false;
            serialCommunicator = new SerialCommunicator(port);
            BikeThread = new Thread(InitBike);
        }

        public override void Start() {
            start = true;
            serialCommunicator.OpenConnection();
            BikeThread.Start();
        }

        public override void Stop() {
            start = false;
            serialCommunicator.CloseConnection();
        }

        private void InitBike()
        {
            Thread.Sleep(500);
            Reset();
            Thread.Sleep(500);
            SetManual();
            Thread.Sleep(500);
            Run();
        }

        private void Run() {
            while (serialCommunicator.IsConnected() && start) {
                Update();
                if (autoCalculateResistance == true)
                {
                    string objectName = "bike"; //name of bike object in our simulator.
                    SetResistance((int)connector.CalculateIncline(objectName));
                } else
                if(autoCalculateResistanceNotExactly == true)
                {
                    string objectName = "bike"; //name of bike object in our simulator.
                    SetResistance((int)connector.CalculateInclineNotExactly(objectName));
                }
                Thread.Sleep(500);
            }
        }

        public override void Reset() {
            serialCommunicator.SendMessage("RS");
            RecordedData.Clear();
        }

        public override void SetManual() {
            serialCommunicator.SendMessage("CM");
            if (serialCommunicator.ReadInput() != "RUN") {
                Thread.Sleep(500);
                serialCommunicator.ReadInput();
            }
        }

        public override void SetResistance(int resistance) {
            int trueResistance;
            if (resistance > 400) {
                trueResistance = 400;
            }
            else if (resistance < 25) {
                trueResistance = 25;
            }
            else {
                trueResistance = resistance;
            }
            serialCommunicator.SendMessage("PW " + trueResistance);
            serialCommunicator.ReadInput();
        }

        public override void SetTime(int mm, int ss) {
            string time = (mm.ToString() + ss.ToString());
            serialCommunicator.SendMessage("PT " + time);
            serialCommunicator.ReadInput();
        }

        public override void SetDistance(int distance) {
            int trueDistance;
            if (distance > 999) {
                trueDistance = 999;
            }
            else if (distance < 0) {
                trueDistance = 0;
            }
            else {
                trueDistance = distance;
            }
            serialCommunicator.SendMessage("PD " + trueDistance);
            serialCommunicator.ReadInput();
        }

        public override void Update() {
            serialCommunicator.SendMessage("ST");
            string data = serialCommunicator.ReadInput();
            data = data.Replace("\r", "");
            string[] dataSplitted = data.Split('\t');

            BikeData bikeData = new BikeData(
                int.Parse(dataSplitted[0]), int.Parse(dataSplitted[1]),
                dataSplitted[2],
                int.Parse(dataSplitted[3]), int.Parse(dataSplitted[4]), int.Parse(dataSplitted[5]),
                dataSplitted[6],
                int.Parse(dataSplitted[7]));

            if (RecordedData.Count == 0) {
                RecordedData.Add(bikeData);
            }
            else if(RecordedData.Last().Time != bikeData.Time) {
                RecordedData.Add(bikeData);
            }


            client.SendMessage(bikeData);

            SetDataToGUI();
        }
    }
}
