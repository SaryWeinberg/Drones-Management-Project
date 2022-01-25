using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{
    public class ParcelByDelivery
    {
        public int ID { get; set; }
        public bool DeliveryStatus { get; set; }
        public double Latitude { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Property { get; set; }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Target { get; set; }
        public Location PickUpLocation { get; set; }
        public Location TargetLocation { get; set; }
        public double Distance { get; set; }
        public ParcelByDelivery(Parcel parcel, Drone drone, Customer sender, Customer target)
        {
            ID = parcel.ID;
            DeliveryStatus = (drone.Status == DroneStatus.Delivery);
            Latitude = drone.Location.Latitude;
            Weight = parcel.Weight;
            Property = parcel.Priority;
            Sender = parcel.Sender;
            Target = parcel.Target;
            PickUpLocation = sender.Location;
            TargetLocation = target.Location;
            Distance = Math.Sqrt(Math.Pow((TargetLocation.Longitude - PickUpLocation.Longitude), 2) + Math.Pow((TargetLocation.Latitude - PickUpLocation.Latitude), 2)); 
        }

    public override string ToString()
    {
        return "Parcel: id:" + ID + " sender: " + Sender + " target: " + Target + "DeliveryStatus(T/F): " + DeliveryStatus + "\n ";
    }
}
}
