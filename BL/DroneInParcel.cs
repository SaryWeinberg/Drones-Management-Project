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
        private double batteryStatus;

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

        public double BatteryStatus {
            get { return batteryStatus; }
            set
            {
                if (value >= 0)
                    batteryStatus = value;
                else
                    throw new InvalidObjException("batteryStatus");
            }
        }

        public Location Location { get; set; }

        public override string ToString()
        {
            return "Drone: id: " + ID + " BettaryStatus:  " + BatteryStatus + " Location: " + Location;
        }
    }
}
