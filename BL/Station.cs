using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class Station
    {
        private int pid;
        private int pname;
        public int id
        {
            get { return pid; }
            set
            {
                if (value != null)
                    pid = value;
                else throw new InvalidID();
            }
        }
        public int name
        {
            get { return pname; }
            set
            {
                if (value != null)
                    pname = value;
                else throw new InvalidName();
            }
        }
        public Location location { get; set; }
        public double aveChargeSlots { get; set; }

        public List<DroneInCharge> dronesInChargelist = new List<DroneInCharge>();
    }
}
