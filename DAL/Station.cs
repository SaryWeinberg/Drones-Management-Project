using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.DO
{
    public struct Station
    {
        public int id { get; set; }
        public int name { get; set; }
        public int longitude { get; set; }
        public int latitude { get; set; }
        public int chargeSlots { get; set; }
        public int active { get; set; }
        public override string ToString()
        {
            return "Station: " + id + " " + name +" " + longitude + " " + latitude + " " + chargeSlots;
        }
    }
}
