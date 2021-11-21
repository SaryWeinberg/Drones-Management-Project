using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Location
    {

        private int longitude;
        private int latitude;

        public int Longitude {
            get { return longitude; }
            set
            {
                if (value >= 0 && value < 41)
                    longitude = value;
                else
                    throw new InvalidObjException($"longitude {value}");
            }
        }

        public int Latitude {
            get { return latitude; }
            set
            {
                if (value >= 0 && value < 41)
                    latitude = value;
                else
                    throw new InvalidObjException($"latitude {value}");
            }
        }

        public override string ToString()
        {
            return " Longitude: " + Longitude + " Latitude: " + Latitude + " ";
        }
    }
}
