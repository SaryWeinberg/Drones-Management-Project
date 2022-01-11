using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;


namespace DAL
{
    partial class DalObject : IDal
    {
        /// <summary>
        /// Adding new parcel to DataBase
        /// </summary>
        /// <param name="parcel"></param>
        public void AddParcel(Parcel parcel)
        {
            DataSource.Parcels.Add(parcel);
        }

        /// <summary>
        /// Update parcel in DataBase
        /// </summary>
        /// <param name="parcel"></param>
        public void UpdateParcel(Parcel parcel)
        {
            int index = DataSource.Parcels.FindIndex(d => d.ID == parcel.ID);
            DataSource.Parcels[index] = parcel;
        }

        /// <summary>
        /// Returns a specific parcel by ID number
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns></returns>
        public Parcel GetSpesificParcel(int parcelId)
        {
            try
            {
                return GetParcels(parcel => parcel.ID == parcelId).First();
            }
            catch
            {
                throw new ObjectDoesNotExist("Parcel", parcelId);
            }
        }


        /*public IEnumerable<Parcel> GetParcelByCondition(Predicate<Parcel> condition)
        {
            return from parcel in GetParcels()
                   where condition(parcel)
                   select parcel;
        }*/

        /// <summary>
        /// Returns the parcel list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> condition = null)
        {
            condition ??= (p => true);
            return from parcel in DataSource.Parcels
                   where parcel.Active == true && condition(parcel)
                   select parcel;
        }

/*        public void RemoveParcel(int ID)
        {
            DataSource.Parcels.First(parcel => parcel.ID == ID)
            int index = DataSource.Parcels.FindIndex(p => p.ID == ID);
            DataSource.Parcels[index].Active = false; 
      *//*      Parcel P = DataSource.Parcels.First(p=>p.ID == ID);
            P.Active = false;*//*
        }*/
    }
}
