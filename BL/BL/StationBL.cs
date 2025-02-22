﻿using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Runtime.CompilerServices;


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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStationDal(int id, int name, Location location, int chargeSlots)
        {
            DO.Station station = new DO.Station();
            station.ID = id;
            station.Name = name;
            station.Longitude = location.Longitude;
            station.Latitude = location.Latitude;
            station.ChargeSlots = chargeSlots;
            station.Active = true;
            lock (dal)
            {
                dal.AddStation(station);
            }
        }

        /// <summary>
        /// Functions for adding a station to BL,
        /// If no exception are thrown the function will call the function: AddStationDal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="chargeSlots"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string AddStationBL(int id, int name, Location location, int chargeSlots)
        {
            lock (dal)
            {
                if (dal.GetStations().Any(s => s.ID == id))
                    throw new ObjectAlreadyExistException("Station", id);
            }

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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string UpdateStationData(int id, int name = -1, int ChargeSlots = -1)
        {
            lock (dal)
            {
                DO.Station station = dal.GetSpesificStation(id);
                if (name != -1) station.Name = name;
                if (ChargeSlots != -1) station.ChargeSlots = ChargeSlots;
                dal.UpdateStation(station);
                return "The update was successful!";
            }
        }

        /// <summary>
        /// Convert from BL station to DAL station
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Station ConvertDalStationToBL(DO.Station s)
        {
            List<DroneInCharge> droneInCharge = new List<DroneInCharge>();
            lock (dal)
            {
                foreach (DO.DroneCharge droneCharge in dal.GetDroneCharges())
                {
                    if (droneCharge.StationId == s.ID)
                        droneInCharge.Add(new DroneInCharge(droneCharge.DroneId, GetSpesificDrone(droneCharge.DroneId).Battery, DateTime.Now));
                }
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Station GetSpesificStation(int stationId)
        {
            try
            {
                lock (dal)
                {
                    return ConvertDalStationToBL(dal.GetSpesificStation(stationId));
                }
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.Station> GetStations(Predicate<BO.Station> condition = null)
        {            
            condition ??= (s => true);
            lock (dal)
            {
                return from s in dal.GetStations()
                       where condition(ConvertDalStationToBL(s))
                       select ConvertDalStationToBL(s);
            }
        }


        /// <summary>
        /// Returns the station in the location closest to the received location
        /// </summary>
        /// <param name="Targlocation"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
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

        /// <summary>
        /// Returns the station list with StationToList
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.StationToList> GetStationsToList(Predicate<BO.Station> condition = null)
        {
            condition ??= (s => true);
            return from s in GetStations()
                   where condition(s)
                   select new StationToList(s);
        }
    }
}
