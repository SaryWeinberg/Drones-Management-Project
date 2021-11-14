using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class DroneBL
    {
        private int pid;
        public int id
        {
            get { return pid; }
            set
            {
                if (value != null)
                    pid = value;
                else 
                    throw new InvalidID();
            }
        }
        public string model { get; set; }
        public WeightCategories maxWeight { get; set; }
        public double batteryStatus { get; set; }
        public DroneStatus status { get; set; }
        public ParcelByDelivery parcel { get; set; }
        public Location location { get; set; }
    }
}

