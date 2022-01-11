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
        public void AddStation(Station station)
        {
            IEnumerable<Station> stationList = GetStations();
            stationList.ToList().Add(station);
            XMLTools.SaveListToXMLSerializer(stationList, dir + stationFilePath);
        }
        public Station GetSpesificStation(int stationId)
        {
            try { return GetStations(s => s.ID == stationId).First(); }
            catch (Exception) { throw new ObjectDoesNotExist("Station", stationId); }
        }
        public IEnumerable<Station> GetStations(Predicate<Station> condition = null)
        {
            return from station in XMLTools.LoadListFromXMLSerializer<Station>(dir + stationFilePath)
                   where condition(station)
                   select station;
        }
        public void UpdateStation(Station station)
        {
            List<Station> stationList = GetStations().ToList();
            stationList[stationList.FindIndex(s => s.ID == station.ID)] = station;
            XMLTools.SaveListToXMLSerializer(stationList, dir + stationFilePath);
        }
    }
}
