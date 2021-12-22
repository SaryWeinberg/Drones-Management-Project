using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
/*using IDAL;*/
using DalApi;


namespace DalObject
{
    partial class DalObject : IDal
    {
        /// <summary>
        /// Adding new drone to DataBase
        /// </summary>
        /// <param name="drone"></param>
        public void AddDrone(Drone drone)
        {
            DataSource.Drones.Add(drone);
        }

        /// <summary>
        /// Adding new droneCharge to DataBase
        /// </summary>
        /// <param name="droneCharge"></param>
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            droneCharge.Active = true;
            DataSource.DroneCharges.Add(droneCharge);
        }

        /// <summary>
        /// Removing a drone charge from the DataBase
        /// </summary>
        /// <param name="droneId"></param>
        public void RemoveDroneInCharge(int droneId)
        {
            DataSource.DroneCharges.RemoveAt(DataSource.DroneCharges.FindIndex(Dich => Dich.DroneId == droneId));
        }

        /// <summary>
        /// Update drone in DataBase
        /// </summary>
        /// <param name="drone"></param>
        public void UpdateDrone(Drone drone)
        {
            int index = DataSource.Drones.FindIndex(d => d.ID == drone.ID);
            DataSource.Drones[index] = drone;
        }       
        
        /// <summary>
        /// Returns the drone list
        /// </summary>
        /// <returns></returns>
        public List<Drone> GetDrones()
        {
            return DataSource.Drones;
        }

        /// <summary>
        /// Returns the drone charge list
        /// </summary>
        /// <returns></returns>
        public List<DroneCharge> GetDroneCharges()
        {
            return DataSource.DroneCharges;
        }
    }
}
