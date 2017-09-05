using System;
using System.IO.Ports;

namespace Simulator
{
    class SerialCommunicator
    {
        private SerialPort SerialPort;
        public SerialCommunicator(String port)
        {
            SerialPort = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);
            SerialPort.Open();
        }


    }
}