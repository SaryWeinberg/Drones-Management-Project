using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    partial class BL : BLApi.IBL
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
            DO.Station station = new DO.Station();
            station.ID = id;
            station.Name = name;
            station.Longitude = location.Longitude;
            station.Latitude = location.Latitude;
            station.ChargeSlots = chargeSlots;
            station.Active = true;
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
        public string AddStationBL(int id, int name, Location location, int chargeSlots)
        {
            if (dalObj.GetStations().Any(s => s.ID == id))
                throw new ObjectAlreadyExistException("Station", id);            
            
            BO.Station station = new BO.Station();
            try
            {
                station.ID = id;
                station.Name = name;

                station.Location = location;
                station.AveChargeSlots = chargeSlots;
            }
            catch (InvalidObjException e) { throw e; }

            AddStationDal(id, name, location, chargeSlots);

            return "Station added successfully!";
        }

        /// <summary>
        /// Function for update the station data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="ChargeSlots"></param>
        public string UpdateStationData(int id, int name = -1, int ChargeSlots = -1)
        {
            DO.Station station = dalObj.GetSpesificStation(id);
            if (name != -1) station.Name = name;
            if (ChargeSlots != -1) station.ChargeSlots = ChargeSlots;
            dalObj.UpdateStation(station);
            return "The update was successful!";
        }

        /// <summary>
        /// Convert from BL station to DAL station
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public DO.Station ConvertBLStationToDAL(BO.Station s)
        {
            return new DO.Station
            {
                ID = s.ID,
                ChargeSlots = (int)s.AveChargeSlots,
                Latitude = s.Location.Latitude,
                Longitude = s.Location.Longitude,
                Name = s.Name
            };
        }

        /// <summary>
        /// Convert from DAL station to BL station
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public BO.Station ConvertDalStationToBL(DO.Station s)
        {
            List<DroneInCharge> droneInCharge = new List<DroneInCharge>();
            foreach (DO.DroneCharge droneCharge in dalObj.GetDroneCharges())
            {
                if (droneCharge.StationId == s.ID)
                    droneInCharge.Add(new DroneInCharge(droneCharge.DroneId, GetSpesificDrone(droneCharge.DroneId).Battery, DateTime.Now));
            }

            return new BO.Station
            {
                ID = s.ID,
                Name = s.Name,
                Location = new Location { Latitude = s.Latitude, Longitude = s.Longitude },
                AveChargeSlots = s.ChargeSlots,
                DronesInChargelist = droneInCharge
            };
        }

        /// <summary>
        /// Returning a specific station by ID number
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public BO.Station GetSpesificStation(int stationId)
        {
            try
            {
                return ConvertDalStationToBL(dalObj.GetSpesificStation(stationId));
            }
            catch (ObjectDoesNotExist e)
            {
                throw new ObjectNotExistException(e.Message);
            }
        }




        /// <summary>
        /// Returning the station list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.Station> GetStations()
        {
            List<DO.Station> stationsDal = dalObj.GetStations().ToList();
            List<BO.Station> stationsBL = new List<BO.Station>();
            stationsDal.ForEach(s => stationsBL.Add(ConvertDalStationToBL(s)));
            return stationsBL;
        }

        /// <summary>
        /// Returns stations with available charge slots
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.Station> GetAvailableStationsList()
        {
            List<BO.Station> stationsBL = new List<BO.Station>().ToList();
            foreach (DO.Station station in dalObj.GetStations())
            {
                if (station.ChargeSlots > 0)
                {
                    stationsBL.Add(ConvertDalStationToBL(station));
                }
            }
            return stationsBL;
        }

        /// <summary>
        /// Returns the station in the location closest to the received location
        /// </summary>
        /// <param name="Targlocation"></param>
        /// <returns></returns>
        public BO.Station GetNearestAvailableStation(Location Targlocation)
        {
            BO.Station station = null;
            List<BO.Station> stations = GetStations().ToList();

            double minDistance = Distance(stations[0].Location, Targlocation);
            foreach (BO.Station currentStation in stations)
            {
                if (currentStation.AveChargeSlots > 0 && Distance(currentStation.Location, Targlocation) <= minDistance)
                {
                    minDistance = Distance(currentStation.Location, Targlocation);
                    station = currentStation;
                }
            }
            if (station == null)
            {
                throw new ThereAreNoAvelableChargeSlotsException();
            }
            return station;
        }

        public IEnumerable<BO.Station> GetStationsByCondition(Predicate<BO.Station> condition)
        {
            return from station in GetStations()
                   where condition(station)
                   select station;
        }

        /// <summary>
        /// Returns the station list with StationToList
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.StationToList> GetStationsList()
        {
            List<BO.Station> stations = GetStations().ToList();
            List<BO.StationToList> stationToList = new List<BO.StationToList>();
            foreach (BO.Station station in stations)
            {
                stationToList.Add(new BO.StationToList(station, dalObj));
            }
            return stationToList;
        }


        public IEnumerable<StationToList> GetStationsToListByCondition(Predicate<StationToList> condition)
        {
            return from station in GetStationsList()
                   where condition(station)
                   select station;
        }
    }
}
