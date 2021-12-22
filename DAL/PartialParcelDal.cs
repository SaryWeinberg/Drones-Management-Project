using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;


namespace DalObject
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
                return GetParcelByCondition(parcel => parcel.ID == parcelId).First();
            }
            catch
            {
                throw new ObjectDoesNotExist("Parcel", parcelId);
            }
        }


        public IEnumerable<Parcel> GetParcelByCondition(Predicate<Parcel> condition)
        {
            return from parcelBL in GetParcels()
                   where condition(parcelBL)
                   select parcelBL;
        }

        /// <summary>
        /// Returns the parcel list
        /// </summary>
        /// <returns></returns>
        public List<Parcel> GetParcels()
        {
            return DataSource.Parcels;
        }
    }
}
