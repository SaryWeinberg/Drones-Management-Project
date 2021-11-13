using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class ParcelByDelivery
    {
        public int id { get; set; }
        public bool deliveryStatus { get; set; }
        public double latitude { get; set; }
        public WeightCategories weight { get; set; }
        public Priorities property  { get; set; }
        public CustomerInParcel sender { get; set; }
        public CustomerInParcel target { get; set; }
        public Location pickUpLocation { get; set; }
        public Location targetLocation { get; set; }
        public double distance { get; set; }

    }
}
