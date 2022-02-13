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
        public string UpdateCustomerData(int id, string name = null, int phoneNum = -1);
        public string UpdateDroneData(int id, string model);
        public string UpdateStationData(int id, int name = -1, int ChargeSlots = -1);
        public string UpdateDrone(BO.Drone droneBL);
        public string SendDroneToCharge(int droneId);
        public string ReleaseDroneFromCharge(int droneId, int timeInCharge);
        public string AssignParcelToDrone(int droneId);
        public string CollectParcelByDrone(int droneId);
        public string SupplyParcelByDrone(int droneId);
        public string RemoveParcel(int ID);
        public BO.Customer ConvertDalCustomerToBL(DO.Customer c);
        public BO.Drone ConvertDalDroneToBL(DO.Drone d);
        public BO.Parcel ConvertDalParcelToBL(DO.Parcel p);
        public BO.Station ConvertDalStationToBL(DO.Station s);
        public DO.Drone ConvertBLDroneToDAL(BO.Drone d);
        public DO.Parcel ConvertBLParcelToDAL(BO.Parcel p);
        public DO.Station ConvertBLStationToDAL(BO.Station s);
        public BO.Customer GetSpesificCustomer(int customerId);
        public BO.Drone GetSpesificDrone(int droneId);
        public BO.Parcel GetSpesificParcel(int parcelId);
        public BO.Station GetSpesificStation(int stationId);
        public DroneInCharge GetSpecificDroneInCharge(int droneId);
        public BO.Station GetNearestAvailableStation(Location Targlocation);
        public double TotalBatteryUsage(int senderId, int targetId, int parcelweight, Location droneLocation);
        public IEnumerable<BO.Customer> GetCustomers(Predicate<BO.Customer> condition = null);
        public IEnumerable<BO.Drone> GetDalDronesListAsBL();
        public IEnumerable<BO.Drone> GetDronesList(Predicate<BO.Drone> condition = null);
        public IEnumerable<BO.Parcel> GetParcels(Predicate<BO.Parcel> condition = null);
        public IEnumerable<BO.Station> GetStations(Predicate<BO.Station> condition = null);
        public IEnumerable<CustomerToList> GetCustomersToList(Predicate<BO.Customer> condition = null);
        public IEnumerable<DroneToList> GetDronesToList(Predicate<BO.Drone> condition = null);
        public IEnumerable<ParcelToList> GetParcelsToList(Predicate<BO.Parcel> condition = null);
        public IEnumerable<StationToList> GetStationsToList(Predicate<BO.Station> condition = null);
        public void StartSimulation(int DroneId, Action<int> ViewUpdate, Func<bool> ToStop);      
    }
}
