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
        public int ID
        {
            get { return id; }
            set
            {
                //if ()
                id = value;
                //else throw new NotValidID("the ID already exist");
            }
        }
        public int Name { get; set; }
        public Location location { get; set; }
        public double AveChargeSlots { get; set; }

        public List<DroneInCharge> DronesInCharge = new List<DroneInCharge>();

    }
}
