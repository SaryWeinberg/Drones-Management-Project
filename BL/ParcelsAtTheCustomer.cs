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
        public ParcelsAtTheCustomer(BO.Parcel parcel)
        {
            ID = parcel.ID;
            Weight = parcel.Weight;
            Priority = parcel.Priority;
            if (parcel.Delivered != null)
                Status = Status.provided;
            else if (parcel.PickedUp != null)
                Status = Status.collected;
            else if (parcel.Associated != null)
                Status = Status.associated;
            else Status = Status.created;
            Target = parcel.Target;
        }
    }
}
