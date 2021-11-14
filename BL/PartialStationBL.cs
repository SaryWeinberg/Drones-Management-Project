using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace BL
{
   partial class BL : IBL.IBL
    {
        /// <summary>
        /// Functions for adding a station to DAL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="chargeSlots"></param>
        public void AddStationDal(int id, int name, Location location, int chargeSlots)
        {
            Station station = new Station();
            station.id = id;
            station.name = name;
            station.longitude = location.longitude;
            station.latitude = location.latitude;
            station.chargeSlots = chargeSlots;
            dalObj.AddStation(station);
        }

        /// <summary>
        /// Functions for adding a station to BL,
        /// If no exception are thrown the function will call the function: AddStationDal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="chargeSlots"></param>
        public void AddStation(int id, int name, Location location, int chargeSlots)
        {
            StationBL station = new StationBL();
            try
            {
                station.id = id;
                station.name = name;
                station.location.longitude = location.longitude;
                station.location.latitude = location.latitude;
                station.aveChargeSlots = chargeSlots;
            }
            catch (InvalidID e)
            {
                throw e;
            }
            catch (InvalidName e)
            {
                throw e;
            }
            AddStationDal(id, name, location, chargeSlots);
        }

        /// <summary>
        /// Function for update the station data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="ChargeSlots"></param>
        public void UpdateStationData(int id, int name = 0, int ChargeSlots = 0)
        {
            Station station = dalObj.GetSpesificStation(id);
            if (name != 0)
                station.name = name;
            if (ChargeSlots != 0)
                station.chargeSlots = ChargeSlots;
        }

        /// <summary>
        /// Convert from dal station to BL station
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public StationBL ConvertDalStationToBL(Station s)
        {
            return new StationBL
            {
                id = s.id,
                name = s.name,
                location = new Location { latitude = s.latitude, longitude = s.longitude },
                aveChargeSlots = s.chargeSlots
            };
        }

        /// <summary>
        /// Returning a specific station by ID number
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public StationBL GetSpesificStationBL(int stationId)
        {
            try
            {
                return ConvertDalStationToBL(dalObj.GetSpesificStation(stationId));
            }
            catch (ObjectDoesNotExist e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Returning the station list
        /// </summary>
        /// <returns></returns>
        public List<StationBL> GetStationsBL()
        {
            List<Station> stationsDal = dalObj.GetStations();
            List<StationBL> stationsBL = new List<StationBL>();
            stationsDal.ForEach(s => stationsBL.Add(ConvertDalStationToBL(s)));
            return stationsBL;
        }
    }
}
