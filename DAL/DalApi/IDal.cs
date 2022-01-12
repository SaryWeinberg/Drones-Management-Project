using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi
{
    public interface IDal
    {
        public void AddStation(Station station);
        public void AddDrone(Drone drone);
        public void AddCustomer(Customer customer);
        public void AddParcel(Parcel parcel);
        public void AddDroneCharge(DroneCharge droneCharge);
        public void RemoveDroneInCharge(int droneId);
        public Station GetSpesificStation(int stationId);
        public Customer GetSpesificCustomer(int customerId);
        public Parcel GetSpesificParcel(int parcelId);
        public IEnumerable<Drone> GetDrones(Predicate<Drone> condition = null);
        public IEnumerable<Station> GetStations(Predicate<Station> condition = null);
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> condition = null);
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> condition = null);
        public IEnumerable<DroneCharge> GetDroneCharges(Predicate<DroneCharge> condition = null);
        public void UpdateDrone(Drone drone);
        public void UpdateCustomer(Customer customer);
        public void UpdateStation(Station station);
        public void UpdateParcel(Parcel parcel);
        public double[] ElectricalPowerRequest();
    }
}