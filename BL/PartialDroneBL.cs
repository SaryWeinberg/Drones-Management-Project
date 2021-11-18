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
            dalObj.AddDrone(droneDal.Clone());
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
            dalObj.AddDroneCharge(droneCharge.Clone());
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
            DroneBL droneBL = new DroneBL();
            try
            {
                droneBL.ID = id;
                droneBL.Model = model;
                droneBL.MaxWeight = maxWeight;
                droneBL.BatteryStatus = rand.Next(20, 40);
                droneBL.Status = DroneStatus.Maintenance;

                Station station = dalObj.GetSpesificStation(id);
                droneBL.Location.Longitude = station.Longitude;
                droneBL.Location.Latitude = station.Latitude;
            }
            catch (InvalidID e)
            {
                throw e;
            }
            droneBlList.Add(droneBL.Clone());
            AddDroneDal(id, model, maxWeight);
            AddDroneChargeDAL(stationID);
            return "Drone added successfully!";
        }

        /// <summary>
        /// Function for update the drone name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public string UpdateDroneName(int id, string model)
        {
            Drone drone = dalObj.GetSpesificDrone(id);
            drone.Model = model;
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
            /*BatteryStatus*/
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
            dronesDal.ForEach(d => dronesBL.Add(ConvertDalDroneToBL(d.Clone())));
            return dronesBL;
        }
    }
}
