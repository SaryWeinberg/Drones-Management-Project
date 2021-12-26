using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    partial class BL : BLApi.IBL
    {
        /// <summary>
        /// Functions for adding a parcel to DAL
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="targetId"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
        public void AddParcelDal(int id, int senderId, int targetId, WeightCategories weight, Priorities priority)
        {
            if (dalObj.GetParcels().Any(p => p.ID == id))
            {
                throw new ObjectAlreadyExistException("Parcel", id);
            }

            DO.Parcel parcel = new DO.Parcel();

            parcel.ID = id;
            parcel.SenderId = senderId;
            parcel.TargetId = targetId;
            parcel.Weight = weight;
            parcel.Priority = priority;
            parcel.Created = DateTime.Now;
            parcel.Active = true;

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
        public string AddParcelBL(int senderId, int targetId, WeightCategories weight, Priorities priority)
        {
            BO.Parcel parcel = new BO.Parcel();
            try
            {
                CustomerInParcel senderCustomer = new CustomerInParcel();
                senderCustomer.ID = senderId;
                senderCustomer.Name = GetSpesificCustomer(senderId).Name;

                CustomerInParcel targetCustomer = new CustomerInParcel();
                targetCustomer.ID = targetId;
                targetCustomer.Name = GetSpesificCustomer(targetId).Name;

                parcel.ID = GetCustomers().Count();
                parcel.Sender = senderCustomer;
                parcel.Target = targetCustomer;
                parcel.Weight = weight;
                parcel.Priority = priority;
                parcel.Drone = null;

                parcel.Created = DateTime.Now;
            }
            catch (InvalidObjException e) { throw e; }
            AddParcelDal(GetCustomers().Count() + 1, senderId, targetId, weight, priority);
            return "Parcel added successfully!";
        }

        /// <summary>
        /// Convert from BL parcel to DAL parcel
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public DO.Parcel ConvertBLParcelToDAL(BO.Parcel p)
        {
            return new DO.Parcel
            {
                ID = p.ID,
                Delivered = p.Delivered,
                DroneId = p.Drone.ID,
                PickedUp = p.PickedUp,
                Priority = p.Priority,
                Created = p.Associated,
                SenderId = p.Sender.ID,
                TargetId = p.Target.ID,
                Weight = p.Weight,
                Associated = p.Associated
            };
        }

        /// <summary>
        /// Convert from DAL parcel to BL parcel
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public BO.Parcel ConvertDalParcelToBL(DO.Parcel p)
        {
            BO.Drone droneBL = GetDronesList().Find(d => d.ID == p.DroneId);

            CustomerInParcel Scustomer = new CustomerInParcel();
            Scustomer.ID = p.SenderId;
            Scustomer.Name = dalObj.GetSpesificCustomer(p.SenderId).Name;

            CustomerInParcel Tcustomer = new CustomerInParcel();
            Tcustomer.ID = p.TargetId;
            Tcustomer.Name = dalObj.GetSpesificCustomer(p.TargetId).Name;

            DroneInParcel droneInparcel = new DroneInParcel();
            droneInparcel.ID = droneBL.ID;
            droneInparcel.Location = droneBL.Location;
            droneInparcel.Battery = droneBL.Battery;

            return new BO.Parcel
            {
                ID = p.ID,
                Associated = p.Created,
                Created = p.PickedUp,
                Delivered = p.Delivered,
                PickedUp = p.PickedUp,
                Priority = p.Priority,
                Weight = p.Weight,
                Sender = Scustomer,
                Target = Tcustomer,
                Drone = droneInparcel
            };
        }

        /// <summary>
        /// Returning a specific parcel by ID number
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns></returns>
        public BO.Parcel GetSpesificParcelBL(int parcelId)
        {
            try
            {
                return ConvertDalParcelToBL(dalObj.GetSpesificParcel(parcelId));
            }
            catch (ObjectDoesNotExist e)
            {
                throw new ObjectNotExistException(e.Message);
            }
        }

        /// <summary>
        /// Returning the parcel list
        /// </summary>
        /// <returns></returns>
        public List<BO.Parcel> GetParcels()
        {
            List<DO.Parcel> parcelsDal = dalObj.GetParcels();
            List<BO.Parcel> parcelsBL = new List<BO.Parcel>();
            parcelsDal.ForEach(p => parcelsBL.Add(ConvertDalParcelToBL(p)));
            return parcelsBL;
        }

        /// <summary>
        /// Returns a list of parcels that meet the condition 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<BO.Parcel> GetParcelsByCondition(Predicate<BO.Parcel> condition)
        {
            return from parcelBL in GetParcels()
                   where condition(parcelBL)
                   select parcelBL;
        }

        public IEnumerable<BO.ParcelToList> GetParcelsToListByCondition(Predicate<BO.ParcelToList> condition)
        {
            return from ParcelToList in GetParcelsToList()
                   where condition(ParcelToList)
                   select ParcelToList;
        }

        public IEnumerable<BO.Parcel> GetParcelsNotYetAssignedToDrone()
        {
            return GetParcelsByCondition(parcel => parcel.Associated == null);
        }

        /// <summary>
        /// Returns the parcel list with ParcelToList
        /// </summary>
        /// <returns></returns>
        public List<BO.ParcelToList> GetParcelsToList()
        {
            List<BO.Parcel> parcels = GetParcels();
            List<BO.ParcelToList> parcelToList = new List<BO.ParcelToList>();
            foreach (BO.Parcel parcel in parcels)
            {
                parcelToList.Add(new BO.ParcelToList(parcel, dalObj));
            }
            return parcelToList;
        }
    }
}
