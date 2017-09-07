using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Remote_Healtcare_Console
{
    class BikeSimulator : Kettler
    {

        private Console console;

        public BikeSimulator(Console console)
        {
            this.console = console;
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }

        public override void SetAscending()
        {
            throw new NotImplementedException();
        }

        public override void SetDescending()
        {
            throw new NotImplementedException();
        }

        public override void SetResistance(int power)
        {
            throw new NotImplementedException();
        }

        public override void SetTime(int mm, int ss)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void SetDistance(int distance)
        {
            throw new NotImplementedException();
        }

        public override void SetDataToGUI()
        {
            BikeData bikeData = GetLatestBikeData();

            try
            {
                console.Invoke((MethodInvoker)delegate {
                    // Running on the UI thread
                    console.SetPulse(bikeData.Pulse.ToString());
                    console.SetRoundMin(bikeData.Rpm.ToString());
                    console.SetSpeed(bikeData.Speed.ToString());
                    console.SetDistance(bikeData.Distance.ToString());
                    console.SetResistance(bikeData.Resistance.ToString());
                    console.SetEnergy(bikeData.Energy.ToString());
                    console.SetTime(((bikeData.Time < TimeSpan.Zero) ? "-" : "") + bikeData.Time.ToString(@"mm\:ss"));
                    console.SetWatt(bikeData.Power.ToString());
                });
            }
            catch (System.InvalidOperationException) { }
            catch (System.ComponentModel.InvalidAsynchronousStateException) { }
        }

        public override BikeData GetLatestBikeData()
        {
            throw new NotImplementedException();
        }
    }
}
