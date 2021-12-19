using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class CustomerToList
    {
        public CustomerToList(Customer customer, DalApi.IDal dalObj)
        {
            ID = customer.ID;
            Name = customer.Name;
            PhoneNum = customer.PhoneNum;
            ParcelSentAndDelivered = dalObj.getParceleByCondition(p => p.SenderId == ID && (p.Delivered != null && p.PickedUp != null)).Count();
            ParcelSentButNotDelivered = dalObj.getParceleByCondition(p => p.SenderId == ID && (p.Delivered == null && p.PickedUp != null)).Count();
            ParcelReceived = dalObj.getParceleByCondition(p => p.TargetId == ID && (p.Delivered != null)).Count();
            ParcelOnTheWayToCustomer = dalObj.getParceleByCondition(p => p.TargetId == ID && (p.Delivered == null && p.PickedUp != null)).Count();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public int PhoneNum { get; set; }
        public int ParcelSentAndDelivered { get; set; }
        public int ParcelSentButNotDelivered { get; set; }
        public int ParcelReceived { get; set; }
        public int ParcelOnTheWayToCustomer { get; set; }

        public override string ToString()
        {
            return "Customer: id: " + ID + " Phone number: " + PhoneNum + " name: " + Name + " Parcel Sent And Delivered: " +
                ParcelSentAndDelivered + " Parcel Sent But Not Delivered: " + ParcelSentButNotDelivered + " Parcel Received: " + 
                ParcelReceived + " Parcel On The Way To Customer: " + ParcelOnTheWayToCustomer;
        }
    }
}
