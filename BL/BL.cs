using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;


namespace BL
{
    class BL : IBL.IBL
    {
        IDal dalObj;
        List<IBL.BO.Drone> droneBlList;
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
        public static bool ValidateIDNumber(ulong id)
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

        //====================
        //Functions for adding
        //====================

        /// <summary>
        /// Functions for adding a drone to DAL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        public void AddDroneDal(int id, string model, WeightCategories maxWeight)
        {
            IDAL.DO.Drone droneDal = new IDAL.DO.Drone();
            droneDal.ID = id;
            droneDal.Model = model;
            droneDal.MaxWeight = maxWeight;
            dalObj.AddDrone(droneDal);
        }

        /// <summary>
        /// Functions for adding a droneCharge to DAL
        /// </summary>
        /// <param name="stationID"></param>
        public void AddDroneChargeDAL(int stationID)
        {
            IDAL.DO.DroneCharge droneCharge = new IDAL.DO.DroneCharge();
            droneCharge.DroneId = stationID;
            droneCharge.StationId = stationID;
            dalObj.AddDroneCharge(droneCharge);
        }

        /// <summary>
        /// Functions for adding a drone to BL, 
        /// If no exception are thrown the function will call the functions:
        /// AddDroneChargeDAL and AddDroneDAL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        /// <param name="stationID"></param>
        public void AddDrone(int id, string model, WeightCategories maxWeight, int stationID)
        {
            IBL.BO.Drone droneBL = new IBL.BO.Drone();
            try
            {
                droneBL.ID = id;
                droneBL.Model = model;
                droneBL.MaxWeight = maxWeight;
                droneBL.BatteryStatus = rand.Next(20, 40);
                droneBL.Status = DroneStatus.Maintenance;

                IDAL.DO.Station station = dalObj.GetSpesificStation(id);
                droneBL.location.longitude = station.Longitude;
                droneBL.location.latitude = station.Latitude;
            }
            catch (InvalidID e)
            {
                throw e;
            }
            droneBlList.Add(droneBL);
            AddDroneDal(id, model, maxWeight);
            AddDroneChargeDAL(stationID);
        }

        /// <summary>
        /// Functions for adding a station to DAL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="chargeSlots"></param>
        public void AddStationDal(int id, int name, Location location, int chargeSlots)
        {
            IDAL.DO.Station station = new IDAL.DO.Station();
            station.ID = id;
            station.Name = name;
            station.Longitude = location.longitude;
            station.Latitude = location.latitude;
            station.ChargeSlots = chargeSlots;
            dalObj.AddStation(station);
        }

        /// <summary>
        /// Functions for adding a station to BL,
        /// If no exception are thrown the function will call the function: AddStationDal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="chargeSlots"></param>
        public void AddStation(int id, int name, Location location, int chargeSlots)
        {
            IBL.BO.Station station = new IBL.BO.Station();
            try
            {
                station.ID = id;
                station.Name = name;
                station.location.longitude = location.longitude;
                station.location.latitude = location.latitude;
                station.AveChargeSlots = chargeSlots;
            }
            catch (InvalidID e)
            {
                throw e;
            }
            catch (InvalidName e)
            {
                throw e;
            }
            AddStationDal(id, name, location, chargeSlots);
        }

        /// <summary>
        /// Functions for adding a customer to DAL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="phone"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        public void AddCustomerDal(ulong id, ulong phone, string name, Location location)
        {
            IDAL.DO.Customer customer = new IDAL.DO.Customer();
            customer.ID = id;
            customer.Phone = phone;
            customer.Name = name;
            customer.longitude = location.longitude;
            customer.latitude = location.latitude;
            dalObj.AddCustomer(customer);
        }

        /// <summary>
        /// Functions for adding a customer to BL,
        /// If no exception are thrown the function will call the function: AddCustomerDal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="phone"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        public void AddCustomer(ulong id, ulong phone, string name, Location location)
        {
            IBL.BO.Customer customer = new IBL.BO.Customer();
            try
            {
                customer.ID = id;
                customer.PhoneNum = phone;
                customer.Name = name;
                customer.location.longitude = location.longitude;
                customer.location.latitude = location.latitude;
            }
            catch (InvalidID e)
            {
                throw e;
            }
            catch (InvalidName e)
            {
                throw e;
            }
            catch (InvalidPhoneNumber e)
            {
                throw e;
            }
            AddCustomerDal(id, phone, name, location);
        }

        /// <summary>
        /// Functions for adding a parcel to DAL
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="targetId"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
        public void AddParcelDal(ulong senderId, ulong targetId, WeightCategories weight, Priorities priority)
        {
            IDAL.DO.Parcel parcel = new IDAL.DO.Parcel();
            //parcel.ID = ;
            parcel.SenderId = senderId;
            parcel.TargetId = targetId;
            parcel.Weight = weight;
            parcel.Priority = priority;
            //parcel.DroneId = droneId;
            dalObj.AddParcel(parcel);
        }

        /// <summary>
        /// Functions for adding a parcel to BL,
        /// If no exception are thrown the function will call the function: AddParcelDal
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="targetId"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
        public void AddParcel(ulong senderId, ulong targetId, WeightCategories weight, Priorities priority)
        {
            IBL.BO.Parcel parcel = new IBL.BO.Parcel();
            try
            {
                parcel.Sender.ID = senderId;
                parcel.Target.ID = targetId;
                parcel.Weight = weight;
                parcel.Priority = priority;
                parcel.Drone = null;
                parcel.Associated = new DateTime();
                parcel.Created = DateTime.Now;
                parcel.PickedUp = new DateTime();
                parcel.Delivered = new DateTime();
            }
            catch (InvalidID e)
            {
                throw e;
            }
            AddParcelDal(senderId, targetId, weight, priority);
        }

        //================
        //Updats functions
        //================

        /// <summary>
        /// Function for update the drone name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public void UpdateDroneName(int id, string model)
        {
            IDAL.DO.Drone drone = dalObj.GetSpesificDrone(id);
            drone.Model = model;
        }

        /// <summary>
        /// Function for update the station data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="ChargeSlots"></param>
        public void UpdateStationData(int id, int name = 0, int ChargeSlots = 0)
        {
           IDAL.DO.Station station = dalObj.GetSpesificStation(id);
           if (name != 0)
               station.Name = name;
           if (ChargeSlots != 0)
               station.ChargeSlots = ChargeSlots;
        }

        /// <summary>
        /// Function for update the customer data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phoneNum"></param>
        public void UpdateCustomerData(ulong id, string name = null, ulong phoneNum = 0)
        {
            IDAL.DO.Customer customer = dalObj.GetSpesificCustomer(id);
            if (name != null)
                customer.Name = name;
            if (phoneNum != 0)
                customer.Phone = phoneNum;
        }


        public void SendDroneToCharge(int droneId)
        {
           
                IBL.BO.DroneInCharge droneInCharge = new IBL.BO.DroneInCharge();
                droneInCharge.ID = droneId;
         
        }

        /// <summary>
        /// Convert from dal station to BL station
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public IBL.BO.Station ConvertDalStationToBL(IDAL.DO.Station s)
        {
            return new IBL.BO.Station { 
                ID = s.ID, 
                Name = s.Name,
                location = new Location { latitude = s.Latitude, longitude = s.Longitude },
                AveChargeSlots = s.ChargeSlots
            };
        }

        /// <summary>
        /// Convert from dal customer to BL customer
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public IBL.BO.Customer ConvertDalCustomerToBL(IDAL.DO.Customer c)
        {
            return new IBL.BO.Customer
            {
                ID = c.ID,
                Name = c.Name,
                PhoneNum = c.Phone,
                location = new Location { latitude = c.latitude, longitude = c.longitude }               
            };
        }

        /// <summary>
        /// Convert from dal drone to BL drone
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public IBL.BO.Drone ConvertDalDroneToBL(IDAL.DO.Drone d)
        {
            return new IBL.BO.Drone
            {
                ID = d.ID,
                MaxWeight = d.MaxWeight,
                Model = d.Model
            };
                /*BatteryStatus*/
        }

        /// <summary>
        /// Convert from dal parcel to BL parcel
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public IBL.BO.Parcel ConvertDalParcelToBL(IDAL.DO.Parcel p)
        {
            return new IBL.BO.Parcel
            {
                ID = p.ID,
                Drone = ConvertDalDroneToBL(dalObj.GetSpesificDrone(p.DroneId)),
                Associated = p.Requested, 
                Created= p.PickedUp, 
                Delivered= p.Delivered, 
                PickedUp= p.PickedUp, 
                Priority= p.Priority, 
                Sender= ConvertDalCustomerToBL(dalObj.GetSpesificCustomer(p.SenderId)), 
                Target= ConvertDalCustomerToBL(dalObj.GetSpesificCustomer(p.TargetId)), 
                Weight = p.Weight
            };
        }

        //display by id

        /// <summary>
        /// Returning a specific station by ID number
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public IBL.BO.Station GetSpesificStationBL(int stationId)
        {
            try
            {
                return ConvertDalStationToBL(dalObj.GetSpesificStation(stationId));
            }
            catch
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Returning a specific drone by ID number
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        public IBL.BO.Drone GetSpesificDroneBL(int droneId)
        {
            try
            {
                return ConvertDalDroneToBL(dalObj.GetSpesificDrone(droneId));
            }
            catch
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Returning a specific customer by ID number
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IBL.BO.Customer GetSpesificCustomerBL(ulong customerId)
        {
            try
            {
                return ConvertDalCustomerToBL(dalObj.GetSpesificCustomer(customerId));
            }
            catch
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Returning a specific parcel by ID number
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns></returns>
        public IBL.BO.Parcel GetSpesificParcelBL(int parcelId)
        {
            try
            {
                return ConvertDalParcelToBL(dalObj.GetSpesificParcel(parcelId));
            }
            catch
            {
                throw new Exception();
            }
        }


        //display lists

        /// <summary>
        /// Returning the drone list
        /// </summary>
        /// <returns></returns>
        public List<IBL.BO.Drone> GetDronesBL()
        {
            List<IDAL.DO.Drone> dronesDal = dalObj.GetDrones();
            List<IBL.BO.Drone> dronesBL = new List<IBL.BO.Drone>();
            dronesDal.ForEach(d => dronesBL.Add(ConvertDalDroneToBL(d)));            
            return dronesBL;
        }

        /// <summary>
        /// Returning the parcel list
        /// </summary>
        /// <returns></returns>
        public List<IBL.BO.Parcel> GetParcelsBL()
        {
            List<IDAL.DO.Parcel> parcelsDal = dalObj.GetParcels();
            List<IBL.BO.Parcel> parcelsBL = new List<IBL.BO.Parcel>();
            parcelsDal.ForEach(p => parcelsBL.Add(ConvertDalParcelToBL(p))) ;
            return parcelsBL;
        }

        /// <summary>
        /// Returning the station list
        /// </summary>
        /// <returns></returns>
        public List<IBL.BO.Station> GetStationsBL()
        {
            List<IDAL.DO.Station> stationsDal = dalObj.GetStations();
            List<IBL.BO.Station> stationsBL = new List<IBL.BO.Station>();
            stationsDal.ForEach(s => stationsBL.Add(ConvertDalStationToBL(s))) ;
            return stationsBL;
        }

        /// <summary>
        /// Returning the customer list
        /// </summary>
        /// <returns></returns>
        public List<IBL.BO.Customer> GetCustomers()
        {
            List<IDAL.DO.Customer> customersDal = dalObj.GetCustomers();
            List<IBL.BO.Customer> customersBL = new List<IBL.BO.Customer>();
            customersDal.ForEach(c => customersBL.Add(ConvertDalCustomerToBL(c))) ;
            return customersBL;
        }
    }
}
