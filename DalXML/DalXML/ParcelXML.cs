using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
using System.Runtime.CompilerServices;
namespace Dal
{
    public partial class DalXml : IDal
    {
        /// <summary>
        /// Adding new parcel to DataBase
        /// </summary>
        /// <param name="parcel"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcel(Parcel parcel)
        {
            List<Parcel> parcelList = GetParcels().ToList();
            parcelList.Add(parcel);
            XmlTools.SaveListToXmlSerializer(parcelList, direction + parcelFilePath);
        }

        /// <summary>
        /// Update parcel in DataBase
        /// </summary>
        /// <param name="parcel"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcel(Parcel parcel)
        {
            List<Parcel> parcelList = GetParcels().ToList();
            parcelList[parcelList.FindIndex(p => p.ID == parcel.ID)] = parcel;
            XmlTools.SaveListToXmlSerializer(parcelList, direction + parcelFilePath);
        }

        /// <summary>
        /// Returns a specific parcel by ID number
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetSpesificParcel(int parcelId)
        {
            try { return GetParcels(p => p.ID == parcelId).First(); }
            catch (Exception) { throw new ObjectDoesNotExist("Parcel", parcelId); }
        }

        /// <summary>
        /// Returns the parcel list
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> condition = null)
        {
            condition ??= (c => true);
            return from parcel in XmlTools.LoadListFromXmlSerializer<Parcel>(direction + parcelFilePath)
                   where condition(parcel)
                   select parcel;
        }        
    }
}