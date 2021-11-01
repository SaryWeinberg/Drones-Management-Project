using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.DO
{
    public interface IDal
    {
        public void AddStation(double longitude, double latitude, int chargeSlots);
        public void AddDrone(string model, WeightCategories maxWeight, DroneStatus status, double battery);
        public void AddCustomer(int id, string phone, string name, double Longitude, double Latitude);
        public void AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority, DateTime requested, int droneId, DateTime scheduled, DateTime pickedUp, DateTime delivered);
        public void AssingParcelToDrone(Parcel parcel);
        public Parcel FindParcel();
        public Drone FindDrone();
        public void CollectParcelByDrone(Parcel parcel);
        public void ProvideParcelToCustomer(Parcel parcel);
        public void SendDroneToChargeInStation(Drone drone, int stationId);
        public void ReleaseDroneFromChargeInStation(Drone drone);
        public Station DisplayStation(int stationId);
        public Drone DisplayDrone(int droneId);
        public Customer DisplayCustomer(int customerId);
        public Parcel DisplayParcel(int parcelId);
        public IEnumerable<Station> ViewStationLists();
        public IEnumerable<Drone> ViewDroneLists();
        public IEnumerable<Customer> ViewCustomerLists();
        public IEnumerable<Parcel> ViewParcelLists();
        public IEnumerable<Parcel> ViewFreeParcelLists();
        public IEnumerable<Station> ViewAvailableStationLists();
    }
}


