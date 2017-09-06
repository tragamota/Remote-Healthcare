using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Remote_Healtcare_Console
{
    abstract class Kettler
    {
        protected ISet<BikeData> RecordedData;
        public abstract void Run();
        public abstract void Update();
        public abstract void SetResistance(int power);
        public abstract void SetAscending();
        public abstract void SetDescending();
        public abstract void SetDistance(int distance);
        public abstract void Reset();
        public abstract void SetTime(int mm, int ss);
    }

    

    struct BikeData
    {
        public int Pulse { get; }
        public int Rpm { get; }
        public int Distance { get; }
        public int Resistance { get; }
        public int Power { get; }
        public int Energy { get; }
        public TimeSpan Time { get; }
        public double Speed { get; }

        public BikeData(int pulse, int rpm, string speed, int distance, int resistance, int energy, string time, int power)
        {
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

        public override string ToString()
        {
             return $"{Pulse}-{Rpm}-{Speed}-{Distance}-{Resistance}-{Energy}-{Time.Minutes + ":" + Time.Seconds}-{Power}";
        }
    }
}