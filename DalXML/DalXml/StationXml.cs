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
        /// Adding new station to DataBase
        /// </summary>
        /// <param name="station"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(Station station)
        {
            List<Station> stationList = GetStations().ToList();
            stationList.Add(station);
            XmlTools.SaveListToXmlXElement(stationList, direction + stationFilePath);
        }

        /// <summary>
        /// Update station in DataBase
        /// </summary>
        /// <param name="station"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(Station station)
        {
            List<Station> stationList = GetStations().ToList();
            stationList[stationList.FindIndex(s => s.ID == station.ID)] = station;
            XmlTools.SaveListToXmlXElement(stationList, direction + stationFilePath);
        }

        /// <summary>
        /// Returns a specific station by ID number
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetSpesificStation(int stationId)
        {
            try { return GetStations(s => s.ID == stationId).First(); }
            catch (Exception) { throw new ObjectDoesNotExist("Station", stationId); }
        }

        /// <summary>
        /// Returns the station list
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetStations(Predicate<Station> condition = null)
        {
            var x = from station in XmlTools.LoadListFromXmlXElement(direction + stationFilePath)
                    select new Station()
                    {
                        ID = Convert.ToInt32(station.Element("ID").Value),
                        Name = Convert.ToInt32(station.Element("Name").Value),
                        Longitude = Convert.ToInt32(station.Element("Longitude").Value),
                        Latitude = Convert.ToInt32(station.Element("Latitude").Value),
                        ChargeSlots = Convert.ToInt32(station.Element("ChargeSlots").Value),
                        Active = Convert.ToBoolean(station.Element("Active").Value)
                    };

            condition ??= (s => true);
            return from station in x
                   where condition(station)
                   orderby(station.ID)
                   select station;
        }
    }
}