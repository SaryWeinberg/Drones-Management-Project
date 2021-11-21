using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.DO
{
    public struct Parcel
    {
        public int ID { get; set; }
        public int SenderId { get; set; }
        public int TargetId { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DateTime Created { get; set; }
        public int DroneId { get; set; }
        public DateTime Associated { get; set; }
        public DateTime PickedUp { get; set; }
        public DateTime Delivered { get; set; }
        public int Active { get; set; }

        public override string ToString()
        {
            return "Parcel: " + ID + " " + SenderId + " " + TargetId + " " + Weight + " " + Priority + " " + Created + " " + DroneId + " " + Associated + " " + PickedUp + " " + Delivered;
        }
    }    
}

