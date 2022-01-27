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
        /// Adding new station to DataBase
        /// </summary>
        /// <param name="station"></param>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(Station station)
        {
            DataSource.Stations.Add(station);
        }

        /// <summary>
        /// Update station in DataBase
        /// </summary>
        /// <param name="station"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetSpesificStation(int stationId)
        {           
            try
            {
                Station station = DataSource.Stations.First(s => s.ID == stationId);
                return station;
            }
            catch (InvalidOperationException e)
            {
                throw new ObjectDoesNotExist("station", stationId);             
            }           
        }

        /// <summary>
        /// Returns the station list
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetStations(Predicate<Station> condition = null)
        {
            condition ??= (s => true);
            return from station in DataSource.Stations
                   where condition(station)
                   select station;
        }
    }
}
