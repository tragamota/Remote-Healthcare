using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    class Bike : Kettler
    {
        //private Client client;
        private List<BikeData> recordData = new List<BikeData>();
        private SerialCommunicator serialCommunicator;

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

        public override void SetResistance()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
