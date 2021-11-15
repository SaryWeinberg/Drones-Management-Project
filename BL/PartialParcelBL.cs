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
        public void AddParcelDal(ulong senderId, ulong targetId, WeightCategories weight, Priorities priority)
        {
            Parcel parcel = new Parcel();
            //parcel.ID = ;
            parcel.senderId = senderId;
            parcel.targetId = targetId;
            parcel.weight = weight;
            parcel.priority = priority;
            //parcel.DroneId = droneId;
            dalObj.AddParcel(parcel);
        }

        /// <summary>
        /// Functions for adding a parcel to BL,
        /// If no exception are thrown the function will call the function: AddParcelDal
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="targetId"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
        public void AddParcelBL(ulong senderId, ulong targetId, WeightCategories weight, Priorities priority)
        {
            ParcelBL parcel = new ParcelBL();
            try
            {
                parcel.sender.id = senderId;
                parcel.target.id = targetId;
                parcel.weight = weight;
                parcel.priority = priority;
                parcel.drone = null;
                parcel.associated = new DateTime();
                parcel.created = DateTime.Now;
                parcel.pickedUp = new DateTime();
                parcel.delivered = new DateTime();
            }
            catch (InvalidID e)
            {
                throw e;
            }
            AddParcelDal(senderId, targetId, weight, priority);
        }

        /// <summary>
        /// Convert from dal parcel to BL parcel
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public ParcelBL ConvertDalParcelToBL(Parcel p)
        {
            return new ParcelBL
            {
                id = p.id,
                // drone = ConvertDalDroneToBL(dalObj.GetSpesificDrone(p.droneId)),
                associated = p.requested,
                created = p.pickedUp,
                delivered = p.delivered,
                pickedUp = p.pickedUp,
                priority = p.priority,
                //sender = ConvertDalCustomerToBL(dalObj.GetSpesificCustomer(p.senderId)),
                // target = ConvertDalCustomerToBL(dalObj.GetSpesificCustomer(p.targetId)),
                weight = p.weight
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
            parcelsDal.ForEach(p => parcelsBL.Add(ConvertDalParcelToBL(p)));
            return parcelsBL;
        }

    }
}
