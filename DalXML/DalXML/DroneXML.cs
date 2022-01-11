using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace DAL
{
    public partial class DalXml : IDal
    {
        public void AddDrone(Drone drone)
        {
            IEnumerable<Drone> droneList = GetDrones();
            droneList.ToList().Add(drone);
            XMLTools.SaveListToXMLSerializer(droneList, dir + droneFilePath);
        }
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            IEnumerable<DroneCharge> droneChargeList = GetDroneCharges();
            droneChargeList.ToList().Add(droneCharge);
            XMLTools.SaveListToXMLSerializer(droneChargeList, dir + parcelFilePath);
        }
        public void RemoveDroneInCharge(int droneId)
        {
            IEnumerable<Drone> droneList = GetDrones();
            Drone drone = GetDrones(d => d.ID == droneId).First();
            droneList.ToList().Remove(drone);
            XMLTools.SaveListToXMLSerializer(droneList, dir + droneFilePath);
        }
        public IEnumerable<Drone> GetDrones(Predicate<Drone> condition = null)
        {
            return from drone in XMLTools.LoadListFromXMLSerializer<Drone>(dir + droneFilePath)
                   where condition(drone)
                   select drone;
        }
        public IEnumerable<DroneCharge> GetDroneCharges(Predicate<DroneCharge> condition = null)
        {
            return from droneCharge in XMLTools.LoadListFromXMLSerializer<DroneCharge>(dir + droneChargeFilePath)
                   where condition(droneCharge)
                   select droneCharge;
        }
        public void UpdateDrone(Drone drone)
        {
            List<Drone> droneList = GetDrones().ToList();
            droneList[droneList.FindIndex(d => d.ID == drone.ID)] = drone;
            XMLTools.SaveListToXMLSerializer(droneList, dir + droneFilePath);
        }
    }
}
