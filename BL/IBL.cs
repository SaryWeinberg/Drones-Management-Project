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
        public void AddParcelDal(int senderId, int targetId, WeightCategories weight, Priorities priority);
        public void AddStationDal(int id, int name, Location location, int chargeSlots);
        public void AddDroneChargeDAL(int stationID);
        public void AddCustomerBL(int id, int phone, string name, Location location);
        public void AddDroneBL(int id, string model, WeightCategories maxWeight, int stationID);
        public void AddParcelBL(int senderId, int targetId, WeightCategories weight, Priorities priority);
        public void AddStationBL(int id, int name, Location location, int chargeSlots);
        public void UpdateCustomerData(int id, string name = null, int phoneNum = 0);
        public void UpdateDroneName(int id, string model);
        public void UpdateStationData(int id, int name = 0, int ChargeSlots = 0);
        public CustomerBL ConvertDalCustomerToBL(Customer c);
        public DroneBL ConvertDalDroneToBL(Drone d);
        public Drone ConvertBLDroneToDAL(DroneBL d);
        public ParcelBL ConvertDalParcelToBL(Parcel p);
        public StationBL ConvertDalStationToBL(Station s);
        public CustomerBL GetSpesificCustomerBL(int customerId);
        public DroneBL GetSpesificDroneBL(int droneId);
        public ParcelBL GetSpesificParcelBL(int parcelId);
        public StationBL GetSpesificStationBL(int stationId);
        public List<CustomerBL> GetCustomersBL();
        public List<DroneBL> GetDronesBL();
        public List<ParcelBL> GetParcelsBL();
        public List<StationBL> GetStationsBL();
        public StationBL GetNearestAvailableStation( Location Targlocation);
    }
}
