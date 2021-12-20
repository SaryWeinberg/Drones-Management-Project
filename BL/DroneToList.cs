using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneToList
    {
        public DroneToList(Drone drone, DalApi.IDal dalObj)
        {
            ID = drone.ID;
            Model = drone.Model;
            MaxWeight = drone.MaxWeight;
            BatteryStatus = drone.BatteryStatus;
            Status = drone.Status;
            Location = drone.Location;
            Parcel = drone.Parcel;           
        }

        public int ID { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public double BatteryStatus { get; set; }
        public DroneStatus Status { get; set; }
        public Location Location { get; set; }
        public ParcelByDelivery Parcel { get; set; }

        public override string ToString()
        {
            return "Drone: id: " + ID + " Model: " + Model + " MaxWeight: " + MaxWeight + " Battery Status: " +
                BatteryStatus + " Status: " + Status + " Location: " +
                Location + " Parcel: " + Parcel;
        }
    }
}
