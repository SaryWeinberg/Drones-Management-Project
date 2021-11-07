using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class CustomerToList
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double PhoneNum { get; set; }
        public int ProvidedParcels { get; set; }
        public int NotProvidedParcels { get; set; }
        public int recievedParcels { get; set; }
        public int OnTheWayParcels { get; set; }
    }
}
