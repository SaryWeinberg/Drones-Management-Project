using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class Customer
    {
        private ulong id;
        public ulong ID
        {
            get { return id; }
            set
            {
                //if (value > 100000000 && value < 999999999 && validateIDNumber(id))
                    id = value;
                //else
                //throw new NotValidID("Id not in the right lenght");
            }
        }
       
        public string Name { get; set; }
        public double PhoneNum { get; set; }
        public Location location { get; set; }

        public List<DeliveryToCustomer> DeliveryToCustomer = new List<DeliveryToCustomer>();

        public List<DeliveryToCustomer> DeliveryFromCustomer = new List<DeliveryToCustomer>();
    }
}

