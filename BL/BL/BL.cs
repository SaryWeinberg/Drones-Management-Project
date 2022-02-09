using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DalApi;
using System.Runtime.CompilerServices;


namespace BL
{
    sealed public partial class BL : BLApi.IBL
    {
        IDal dal;

   /*    internal IDal dalObj;*/

        Random rand = new Random();
        List<BO.Drone> dronesList = new List<BO.Drone>();

        private static BL instance;

        public static BL GetInstance {
            get
            {
                if (instance == null)
                    instance = new BL();
                return instance;
            }
        }

        private BL()
        {
            dal = DalFactory.GetDal();
            double[] ElectricUse = dal.ElectricalPowerRequest();
            double Available = ElectricUse[0];
            double Light = ElectricUse[1];
            double medium = ElectricUse[2];
            double heavy = ElectricUse[3];
            double chargingRate = ElectricUse[4];
            dronesList = GetDalDronesListAsBL().ToList();
            lock (dal)
            {
                List<DO.Parcel> parcels = dal.GetParcels().ToList();

                foreach (BO.Drone drone in dronesList)
                {
                    drone.Location = new Location { Longitude = rand.Next(0, 40), Latitude = rand.Next(0, 40) };
                    DO.Parcel parcel = parcels.Find(p => p.DroneId == drone.ID);

                    //There is a parcel that has been associated but not delivered
                    if (parcels.Any(p => p.DroneId == drone.ID) && parcel.Delivered == null && parcel.Associated != null)
                    {
                        //Package not collected
                        if (parcel.PickedUp == null)
                        {
                            drone.Location = GetNearestAvailableStation(GetSpesificCustomer(parcel.SenderId).Location).Location;
                        }
                        //There must be a package that is not delivered
                        else
                        {
                            drone.Location = GetSpesificCustomer(parcel.SenderId).Location;
                        }

                        drone.Battery = rand.Next((int)TotalBatteryUsage(parcel.SenderId, parcel.TargetId, (int)parcel.Weight, drone.Location), 100);
                        drone.Status = DroneStatus.Delivery;

                        ParcelByDelivery parcelBD = new ParcelByDelivery(GetSpesificParcel(parcel.ID), drone, GetSpesificCustomer(parcel.SenderId), GetSpesificCustomer(parcel.TargetId));
                        drone.Parcel = parcelBD;
                    }
                    else
                    {
                        drone.Status = (DroneStatus)rand.Next(0, 2);

                        if (drone.Status == 0)
                        {
                            List<DO.Parcel> parcelProvided = parcels.FindAll(p => p.PickedUp != null);
                            double randIDX = rand.Next(0, parcelProvided.Count() - 1);
                            drone.Location = GetSpesificCustomer(parcelProvided[(int)randIDX].TargetId).Location;
                            drone.Battery = rand.Next((int)(Distance(GetNearestAvailableStation(drone.Location).Location, drone.Location) * Available), 100);
                        }
                        else
                        {
                            drone.Battery = rand.Next(1, 20);
                            List<BO.Station> stationBLs = GetStations().ToList();
                            int stationId = rand.Next(1);
                            drone.Location = stationBLs[stationId].Location;
                            AddDroneCharge(stationId, drone.ID, drone.Battery);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Function for checking the correctness of an ID number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static bool ValidateIDNumber(int id)
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
        double Distance(Location location1, Location location2)
        {
            int x1 = location1.Latitude;
            int x2 = location1.Longitude;
            int y1 = location2.Latitude;
            int y2 = location2.Longitude;
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        internal double ElectricalPowerRequest(int parcelWeight)
        {
            
            return dal.ElectricalPowerRequest()[parcelWeight];
        }

        /// <summary>
        /// function for Sending a drone for charging
        /// </summary>
        /// <param name="droneId"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string SendDroneToCharge(int droneId)
        {
            //  DroneCharge droneCharge = new DroneCharge();
            BO.Drone drone = GetSpesificDrone(droneId);

            if (drone.Status != DroneStatus.Available)
            {
                throw new TheDroneNotAvailableException();
            }
            BO.Station station = GetNearestAvailableStation(drone.Location);
            lock (dal)
            {
                //If there is not enough battery until you reach the station
                if (dal.ElectricalPowerRequest()[0] * Distance(drone.Location, station.Location) > drone.Battery)
                {
                    throw new NoBatteryToReachChargingStationException();
                }

            }
            lock (dal)
            {
                drone.Battery -= Math.Round(dal.ElectricalPowerRequest()[0] * Distance(drone.Location, station.Location), 2);
            }
            drone.Location = station.Location;
            drone.Status = DroneStatus.Maintenance;
            /*      station.AveChargeSlots -= 1;*/
            AddDroneCharge(station.ID, drone.ID, drone.Battery);
            lock (dal)
            {
                dal.UpdateDrone(ConvertBLDroneToDAL(drone));
            }
            //     station.DronesInChargelist.Add(new DroneInCharge(drone.ID, drone.BatteryStatus));
            //      droneCharge.StationId = station.ID;
            //      droneCharge.DroneId = drone.ID;
            //    dalObj.AddDroneCharge(droneCharge);

            return "The drone was sent for charging successfully!";
        }

        /// <summary>
        /// Function for releasing drone from charging
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="timeInCharge"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string ReleaseDroneFromCharge(int droneId, int timeInCharge)
        {
            BO.Drone droneBL = GetSpesificDrone(droneId);
            if (droneBL.Status != DroneStatus.Maintenance)
            {
                throw new TheDroneNotInChargeException();
            }

            //If the charge exceeds one hundred percent, only 100 are charged
            lock (dal)
            {
                if (dal.ElectricalPowerRequest()[4] * timeInCharge + droneBL.Battery >= 100)
                {
                    droneBL.Battery = 100;
                }
                else
                {
                    droneBL.Battery += Math.Round(dal.ElectricalPowerRequest()[4] * timeInCharge, 2);
                }

                droneBL.Status = DroneStatus.Available;
                List<DO.Station> stations = dal.GetStations().ToList();
                DO.Station station = stations.Find(s => s.Latitude == droneBL.Location.Latitude && s.Longitude == droneBL.Location.Longitude);
                station.ChargeSlots += 1;

                UpdateDrone(droneBL);
                dal.UpdateStation(station);
                dal.RemoveDroneInCharge(droneId);
                return "The drone was successfully released from charging!";
            }
        }


        /// <summary>
        /// Function for assigning a parcel to a drone
        /// </summary>
        /// <param name="droneId"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string AssignParcelToDrone(int droneId)
        {
            BO.Drone droneBL = GetSpesificDrone(droneId);

            if (droneBL.Status != DroneStatus.Available)
            {
                throw new TheDroneNotAvailableException();
            }

            List<BO.Parcel> parcels = GetParcels(parcel => ConvertBLParcelToDAL(parcel).Active && parcel.Associated == null).ToList();
            if (!parcels.Any())
            {//no parcel wait to collect
                throw new ObjectDoesNotExist("parcel that wait to pack", 0);
            }
            int flag = 0;
            double bestDistance = 100;

            //Set up a parcel with bad conditions to get to a parcel with the best conditions
            BO.Parcel BestParcel = new BO.Parcel { Weight = droneBL.MaxWeight, Priority = Priorities.Normal, Sender = parcels[parcels.Count() - 1].Sender, Target = parcels[parcels.Count() - 1].Sender };
            foreach (BO.Parcel parcel in parcels)
            {
                //Checking priorities for associating a parcel with a drone:
                //First thing is checking if the parcel at all meets the drone's options
                if (parcel.Weight <= droneBL.MaxWeight && TotalBatteryUsage(parcel.Sender.ID, parcel.Target.ID, (int)parcel.Weight, droneBL.Location) < droneBL.Battery)
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
                                    if (Distance(droneBL.Location, GetSpesificCustomer(parcel.Sender.ID).Location) <= bestDistance)
                                    {
                                        BestParcel = parcel;
                                        bestDistance = Distance(droneBL.Location, GetSpesificCustomer(BestParcel.Sender.ID).Location);
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
                droneBL.Parcel = new ParcelByDelivery(BestParcel, droneBL, GetSpesificCustomer(BestParcel.Sender.ID), GetSpesificCustomer(BestParcel.Target.ID));
                DroneInParcel droneInP = new DroneInParcel { ID = droneBL.ID, Battery = droneBL.Battery, Location = droneBL.Location };
                BestParcel.Drone = droneInP;
                BestParcel.Associated = DateTime.Now;
                lock (dal)
                {
                    dal.UpdateParcel(ConvertBLParcelToDAL(BestParcel));
                    return $"The parcel ID - {BestParcel.ID} was successfully associated with the drone!";
                }
            }
            throw new CanNotAssignParcelToDroneException();
        }

        /// <summary>
        /// Function for collecting a parcel by drone
        /// </summary>
        /// <param name="droneId"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string CollectParcelByDrone(int droneId)
        {
            BO.Drone droneBL = GetSpesificDrone(droneId);

            if (droneBL.Status != DroneStatus.Delivery)
            {
                throw new TheDroneNotAvailableException();
            }

            try
            {
                lock (dal)
                {
                    BO.Parcel currentParcel = GetParcels().First(parcel => parcel.Drone.ID == droneId && parcel.Delivered == null && parcel.PickedUpByDrone == null && parcel.Associated != null);
                    List<BO.Customer> customers = GetCustomers().ToList();
                    BO.Customer senderCustomer = customers.Find(c => c.ID == currentParcel.Sender.ID);
                    droneBL.Battery = Math.Round(Distance(droneBL.Location, senderCustomer.Location) * dal.ElectricalPowerRequest()[(int)droneBL.MaxWeight], 2);
                    droneBL.Location = senderCustomer.Location;
                    
                    currentParcel.PickedUpByDrone = DateTime.Now;
                  

                    dal.UpdateParcel(ConvertBLParcelToDAL(currentParcel));
                    ParcelByDelivery parcelByDelivery = droneBL.Parcel;
                    parcelByDelivery.isWaitingToDelivery = false;
                    droneBL.Parcel = parcelByDelivery;
                    return $"The parcel ID - {currentParcel.ID} was successfully collected by the drone!";
                }
            }
            catch
            {
                throw new TheParcelCouldNotCollectedOrDeliveredException("collected");
            }
        }

        /// <summary>
        /// Function for supply a parcel by drone
        /// </summary>
        /// <param name="droneId"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string SupplyParcelByDrone(int droneId)
        {
            BO.Drone droneBL = GetSpesificDrone(droneId);

            if (droneBL.Status != DroneStatus.Delivery)
            {
                throw new TheDroneNotAvailableException();
            }

            try
            {
                BO.Parcel currentParcel = GetParcels().First(parcel => parcel.Drone.ID == droneId && parcel.Delivered == null && parcel.PickedUpByDrone != null && parcel.Associated != null);
                List<BO.Customer> customers = GetCustomers().ToList();
                BO.Customer TargetCustomer = customers.Find(c => c.ID == currentParcel.Target.ID);
                droneBL.Battery = Math.Round(Distance(droneBL.Location, TargetCustomer.Location) * dal.ElectricalPowerRequest()[(int)droneBL.MaxWeight], 2);
                droneBL.Location = TargetCustomer.Location;
                currentParcel.Delivered = DateTime.Now;
                droneBL.Status = DroneStatus.Available;
                lock (dal)
                {
                    droneBL.Parcel = null;
                    dal.UpdateParcel(ConvertBLParcelToDAL(currentParcel));

                    return $"The parcel ID - {currentParcel.ID} was successfully delivered by the drone!";
                }
            }
            catch
            {
                throw new TheParcelCouldNotCollectedOrDeliveredException("supplied");
            }



            /*            List<BO.Parcel> parcels = GetParcels();
                        foreach (BO.Parcel currentParcel in parcels)
                        {
                            if (currentParcel.Drone.ID == droneId)
                            {
                                if (currentParcel.PickedUp == null && currentParcel.Delivered != null)
                                {
                                    throw new TheParcelCouldNotCollectedOrDeliveredException(currentParcel.ID, "delivered");
                                }*/
            //A parcel collected by a drone but not delivered by him
            /*                    List<BO.Customer> customers = GetCustomers();
                                BO.Customer targetCustomer = customers.Find(c => c.ID == currentParcel.Sender.ID);*/
            /*                    droneBL.BatteryStatus = Distance(droneBL.Location, targetCustomer.location) * dalObj.ElectricalPowerRequest()[(int)droneBL.MaxWeight];
            *//*                    droneBL.Location = targetCustomer.location;
                                droneBL.Status = DroneStatus.Available;*/
            /*                currentParcel.Delivered = DateTime.Now;*/
            /*         dalObj.UpdateParcel(ConvertBLParcelToDAL(currentParcel));*/
            /*                    return $"The parcel ID - {currentParcel.ID} was successfully delivered by the drone!";
                            }
                        }*/
            /*  return "";*/
        }


        public void StartSimulation(int DroneId, Action<int> ViewUpdate, Func<bool> ToStop)
        {
            Simulation simulation = new Simulation(this, DroneId, ViewUpdate, ToStop);
        }
    }
}
