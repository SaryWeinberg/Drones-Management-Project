using System;
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
        public void AddParcel(Parcel parcel)
        {
            IEnumerable<Parcel> parcelList = GetParcels();
            parcelList.ToList().Add(parcel);
            XMLTools.SaveListToXMLSerializer(parcelList, dir + parcelFilePath);
        }
        public Parcel GetSpesificParcel(int parcelId)
        {
            try { return GetParcels(p => p.ID == parcelId).First(); }
            catch (Exception) { throw new ObjectDoesNotExist("Parcel", parcelId); }
        }
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> condition = null)
        {
            return from parcel in XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath)
                   where condition(parcel)
                   select parcel;
        }
        public void UpdateParcel(Parcel parcel)
        {
            List<Parcel> parcelList = GetParcels().ToList();
            parcelList[parcelList.FindIndex(p => p.ID == parcel.ID)] = parcel;
            XMLTools.SaveListToXMLSerializer(parcelList, dir + parcelFilePath);
        }
    }
}
