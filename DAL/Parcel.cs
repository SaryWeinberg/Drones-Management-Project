using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.DO
{
    public struct Parcel
    {
        public int id { get; set; }
        public ulong senderId { get; set; }
        public ulong targetId { get; set; }
        public WeightCategories weight { get; set; }
        public Priorities priority { get; set; }
        public DateTime requested { get; set; }
        public int droneId { get; set; }
        public DateTime scheduled { get; set; }
        public DateTime pickedUp { get; set; }
        public DateTime delivered { get; set; }
        public int active { get; set; }

        public override string ToString()
        {
            return "Parcel: " + id + " " + senderId + " " + targetId + " " + weight + " " + priority + " " + requested + " " + droneId + " " + scheduled + " " + pickedUp + " " + delivered;
        }
    }


    
}

