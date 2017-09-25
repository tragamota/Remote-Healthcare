using System;

namespace UserData
{
    [Serializable]
    public struct BikeData {
        public string id { get; set; }
        public int Pulse { get; set; }
        public int Rpm { get; set; }
        public int Distance { get; set; }
        public int Resistance { get; set; }
        public int Power { get; set; }
        public int Energy { get; set; }
        public TimeSpan Time { get; set; }
        public double Speed { get; set; }

        public BikeData(int pulse, int rpm, string speed, int distance, int resistance, int energy, string time, int power) {
            id = "BikeData";
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