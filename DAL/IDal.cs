using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.DO
{
    public interface IDal
    {
        public void AddCustomer(Customer customer);
        public void AddDrone(Drone drone);
        public void AddParcel(Parcel parcel);
        public void AddStation(Station station);
        public void AddDroneCharge(DroneCharge droneCharge);
        public void AssingParcelToDrone(Parcel parcel);
        public void CollectParcelByDrone(Parcel parcel);
        public void ProvideParcelToCustomer(Parcel parcel);
        public void SendDroneToChargeInStation(Drone drone, int stationId);
        public void ReleaseDroneFromChargeInStation(Drone drone);
        public void RemoveDroneInCharge(int droneID);
        public Customer GetSpesificCustomer(int customerId);
        public Drone GetSpesificDrone(int droneId);
        public Parcel GetSpesificParcel(int parcelId);
        public Station GetSpesificStation(int stationId);
        public IEnumerable<Customer> GetCustomerLists();
        public IEnumerable<Drone> GetDroneLists();
        public IEnumerable<Parcel> GetParcelLists();
        public IEnumerable<Station> GetStationLists();
        public IEnumerable<Parcel> GetFreeParcelLists();
        public IEnumerable<Station> GetAvailableStationLists();
        List<Customer> GetCustomers();
        List<Drone> GetDrones();
        List<Parcel> GetParcels();
        List<Station> GetStations();
        public void UpdateCustomer(Customer customer);
        public void UpdateParcel(Parcel parcel);
        public void UpdateDrone(Drone drone);
        public void UpdateStation(Station station);
        public double[] ElectricalPowerRequest();
    }
}


