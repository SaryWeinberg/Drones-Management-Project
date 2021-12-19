/*using DO;*/
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
        /// Functions for adding a drone to DAL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        public void AddDroneDal(int id, string model, WeightCategories maxWeight)
        {
            DO.Drone droneDal = new DO.Drone();
            droneDal.ID = id;
            droneDal.Model = model;
            droneDal.MaxWeight = maxWeight;
            dalObj.AddDrone(droneDal);
        }

        /// <summary>
        /// Functions for adding a droneCharge to DAL
        /// </summary>
        /// <param name="stationID"></param>
        public void AddDroneChargeDAL(int stationID, int DroneID)
        {
            DO.DroneCharge droneCharge = new DO.DroneCharge();
            droneCharge.DroneId = DroneID;
            droneCharge.StationId = stationID;
            dalObj.AddDroneCharge(droneCharge);
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
        public string AddDroneBL(int id, string model, WeightCategories maxWeight, int stationID)
        {
            if (GetDronesBLList().Any(d => d.ID == id))
            {
                throw new ObjectAlreadyExistException("drone", id);
            }
            Drone droneBL = new Drone();
            try
            {
                droneBL.ID = id;
                droneBL.Model = model;
                droneBL.MaxWeight = maxWeight;
                droneBL.BatteryStatus = rand.Next(20, 40);
                droneBL.Status = DroneStatus.Maintenance;
                BO.Station Station = GetSpesificStationBL(stationID);
                DO.Station station = ConvertBLStationToDAL(Station);
/*                Station station = dalObj.GetSpesificStation(stationID);*/
                Location Slocation = new Location();
                Slocation.Longitude = station.Longitude;
                Slocation.Latitude = station.Latitude;
                droneBL.Location = Slocation;
            }
            catch (InvalidObjException e) { throw e; }
            dronesBLList.Add(droneBL);
            AddDroneDal(id, model, maxWeight);
            AddDroneChargeDAL(stationID, id);
            return "Drone added successfully!";
        }

        /// <summary>
        /// Function for update the drone name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public string UpdateDroneData(int id, string model = "")
        {
            if (model != "")
            {
                Drone drone = GetSpesificDroneBL(id);
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
        public Drone ConvertDalDroneToBL(DO.Drone d)
        {
            return new Drone
            {
                ID = d.ID,
                MaxWeight = d.MaxWeight,
                Model = d.Model
            };
        }

        /// <summary>
        /// Convert from bl drone to dal drone
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
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

        public string UpdateDrone(Drone droneBL)
        {
            int idx = dronesBLList.FindIndex(d => d.ID == droneBL.ID);
            dronesBLList[idx] = droneBL;
            dalObj.UpdateDrone(ConvertBLDroneToDAL(droneBL));
            return "The update was successful!";
        }

        /// <summary>
        /// Returning a specific drone by ID number
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        public Drone GetSpesificDroneBL(int droneId)
        {
            try
            {
                return dronesBLList.Find(d => d.ID == droneId);
            }
            catch (ArgumentNullException)
            {
                throw new ObjectNotExistException($"There is no drone with ID - {droneId}");
            }
        }

        /// <summary>
        /// Returning the drone list from DAL
        /// </summary>
        /// <returns></returns>
        public List<Drone> GetDronesBL()
        {
            List<DO.Drone> dronesDal = dalObj.GetDrones();
            List<Drone> dronesBL = new List<Drone>();
            dronesDal.ForEach(d => dronesBL.Add(ConvertDalDroneToBL(d)));
            return dronesBL;



        }

        /// <summary>
        /// Returning the drone list from BL
        /// </summary>
        /// <returns></returns>
        public List<Drone> GetDronesBLList()
        {
            return dronesBLList;
        }

        public IEnumerable<Drone> GetDronesByCondition(Predicate<Drone> condition)
        {
            return from droneBL in GetDronesBLList()
                   where condition(droneBL)
                   select droneBL;
        }

        /// <summary>
        /// The function returns the total battery usage
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="targetId"></param>
        /// <param name="parcelweight"></param>
        /// <param name="droneLocation"></param>
        /// <returns></returns>
        public double TotalBatteryUsage(int senderId, int targetId, int parcelweight, Location droneLocation)
        {
            return ((Distance(droneLocation,
            GetSpesificCustomerBL(senderId).Location) * dalObj.ElectricalPowerRequest()[0])//מרחק שולח מהרחפן*צריכה כשהוא ריק 
            + (Distance(GetSpesificCustomerBL(senderId).Location, GetSpesificCustomerBL(targetId).Location) * dalObj.ElectricalPowerRequest()[parcelweight])
            + (Distance(GetSpesificCustomerBL(targetId).Location, GetNearestAvailableStation(GetSpesificCustomerBL(targetId).Location).Location) * dalObj.ElectricalPowerRequest()[0]));
        }
    }
}





