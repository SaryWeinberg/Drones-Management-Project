using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Location
    {
        public int Longitude { get; set; }
        public int Latitude { get; set; }



        public override string ToString()
        {
            return " Longitude: " + Longitude + " Latitude: " + Latitude + " " ;
        } }
}
