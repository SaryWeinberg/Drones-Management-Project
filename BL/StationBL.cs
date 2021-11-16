using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class StationBL
    {
        private int id;
        private int name;
        public int ID {
            get { return id; }
            set
            {
                if (value != null)
                    id = value;
                else throw new InvalidID();
            }
        }
        public int Name {
            get { return name; }
            set
            {
                if (value != null)
                    name = value;
                else throw new InvalidName();
            }
        }
        public Location Location { get; set; }
        public double AveChargeSlots { get; set; }

        public List<DroneInCharge> DronesInChargelist = new List<DroneInCharge>();
    }
}
