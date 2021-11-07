using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class Customer
    {
        DAL.DO.Customer
        public Customer()
        {
        cutomer = new IDAL.DO.Customer(); 
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public double PhoneNum { get; set; }
        public Location location { get; set; }

        public List<DeliveryToCustomer> DeliveryToCustomer = new List<DeliveryToCustomer>();
        public List<DeliveryToCustomer> DeliveryFromCustomer = new List<DeliveryToCustomer>();


    }
}
