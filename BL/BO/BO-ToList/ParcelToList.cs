using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ParcelToList
    {
        public ParcelToList(Parcel parcel)
        {
            ID = parcel.ID;
            SenderName = parcel.Sender.Name;
            TargetName = parcel.Target.Name;
            Weight = parcel.Weight;
            Priority = parcel.Priority;
            if (parcel.Delivered != null) Status = Status.Delivered;
            else if (parcel.PickedUpByDrone != null) Status = Status.PickedUp;
            else if (parcel.Associated != null) Status = Status.Associated;
            else  Status = Status.Created;
        }

        public int ID { get; set; }
        public string SenderName { get; set; }
        public string TargetName { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public Status Status { get; set; }

        public override string ToString()
        {
            return "ID: " + ID + " SenderName: " + SenderName + " TargetName: " + TargetName + " Weight: " + Weight + " Priority: " + Priority + " Status: " + Status;
        }
    }
}
