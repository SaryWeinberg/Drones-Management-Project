using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class CustomerToList
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PhoneNum { get; set; }
        public int ParcelSentAndDelivered { get; set; }
        public int ParcelSentsentButNotDelivered { get; set; }
        public int ParcelReceived { get; set; }
        public int ParcelOnTheWayToCustomer { get; set; }

    }
}
