using Newtonsoft.Json;
using Remote_Healtcare_Console;
using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ClientServer
{
    class Client
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient("127.0.0.1", 1330);
            bool done = false;
            System.Console.WriteLine("Type 'bye' to end connection");
            while (!done)
            {
                System.Console.Write("Enter a message to send to server: ");
                string message = System.Console.ReadLine();

                SendMessage(client, message);

                string response = ReadMessage(client);
                System.Console.WriteLine("Response: " + response);
                done = response.Equals("BYE");
            }
        }

        public static string ReadMessage(TcpClient client)
        {
            byte[] buffer = new byte[256];
            int totalRead = 0;
            
            do
            {
                int read = client.GetStream().Read(buffer, totalRead, buffer.Length - totalRead);
                totalRead += read;
                System.Console.WriteLine("ReadMessage: " + read);
            } while (client.GetStream().DataAvailable);

            return Encoding.UTF8.GetString(buffer, 0, totalRead);
        }

        public static void SendMessage(TcpClient client, string message)
        {
            NetworkStream stream = client.GetStream();
            byte[] bytes;
            if (message == "hoi")
            {
                string json = JsonConvert.SerializeObject(new BikeData());

                byte[] prefixArray = BitConverter.GetBytes(json.Length);
                byte[] requestArray = Encoding.Default.GetBytes(json);

                byte[] buffer = new Byte[prefixArray.Length + json.Length];
                prefixArray.CopyTo(buffer, 0);
                requestArray.CopyTo(buffer, prefixArray.Length);
                stream.Write(buffer, 0, buffer.Length);
            }
            else {
                bytes = Encoding.UTF8.GetBytes(message);
                stream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
