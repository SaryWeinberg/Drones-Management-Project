using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
/*using IDAL;*/
using DalApi;



namespace BL
{
    public partial class BL : BLApi.IBL
    {
        /*   IDal dalObj;*/
        IDal dalObj;

        Random rand = new Random();
        List<BO.Drone> dronesBLList = new List<BO.Drone>();


        internal static BL instance;

        public static BL GetInstance {
            get
            {

                if (instance == null)
                    instance = new BL();
                return instance;
            }

        }

        BL()
        {
            /*            dalObj = new DalObject.DalObject();*/
            dalObj = DalObject.DalFactory.GetDal("object");
            double[] ElectricUse = dalObj.ElectricalPowerRequest();
            double Available = ElectricUse[0];
            double Light = ElectricUse[1];
            double medium = ElectricUse[2];
            double heavy = ElectricUse[3];
            double chargingRate = ElectricUse[4];
            dronesBLList = GetDronesBL();
            List<DO.Parcel> parcels = dalObj.GetParcels();

            foreach (BO.Drone drone in dronesBLList)
            {
                drone.Location = new Location { Longitude = rand.Next(0, 40), Latitude = rand.Next(0, 40) };
                DO.Parcel parcel = parcels.Find(p => p.DroneId == drone.ID);

                //There is a parcel that has been associated but not delivered
                if (parcels.Any(p => p.DroneId == drone.ID) && parcel.Delivered == null)
                {
                    //Package not collected
                    if (parcel.PickedUp ==null)
                    {
                        drone.Location = GetNearestAvailableStation(GetSpesificCustomerBL(parcel.SenderId).Location).Location;
                    }
                    //There must be a package that is not delivered
                    else
                    {
                        drone.Location = GetSpesificCustomerBL(parcel.SenderId).Location;
                    }

                    drone.BatteryStatus = rand.Next((int)TotalBatteryUsage(parcel.SenderId, parcel.TargetId, (int)parcel.Weight, drone.Location), 100);
                    drone.Status = DroneStatus.Delivery;
                    ParcelByDelivery parcelBD = new ParcelByDelivery();
                    parcelBD.ID = parcel.ID;
                    drone.Parcel = parcelBD;
                    
                    
                    
                }
                else
                {
                    drone.Status = (DroneStatus)rand.Next(0, 2);

                    if (drone.Status == 0)
                    {
                        List<DO.Parcel> parcelProvided = parcels.FindAll(p => p.PickedUp != null);
                        double randIDX = rand.Next(0, parcelProvided.Count() - 1);
                        drone.Location = GetSpesificCustomerBL(parcelProvided[(int)randIDX].TargetId).Location;
                        drone.BatteryStatus = rand.Next((int)(Distance(GetNearestAvailableStation(drone.Location).Location, drone.Location) * Available), 100);
                    }
                    else
                    {
                        drone.BatteryStatus = rand.Next(1, 20);
                        List<BO.Station> stationBLs = GetStationsBL();
                        int stationId = rand.Next(stationBLs.Count());
                        drone.Location = stationBLs[stationId].Location;
                     
                        AddDroneChargeDAL(stationId, drone.ID);
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
            BO.Drone drone = GetSpesificDroneBL(droneId);

            if (drone.Status != DroneStatus.Available)
            {
                throw new TheDroneNotAvailableException();
            }
            BO.Station station = GetNearestAvailableStation(drone.Location);

            //If there is not enough battery until you reach the station
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
            BO.Drone droneBL = GetSpesificDroneBL(droneId);
            if (droneBL.Status != DroneStatus.Maintenance)
            {
                throw new TheDroneNotInChargeException();
            }

            //If the charge exceeds one hundred percent, only 100 are charged
            if (dalObj.ElectricalPowerRequest()[4] * timeInCharge + droneBL.BatteryStatus >= 100)
            {
                droneBL.BatteryStatus = 100;
            }
            else
            {
                droneBL.BatteryStatus += dalObj.ElectricalPowerRequest()[4] * timeInCharge;
            }

            droneBL.Status = DroneStatus.Available;
            List<DO.Station> stations = dalObj.GetStations();
            DO.Station station = stations.Find(s => s.Latitude == droneBL.Location.Latitude && s.Longitude == droneBL.Location.Longitude);
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
        public string AssignParcelToDrone(int droneId)
        {
            BO.Drone droneBL = GetSpesificDroneBL(droneId);

            if (droneBL.Status != DroneStatus.Available)
            {
                throw new TheDroneNotAvailableException();
            }

            List<BO.Parcel> parcels = GetParcelsBL();
            int flag = 0;
            double bestDistance = 100;

            //Set up a parcel with bad conditions to get to a parcel with the best conditions
            BO.Parcel BestParcel = new BO.Parcel { Weight = droneBL.MaxWeight, Priority = Priorities.Normal, Sender = parcels[parcels.Count() - 1].Sender, Target = parcels[parcels.Count() - 1].Sender };
            foreach (BO.Parcel parcel in parcels)
            {
                //Checking priorities for associating a parcel with a drone:
                //First thing is checking if the parcel at all meets the drone's options
                if (parcel.Weight <= droneBL.MaxWeight && TotalBatteryUsage(parcel.Sender.ID, parcel.Target.ID, (int)parcel.Weight, droneBL.Location) < droneBL.BatteryStatus)
                {
                    //1. In the highest priority
                    if (BestParcel.Priority <= parcel.Priority)
                    {
                        if (BestParcel.Priority < parcel.Priority)
                        {
                            flag = 1;
                            BestParcel = parcel;
                        }
                        else
                        {
                            //2. Maximum parcel weight possible for drone                            
                            if (parcel.Weight >= BestParcel.Weight)
                            {
                                if (parcel.Weight > BestParcel.Weight)
                                {
                                    flag = 1;
                                    BestParcel = parcel;
                                }
                                else
                                {
                                    //3. Very nearest package
                                    if (Distance(droneBL.Location, this.GetSpesificCustomerBL(parcel.Sender.ID).Location) <= bestDistance)
                                    {
                                        BestParcel = parcel;
                                        bestDistance = Distance(droneBL.Location, this.GetSpesificCustomerBL(BestParcel.Sender.ID).Location);
                                        flag = 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //Finding the parcel with the best conditions
            if (flag == 1)
            {
                droneBL.Status = DroneStatus.Delivery;
                DroneInParcel droneInP = new DroneInParcel { ID = droneBL.ID, BatteryStatus = droneBL.BatteryStatus, Location = droneBL.Location };
                BestParcel.Drone = droneInP;
                BestParcel.Associated = DateTime.Now;

                dalObj.UpdateParcel(ConvertBLParcelToDAL(BestParcel));
                return $"The parcel ID - {BestParcel.ID} was successfully associated with the drone!";
            }
            throw new CanNotAssignParcelToDroneException();
        }

        /// <summary>
        /// Function for collecting a parcel by drone
        /// </summary>
        /// <param name="droneId"></param>
        public string CollectParcelByDrone(int droneId)
        {
            BO.Drone droneBL = GetSpesificDroneBL(droneId);
            List<BO.Parcel> parcels = GetParcelsBL();
            foreach (BO.Parcel currentParcel in parcels)
            {
                if (currentParcel.Drone.ID == droneId)
                {
                    if (currentParcel.Associated == null && currentParcel.PickedUp != null)
                    {
                        throw new TheParcelCouldNotCollectedOrDeliveredException(currentParcel.ID, "collected");
                    }
                    //A parcel associated with the drone but not collected for it
                    List<BO.Customer> customers = GetCustomersBL();
                    BO.Customer senderCustomer = customers.Find(c => c.ID == currentParcel.Sender.ID);
                    droneBL.BatteryStatus = Distance(droneBL.Location, senderCustomer.Location) * dalObj.ElectricalPowerRequest()[(int)droneBL.MaxWeight];
                    droneBL.Location = senderCustomer.Location;
                    currentParcel.PickedUp = DateTime.Now;
                    dalObj.UpdateParcel(ConvertBLParcelToDAL(currentParcel));
                    return $"The parcel ID - {currentParcel.ID} was successfully collected by the drone!";
                }
            }
            return "";
        }

        /// <summary>
        /// Function for delivering a parcel by drone
        /// </summary>
        /// <param name="droneId"></param>
        public string DeliveryParcelByDrone(int droneId)
        {
            BO.Drone droneBL = GetSpesificDroneBL(droneId);
            List<BO.Parcel> parcels = GetParcelsBL();
            foreach (BO.Parcel currentParcel in parcels)
            {
                if (currentParcel.Drone.ID == droneId)
                {
                    if (currentParcel.PickedUp ==null && currentParcel.Delivered != null)
                    {
                        throw new TheParcelCouldNotCollectedOrDeliveredException(currentParcel.ID, "delivered");
                    }
                    //A parcel collected by a drone but not delivered by him
                    List<BO.Customer> customers = GetCustomersBL();
                    BO.Customer targetCustomer = customers.Find(c => c.ID == currentParcel.Sender.ID);
                    droneBL.BatteryStatus = Distance(droneBL.Location, targetCustomer.Location) * dalObj.ElectricalPowerRequest()[(int)droneBL.MaxWeight];
                    droneBL.Location = targetCustomer.Location;
                    droneBL.Status = DroneStatus.Available;
                    currentParcel.Delivered = DateTime.Now;
                    dalObj.UpdateParcel(ConvertBLParcelToDAL(currentParcel));
                    return $"The parcel ID - {currentParcel.ID} was successfully delivered by the drone!";
                }
            }
            return "";
        }
    }
}
