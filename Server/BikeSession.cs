using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserData;

namespace Server {
    public class BikeSession {
        private DateTime sessionDateTime;
        private string userHash;
        public List<BikeData> data { get; set; }
        public List<BikeData> notSendData { get; set; }

        public BikeSession(string userHash) {
            this.userHash = userHash;
            sessionDateTime = DateTime.UtcNow;
            data = new List<BikeData>();
            notSendData = new List<BikeData>();
        }

        public void SaveSessionToFile() {
            string pathToUserDir = Directory.GetCurrentDirectory() + @"\ClientData\" + userHash + @"\";
            string pathToSessionFile = Path.Combine(pathToUserDir, sessionDateTime.ToString().Replace(":", "-") + ".json");
            if(!Directory.Exists(pathToUserDir)) {
                Directory.CreateDirectory(pathToUserDir);
            }
            else {
                File.Create(pathToSessionFile);
            }

            try {
                File.WriteAllText(pathToSessionFile ,JsonConvert.SerializeObject(data, Formatting.Indented));
            }
            catch(IOException e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void AddBikeData(BikeData bikeData)
        {
              data.Add(bikeData);
        }

    public BikeData GetLatestBikeData()
        {
            return data.Last();
        }
    }
}
