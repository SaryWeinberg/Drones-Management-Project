using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;


namespace DalObject
{
    public partial class DalObject : IDal
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
        /// Returns a specific drone by ID number
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        public Drone GetSpesificDrone(int droneId)
        {
            foreach (Drone d in DataSource.Drones)
            {
                if (d.ID == droneId)
                    return d;
            }
            throw new ObjectDoesNotExist("Drone", droneId);
        }

        /// <summary>
        /// Returns the list of drones one by one
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Drone> GetDroneLists()
        {
            foreach (Drone drone in DataSource.Drones)
            {
                yield return drone;
            }
        }

        /// <summary>
        /// Returns the drone list
        /// </summary>
        /// <returns></returns>
        public List<Drone> GetDrones()
        {
            return DataSource.Drones;
        }
    }
}
