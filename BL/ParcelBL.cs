using System;

namespace IBL.BO
{
    public class ParcelBL
    {
        private int id;
        public int ID {
            get { return id; }
            set
            {
                if (value != null)
                    id = value;
                else throw new InvalidID();
            }
        }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Target { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DroneInParcel Drone { get; set; }
        public DateTime Created { get; set; }
        public DateTime Associated { get; set; }
        public DateTime PickedUp { get; set; }
        public DateTime Delivered { get; set; }

        public override string ToString()
        {
            return "Parcel: id: " + ID + " Sender:  " + Sender + "  Target:  " + Target + " Weight: " + Weight + " Priority: " + Priority + " Drone: " + Drone + "\nCreated: " + Created + " Associated: " + Associated + " PickedUp: " + PickedUp + " Delivered: " + Delivered;
        }
    }
}
