using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneBL
    {
        private int id;
        private double batteryStatus;
        private WeightCategories maxWeight;
        public int ID {
            get { return id; }
            set
            {
                if (value != null)
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
                if (value > 0)
                    maxWeight = value;
                else
                    throw new InvalidObjException("maxWeight");
            }
        }

        public double BatteryStatus {
            get { return batteryStatus; }
            set
            {
                if (value > 0)
                    batteryStatus = value;
                else
                    throw new InvalidObjException("maxWeight");
            }
        }

        public DroneStatus Status { get; set; }
        public ParcelByDelivery Parcel { get; set; }
        public Location Location { get; set; }

        public override string ToString()
        {
            return "Drone: id: " + ID + " model:  " + Model + " MaxWeight: " + MaxWeight + " BatteryStatus: " + BatteryStatus + " Status: " + Status + " Parcel: " + Parcel + " Location: " + Location;
        }
    }
}

