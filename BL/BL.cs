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

        Random rand = new Random();
        List<DroneBL> dronesBList = new List<DroneBL>();

        public BL()
        {
            dalObj = new DalObject.DalObject();
            double[] ElectricUse = dalObj.ElectricalPowerRequest();
            double Available = ElectricUse[0];
            double Light = ElectricUse[1];
            double medium = ElectricUse[2];
            double heavy = ElectricUse[3];
            double chargingRate = ElectricUse[4];
            dronesBList = GetDronesBL();
            List<Parcel> parcels = dalObj.GetParcels();
            foreach (DroneBL drone in dronesBList)
            {
                drone.Location = new Location { Longitude = rand.Next(0, 40), Latitude = rand.Next(0, 40) };

                Parcel parcel = parcels.Find(p => p.DroneId == drone.ID);//לעשות כאן בדיקה
                if (parcels.Any(p => p.DroneId == drone.ID) && parcel.Delivered == DateTime.MinValue)//ישנה חבילה ששויכה אך לא סופקה
                {

                    if (parcel.PickedUp == DateTime.MinValue)//חבילה שלא נאספה
                    {
                        drone.Location = GetNearestAvailableStation(GetSpesificCustomerBL(parcel.SenderId).Location).Location;
                    }
                    else//חייב להיות חבילה שלא סופקה 
                    {

                        drone.Location = GetSpesificCustomerBL(parcel.SenderId).Location;
                    }

                    drone.BatteryStatus = rand.Next((int)TotalBatteryUsage(parcel.SenderId, parcel.TargetId, (int)parcel.Weight, drone.Location), 100);
                    drone.Status = DroneStatus.Delivery;
                }
                else
                {
                    drone.Status = (DroneStatus)rand.Next(0, 2);
                    if (drone.Status == 0)
                    {

                        List<Parcel> parcelProvided = parcels.FindAll(p => p.PickedUp > new DateTime());
                        double randIDX = rand.Next(0, parcelProvided.Count() - 1);
                        drone.Location = GetSpesificCustomerBL(parcelProvided[(int)randIDX].TargetId).Location;

                        drone.BatteryStatus = rand.Next((int)(Distance(GetNearestAvailableStation(drone.Location).Location, drone.Location) * Available), 100);
                    }
                    else
                    {
                        drone.BatteryStatus = rand.Next(1, 20);
                        List<StationBL> stationBLs = GetStationsBL();
                        drone.Location = stationBLs[rand.Next(stationBLs.Count())].Location;
                    }
                }
            }
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

        /// <summary>
        /// function for Sending a drone for charging
        /// </summary>
        /// <param name="droneId"></param>
        public string SendDroneToCharge(int droneId)
        {
            DroneCharge droneCharge = new DroneCharge();
            DroneBL drone = GetSpesificDroneBL(droneId);
            if (drone.Status != DroneStatus.Available)
            {
                throw new TheDroneNotAvailableException();
            }
            StationBL station = GetNearestAvailableStation(drone.Location);
            if (dalObj.ElectricalPowerRequest()[0] * Distance(drone.Location, station.Location) > drone.BatteryStatus)
            {
                throw new NoBatteryToReachChargingStationException();
            }
            drone.BatteryStatus -= dalObj.ElectricalPowerRequest()[0] * Distance(drone.Location, station.Location);
            drone.Location = station.Location;
            drone.Status = DroneStatus.Maintenance;
            station.AveChargeSlots -= 1;
            droneCharge.StationId = station.ID;
            droneCharge.DroneId = drone.ID;
            dalObj.UpdateDrone(ConvertBLDroneToDAL(drone));
            dalObj.UpdateStation(ConvertBLStationToDAL(station));
            dalObj.AddDroneCharge(droneCharge);
            return "The drone was sent for charging successfully!";
        }

        /// <summary>
        /// Function for releasing drone from charging
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="timeInCharge"></param>
        public string ReleaseDroneFromCharge(int droneId, int timeInCharge)
        {
            DroneBL droneBL = GetSpesificDroneBL(droneId);
            if (droneBL.Status != DroneStatus.Maintenance)
            {
                throw new TheDroneNotInChargeException();
            }
            if (dalObj.ElectricalPowerRequest()[4] * timeInCharge + droneBL.BatteryStatus >= 100)
            { droneBL.BatteryStatus = 100; }
            else { droneBL.BatteryStatus += dalObj.ElectricalPowerRequest()[4] * timeInCharge; }
            droneBL.Status = DroneStatus.Available;
            List<Station> stations = dalObj.GetStations();
            Station station = stations.Find(s => s.Latitude == droneBL.Location.Latitude && s.Longitude == droneBL.Location.Longitude);
            station.ChargeSlots += 1;
            UpdateDrone(droneBL);
            dalObj.UpdateStation(station);
            dalObj.RemoveDroneInCharge(droneId);
            return "The drone was successfully released from charging!";
        }

        /// <summary>
        /// Function for assigning a parcel to a drone
        /// </summary>
        /// <param name="droneId"></param>
/*        public string AssignParcelToDrone(int droneId)
        {
            DroneBL droneBL = GetSpesificDroneBL(droneId);

            if (droneBL.Status != DroneStatus.Available)
            {
                throw new TheDroneNotAvailableException();
            }
            List<ParcelBL> parcels = GetParcelsBL();
            ParcelBL BestParcel = new ParcelBL { Weight = droneBL.MaxWeight, Priority = Priorities.Normal, Sender = parcels[parcels.Count() - 1].Sender, Target = parcels[parcels.Count() - 1].Sender };
            foreach (ParcelBL parcel in parcels)
            {
                if (parcel.Weight <= droneBL.MaxWeight && BestParcel.Priority <= parcel.Priority)
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

                                    if (TotalBatteryUsage(parcel.Sender.ID, parcel.Target.ID, (int)parcel.Weight, droneBL.Location) < droneBL.BatteryStatus)
                                    {
                                        BestParcel = parcel;
                                        droneBL.Status = DroneStatus.Delivery;
                                        DroneInParcel droneInP = new DroneInParcel { ID = droneBL.ID, BatteryStatus = droneBL.BatteryStatus, Location = droneBL.Location };
                                        *//*                 BestParcel.Drone.ID = droneBL.ID;
                                                               BestParcel.Drone.Location = droneBL.Location;
                                                BestParcel.Drone.BatteryStatus = droneBL.BatteryStatus;*//*
                                        BestParcel.Drone = droneInP;
                                        BestParcel.Associated = DateTime.Now;
                                        dalObj.UpdateParcel(ConvertBLParcelToDAL(BestParcel));
                                        return "The parcel was successfully associated with the drone!";
                                    }
                                }
                            }
                        }
                    }
                }
            }


            throw new CanNotAssignParcelToDroneException();
        }*/



        public string AssignParcelToDrone(int droneId)
        {
            DroneBL droneBL = GetSpesificDroneBL(droneId);

            if (droneBL.Status != DroneStatus.Available)
            {
                throw new TheDroneNotAvailableException();
            }
            List<ParcelBL> parcels = GetParcelsBL();
            int flag = 0;
            double bestDistance = 100;
            ParcelBL BestParcel = new ParcelBL { Weight = droneBL.MaxWeight, Priority = Priorities.Normal, Sender = parcels[parcels.Count() - 1].Sender, Target = parcels[parcels.Count() - 1].Sender };
            foreach (ParcelBL parcel in parcels)
            {
                if (parcel.Weight <= droneBL.MaxWeight && TotalBatteryUsage(parcel.Sender.ID, parcel.Target.ID, (int)parcel.Weight, droneBL.Location) < droneBL.BatteryStatus)
                {
                    if (BestParcel.Priority <= parcel.Priority)
                    {
                        if (BestParcel.Priority < parcel.Priority)
                        {
                            flag = 1;
                            BestParcel = parcel;
                        }
                        else
                        {
                            if (parcel.Weight >= BestParcel.Weight)
                            {
                                if (parcel.Weight > BestParcel.Weight)
                                {
                                    flag = 1;
                                    BestParcel = parcel;
                                }
                                else
                                {
                                    if (Distance(droneBL.Location, GetSpesificCustomerBL(parcel.Sender.ID).Location) <= bestDistance)
                                    {//מרחק חבילה הנוכחית לעומת הטובה ביותר
                                        BestParcel = parcel;
                                        bestDistance = Distance(droneBL.Location, GetSpesificCustomerBL(BestParcel.Sender.ID).Location);
                                        flag = 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (flag == 1)
            {

                droneBL.Status = DroneStatus.Delivery;
                DroneInParcel droneInP = new DroneInParcel { ID = droneBL.ID, BatteryStatus = droneBL.BatteryStatus, Location = droneBL.Location };
                /*                 BestParcel.Drone.ID = droneBL.ID;
                                       BestParcel.Drone.Location = droneBL.Location;
                        BestParcel.Drone.BatteryStatus = droneBL.BatteryStatus;*/
                BestParcel.Drone = droneInP;
                BestParcel.Associated = DateTime.Now;
                dalObj.UpdateParcel(ConvertBLParcelToDAL(BestParcel));
                return "The parcel was successfully associated with the drone!";
            }
            throw new CanNotAssignParcelToDroneException();

        }







        /// <summary>
        /// Function for collecting a parcel by drone
        /// </summary>
        /// <param name="droneId"></param>
        public string CollectParcelByDrone(int droneId)
        {
            DroneBL droneBL = GetSpesificDroneBL(droneId);
            List<ParcelBL> parcels = GetParcelsBL();
            foreach (ParcelBL currentParcel in parcels)
            {
                if (currentParcel.Drone.ID == droneId || currentParcel.Associated != DateTime.MinValue || currentParcel.PickedUp == DateTime.MinValue)
                {
                    throw new TheParcelCouldNotCollectedOrDeliveredException(currentParcel.ID, "collected");
                }
                List<CustomerBL> customers = GetCustomersBL();
                CustomerBL senderCustomer = customers.Find(c => c.ID == currentParcel.Sender.ID);
                droneBL.BatteryStatus = Distance(droneBL.Location, senderCustomer.Location) * dalObj.ElectricalPowerRequest()[(int)droneBL.MaxWeight];
                droneBL.Location = senderCustomer.Location;
                currentParcel.PickedUp = DateTime.Now;
                dalObj.UpdateParcel(ConvertBLParcelToDAL(currentParcel));
                return "The parcel was successfully collected by the drone!";
            }
            return "";
        }

        /// <summary>
        /// Function for delivering a parcel by drone
        /// </summary>
        /// <param name="droneId"></param>
        public string DeliveryParcelByDrone(int droneId)
        {
            DroneBL droneBL = GetSpesificDroneBL(droneId);
            List<ParcelBL> parcels = GetParcelsBL();
            foreach (ParcelBL currentParcel in parcels)
            {
                if (currentParcel.Drone.ID != droneId || currentParcel.PickedUp == DateTime.MinValue || currentParcel.Delivered != DateTime.MinValue)
                {
                    throw new TheParcelCouldNotCollectedOrDeliveredException(currentParcel.ID, "delivered");
                }
                List<CustomerBL> customers = GetCustomersBL();
                CustomerBL targetCustomer = customers.Find(c => c.ID == currentParcel.Sender.ID);
                droneBL.BatteryStatus = Distance(droneBL.Location, targetCustomer.Location) * dalObj.ElectricalPowerRequest()[(int)droneBL.MaxWeight];
                droneBL.Location = targetCustomer.Location;
                droneBL.Status = DroneStatus.Available;
                currentParcel.Delivered = DateTime.Now;
                dalObj.UpdateParcel(ConvertBLParcelToDAL(currentParcel));
                return "The parcel was successfully delivered by the drone!";
            }
            return "";
        }
    }
}
