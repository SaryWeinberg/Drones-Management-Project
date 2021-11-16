using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;


namespace BL
{
    public partial class BL : IBL.IBL
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
            int x1 = location1.Latitude;
            int x2 = location1.Longitude;
            int y1 = location2.Latitude;
            int y2 = location2.Longitude;
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        public void SendDroneToCharge(int droneId)
        {
            DroneCharge droneCharge = new DroneCharge();
            DroneBL drone = GetSpesificDroneBL(droneId);
            if (drone.Status != DroneStatus.Available)
            {
                throw new TheDroneNotAvailable();
            }
            StationBL station = GetNearestAvailableStation(drone.Location);
            drone.BatteryStatus -= dalObj.ElectricalPowerRequest()[0] * Distance(drone.Location, station.Location);
            drone.Location = station.Location;
            drone.Status = DroneStatus.Maintenance;
            station.AveChargeSlots -= 1;
            droneCharge.StationId = station.ID;
            droneCharge.DroneId = drone.ID;
            dalObj.UpdateDrone(ConvertBLDroneToDAL(drone));
            dalObj.UpdateStation(ConvertBLStationToDAL(station));
            dalObj.AddDroneCharge(droneCharge);
        }

        public void ReleaseDroneFromCharge(int droneId, int timeInCharge)
        {
            DroneBL droneBL = GetSpesificDroneBL(droneId);
            if (droneBL.Status != DroneStatus.Maintenance)
            {
                throw new TheDroneNotInCharge();
            }
            droneBL.BatteryStatus += dalObj.ElectricalPowerRequest()[4] * timeInCharge;
            droneBL.Status = DroneStatus.Available;
            List<Station> stations = dalObj.GetStations();
            Station station = stations.Find(s => s.Latitude == droneBL.Location.Latitude && s.Longitude == droneBL.Location.Longitude);
            station.ChargeSlots += 1;            
            //dalObj.RemoveDroneInCharge(droneId);
        }


        public void AssignParcelToDrone(int droneId)
        {
            DroneBL droneBL = GetSpesificDroneBL(droneId);

            if (droneBL.Status != DroneStatus.Available)
            {
                throw new TheDroneNotAvailable();
            }
            List<ParcelBL> parcels = GetParcelsBL();
            ParcelBL BestParcel = parcels[0];
            foreach (ParcelBL parcel in parcels)
            {
                if (parcel.Weight > droneBL.MaxWeight && BestParcel.Priority <= parcel.Priority)
                {
                    if (BestParcel.Priority < parcel.Priority)
                        BestParcel = parcel;
                    else
                    {
                        if (parcel.Weight >= BestParcel.Weight)
                        {
                            if (parcel.Weight > BestParcel.Weight)
                                BestParcel = parcel;
                            else
                            {
                                if (Distance(droneBL.Location, GetSpesificCustomerBL(parcel.Sender.ID).Location) <=//מרחק חבילה הנוכחית לעומת הטובה ביותר
                                Distance(droneBL.Location, GetSpesificCustomerBL(BestParcel.Sender.ID).Location))
                                {

                                    if (
                                        (Distance(droneBL.Location, GetSpesificCustomerBL(parcel.Sender.ID).Location)) * dalObj.ElectricalPowerRequest()[0]//מרחק שולח מהרחפן*צריכה כשהוא ריק 
                                        + (Distance(GetSpesificCustomerBL(parcel.Sender.ID).Location, GetSpesificCustomerBL(parcel.Target.ID).Location)) * dalObj.ElectricalPowerRequest()[(int)parcel.Weight]
                                        + (Distance(GetSpesificCustomerBL(parcel.Target.ID).Location, GetNearestAvailableStation(GetSpesificCustomerBL(parcel.Target.ID).Location).Location)) * dalObj.ElectricalPowerRequest()[0] < droneBL.BatteryStatus)
                                    {
                                        BestParcel = parcel;
                                        droneBL.Status = DroneStatus.Delivery;
                                        BestParcel.Drone.ID = droneBL.ID;
                                        BestParcel.Drone.Location = droneBL.Location;
                                        BestParcel.Drone.BettaryStatus = droneBL.BatteryStatus;
                                        BestParcel.Associated = DateTime.Now;
                                        dalObj.UpdateParcel(ConvertBLParcelToDAL(BestParcel));
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
            DroneBL droneBL = GetSpesificDroneBL(droneId);
            List<ParcelBL> parcels = GetParcelsBL();
            foreach (ParcelBL currentParcel in parcels)
            {
                if (currentParcel.Drone.ID == droneId || currentParcel.Associated != new DateTime() || currentParcel.PickedUp == new DateTime())
                {
                    throw new TheParcelCouldNotCollectedOrDelivered(currentParcel.ID, "collected");
                }
                List<CustomerBL> customers = GetCustomersBL();
                CustomerBL senderCustomer = customers.Find(c => c.ID == currentParcel.Sender.ID);
                droneBL.BatteryStatus = Distance(droneBL.Location, senderCustomer.Location) * dalObj.ElectricalPowerRequest()[(int)droneBL.MaxWeight];
                droneBL.Location = senderCustomer.Location;
                currentParcel.PickedUp = DateTime.Now;
                dalObj.UpdateParcel(ConvertBLParcelToDAL(currentParcel));
            }
        }

        public void DeliveryParcelByDrone(int droneId)
        {
            DroneBL droneBL = GetSpesificDroneBL(droneId);
            List<ParcelBL> parcels = GetParcelsBL();
            foreach (ParcelBL currentParcel in parcels)
            {
                if (currentParcel.Drone.ID != droneId || currentParcel.PickedUp == new DateTime() || currentParcel.Delivered != new DateTime())
                {
                    throw new TheParcelCouldNotCollectedOrDelivered(currentParcel.ID, "delivered");
                }
                List<CustomerBL> customers = GetCustomersBL();
                CustomerBL targetCustomer = customers.Find(c => c.ID == currentParcel.Sender.ID);
                droneBL.BatteryStatus = Distance(droneBL.Location, targetCustomer.Location) * dalObj.ElectricalPowerRequest()[(int)droneBL.MaxWeight];
                droneBL.Location = targetCustomer.Location;
                droneBL.Status = DroneStatus.Available;
                currentParcel.Delivered = DateTime.Now;
                dalObj.UpdateParcel(ConvertBLParcelToDAL(currentParcel));
            }
        }
    }
}
