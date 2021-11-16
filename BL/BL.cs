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
        IDal dalObj;
        List<IBL.BO.DroneBL> droneBlList;
        Random rand = new Random();

        public BL()
        {
            dalObj = new DalObject.DalObject();
            double[] ElectricUse = dalObj.ElectricalPowerRequest();
            double Available = ElectricUse[0];
            double Light = ElectricUse[1];
            double medium = ElectricUse[2];
            double heavy = ElectricUse[3];
            double chargingRate = ElectricUse[4];
            /*List<Drone> DroneList = dalObj.getDrones();
            List<Station> StationList = dalObj.getStations();
            List<Parcel> ParcelList = dalObj.getParcels();
            List<Customer> CustomerList = dalObj.getCustomers();*/
        }

        /// <summary>
        /// Function for checking the correctness of an ID number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool ValidateIDNumber(int id)
        {
            int sum = 0, digit;
            for (int i = 0; i < 9; i++)
            {
                digit = (int)(id % 10);
                if ((i % 2) == 0)
                    sum += digit * 2;
                else
                    sum += digit;
                id /= 10;
            }
            if ((sum % 10) == 0)
                return true;
            return false;
        }

        /// <summary>
        /// Function that finds a distance between 2 points
        /// </summary>
        /// <param name="location1"></param>
        /// <param name="location2"></param>
        /// <returns></returns>
        public static double Distance(Location location1, Location location2)
        {
            int x1 = location1.latitude;
            int x2 = location1.longitude;
            int y1 = location2.latitude;
            int y2 = location2.longitude;
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        public void SendDroneToCharge(int droneId)
        {
            DroneCharge droneCharge = new DroneCharge();
            DroneBL drone = GetSpesificDroneBL(droneId);
            if (drone.status != DroneStatus.Available)
            {
                throw new TheDroneNotAvailable();
            }

            StationBL station = GetNearestAvailableStation(drone.location);
            drone.batteryStatus -= dalObj.ElectricalPowerRequest()[0] * Distance(drone.location, station.location);
            drone.location = station.location;
            drone.status = DroneStatus.Maintenance;

            station.aveChargeSlots -= 1;

            droneCharge.StationId = station.id;
            droneCharge.DroneId = drone.id;
            /*UpdateDrone(drone);*/

        }

        public void ReleaseDroneFromCharge(int droneId, int timeInCharge)
        {
            DroneBL droneBL = GetSpesificDroneBL(droneId);

            if (droneBL.status != DroneStatus.Maintenance)
            {
                throw new TheDroneNotInCharge();
            }
            droneBL.batteryStatus += dalObj.ElectricalPowerRequest()[4] * timeInCharge;
            droneBL.status = DroneStatus.Available;

            List<Station> stations = dalObj.GetStations();
            //try

            Station station = stations.Find(s => s.latitude == droneBL.location.latitude && s.longitude == droneBL.location.longitude);
            station.chargeSlots += 1;

            ///////////////////////////////////////////////////
            /*  }
              throw new //זרוק שגיאה, לא קיימת תחנה כזאת
              }*/
            /////////////////////////////////////////
            ///
            dalObj.RemoveDroneInCharge(droneId);
        }


        public void AssignParcelToDrone(int droneId)
        {
            DroneBL droneBL = GetSpesificDroneBL(droneId);

            if (droneBL.status != DroneStatus.Available)
            {
                throw new TheDroneNotAvailable();
            }
            List<ParcelBL> parcels = GetParcelsBL();
            ParcelBL BestParcel = parcels[0];
            foreach (ParcelBL parcel in parcels)
            {
                if (parcel.weight > droneBL.maxWeight && BestParcel.priority <= parcel.priority)
                {
                    if (BestParcel.priority < parcel.priority)
                        BestParcel = parcel;
                    else
                    {
                        if (parcel.weight >= BestParcel.weight)
                        {
                            if (parcel.weight > BestParcel.weight)
                                BestParcel = parcel;
                            else
                            {
                                if (Distance(droneBL.location, GetSpesificCustomerBL(parcel.sender.id).location) <=//מרחק חבילה הנוכחית לעומת הטובה ביותר
                                Distance(droneBL.location, GetSpesificCustomerBL(BestParcel.sender.id).location))
                                {

                                    if (
                                        (Distance(droneBL.location, GetSpesificCustomerBL(parcel.sender.id).location)) * dalObj.ElectricalPowerRequest()[0]//מרחק שולח מהרחפן*צריכה כשהוא ריק 
                                        + (Distance(GetSpesificCustomerBL(parcel.sender.id).location, GetSpesificCustomerBL(parcel.target.id).location)) * dalObj.ElectricalPowerRequest()[(int)parcel.weight]
                                        + (Distance(GetSpesificCustomerBL(parcel.target.id).location, GetNearestAvailableStation(GetSpesificCustomerBL(parcel.target.id).location).location)) * dalObj.ElectricalPowerRequest()[0] < droneBL.batteryStatus)
                                    {
                                        BestParcel = parcel;
                                        droneBL.status = DroneStatus.Delivery;
                                        BestParcel.drone.id = droneBL.id;
                                        BestParcel.drone.location = droneBL.location;
                                        BestParcel.drone.bettaryStatus = droneBL.batteryStatus;
                                        BestParcel.associated = DateTime.Now;
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }

            }

            throw new CanNotAssignParcelToDrone();
        }


        public void CollectParcelByDrone(int droneId)
        {

        }

        public void DeliveryParcelByDrone(int droneId)
        {

        }
    }
}
