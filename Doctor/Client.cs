﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UserData;

namespace Doctor
{
    public class Client {
        private TcpClient client;
        private NetworkStream stream;

        public Client() {
            try {
                client = new TcpClient("localhost", 1337);
                stream = client.GetStream();
            }
            catch (SocketException e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        public string ReadMessage() {
            StringBuilder message = new StringBuilder();

            int numberOfBytesRead = 0;
            byte[] messageBytes = new byte[4];
            byte[] receiveBuffer;

            try {
                stream.Read(messageBytes, 0, messageBytes.Length);
                receiveBuffer = new byte[BitConverter.ToInt32(messageBytes, 0)];
                do {
                    numberOfBytesRead = stream.Read(receiveBuffer, 0, receiveBuffer.Length);

                    message.AppendFormat("{0}", Encoding.ASCII.GetString(receiveBuffer, 0, numberOfBytesRead));

                }
                while (message.Length < receiveBuffer.Length);
            }
            catch (Exception e) {
                System.Console.WriteLine(e.StackTrace);
                return null;
            }

            return message.ToString();
        }

        public void SendMessage(dynamic message) {
            string json = null;

            if (message is string) {
                json = message;
            }
            else {
                json = JsonConvert.SerializeObject(message);
            }

            try {
                byte[] buffer;
                byte[] prefixArray = BitConverter.GetBytes(json.Length);
                byte[] requestArray = Encoding.Default.GetBytes(json);

                buffer = new Byte[prefixArray.Length + json.Length];
                prefixArray.CopyTo(buffer, 0);
                requestArray.CopyTo(buffer, prefixArray.Length);
                stream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception e) {
                System.Console.WriteLine(e.StackTrace);
            }
        }

        public void SendMessage(string message)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer;
            byte[] prefixArray = BitConverter.GetBytes(message.Length);
            byte[] requestArray = Encoding.Default.GetBytes(message);

            buffer = new Byte[prefixArray.Length + message.Length];
            prefixArray.CopyTo(buffer, 0);
            requestArray.CopyTo(buffer, prefixArray.Length);
            stream.Write(buffer, 0, buffer.Length);
        }

        public void SendMessage(User user)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer;
            string json = JsonConvert.SerializeObject(user);

            byte[] prefixArray = BitConverter.GetBytes(json.Length);
            byte[] requestArray = Encoding.Default.GetBytes(json);

            buffer = new Byte[prefixArray.Length + json.Length];
            prefixArray.CopyTo(buffer, 0);
            requestArray.CopyTo(buffer, prefixArray.Length);
            stream.Write(buffer, 0, buffer.Length);
        }

        public void SendMessage(BikeData data)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer;
            string json = JsonConvert.SerializeObject(data);

            byte[] prefixArray = BitConverter.GetBytes(json.Length);
            byte[] requestArray = Encoding.Default.GetBytes(json);

            buffer = new Byte[prefixArray.Length + json.Length];
            prefixArray.CopyTo(buffer, 0);
            requestArray.CopyTo(buffer, prefixArray.Length);
            stream.Write(buffer, 0, buffer.Length);
        }
    }
}