using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class CustomerToList
    {
        public ulong ID { get; set; }
        public string Name { get; set; }
        public ulong PhoneNum { get; set; }
        public Location location { get; set; }

        public List<DeliveryToCustomer> DeliveryToCustomer = new List<DeliveryToCustomer>();

        public List<DeliveryToCustomer> DeliveryFromCustomer = new List<DeliveryToCustomer>();
    }
}
