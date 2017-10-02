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

        public BikeSession(string userHash) {
            this.userHash = userHash;
            sessionDateTime = DateTime.UtcNow;
            data = new List<BikeData>();
        }

        public void SaveSessionToFile() {
            string pathToUserDir = Directory.GetCurrentDirectory() + @"\ClientData\" + userHash;
            string pathToSessionFile = pathToUserDir + @"\" + sessionDateTime + ".json";
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
    }
}
