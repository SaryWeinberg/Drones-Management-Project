﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace DAL
{
    public partial class DalXml : IDal
    {
        /// <summary>
        /// Adding new parcel to DataBase
        /// </summary>
        /// <param name="parcel"></param>
        public void AddParcel(Parcel parcel)
        {
            IEnumerable<Parcel> parcelList = GetParcels();
            parcelList.ToList().Add(parcel);
            XMLTools.SaveListToXMLSerializer(parcelList, dir + parcelFilePath);
        }

        /// <summary>
        /// Returns a specific parcel by ID number
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns></returns>
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
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> condition = null)
        {
            return from parcel in XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath)
                   where condition(parcel)
                   select parcel;
        }

        /// <summary>
        /// Update parcel in DataBase
        /// </summary>
        /// <param name="parcel"></param>
        public void UpdateParcel(Parcel parcel)
        {
            List<Parcel> parcelList = GetParcels().ToList();
            parcelList[parcelList.FindIndex(p => p.ID == parcel.ID)] = parcel;
            XMLTools.SaveListToXMLSerializer(parcelList, dir + parcelFilePath);
        }
    }
}
