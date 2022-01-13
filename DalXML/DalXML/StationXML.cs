using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal
{
    public partial class DalXml : IDal
    {
        /// <summary>
        /// Adding new station to DataBase
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(Station station)
        {
            IEnumerable<Station> stationList = GetStations();
            stationList.ToList().Add(station);
            XmlTools.SaveListToXmlSerializer(stationList, dir + stationFilePath);
        }

        /// <summary>
        /// Returns a specific station by ID number
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
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
        public IEnumerable<Station> GetStations(Predicate<Station> condition = null)
        {
            condition ??= (s => true);
            return from station in XmlTools.LoadListFromXmlSerializer<Station>(dir + stationFilePath)
                   where condition(station)
                   select station;
        }

        /// <summary>
        /// Update station in DataBase
        /// </summary>
        /// <param name="station"></param>
        public void UpdateStation(Station station)
        {
            List<Station> stationList = GetStations().ToList();
            stationList[stationList.FindIndex(s => s.ID == station.ID)] = station;
            XmlTools.SaveListToXmlSerializer(stationList, dir + stationFilePath);
        }
    }
}
