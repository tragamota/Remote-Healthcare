using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Remote_Healtcare_Console {
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

    [Serializable]
    public struct BikeData {
        public int Pulse { get; set; }
        public int Rpm { get; set; }
        public int Distance { get; set; }
        public int Resistance { get; set; }
        public int Power { get; set; }
        public int Energy { get; set; }
        public TimeSpan Time { get; set; }
        public double Speed { get; set; }

        public BikeData(int pulse, int rpm, string speed, int distance, int resistance, int energy, string time, int power) {
            Pulse = pulse;
            Rpm = rpm;
            Distance = distance;
            Resistance = resistance;
            Power = power;
            Energy = energy;
            string[] timeSplitted = time.Split(':');
            Time = new TimeSpan(0, int.Parse(timeSplitted[0]), int.Parse(timeSplitted[1]));
            Speed = (double.Parse(speed) / 10);
        }

        public override string ToString() {
            return $"{Pulse}-{Rpm}-{Speed}-{Distance}-{Resistance}-{Energy}-{Time.Minutes + ":" + Time.Seconds}-{Power}";
        }
    }
}