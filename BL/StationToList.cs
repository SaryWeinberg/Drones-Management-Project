using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class StationToList
    {
        public int ID { get; set; }
        public int Name { get; set; }
        public Location location { get; set; }
        public double AveChargeSlots { get; set; }

        public List<DroneInCharge> DronesInCharge = new List<DroneInCharge>();
    }
}
