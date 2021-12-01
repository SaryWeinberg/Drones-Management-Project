using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDAL.DO;

namespace IBL
{
   public interface IBL
    {
        public void AddCustomerDal(int id, int phone, string name, Location location);
        public void AddDroneDal(int id, string model, WeightCategories maxWeight);
        public void AddParcelDal(int id,int senderId, int targetId, WeightCategories weight, Priorities priority);
        public void AddStationDal(int id, int name, Location location, int chargeSlots);
        public void AddDroneChargeDAL(int stationID);
        public string AddCustomerBL(int id, int phone, string name, Location location);
        public string AddDroneBL(int id, string model, WeightCategories maxWeight, int stationID);
        public string AddParcelBL(int senderId, int targetId, WeightCategories weight, Priorities priority);
        public string AddStationBL(int id, int name, Location location, int chargeSlots);
        public string UpdateCustomerData(int id, string name = null, string phoneNum =null);
        public string UpdateDroneName(int id, string model);
        public string UpdateStationData(int id, string name = null, string ChargeSlots = null);
        public CustomerBL ConvertDalCustomerToBL(Customer c);
        public DroneBL ConvertDalDroneToBL(Drone d);
        public ParcelBL ConvertDalParcelToBL(Parcel p);
        public StationBL ConvertDalStationToBL(Station s);
        public Customer ConvertBLCustomerToDAL(CustomerBL c);
        public Drone ConvertBLDroneToDAL(DroneBL d);
        public Parcel ConvertBLParcelToDAL(ParcelBL p);
        public Station ConvertBLStationToDAL(StationBL s);
        public CustomerBL GetSpesificCustomerBL(int customerId);
        public DroneBL GetSpesificDroneBL(int droneId);
        public ParcelBL GetSpesificParcelBL(int parcelId);
        public StationBL GetSpesificStationBL(int stationId);
        public List<CustomerBL> GetCustomersBL();
        public List<DroneBL> GetDronesBL();
        public List<DroneBL> GetDronesBLList();
        public string UpdateDrone(DroneBL droneBL);
        public List<ParcelBL> GetParcelsBL();
        public List<StationBL> GetStationsBL();
        public IEnumerable<ParcelBL> GetParcelsNotYetAssignedDroneList(Predicate<ParcelBL> findBy);
        public List<StationBL> GetAvailableStationsList();
        public StationBL GetNearestAvailableStation( Location Targlocation);
        public string SendDroneToCharge(int droneId);
        public string ReleaseDroneFromCharge(int droneId, int timeInCharge);
        public string AssignParcelToDrone(int droneId);
        public string CollectParcelByDrone(int droneId);
        public string DeliveryParcelByDrone(int droneId);
        public double TotalBatteryUsage(int senderId, int targetId, int parcelweight, Location droneLocation);
        public IEnumerable<DroneBL> GetDronesBy(Predicate<DroneBL> findBy);
    }
}
