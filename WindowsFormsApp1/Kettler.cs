using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UserData;

namespace Remote_Healtcare_Console
{
    abstract class Kettler {
        protected Console console;
        protected ISet<BikeData> RecordedData;
        public abstract void Start();
        public abstract void Stop();
        public abstract void Update();
        public abstract void SetManual();
        public abstract void Reset();
        public abstract void SetResistance(int power);
        public abstract void SetDistance(int distance);
        public abstract void SetTime(int mm, int ss);

        public Kettler(Console console) {
            this.console = console;
            RecordedData = new HashSet<BikeData>();
        }

        public void SetDataToGUI() {
            BikeData bikeData = RecordedData.Last();

            try {
                console.Invoke((MethodInvoker)delegate {
                    // Running on the UI thread
                    console.SetPulse(bikeData.Pulse.ToString());
                    console.SetRoundMin(bikeData.Rpm.ToString());
                    console.SetSpeed(bikeData.Speed.ToString());
                    console.SetDistance((bikeData.Distance * 100).ToString());
                    console.SetResistance(bikeData.Resistance.ToString());
                    console.SetEnergy(bikeData.Energy.ToString());
                    console.SetTime(((bikeData.Time < TimeSpan.Zero) ? "-" : "") + bikeData.Time.ToString(@"mm\:ss"));
                    console.SetWatt(bikeData.Power.ToString());
                    console.SetDisplay(bikeData.Pulse, bikeData.Speed, bikeData.Distance, bikeData.Rpm, bikeData.Resistance, bikeData.Energy, bikeData.Time.ToString(), bikeData.Power);
                });
            }
            catch (InvalidOperationException e) {
                System.Console.WriteLine(e.StackTrace);
            }
            catch (InvalidAsynchronousStateException e) {
                System.Console.WriteLine(e.StackTrace);
            }

        }
    }
}