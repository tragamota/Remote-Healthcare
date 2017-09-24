using System;
using System.IO;
using System.IO.Ports;

namespace Remote_Healtcare_Console {
    class SerialCommunicator {
        private SerialPort serialPort;

        public SerialCommunicator(String port) {
            try {
                serialPort = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);
            }
            catch(IOException e) {
                System.Console.WriteLine(e.StackTrace);
            }
        }

        public void OpenConnection() {
            if (!IsConnected()) {
                try {
                    serialPort.Open();
                }
                catch (IOException e) {
                    System.Console.WriteLine(e.StackTrace);
                }
            }
        }

        public void CloseConnection() {
            try {
                serialPort.Close();
            }
            catch (IOException e) {
                System.Console.WriteLine(e.StackTrace);
            }
        }

        public void SendMessage(string message) {
            try {
                serialPort.WriteLine(message);
            }
            catch(Exception e) {
                System.Console.WriteLine(e.StackTrace);
            }
        }

        public string ReadInput() {
            try {
                return serialPort.ReadLine();
            }
            catch(TimeoutException e) {
                System.Console.WriteLine(e.StackTrace);
                return "";
            }
        }

        public bool IsConnected() {
            return serialPort.IsOpen;
        }

        public string UsingComPort() {
            return serialPort.PortName;
        }
    }
}