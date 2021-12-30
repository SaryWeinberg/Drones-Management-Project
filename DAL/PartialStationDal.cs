using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
/*using IDAL;*/
using DalApi;


namespace DalObject
{
     partial class DalObject : IDal
    {
        /// <summary>
        /// Adding new station to DataBase
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(Station station)
        {
            DataSource.Stations.Add(station);
        }

        /// <summary>
        /// Update station in DataBase
        /// </summary>
        /// <param name="station"></param>
        public void UpdateStation(Station station)
        {
            int index = DataSource.Stations.FindIndex(d => d.ID == station.ID);
            DataSource.Stations[index] = station;
        }

        /// <summary>
        /// Returns a specific station by ID number
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public Station GetSpesificStation(int stationId)
        {
            /*       try
                   {*/
            try
            {
                Station station = DataSource.Stations.First(s => s.ID == stationId);
                return station;
            }
            catch (InvalidOperationException e)
            {
                throw new ObjectDoesNotExist("station", stationId);
                /*    throw new Exception("station not exist!");*/
            }
            /*  }*/

            /*            catch (ObjectDoesNotExist e)
                        {
                            throw new ObjectDoesNotExist("station", 0);
                        }*/
        }

        /// <summary>
        /// Returns the list of stations one by one
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> GetStationLists()
        {
            return from station in DataSource.Stations
                   select station;
            /*foreach (Station station in DataSource.Stations)
            {
                yield return station;
            }*/
        }

        /// <summary>
        /// Returns the station list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> GetStations()
        {
            foreach (Station x in DataSource.Stations)
                yield return x;
/*            return DataSource.Stations;
*/        }
    }
}
