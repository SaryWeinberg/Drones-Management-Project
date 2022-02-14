/*using DO;*/
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
        /// Functions for adding a drone to DAL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneDal(int id, string model, WeightCategories maxWeight)
        {
            DO.Drone droneDal = new DO.Drone();
            droneDal.ID = id;
            droneDal.Model = model;
            droneDal.MaxWeight = maxWeight;
            droneDal.Active = true;
            lock (dal)
            {
                dal.AddDrone(droneDal);
            }
        }

        /// <summary>
        /// Functions for adding a droneCharge to DAL
        /// </summary>
        /// <param name="stationID"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneCharge(int stationID, int DroneID, double batteryStatus)
        {
            Station station = GetSpesificStation(stationID);
            station.AveChargeSlots -= 1;
            station.DronesInChargelist.Add(new DroneInCharge(DroneID, batteryStatus, DateTime.Now));

            DO.DroneCharge droneCharge = new DO.DroneCharge();
            droneCharge.DroneId = DroneID;
            droneCharge.StationId = stationID;
            lock (dal)
            {
                dal.AddDroneCharge(droneCharge);
                dal.UpdateStation(ConvertBLStationToDAL(station));
            }
        }

        /// <summary>
        /// Functions for adding a drone to BL, 
        /// If no exception are thrown the function will call the functions:
        /// AddDroneChargeDAL and AddDroneDAL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        /// <param name="stationID"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string AddDroneBL(int id, string model, WeightCategories maxWeight, int stationID)
        {
            if (GetDronesList().Any(d => d.ID == id))
                throw new ObjectAlreadyExistException("drone", id);

            Drone droneBL = new Drone();
            try
            {
                droneBL.ID = id;
                droneBL.Model = model;
                droneBL.MaxWeight = maxWeight;
                droneBL.Battery = rand.Next(20, 40);
                droneBL.Status = DroneStatus.Maintenance;
                BO.Station Station = GetSpesificStation(stationID);
                DO.Station station = ConvertBLStationToDAL(Station);
                droneBL.Location = new Location { Longitude = station.Longitude, Latitude = station.Latitude };
            }
            catch (InvalidObjException e) { throw e; }
            dronesList.Add(droneBL);
            AddDroneDal(id, model, maxWeight);
            return "Drone added successfully!";
        }

        /// <summary>
        /// Function for update the drone name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string UpdateDroneData(int id, string model = "")
        {
            if (model != "")
            {
                Drone drone = GetSpesificDrone(id);
                drone.Model = model;
                UpdateDrone(drone);
            }
            return "The update was successful!";
        }

        /// <summary>
        /// Convert from dal drone to BL drone
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone ConvertDalDroneToBL(DO.Drone d)
        {
            return new Drone
            {
                ID = d.ID,
                MaxWeight = d.MaxWeight,
                Model = d.Model
            };
        }

        DroneInCharge ConvertDalDroneChargeToBL(DO.DroneCharge d)
        {
            return new DroneInCharge(d.DroneId, GetSpesificDrone(d.DroneId).Battery, d.DroneEnterToCharge);
        }

        /// <summary>
        /// Convert from bl drone to dal drone
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Drone ConvertBLDroneToDAL(Drone d)
        {
            return new DO.Drone
            {
                ID = d.ID,
                MaxWeight = d.MaxWeight,
                Model = d.Model
            };
        }

        /// <summary>
        /// update drone in Bl and in DAl
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string UpdateDrone(Drone droneBL)
        {
            int idx = dronesList.FindIndex(d => d.ID == droneBL.ID);
            dronesList[idx] = droneBL;
            lock (dal)
            {
                dal.UpdateDrone(ConvertBLDroneToDAL(droneBL));
                return "The update was successful!";
            }
        }

        /// <summary>
        /// Returning a specific drone by ID number
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetSpesificDrone(int droneId)
        {
            try
            {
                return dronesList.Find(d => d.ID == droneId);
            }
            catch (Exception)
            {
                throw new ObjectNotExistException($"There is no drone with ID - {droneId}");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneInCharge GetSpecificDroneInCharge(int droneId)
        {
            try
            {
                lock (dal)
                {
                    return ConvertDalDroneChargeToBL(dal.GetDroneCharges().First(d => d.DroneId == droneId));
                }
            }
            catch (Exception)
            {
                throw new ObjectNotExistException($"There is no drone with ID - {droneId}");
            }
        }

        /// <summary>
        /// Returning the drone list from DAL
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDalDronesListAsBL()
        {
            lock (dal)
            {
                List<DO.Drone> dronesDal = dal.GetDrones().ToList();
                List<Drone> dronesBL = new List<Drone>();
                dronesDal.ForEach(d => dronesBL.Add(ConvertDalDroneToBL(d)));
                return dronesBL;
            }
        }

        /// <summary>
        /// Returning the drone list from BL
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDronesList(Predicate<Drone> condition = null)
        {
            condition ??= (d => true);

            return from d in dronesList
                   where condition(d)
                   select d;
        }

        /// <summary>
        /// Returns the drone list with DroneToList
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetDronesToList(Predicate<Drone> condition = null)
        {
            condition ??= (dc => true);
            return from dc in dronesList
                   where condition(dc)
                   select new DroneToList(dc);
        }

        /// <summary>
        /// The function returns the total battery usage
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="targetId"></param>
        /// <param name="parcelweight"></param>
        /// <param name="droneLocation"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double TotalBatteryUsage(int senderId, int targetId, int parcelweight, Location droneLocation)
        {
            lock (dal)
            {
                return (
              (Distance(droneLocation, GetSpesificCustomer(senderId).Location) * dal.ElectricalPowerRequest()[0])
            + (Distance(GetSpesificCustomer(senderId).Location, GetSpesificCustomer(targetId).Location) * dal.ElectricalPowerRequest()[parcelweight])
            + (Distance(GetSpesificCustomer(targetId).Location, GetNearestAvailableStation(GetSpesificCustomer(targetId).Location).Location) * dal.ElectricalPowerRequest()[0]));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double TotalBatteryToDestination(Location droneLocation, Location targetLocation, int parcelweight = 0)
        {
            lock (dal)
            {
                return Distance(droneLocation, targetLocation) * dal.ElectricalPowerRequest()[parcelweight];
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Location Totalprogress(Location current, Location target, Location source)
        {
            double longitudeDistance = Math.Abs(source.Longitude - target.Longitude) / 100;
            if (current.Longitude <= target.Longitude)
                current.Longitude += longitudeDistance;
            else
                current.Longitude -= longitudeDistance;

            double latitudeDistance = Math.Abs(current.Latitude - target.Latitude) / 100;
            if (current.Latitude <= target.Latitude)
                current.Latitude += latitudeDistance;
            else
                current.Latitude -= latitudeDistance;

            return new Location() { Latitude = Math.Round(current.Latitude, 3), Longitude = Math.Round(current.Longitude, 3) };
        }
    }
}





