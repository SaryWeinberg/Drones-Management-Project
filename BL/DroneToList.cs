using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class DroneToList
    {
        public int id { get; set; }
        public string model { get; set; }
        public WeightCategories maxWeight { get; set; }
        public double batteryStatus { get; set; }
        public DroneStatus status { get; set; }
        public Location location { get; set; }
        public ParcelByDelivery parcel { get; set; }
    }
}
