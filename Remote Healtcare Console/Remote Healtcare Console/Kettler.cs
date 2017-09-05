using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    abstract class Kettler
    {
        public abstract void Run();
        public abstract void Update();
        public abstract void SetResistance();
        public abstract void SetAscending();
        public abstract void SetDescending();       
    }

    struct BikeData
    {
        private int pulse, rpm, distance, resistance, power, energy;
        private TimeSpan time;
        private double speed;

        public BikeData(int pulse, int rpm, int distance, int resistance, int power, int energy, string time, string speed)
        {
            this.pulse = pulse;
            this.rpm = rpm;
            this.distance = distance;
            this.resistance = resistance;
            this.power = power;
            this.energy = energy;
            string[] timeSplitted = time.Split(':');
            this.time = new TimeSpan(0, int.Parse(timeSplitted[0]), int.Parse(timeSplitted[1]));
            this.speed = double.Parse(speed) / 10;
        }

        
    }
}
