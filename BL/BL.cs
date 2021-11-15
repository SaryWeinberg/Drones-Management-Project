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

        public static bool validateIDNumber(ulong id)
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

        //add        
        public void AddDroneDal(int id, string model, WeightCategories maxWeight)
        {
            IDAL.DO.Drone droneDal = new IDAL.DO.Drone();
            droneDal.ID = id;
            droneDal.Model = model;
            droneDal.MaxWeight = maxWeight;
            dalObj.AddDrone(droneDal);
        }

        public void AddChargeDrone(int stationID)
        {
            IDAL.DO.DroneCharge droneCharge = new IDAL.DO.DroneCharge();
            droneCharge.DroneId = stationID;
            droneCharge.StationId = stationID;
            dalObj.AddDroneCharge(droneCharge);
        }

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
            catch (Exception e)
            {
                throw e;
            }
            droneBlList.Add(droneBL);
            AddDroneDal(id, model, maxWeight);
            AddChargeDrone(stationID);
        }

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
            catch (Exception e)
            {
                throw e;
            }
            AddStationDal(id, name, location, chargeSlots);
        }

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
            catch (Exception e)
            {
                throw e;
            }
            AddCustomerDal(id, phone, name, location);
        }

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
            catch (Exception e)
            {
                throw e;
            }
            AddParcelDal(senderId, targetId, weight, priority);
        }

        //Updats//

        public void UpdateDroneName(int id, string model)
        {
            IDAL.DO.Drone drone = dalObj.GetSpesificDrone(id);
            drone.Model = model;
        }

        public void UpdateStationData(int id, int name = 0, int ChargeSlots = 0)
        {
           IDAL.DO.Station station = dalObj.GetSpesificStation(id);
           if (name != 0)
               station.Name = name;
           if (ChargeSlots != 0)
               station.ChargeSlots = ChargeSlots;
        }

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
            IBL.BO.Drone droneBL = droneBlList.First(d => d.ID == droneId);
            if(droneBL.Status == DroneStatus.Available )

                
            
            try
            {
            

                IBL.BO.DroneInCharge droneInCharge = new IBL.BO.DroneInCharge();
                droneInCharge.ID = droneId;
            }
        }

        public IBL.BO.Station convertDalStationToBL(IDAL.DO.Station s)
        {
            return new IBL.BO.Station { 
                ID = s.ID, 
                Name = s.Name,
                location = new Location { latitude = s.Latitude, longitude = s.Longitude },
                AveChargeSlots = s.ChargeSlots
            };
        }

        public IBL.BO.Customer convertDalCustomerToBL(IDAL.DO.Customer c)
        {
            return new IBL.BO.Customer
            {
                ID = c.ID,
                Name = c.Name,
                PhoneNum = c.Phone,
                location = new Location { latitude = c.latitude, longitude = c.longitude }               
            };
        }

        public IBL.BO.Drone convertDalDroneToBL(IDAL.DO.Drone d)
        {
            return new IBL.BO.Drone
            {
                ID = d.ID,
                MaxWeight = d.MaxWeight,
                Model = d.Model
            };
                /*BatteryStatus*/





        }

        public IBL.BO.Parcel convertDalParcelToBL(IDAL.DO.Parcel p)
        {
            return new IBL.BO.Parcel
            {
                ID = p.ID,
                Drone = convertDalDroneToBL(dalObj.GetSpesificDrone(p.DroneId)),
                Associated = p.Requested, 
                Created= p.PickedUp, 
                Delivered= p.Delivered, 
                PickedUp= p.PickedUp, 
                Priority= p.Priority, 
                Sender= convertDalCustomerToBL(dalObj.GetSpesificCustomer(p.SenderId)), 
                Target= convertDalCustomerToBL(dalObj.GetSpesificCustomer(p.TargetId)), 
                Weight = p.Weight
            };
        }

        //display by id
        public IBL.BO.Station GetSpesificStationBL(int stationId)
        {
            try
            {
                return convertDalStationToBL(dalObj.GetSpesificStation(stationId));
            }
            catch
            {
                throw new Exception();
            }
        }

        public IBL.BO.Drone GetSpesificDroneBL(int droneId)
        {
            try
            {
                return convertDalDroneToBL(dalObj.GetSpesificDrone(droneId));
            }
            catch
            {
                throw new Exception();
            }
        }

        public IBL.BO.Customer GetSpesificCustomerBL(ulong customerId)
        {
            try
            {
                return convertDalCustomerToBL(dalObj.GetSpesificCustomer(customerId));
            }
            catch
            {
                throw new Exception();
            }
        }

        public IBL.BO.Parcel GetSpesificParcelBL(int parcelId)
        {
            try
            {
                return convertDalParcelToBL(dalObj.GetSpesificParcel(parcelId));
            }
            catch
            {
                throw new Exception();
            }
        }

        //display lists

        public List<IBL.BO.Drone> getDronesBL()
        {
            List<IDAL.DO.Drone> dronesDal = dalObj.getDrones();
            List<IBL.BO.Drone> dronesBL = new List<IBL.BO.Drone>();
            dronesDal.ForEach(d => dronesBL.Add(convertDalDroneToBL(d)));            
            return dronesBL;
        }

        public List<IBL.BO.Parcel> getParcelsBL()
        {
            List<IDAL.DO.Parcel> parcelsDal = dalObj.getParcels();
            List<IBL.BO.Parcel> parcelsBL = new List<IBL.BO.Parcel>();
            parcelsDal.ForEach(p => parcelsBL.Add(convertDalParcelToBL(p))) ;
            return parcelsBL;
        }

        public List<IBL.BO.Station> getStationsBL()
        {
            List<IDAL.DO.Station> stationsDal = dalObj.getStations();
            List<IBL.BO.Station> stationsBL = new List<IBL.BO.Station>();
            stationsDal.ForEach(s => stationsBL.Add(convertDalStationToBL(s))) ;
            return stationsBL;
        }

        public List<IBL.BO.Customer> getCustomers()
        {
            List<IDAL.DO.Customer> customersDal = dalObj.getCustomers();
            List<IBL.BO.Customer> customersBL = new List<IBL.BO.Customer>();
            customersDal.ForEach(c => customersBL.Add(convertDalCustomerToBL(c))) ;
            return customersBL;
        }
    }
}
