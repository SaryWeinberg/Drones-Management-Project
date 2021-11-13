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
        public int parcelSentAndDelivered { get; set; }
        public int parcelSentsentButNotDelivered { get; set; }
        public int parcelReceived { get; set; }
        public int parcelOnTheWayToCustomer { get; set; }

    }
}
