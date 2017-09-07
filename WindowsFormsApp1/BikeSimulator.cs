using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Remote_Healtcare_Console {
    class BikeSimulator : Kettler {

        public BikeSimulator(Console console) : base(console) {
            this.console = console;
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
            throw new NotImplementedException();
        }

        public override void SetDistance(int distance) {
            throw new NotImplementedException();
        }

        public override void Start() {
            throw new NotImplementedException();
        }

        public override void Stop() {
            throw new NotImplementedException();
        }

        public override void SetManual() {
            throw new NotImplementedException();
        }
    }
}
