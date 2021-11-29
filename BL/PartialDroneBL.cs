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
        /// Functions for adding a drone to DAL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        public void AddDroneDal(int id, string model, WeightCategories maxWeight)
        {
            Drone droneDal = new Drone();
            droneDal.ID = id;
            droneDal.Model = model;
            droneDal.MaxWeight = maxWeight;
            dalObj.AddDrone(droneDal);
        }

        /// <summary>
        /// Functions for adding a droneCharge to DAL
        /// </summary>
        /// <param name="stationID"></param>
        public void AddDroneChargeDAL(int stationID)
        {
            DroneCharge droneCharge = new DroneCharge();
            droneCharge.DroneId = stationID;
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
            DroneBL droneBL = new DroneBL();
            try
            {
                droneBL.ID = id;
                droneBL.Model = model;
                droneBL.MaxWeight = maxWeight;
                droneBL.BatteryStatus = rand.Next(20, 40);
                droneBL.Status = DroneStatus.Maintenance;

                Station station = dalObj.GetSpesificStation(stationID);
                Location Slocation = new Location();
                Slocation.Longitude = station.Longitude;
                Slocation.Latitude = station.Latitude;
                droneBL.Location = Slocation;
            }
            catch (InvalidObjException e) { throw e; }
            AddDroneDal(id, model, maxWeight);
            AddDroneChargeDAL(stationID);
            return "Drone added successfully!";
        }

        /// <summary>
        /// Function for update the drone name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public string UpdateDroneName(int id, string model = "")
        {
            if (model != "")
            {
                DroneBL drone = GetSpesificDroneBL(id);
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
        public DroneBL ConvertDalDroneToBL(Drone d)
        {
            return new DroneBL
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
        public Drone ConvertBLDroneToDAL(DroneBL d)
        {
            return new Drone
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

        public string UpdateDrone(DroneBL droneBL)
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
        public DroneBL GetSpesificDroneBL(int droneId)
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
        public List<DroneBL> GetDronesBL()
        {
            List<Drone> dronesDal = dalObj.GetDrones();
            List<DroneBL> dronesBL = new List<DroneBL>();
            dronesDal.ForEach(d => dronesBL.Add(ConvertDalDroneToBL(d)));
            return dronesBL;
        }

        /// <summary>
        /// Returning the drone list from BL
        /// </summary>
        /// <returns></returns>
        public List<DroneBL> GetDronesBLList()
        {
            return dronesBLList;
        }

        public IEnumerable<DroneBL> GetDronesBy(Predicate<DroneBL> findBy)
        {
            return from droneBL in dronesBLList
                   where findBy(droneBL)
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





