using System;
using System.IO.Ports;

namespace Simulator
{
    public class SerialCommunicator
    {
        private SerialPort serialPort;
        public SerialCommunicator(String port)
        {
            serialPort = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);
        }

        public void OpenConnection()
        {
            serialPort.Open();
        }

        public void CloseConnection()
        {
            serialPort.Close();
        }

        public void Reset()
        {
            serialPort.WriteLine("RS");
        }

        public void Status(out string data)
        {
            serialPort.WriteLine("ST");
            data = serialPort.ReadLine().Replace('\t', ' ');
        }

        public void SetTime(int mm, int ss)
        {
            string time = (mm.ToString() + ss.ToString());
            serialPort.WriteLine(time);
        }

        public void SetResistance(int resistance)
        {
            int trueResistance;
            if(resistance > 400)
            {
                trueResistance = 400;
            }
            else if(resistance < 25)
            {
                trueResistance = 25;
            }
            else
            {
                trueResistance = resistance;
            }
            serialPort.WriteLine("PW " + trueResistance);
        }

        public void SetDistance(int distance)
        {
            int trueDistance;
            if(distance > 999)
            {
                trueDistance = 999;
            }
            else if(distance < 0)
            {
                trueDistance = 0;
            }
            else
            {
                trueDistance = distance;
            }
            serialPort.WriteLine("PD " + trueDistance);
        }

        public void SetManual()
        {
            serialPort.WriteLine("CM");
        }

        public void SetCountForward()
        {
            serialPort.WriteLine("CM");
        }

        public void SetCountDownwards()
        {
            serialPort.WriteLine("CD");
        }

        public bool IsConnected()
        {
            return serialPort.IsOpen;
        }
    }
}