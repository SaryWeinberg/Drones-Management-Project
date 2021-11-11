using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class ParcelToList
    {
        public int ID { get; set; }
        public Customer Sender { get; set; }
        public Customer Target { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public Drone Drone { get; set; }
        public DateTime Created { get; set; }
        public DateTime Associated { get; set; }
        public DateTime PickedUp { get; set; }
        public DateTime Delivered { get; set; }
    }
}
