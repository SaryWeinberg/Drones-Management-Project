using System;

namespace IBL.BO
{
    public class ParcelBL
    {
        private int pid;
        public int id {
            get { return pid; }
            set
            {
                if (value != null)
                    pid = value;
                else throw new InvalidID();
            }
        }
        public CustomerInParcel sender { get; set; }
        public CustomerInParcel target { get; set; }
        public WeightCategories weight { get; set; }
        public Priorities priority { get; set; }
        public DroneInParcel drone { get; set; }
        public DateTime created { get; set; }
        public DateTime associated { get; set; }
        public DateTime pickedUp { get; set; }
        public DateTime delivered { get; set; }
    }
}
