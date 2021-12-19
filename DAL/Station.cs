using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Station
    {
        public int ID { get; set; }
        public int Name { get; set; }
        public int Longitude { get; set; }
        public int Latitude { get; set; }
        public int ChargeSlots { get; set; }
        public int Active { get; set; }
        public override string ToString()
        {
            return "Station: " + ID + " " + Name +" " + Longitude + " " + Latitude + " " + ChargeSlots;
        }
    }
}
