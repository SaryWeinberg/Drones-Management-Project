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
            station.ID = id;
            station.Name = name;
            station.Longitude = location.Longitude;
            station.Latitude = location.Latitude;
            station.ChargeSlots = chargeSlots;
            dalObj.AddStation(station.Clone());
        }

        /// <summary>
        /// Functions for adding a station to BL,
        /// If no exception are thrown the function will call the function: AddStationDal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="chargeSlots"></param>
        public void AddStationBL(int id, int name, Location location, int chargeSlots)
        {
            StationBL station = new StationBL();
            try
            {
                station.ID = id;
                station.Name = name;
                station.Location.Longitude = location.Longitude;
                station.Location.Latitude = location.Latitude;
                station.AveChargeSlots = chargeSlots;
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
                station.Name = name;
            if (ChargeSlots != 0)
                station.ChargeSlots = ChargeSlots;
        }

        /// <summary>
        /// Convert from BL station to DAL station
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public Station ConvertBLStationToDAL(StationBL s)
        {
            return new Station
            {
                ID = s.ID, 
                ChargeSlots= (int)s.AveChargeSlots, 
                Latitude = s.Location.Latitude, 
                Longitude = s.Location.Longitude, 
                Name= s.Name                 
            };
        }

        /// <summary>
        /// Convert from DAL station to BL station
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public StationBL ConvertDalStationToBL(Station s)
        {
            return new StationBL
            {
                ID = s.ID,
                Name = s.Name,
                Location = new Location { Latitude = s.Latitude, Longitude = s.Longitude },
                AveChargeSlots = s.ChargeSlots
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
            stationsDal.ForEach(s => stationsBL.Add(ConvertDalStationToBL(s.Clone())));
            return stationsBL;
        }


        public StationBL GetNearestAvailableStation( Location Targlocation)
        {

            double minDistance = 0;
            StationBL station = null;
            List<StationBL> stations = GetStationsBL();
            foreach (StationBL currentStation in stations)
            {
                if (currentStation.AveChargeSlots > 0 && Distance(currentStation.Location, Targlocation) < minDistance)
                {
                    minDistance = Distance(currentStation.Location, Targlocation);
                    station = currentStation;
                }
                else
                {
                    throw new ThereAreNoAvelableChargeSlots();
                }
            }

            return station;
        }
    }
}
