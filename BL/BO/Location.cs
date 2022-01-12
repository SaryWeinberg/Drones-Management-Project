using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Location
    {

        private int longitude;
        private int latitude;

        public int Longitude {
            get { return longitude; }
            set
            {
                if (value >= 0 && value < 360)
                    longitude = value;
                else
                    throw new InvalidObjException("longitude");
            }
        }

        public int Latitude {
            get { return latitude; }
            set
            {
                if (value >= 0 && value < 90)
                    latitude = value;
                else
                    throw new InvalidObjException("latitude");
            }
        }

        public override string ToString()
        {
            return " Longitude: " + Longitude + " Latitude: " + Latitude + " ";
        }
    }
}
