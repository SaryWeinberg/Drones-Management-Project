using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Station
    {
        private int id;
        private int name;
        private double aveChargeSlots;
        public int ID {
            get { return id; }
            set
            {
                if (value >= 0)
                    id = value;
                else throw new InvalidObjException("ID");
            }
        }

        public int Name {
            get { return name; }
            set
            {
                if (value >= 0)
                    name = value;
                else throw new InvalidObjException("name");
            }
        }

        public Location Location { get; set; }
        public double AveChargeSlots {
            get { return aveChargeSlots; }
            set
            {
                if (value > 0 )
                    aveChargeSlots = value;
                else throw new InvalidObjException("available charge slots");
            }
        }

        public List<DroneInCharge> DronesInChargelist { get; set; }

        public override string ToString()
        {
            return "Station:id:  " + ID + " Name: " + Name + " Location: " + Location + " Avelable ChargeSlots: " + AveChargeSlots;
        }
    }
}
