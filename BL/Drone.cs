using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class Drone
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
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public double BatteryStatus { get; set; }
        public DroneStatus Status { get; set; }
        public ParcelByDelivery Parcel { get; set; }
        public Location location { get; set; }
    }
}

