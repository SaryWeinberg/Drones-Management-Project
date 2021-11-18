using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelsAtTheCustomer
    {
        public int ID { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Property { get; set; }
        public Status Status { get; set; }
        public CustomerInParcel Target { get; set; }
    }
}
