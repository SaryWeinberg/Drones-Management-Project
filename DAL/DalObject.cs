using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;


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

            return DataSource.Parcels.First(p => p.id == id);

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
            return DataSource.Drones.First(d => d.id == id);


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
            d.id == parcel.droneId);
            int indexP = DataSource.Parcels.IndexOf(parcel);
            int indexD = DataSource.Drones.IndexOf(drone);
            parcel.droneId = drone.id;
            parcel.pickedUp = DateTime.Now;
            DataSource.Parcels[indexP] = parcel;
            /*         drone.Status = DroneStatus.Delivery;*/
            DataSource.Drones[indexD] = drone;
        }

        public void ProvideParcelToCustomer(Parcel parcel)
        {
            Drone drone = DataSource.Drones.First(d => d.id == parcel.droneId);
            int indexP = DataSource.Parcels.IndexOf(parcel);
            int indexD = DataSource.Drones.IndexOf(drone);
            parcel.droneId = drone.id;
            parcel.delivered = DateTime.Now;
            DataSource.Parcels[indexP] = parcel;
            /*  drone.Status = DroneStatus.Available;*/
            DataSource.Drones[indexD] = drone;
        }


        public void SendDroneToChargeInStation(Drone drone, int stationId)
        {
            DroneCharge droneCharge = new DroneCharge();
            droneCharge.DroneId = drone.id;
            droneCharge.StationId = stationId;
            DataSource.DroneCharges.Add(droneCharge);
            int indexD = DataSource.Drones.IndexOf(drone);
            /*            drone.Status = DroneStatus.Maintenance;*/
            DataSource.Drones[indexD] = drone;
        }

        public void ReleaseDroneFromChargeInStation(Drone drone)
        {
            DroneCharge droneCharge = DataSource.DroneCharges.First(d =>
            d.DroneId == drone.id);
            DataSource.DroneCharges.Remove(droneCharge);
            int indexD = DataSource.Drones.IndexOf(drone);
            /*           drone.Status = DroneStatus.Available;
                       drone.Battery = 100;*/
            DataSource.Drones[indexD] = drone;
        }

        public void updateDrone(Drone drone)
        {
            int index = DataSource.Drones.FindIndex(d => d.id == drone.id);
            DataSource.Drones[index] = drone;
        }

        public void updateCustomer(Customer customer)
        {
            int index = DataSource.Customers.FindIndex(d => d.id == customer.id);
            DataSource.Customers[index] = customer;
        }

        public void updateStation(Station station)
        {
            int index = DataSource.Stations.FindIndex(d => d.id == station.id);
            DataSource.Stations[index] = station;
        }

        public void updateParcel(Parcel parcel)
        {
            int index = DataSource.Parcels.FindIndex(d => d.id == parcel.id);
            DataSource.Parcels[index] = parcel;
        }

        //Display//

        public Station GetSpesificStation(int stationId)
        {
            try
            {
                return DataSource.Stations.First(station => station.id == stationId);
            }
            catch
            {
                throw new ObjectDoesNotExist("Station", stationId);
            }
        }

        public Drone GetSpesificDrone(int droneId)
        {
            try
            {
                return DataSource.Drones.First(drone => drone.id == droneId);
            }
            catch
            {
                throw new ObjectDoesNotExist("Drone", droneId);
            }
        }

        public Customer GetSpesificCustomer(int customerId)
        {
            try
            {
                return DataSource.Customers.First(customer => customer.id == customerId);
            }
            catch
            {
                throw new ObjectDoesNotExist("Customer", customerId);
            }
        }

        public Parcel GetSpesificParcel(int parcelId)
        {
            try
            {
                return DataSource.Parcels.First(parcel => parcel.id == parcelId);
            }
            catch
            {
                throw new ObjectDoesNotExist("Parcel", parcelId);
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
                if (parcel.droneId == null)
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
                    if (station.id == droneCharge.StationId)
                        counter++;
                }
                if (station.chargeSlots - counter > 0)
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
            double[] arr = { DataSource.config.Available, DataSource.config.Light, DataSource.config.medium, DataSource.config.heavy ,DataSource.config.chargingRate};
            return arr;
        }
    }
}




