using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    public class Bike : Kettler
    {
        //private Client client;
        private SerialCommunicator serialCommunicator;
        private bool start;

        public Bike(string port)
        {
            start = false;
            RecordedData = new HashSet<BikeData>();
            serialCommunicator = new SerialCommunicator(port);
        }

        public void Start()
        {
            start = true;
            serialCommunicator.OpenConnection();
            serialCommunicator.Reset();
            serialCommunicator.SetManual();
            Run();
        }

        public void Stop()
        {
            start = false;
            serialCommunicator.CloseConnection();
        }

        public override void Reset()
        {
            serialCommunicator.Reset();
            RecordedData.Clear();
        }

        public override void Run()
        {
            while(serialCommunicator.isConnection() && start)
            {
                Update();
                System.Threading.Thread.Sleep(750);
            }
        }

        public override void SetAscending()
        {
            serialCommunicator.SetCountForward();
        }

        public override void SetDescending()
        {
            serialCommunicator.SetCountDownwards();
        }

        public override void SetResistance(int resistance)
        {
            serialCommunicator.SetResistance(resistance);
        }

        public override void SetTime(int mm, int ss)
        {
            serialCommunicator.SetTime(mm, ss);
        }

        public override void Update()
        {
            string data;
            serialCommunicator.Status(out data);
            string[] dataSplitted = data.Split(' ');
            RecordedData.Add(new BikeData(int.Parse(dataSplitted[0]), int.Parse(dataSplitted[1]), dataSplitted[2], int.Parse(dataSplitted[3]),
                int.Parse(dataSplitted[4]), int.Parse(dataSplitted[5]), dataSplitted[6], int.Parse(dataSplitted[7])));
            Console.WriteLine(GetLatestBikeData().ToString()); 
        }

        private BikeData GetLatestBikeData()
        {
            return RecordedData.Last();
        }
    }
}
