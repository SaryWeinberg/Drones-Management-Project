using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneInParcel
    {
        private int id;
        private double battery;

        public int ID {
            get { return id; }
            set
            {
                if (value >= 0)
                    id = value;
                else
                    throw new InvalidObjException("ID");
            }
        }

        public double Battery {
            get { return battery; }
            set
            {
                if (value >= 0)
                    battery = value;
                else
                    throw new InvalidObjException("batteryStatus");
            }
        }

        public Location Location { get; set; }

        public override string ToString()
        {
            return "Drone: id: " + ID + " Bettary:  " + Battery + " Location: " + Location;
        }
    }
}
