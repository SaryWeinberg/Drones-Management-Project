using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DO;

namespace BLApi
{
    public interface IBL
    {
        public void AddCustomerDal(int id, int phone, string name, Location location);
        public void AddDroneDal(int id, string model, WeightCategories maxWeight);
        public void AddParcelDal(int id, int senderId, int targetId, WeightCategories weight, Priorities priority);
        public void AddStationDal(int id, int name, Location location, int chargeSlots);
        public void AddDroneCharge(int stationID, int DroneID, double batteryStatus);
        public string AddCustomerBL(int id, int phone, string name, Location location);
        public string AddDroneBL(int id, string model, WeightCategories maxWeight, int stationID);
        public string AddParcelBL(int senderId, int targetId, WeightCategories weight, Priorities priority);
        public string AddStationBL(int id, int name, Location location, int chargeSlots);
        public string UpdateCustomerData(int id, string name = null, string phoneNum = null);
        public string UpdateDroneData(int id, string model);
        public string UpdateStationData(int id, int name = -1, int ChargeSlots = -1);
        public BO.Customer ConvertDalCustomerToBL(DO.Customer c);
        public BO.Drone ConvertDalDroneToBL(DO.Drone d);
        public BO.Parcel ConvertDalParcelToBL(DO.Parcel p);
        public BO.Station ConvertDalStationToBL(DO.Station s);
        /*        public DO.Customer ConvertBLCustomerToDAL(BO.Customer c);
        */
        public DO.Drone ConvertBLDroneToDAL(BO.Drone d);
        public DO.Parcel ConvertBLParcelToDAL(BO.Parcel p);
        public DO.Station ConvertBLStationToDAL(BO.Station s);
        public BO.Customer GetSpesificCustomer(int customerId);
        public BO.Drone GetSpesificDrone(int droneId);
        public BO.Parcel GetSpesificParcelBL(int parcelId);
        public BO.Station GetSpesificStation(int stationId);
        public List<BO.Customer> GetCustomers();
        public List<BO.Drone> GetDalDronesListAsBL();
        public List<BO.Drone> GetDronesList();
        public string UpdateDrone(BO.Drone droneBL);
        public List<BO.Parcel> GetParcels();
        public List<BO.Station> GetStations();
        /*        public IEnumerable<BO.Parcel> GetParcelsNotYetAssignedDroneList(Predicate<BO.Parcel> findBy);
        */
        public List<BO.Station> GetAvailableStationsList();
        public BO.Station GetNearestAvailableStation(Location Targlocation);
        public string SendDroneToCharge(int droneId);
        public string ReleaseDroneFromCharge(int droneId, int timeInCharge);
        public string AssignParcelToDrone(int droneId);
        public string CollectParcelByDrone(int droneId);
        public string SupplyParcelByDrone(int droneId);
        public double TotalBatteryUsage(int senderId, int targetId, int parcelweight, Location droneLocation);
        /*        public IEnumerable<BO.Customer> GeCustomerByCondition(Predicate<BO.Customer> condition);
        */
        public IEnumerable<BO.Drone> GetDronesByCondition(Predicate<BO.Drone> condition);
        public IEnumerable<DroneToList> GetDronesToListByCondition(Predicate<DroneToList> condition);
         public IEnumerable<BO.Parcel> GetParcelsByCondition(Predicate<BO.Parcel> condition);
        public IEnumerable<BO.ParcelToList> GetParcelsToListByCondition(Predicate<BO.ParcelToList> condition);
        public List<BO.CustomerToList> GetCustomersToList();
        public List<BO.DroneToList> GetDronesToList();
        public List<BO.ParcelToList> GetParcelsToList();
        public List<BO.StationToList> GetStationsListBL();
    }
}
