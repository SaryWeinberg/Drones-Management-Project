using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelsAtTheCustomer
    {
        public int id { get; set; }
        public WeightCategories weight { get; set; }
        public Priorities property { get; set; }
        public Status status { get; set; }
        public CustomerInParcel target { get; set; }
    }
}
