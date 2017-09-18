﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;
using Remote_Healtcare_Console;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ClientServer
{
    class Server
    {
        public static object ClientServerUtil { get; private set; }

        static void Main(string[] args)
        {
            IPAddress localhost;

            bool ipIsOk = IPAddress.TryParse("127.0.0.1", out localhost);
            if (!ipIsOk) { System.Console.WriteLine("ip adres kan niet geparsed worden."); Environment.Exit(1); }

            TcpListener listener = new System.Net.Sockets.TcpListener(localhost, 1330);
            listener.Start();

            while (true)
            {
                System.Console.WriteLine(@"
                      ==============================================
                        Server started at {0}
                        Waiting for connection
                      =============================================="
                , DateTime.Now);
                
                TcpClient client = listener.AcceptTcpClient();
                
                Thread thread = new Thread(HandleClientThread);
                thread.Start(client);
            }
        }

        static void HandleClientThread(object obj)
        {
            List<BikeData> data = new List<BikeData>();

            TcpClient client = obj as TcpClient;

            bool done = false;
            while (!done)
            {
                JObject received = ReadMessage(client);
                data.Add((BikeData)received.ToObject(typeof(BikeData)));
                System.Console.WriteLine("Received: {0}", received);
                done = received.Equals("bye");
                if (done) SendMessage(client, "BYE");
                else SendMessage(client, "OK");

            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog.Filter = "JSON (.json)|*.json;";
            saveFileDialog.FileName = "sessie.json";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
            }

            client.Close();
            System.Console.WriteLine("Connection closed");
        }

        public static JObject ReadMessage(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            StringBuilder message = new StringBuilder();
            int numberOfBytesRead = 0;
            byte[] messageBytes = new byte[4];
            stream.Read(messageBytes, 0, messageBytes.Length);
            byte[] receiveBuffer = new byte[BitConverter.ToInt32(messageBytes, 0)];

            do
            {
                numberOfBytesRead = stream.Read(receiveBuffer, 0, receiveBuffer.Length);

                message.AppendFormat("{0}", Encoding.ASCII.GetString(receiveBuffer, 0, numberOfBytesRead));

            }
            while (message.Length < receiveBuffer.Length);
            
            string response = message.ToString();
            return JObject.Parse(response);
        }

        public static void SendMessage(TcpClient client, string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            client.GetStream().Write(bytes, 0, bytes.Length);
        }

    }
}

