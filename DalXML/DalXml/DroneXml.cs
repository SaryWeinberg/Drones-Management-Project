using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
using System.Runtime.CompilerServices;
namespace Dal
{
    public partial class DalXml : IDal
    {
        /// <summary>
        /// Adding new drone to DataBase
        /// </summary>
        /// <param name="drone"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(Drone drone)
        {
            List<Drone> droneList = GetDrones().ToList();
            droneList.Add(drone);
            XmlTools.SaveListToXmlSerializer(droneList, direction + droneFilePath);
        }

        /// <summary>
        /// Adding new droneCharge to DataBase
        /// </summary>
        /// <param name="droneCharge"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            List<DroneCharge> droneChargeList = GetDroneCharges().ToList();
            droneChargeList.Add(droneCharge);
            XmlTools.SaveListToXmlSerializer(droneChargeList, direction + droneChargeFilePath);
        }

        /// <summary>
        /// Update drone in DataBase
        /// </summary>
        /// <param name="drone"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveDroneInCharge(int droneId)
        {
            IEnumerable<DroneCharge> DroneChargeList = GetDroneCharges();
            DroneCharge DroneCharge = DroneChargeList.First(d => d.DroneId == droneId);
            List<DroneCharge> TempDroneChargeList = DroneChargeList.ToList();
            TempDroneChargeList.Remove(DroneCharge);
            XmlTools.SaveListToXmlSerializer(TempDroneChargeList, direction + droneChargeFilePath);
        }

        /// <summary>
        /// Returns the drone list
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDrones(Predicate<Drone> condition = null)
        {
            condition ??= (d => true);
            return from drone in XmlTools.LoadListFromXmlSerializer<Drone>(direction + droneFilePath)
                   where condition(drone)
                   orderby (drone.Model)
                   select drone;
        }

        /// <summary>
        /// Returns the drone charge list
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDroneCharges(Predicate<DroneCharge> condition = null)
        {
            condition ??= (d => true);
            return from droneCharge in XmlTools.LoadListFromXmlSerializer<DroneCharge>(direction + droneChargeFilePath)
                   where condition(droneCharge)
                   select droneCharge;
        }
    }
}