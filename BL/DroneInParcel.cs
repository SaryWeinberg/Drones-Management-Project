using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneInParcel
    {
        public int ID { get; set; }
        public double BettaryStatus { get; set; }
        public Location Location { get; set; }

        public override string ToString()
        {
            return "Drone: id: " + ID + " BettaryStatus:  " + BettaryStatus + " Location: " + Location;
        }
    }
}
