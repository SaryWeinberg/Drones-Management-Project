using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneBL
    {
        private int id;
        public int ID {
            get { return id; }
            set
            {
                if (value != null)
                    id = value;
                else
                    throw new InvalidID();
            }
        }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public double BatteryStatus { get; set; }
        public DroneStatus Status { get; set; }
        public ParcelByDelivery Parcel { get; set; }
        public Location Location { get; set; }
    }
}

