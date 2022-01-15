using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal
{
    public partial class DalXml : IDal
    {
        /// <summary>
        /// Adding new drone to DataBase
        /// </summary>
        /// <param name="drone"></param>
        public void AddDrone(Drone drone)
        {
            IEnumerable<Drone> droneList = GetDrones();
            droneList.ToList().Add(drone);
            XmlTools.SaveListToXmlSerializer(droneList, direction + droneFilePath);
        }

        /// <summary>
        /// Adding new droneCharge to DataBase
        /// </summary>
        /// <param name="droneCharge"></param>
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            IEnumerable<DroneCharge> droneChargeList = GetDroneCharges();
            droneChargeList.ToList().Add(droneCharge);
            XmlTools.SaveListToXmlSerializer(droneChargeList, direction + droneChargeFilePath);
        }

        /// <summary>
        /// Update drone in DataBase
        /// </summary>
        /// <param name="drone"></param>
        public void UpdateDrone(Drone drone)
        {
            List<Drone> droneList = GetDrones().ToList();
            droneList[droneList.FindIndex(d => d.ID == drone.ID)] = drone;
            XmlTools.SaveListToXmlSerializer(droneList, direction + droneFilePath);
        }

        /// <summary>
        /// Removing a drone charge from the DataBase
        /// </summary>
        /// <param name="droneId"></param>
        public void RemoveDroneInCharge(int droneId)
        {
            IEnumerable<Drone> droneList = GetDrones();
            Drone drone = GetDrones(d => d.ID == droneId).First();
            droneList.ToList().Remove(drone);
            XmlTools.SaveListToXmlSerializer(droneList, direction + droneFilePath);
        }

        /// <summary>
        /// Returns the drone list
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<Drone> GetDrones(Predicate<Drone> condition = null)
        {
            condition ??= (d => true);
            return from drone in XmlTools.LoadListFromXmlSerializer<Drone>(direction + droneFilePath)
                   where condition(drone)
                   select drone;
        }

        /// <summary>
        /// Returns the drone charge list
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<DroneCharge> GetDroneCharges(Predicate<DroneCharge> condition = null)
        {
            condition ??= (d => true);
            return from droneCharge in XmlTools.LoadListFromXmlSerializer<DroneCharge>(direction + droneChargeFilePath)
                   where condition(droneCharge)
                   select droneCharge;
        }        
    }
}