using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Remote_Healtcare_Console {
    class BikeSimulator : Kettler {

        private Thread BikeThread;
        private ISet<BikeData> data;
        private int count;

        public BikeSimulator(Console console) : base(console) {
            this.console = console;
            data = console.data;
            count = 0;
            BikeThread = new Thread(Update);
        }

        public override void Reset() {
            throw new NotImplementedException();
        }

        public override void SetResistance(int power) {
            throw new NotImplementedException();
        }

        public override void SetTime(int mm, int ss) {
            throw new NotImplementedException();
        }

        public override void Update() {
            while (count != data.Count) {
                try {
                    console.Invoke((MethodInvoker)delegate {
                        // Running on the UI thread
                        console.SetPulse(data.ElementAt(count).Pulse.ToString());
                        console.SetRoundMin(data.ElementAt(count).Rpm.ToString());
                        console.SetSpeed(data.ElementAt(count).Speed.ToString());
                        console.SetDistance((data.ElementAt(count).Distance * 100).ToString());
                        console.SetResistance(data.ElementAt(count).Resistance.ToString());
                        console.SetEnergy(data.ElementAt(count).Energy.ToString());
                        console.SetTime(((data.ElementAt(count).Time < TimeSpan.Zero) ? "-" : "") + data.ElementAt(count).Time.ToString(@"mm\:ss"));
                        console.SetWatt(data.ElementAt(count).Power.ToString());
                    });
                }
                catch (System.InvalidOperationException e) {
                    System.Console.WriteLine(e.StackTrace);
                }
                catch (System.ComponentModel.InvalidAsynchronousStateException e) {
                    System.Console.WriteLine(e.StackTrace);
                }
                count++;
                System.Threading.Thread.Sleep(1000);
            }
        }

        public override void SetDistance(int distance) {
            throw new NotImplementedException();
        }

        public override void Start() {
            BikeThread.Start();
        }

        public override void Stop() {
            BikeThread.Abort();
        }

        public override void SetManual() {
            throw new NotImplementedException();
        }
    }
}
