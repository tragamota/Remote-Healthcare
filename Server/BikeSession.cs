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
        public List<BikeData> Data { get; set; }
        public List<BikeData> LatestData { get; set; }

        public BikeSession(string userHash) {
            this.userHash = userHash;
            sessionDateTime = DateTime.Now;
            Data = new List<BikeData>();
            LatestData = new List<BikeData>();
        }

        public void SaveSessionToFile() {
            string pathToUserDir = Directory.GetCurrentDirectory() + @"\Data\" + userHash + @"\";
            string pathToSessionFile = pathToUserDir + sessionDateTime.Day + "-" + sessionDateTime.Month + "-" + sessionDateTime.Year + "  " +  sessionDateTime.Hour + "." + sessionDateTime.Minute + "." + sessionDateTime.Second + ".json";
            if (!Directory.Exists(pathToUserDir)) {
                Directory.CreateDirectory(pathToUserDir);
            }

            try {
                File.WriteAllText(pathToSessionFile, JsonConvert.SerializeObject(Data));
            }
            catch (IOException e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}
