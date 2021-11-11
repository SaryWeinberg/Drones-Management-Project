using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class Parcel
    {
        private int id;
        public int ID
        {
            get { return id; }
            set
            {            
                if (value != null)
                    id = value;
                else throw new InvalidID();
            }
        }
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
