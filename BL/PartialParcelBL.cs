using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace BL
{
    partial class BL : IBL.IBL
    {
        /// <summary>
        /// Functions for adding a parcel to DAL
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="targetId"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
        public void AddParcelDal(int senderId, int targetId, WeightCategories weight, Priorities priority)
        {
            Parcel parcel = new Parcel();
            //parcel.ID = ;
            parcel.SenderId = senderId;
            parcel.TargetId = targetId;
            parcel.Weight = weight;
            parcel.Priority = priority;
            //parcel.DroneId = droneId;
            dalObj.AddParcel(parcel.Clone());
        }

        /// <summary>
        /// Functions for adding a parcel to BL,
        /// If no exception are thrown the function will call the function: AddParcelDal
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="targetId"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
        public string AddParcelBL(int senderId, int targetId, WeightCategories weight, Priorities priority)
        {
            ParcelBL parcel = new ParcelBL();
            try
            {
                parcel.Sender.ID = senderId;
                parcel.Target.ID = targetId;
                parcel.Weight = weight;
                parcel.Priority = priority;
                parcel.Drone = null;
                parcel.Associated = new DateTime();
                parcel.Created = DateTime.Now;
                parcel.PickedUp = new DateTime();
                parcel.Delivered = new DateTime();
            }
            catch (InvalidID e)
            {
                throw e;
            }
            AddParcelDal(senderId, targetId, weight, priority);
            return "Parcel added successfully!";
        }

        /// <summary>
        /// Convert from BL parcel to DAL parcel
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Parcel ConvertBLParcelToDAL(ParcelBL p)
        {
            return new Parcel
            {
                ID = p.ID, 
                Delivered = p.Delivered, 
                DroneId = p.Drone.ID, 
                PickedUp = p.PickedUp, 
                Priority = p.Priority, 
                Requested = p.Associated, 
                SenderId = p.Sender.ID, 
                TargetId= p.Target.ID, 
                Weight = p.Weight, 
                Scheduled = p.Associated                 
            };
        }

        /// <summary>
        /// Convert from DAL parcel to BL parcel
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public ParcelBL ConvertDalParcelToBL(Parcel p)
        {
            return new ParcelBL
            {
                ID = p.ID,
                //Drone = ConvertDalDroneToBL(dalObj.GetSpesificDrone(p.droneId)),
                Associated = p.Requested,
                Created = p.PickedUp,
                Delivered = p.Delivered,
                PickedUp = p.PickedUp,
                Priority = p.Priority,
                //Sender = ConvertDalCustomerToBL(dalObj.GetSpesificCustomer(p.SenderId)),
                //Target = ConvertDalCustomerToBL(dalObj.GetSpesificCustomer(p.TargetId)),
                Weight = p.Weight
            };
        }

        /// <summary>
        /// Returning a specific parcel by ID number
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns></returns>
        public ParcelBL GetSpesificParcelBL(int parcelId)
        {
            try
            {
                return ConvertDalParcelToBL(dalObj.GetSpesificParcel(parcelId));
            }
            catch (ObjectDoesNotExist e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Returning the parcel list
        /// </summary>
        /// <returns></returns>
        public List<ParcelBL> GetParcelsBL()
        {
            List<Parcel> parcelsDal = dalObj.GetParcels();
            List<ParcelBL> parcelsBL = new List<ParcelBL>();
            parcelsDal.ForEach(p => parcelsBL.Add(ConvertDalParcelToBL(p.Clone())));
            return parcelsBL;
        }
    }
}
