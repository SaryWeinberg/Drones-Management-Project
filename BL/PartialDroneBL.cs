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
            droneDal.id = id;
            droneDal.model = model;
            droneDal.maxWeight = maxWeight;
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
        public void AddDroneBL(int id, string model, WeightCategories maxWeight, int stationID)
        {
            DroneBL droneBL = new DroneBL();
            try
            {
                droneBL.id = id;
                droneBL.model = model;
                droneBL.maxWeight = maxWeight;
                droneBL.batteryStatus = rand.Next(20, 40);
                droneBL.status = DroneStatus.Maintenance;

                Station station = dalObj.GetSpesificStation(id);
                droneBL.location.longitude = station.longitude;
                droneBL.location.latitude = station.latitude;
            }
            catch (InvalidID e)
            {
                throw e;
            }
            droneBlList.Add(droneBL);
            AddDroneDal(id, model, maxWeight);
            AddDroneChargeDAL(stationID);
        }

        /// <summary>
        /// Function for update the drone name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public void UpdateDroneName(int id, string model)
        {
            Drone drone = dalObj.GetSpesificDrone(id);
            drone.model = model;
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
                id = d.id,
                maxWeight = d.maxWeight,
                model = d.model
            };
            /*BatteryStatus*/
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
                return ConvertDalDroneToBL(dalObj.GetSpesificDrone(droneId));
            }
            catch (ObjectDoesNotExist e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Returning the drone list
        /// </summary>
        /// <returns></returns>
        public List<DroneBL> GetDronesBL()
        {
            List<Drone> dronesDal = dalObj.GetDrones();
            List<DroneBL> dronesBL = new List<DroneBL>();
            dronesDal.ForEach(d => dronesBL.Add(ConvertDalDroneToBL(d)));
            return dronesBL;
        }

    }
}
