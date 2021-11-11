using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using IDAL;

namespace DalObject
{
    public class DalObject : IDal
    {
        public DalObject()
        {
            DataSource.Initialize();
        }

        //Addings//
        public void AddStation(Station station)
        {
            DataSource.Stations.Add(station);
        }

        public void AddDrone(Drone drone)
        {
            DataSource.Drones.Add(drone);
        }

        public void AddCustomer(Customer customer)
        {
            DataSource.Customers.Add(customer);
        }

        public void AddParcel(Parcel parcel)
        {
            DataSource.Parcels.Add(parcel);
        }

        public void AddDroneCharge(DroneCharge droneCharge)
        {
            DataSource.DroneCharges.Add(droneCharge);
        }

        //Updats//

        public void AssingParcelToDrone(Parcel parcel)
        {
            /* Drone drone = DataSource.Drones.Find(d =>
             d.Status == DroneStatus.Available && d.MaxWeight >= parcel.Weight);
             int indexP = DataSource.Parcels.IndexOf(parcel);
             int indexD = DataSource.Drones.IndexOf(drone);//
             parcel.DroneId = drone.ID;
             parcel.Scheduled = DateTime.Now;
             DataSource.Parcels[indexP] = parcel;
             drone.Status = DroneStatus.Delivery;
             DataSource.Drones[indexD] = drone;*/
        }

        public Parcel FindParcel(int id)
        {

            return DataSource.Parcels.First(p => p.ID == id);

            /* foreach (Parcel parcel in DataSource.Parcels)
             {
                 if (parcel.ID == id)
                 {
                     return parcel;
                 }
             }
             return new Parcel();*/
        }

        public Drone FindDrone(int id)
        {
            return DataSource.Drones.First(d => d.ID == id);


            /* foreach (Drone drone in DataSource.Drones)
             {
                 if (drone.ID == id)
                 {
                     return drone;
                 }
             }
             return new Drone();*/
        }

        public void CollectParcelByDrone(Parcel parcel)
        {
            Drone drone = DataSource.Drones.First(d =>
            d.ID == parcel.DroneId);
            int indexP = DataSource.Parcels.IndexOf(parcel);
            int indexD = DataSource.Drones.IndexOf(drone);
            parcel.DroneId = drone.ID;
            parcel.PickedUp = DateTime.Now;
            DataSource.Parcels[indexP] = parcel;
            /*         drone.Status = DroneStatus.Delivery;*/
            DataSource.Drones[indexD] = drone;
        }

        public void ProvideParcelToCustomer(Parcel parcel)
        {
            Drone drone = DataSource.Drones.First(d => d.ID == parcel.DroneId);
            int indexP = DataSource.Parcels.IndexOf(parcel);
            int indexD = DataSource.Drones.IndexOf(drone);
            parcel.DroneId = drone.ID;
            parcel.Delivered = DateTime.Now;
            DataSource.Parcels[indexP] = parcel;
            /*  drone.Status = DroneStatus.Available;*/
            DataSource.Drones[indexD] = drone;
        }


        public void SendDroneToChargeInStation(Drone drone, int stationId)
        {
            DroneCharge droneCharge = new DroneCharge();
            droneCharge.DroneId = drone.ID;
            droneCharge.StationId = stationId;
            DataSource.DroneCharges.Add(droneCharge);
            int indexD = DataSource.Drones.IndexOf(drone);
            /*            drone.Status = DroneStatus.Maintenance;*/
            DataSource.Drones[indexD] = drone;
        }

        public void ReleaseDroneFromChargeInStation(Drone drone)
        {
            DroneCharge droneCharge = DataSource.DroneCharges.First(d =>
            d.DroneId == drone.ID);
            DataSource.DroneCharges.Remove(droneCharge);
            int indexD = DataSource.Drones.IndexOf(drone);
            /*           drone.Status = DroneStatus.Available;
                       drone.Battery = 100;*/
            DataSource.Drones[indexD] = drone;
        }


        //Display//

        public Station GetSpesificStation(int stationId)
        {
            try
            {
                return DataSource.Stations.First(station => station.ID == stationId);
            }
            catch
            {
                throw new Exception();
            }
        }

        public Drone GetSpesificDrone(int droneId)
        {
            try
            {
                return DataSource.Drones.First(drone => drone.ID == droneId);
            }
            catch
            {
                throw new Exception();
            }
        }

        public Customer GetSpesificCustomer(ulong customerId)
        {
            try
            {
                return DataSource.Customers.First(customer => customer.ID == customerId);
            }
            catch
            {
                throw new Exception();
            }
        }

        public Parcel GetSpesificParcel(int parcelId)
        {
            try
            {
                return DataSource.Parcels.First(parcel => parcel.ID == parcelId);
            }
            catch
            {
                throw new Exception();
            }
        }

        //Display Lists//

        public IEnumerable<Station> GetStationLists()
        {
            foreach (Station station in DataSource.Stations)
            {
                yield return station;
            }
        }

        public IEnumerable<Drone> GetDroneLists()
        {
            foreach (Drone drone in DataSource.Drones)
            {
                yield return drone;
            }
        }

        public IEnumerable<Customer> GetCustomerLists()
        {
            foreach (Customer customer in DataSource.Customers)
            {
                yield return customer;
            }
        }

        public IEnumerable<Parcel> GetParcelLists()
        {
            foreach (Parcel parcel in DataSource.Parcels)
            {
                yield return parcel;
            }
        }

        public IEnumerable<Parcel> GetFreeParcelLists()
        {
            foreach (Parcel parcel in DataSource.Parcels)
            {
                if (parcel.DroneId == null)
                    yield return parcel;
            }
        }

        public IEnumerable<Station> GetAvailableStationLists()
        {
            foreach (Station station in DataSource.Stations)
            {
                int counter = 0;
                foreach (DroneCharge droneCharge in DataSource.DroneCharges)
                {
                    if (station.ID == droneCharge.StationId)
                        counter++;
                }
                if (station.ChargeSlots - counter > 0)
                {
                    yield return station;
                }
            }
        }

        public List<Drone> GetDrones()
        {
            return DataSource.Drones;
        }
        public List<Parcel> GetParcels()
        {
            return DataSource.Parcels;
        }
        public List<Station> GetStations()
        {
            return DataSource.Stations;
        }
        public List<Customer> GetCustomers()
        {
            return DataSource.Customers;
        }

        public double[] ElectricalPowerRequest()
        {
            double[] arr = { 1, 2, 3, 4, 5 };
            return arr;
        }
    }
}




