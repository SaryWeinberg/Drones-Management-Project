using System;

namespace BO
{
    public class Parcel 
    {
        private int id;
        public int ID {
            get { return id; }
            set
            {
                if (value >= 0)
                    id = value;
                else throw new InvalidObjException("ID");
            }
        }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Target { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DroneInParcel Drone { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Associated { get; set; }
        public DateTime? PickedUpByDrone { get; set; }
        public DateTime? Delivered { get; set; }

        public override string ToString()
        {
            return "Parcel: id: " + ID + " Sender:  " + Sender + "  Target:  " + Target + " Weight: " + Weight + " Priority: " + Priority + " Drone: " + Drone + "\nCreated: " + Created?.ToString("dd/MM/yyyy") + " Associated: " + Associated?.ToString("dd/MM/yyyy") + " PickedUp: " + PickedUpByDrone?.ToString("dd/MM/yyyy") + " Delivered: " + Delivered?.ToString("dd/MM/yyyy");
        }
    }
}
