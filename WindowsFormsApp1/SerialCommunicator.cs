using System;
using System.IO.Ports;

namespace Remote_Healtcare_Console {
    class SerialCommunicator {
        private SerialPort serialPort;

        public SerialCommunicator(String port) {
            serialPort = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);
        }

        public void OpenConnection() {
            if(!IsConnected())
                serialPort.Open();
        }

        public void CloseConnection() {
            serialPort.Close();
        }

        public void SendMessage(string message) {
            serialPort.WriteLine(message);
        }

        public string ReadInput() {
            return serialPort.ReadLine();
        }

        public bool IsConnected() {
            return serialPort.IsOpen;
        }

        public string UsingComPort() {
            return serialPort.PortName;
        }
    }
}