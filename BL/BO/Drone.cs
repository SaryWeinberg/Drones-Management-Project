using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class Drone
    {

        public event Action<Drone> droneChanged;


        private int id;
        private double battery;
        private WeightCategories maxWeight;
        public int ID {

            get { return id; }
            set
            {
                if (value >= 0)
                    id = value;
                else
                    throw new InvalidObjException("ID");
            }
        }

        public string Model { get; set; }
        public WeightCategories MaxWeight {
            get { return maxWeight; }
            set
            {
                if (value >= 0)
                    maxWeight = value;
                else
                    throw new InvalidObjException("max weight");
            }
        }

        public double Battery {
            get { return battery; }
            set
            {
                if (value >= 0)
                    battery = value;
                else
                    throw new InvalidObjException("battery");
            }
        }

        public DroneStatus Status { get; set; }
        public ParcelByDelivery Parcel { get; set; }
        public Location Location { get; set; }

        public override string ToString()
        {
            return "Drone: id: " + ID + " model:  " + Model + " MaxWeight: " + MaxWeight + " BatteryStatus: " + Battery + " Status: " + Status + " Parcel: " + Parcel + " Location: " + Location;
        }
    }
}

