using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Runtime.CompilerServices;

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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcelDal(int id, int senderId, int targetId, WeightCategories weight, Priorities priority)
        {
            lock (dal)
            {
                if (dal.GetParcels().Any(p => p.ID == id))
                {
                    throw new ObjectAlreadyExistException("Parcel", id);
                }
            }

            DO.Parcel parcel = new DO.Parcel();

            parcel.ID = id;
            parcel.SenderId = senderId;
            parcel.TargetId = targetId;
            parcel.Weight = weight;
            parcel.Priority = priority;
            parcel.Created = DateTime.Now;
            parcel.Active = true;
            lock (dal)
            {
                dal.AddParcel(parcel);
            }
        }

        /// <summary>
        /// Functions for adding a parcel to BL,
        /// If no exception are thrown the function will call the function: AddParcelDal
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="targetId"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
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

                parcel.ID = GetParcels().ToList().Count();
                parcel.Sender = senderCustomer;
                parcel.Target = targetCustomer;
                parcel.Weight = weight;
                parcel.Priority = priority;
                parcel.Drone = null;
                parcel.Created = DateTime.Now;
            }
            catch (InvalidObjException e) { throw e; }
            AddParcelDal(parcel.ID, senderId, targetId, weight, priority);
            return "Parcel added successfully!";
        }

        /// <summary>
        /// Convert from BL parcel to DAL parcel
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Parcel ConvertBLParcelToDAL(BO.Parcel p)
        {
            return new DO.Parcel
            {
                ID = p.ID,
                Delivered = p.Delivered,
                DroneId = p.Drone == null? 0: p.Drone.ID,
                PickedUp = p.PickedUp,
                Priority = p.Priority,
                Created = p.Created,
                SenderId = p.Sender.ID,
                TargetId = p.Target.ID,
                Weight = p.Weight,
                Associated = p.Associated,
                Active = true
            };
        }

        /// <summary>
        /// Convert from DAL parcel to BL parcel
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Parcel ConvertDalParcelToBL(DO.Parcel p)
        {
            BO.Drone droneBL = GetDronesList().ToList().Find(d => d.ID == p.DroneId);

            CustomerInParcel Scustomer = new CustomerInParcel();
            Scustomer.ID = p.SenderId;
            lock (dal)
            {
                Scustomer.Name = dal.GetSpesificCustomer(p.SenderId).Name;
            }

            CustomerInParcel Tcustomer = new CustomerInParcel();
            Tcustomer.ID = p.TargetId;
            lock (dal)
            {
                Tcustomer.Name = dal.GetSpesificCustomer(p.TargetId).Name;
            }
            DroneInParcel droneInparcel = new DroneInParcel();
            if (p.Associated == null)
            {
                droneInparcel.ID = 0;
            }
            else
            {
                
                droneInparcel.ID = droneBL.ID;
                droneInparcel.Location = droneBL.Location;
                droneInparcel.Battery = droneBL.Battery;
            }

            return new BO.Parcel
            {
                ID = p.ID,
                Associated = p.Associated,
                Created = p.Created,
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Parcel GetSpesificParcel(int parcelId)
        {
            try
            {
                lock (dal)
                {
                    return ConvertDalParcelToBL(dal.GetSpesificParcel(parcelId));
                }
            }
            catch (ObjectDoesNotExist e)
            {
                throw new ObjectNotExistException(e.Message);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public string RemoveParcel(int ID)
        {
            lock (dal)
            {
                DO.Parcel parcel = dal.GetParcels().First(p => p.ID == ID);
                if (parcel.Active)
                {
                    parcel.Active = false;
                    dal.UpdateParcel(parcel);
                    return $"The parcel ID - {ID} remove successfully";
                }
                else throw new ObjectNotExistException($"The parcel ID - {ID} not exist");
            }
        }


        /// <summary>
        /// Returning the parcel list
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.Parcel> GetParcels(Predicate<BO.Parcel> condition = null)
        {            
            condition ??= (p => true);
            lock (dal)
            {
                return from p in dal.GetParcels()
                       where condition(ConvertDalParcelToBL(p))
                       select ConvertDalParcelToBL(p);
            }
        }

        /*public IEnumerable<BO.Parcel> GetParcelsNotYetAssignedToDrone()
        {
            return GetParcels(parcel => parcel.Associated == null);
        }*/

        /// <summary>
        /// Returns the parcel list with ParcelToList
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetParcelsToList(Predicate<BO.Parcel> condition)
        {
            condition ??= (p => true);
            return from p in GetParcels()
                   where condition(p)
                   select new ParcelToList(p);
        }
    }
}
