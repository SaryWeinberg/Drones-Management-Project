using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneToList
    {
        public DroneToList(Drone drone)
        {
            ID = drone.ID;
            Model = drone.Model;
            MaxWeight = drone.MaxWeight;
            Battery = Math.Round(drone.Battery);
            Status = drone.Status;
            Location = drone.Location;
            Parcel = drone.Parcel;      
        }

        public int ID { get; set; }
        public string Model {get; set;}
        public WeightCategories MaxWeight {get; set;}
        public double Battery { get; set; }
        public DroneStatus Status { get; set; }
        public Location Location { get; set; }
        public ParcelByDelivery Parcel { get; set; }

        public override string ToString()
        {
            return "Drone: id: " + ID + " Model: " + Model + " MaxWeight: " + MaxWeight + " Battery: " +
                Battery + " Status: " + Status + " Location: " +
                Location + " Parcel: " + Parcel;
        }
    }
}
