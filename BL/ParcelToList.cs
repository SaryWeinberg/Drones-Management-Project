using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class ParcelToList
    {


        public ParcelToList()
        {
             
        }

        public int ID { get; set; }
        public Customer Sender { get; set; }
        public Customer Target { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }       
        public Status status { get; set; }
    }
}
