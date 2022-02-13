using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using System.Runtime.CompilerServices;

namespace Dal
{
    internal partial class DalObject : IDal
    {
        /// <summary>
        /// Adding new parcel to DataBase
        /// </summary>
        /// <param name="parcel"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcel(Parcel parcel)
        {
            DataSource.Parcels.Add(parcel);
        }

        /// <summary>
        /// Update parcel in DataBase
        /// </summary>
        /// <param name="parcel"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
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

        /// <summary>
        /// Returns the parcel list
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> condition = null)
        {
            condition ??= (p => true);
            return from parcel in DataSource.Parcels
                   where parcel.Active == true && condition(parcel)
                   select parcel;
        }
    }
}
