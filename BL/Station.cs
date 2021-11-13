using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class Station
    {
        private int id;
        private int name;
        public int ID
        {
            get { return id; }
            set
            {
                if (value != null)
                    id = value;
                else throw new InvalidID();
            }
        }
        public int Name
        {
            get { return name; }
            set
            {
                if (value != null)
                    name = value;
                else throw new InvalidName();
            }
        }
        public Location location { get; set; }
        public double aveChargeSlots { get; set; }

        public List<DroneInCharge> dronesInChargelist = new List<DroneInCharge>();
    }
}
