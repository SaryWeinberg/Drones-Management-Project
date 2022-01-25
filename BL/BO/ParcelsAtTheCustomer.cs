using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ParcelsAtTheCustomer
    {
        public int ID { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public Status Status { get; set; }
        public CustomerInParcel Target { get; set; }
        public ParcelsAtTheCustomer(Parcel parcel)
        {
            ID = parcel.ID;
            Weight = parcel.Weight;
            Priority = parcel.Priority;
            if (parcel.Delivered != null) Status = Status.Delivered;
            else if (parcel.PickedUp != null) Status = Status.PickedUp;
            else if (parcel.Associated != null) Status = Status.Associated;
            else Status = Status.Created;
            Target = parcel.Target;
        }

        public override string ToString()
        {
            return "Parcel: id:" + ID + " Priority: " + Priority.ToString() + " Weight: " + Weight.ToString() + "Status: " + Status + "Target: " + Target.ToString() + "\n ";
        }
    }
}
