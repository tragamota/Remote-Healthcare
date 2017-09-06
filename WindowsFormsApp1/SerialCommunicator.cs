using System;
using System.IO.Ports;

namespace Remote_Healtcare_Console
{
    class SerialCommunicator
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

        public String readLine()
        {
            return serialPort.ReadLine();
        }

        public string Status(Bike bike)
        {
            if(bike.getRecordedDataSize() < 1)
            {
                System.Console.WriteLine(serialPort.ReadLine());
            }
            serialPort.WriteLine("ST");
            string data = serialPort.ReadLine().Replace('\t', ' ');
            return data;
        }

        public void SetTime(int mm, int ss)
        {
            string time = (mm.ToString() + ss.ToString());
            serialPort.WriteLine("PT " + time);
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
            clearBuffer();
            System.Threading.Thread.Sleep(1000);
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

        public void clearBuffer()
        {
            serialPort.DiscardInBuffer();
        }
    }
}