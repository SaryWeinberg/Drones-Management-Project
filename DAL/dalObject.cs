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
        public void AddStation(double longitude, double latitude, int chargeSlots)
        {
            Station station = new Station();
            station.ID = DataSource.Stations.Count;
            station.Name = DataSource.Stations.Count;
            station.Longitude = longitude;
            station.Latitude = latitude;
            station.ChargeSlots = chargeSlots;
            DataSource.Stations.Add(station);
        }
        public void AddDrone(string model, WeightCategories maxWeight)
        /*     public void AddDrone(string model, WeightCategories maxWeight, DroneStatus status, double battery)*/
        {
            Drone drone = new Drone();
            drone.ID = DataSource.Drones.Count;
            drone.Model = model;
            drone.MaxWeight = maxWeight;
            /*            drone.Status = status;
                        drone.Battery = battery;*/
            DataSource.Drones.Add(drone);
        }
        public void AddCustomer(int id, string phone, string name, double Longitude, double Latitude)
        {
            Customer customer = new Customer();
            customer.ID = id;
            customer.Phone = phone;
            customer.Name = name;
            customer.longitude = Longitude;
            customer.latitude = Latitude;
            DataSource.Customers.Add(customer);
        }
        public void AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority, DateTime requested, int droneId, DateTime scheduled, DateTime pickedUp, DateTime delivered)
        {
            Parcel parcel = new Parcel();

            parcel.ID = DataSource.Parcels.Count;
            parcel.SenderId = senderId;
            parcel.TargetId = targetId;
            parcel.Weight = weight;
            parcel.Priority = priority;
            parcel.Requested = requested;
            parcel.DroneId = droneId;
            parcel.Scheduled = scheduled;
            parcel.PickedUp = pickedUp;
            parcel.Delivered = delivered;
            DataSource.Parcels.Add(parcel);
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

        public Station DisplayStation(int stationId)
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

        public Drone DisplayDrone(int droneId)
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

        public Customer DisplayCustomer(int customerId)
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

        public Parcel DisplayParcel(int parcelId)
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

        public IEnumerable<Station> ViewStationLists()
        {
            foreach (Station station in DataSource.Stations)
            {
                yield return station;
            }
        }

        public IEnumerable<Drone> ViewDroneLists()
        {
            foreach (Drone drone in DataSource.Drones)
            {
                yield return drone;
            }
        }

        public IEnumerable<Customer> ViewCustomerLists()
        {
            foreach (Customer customer in DataSource.Customers)
            {
                yield return customer;
            }
        }

        public IEnumerable<Parcel> ViewParcelLists()
        {
            foreach (Parcel parcel in DataSource.Parcels)
            {
                yield return parcel;
            }
        }

        public IEnumerable<Parcel> ViewFreeParcelLists()
        {
            foreach (Parcel parcel in DataSource.Parcels)
            {
                if (parcel.DroneId == null)
                    yield return parcel;
            }
        }

        public IEnumerable<Station> ViewAvailableStationLists()
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
        public double[] ElectricalPowerRequest()
        {
            double[] arr = { 1, 2, 3, 4, 5 };
            return arr;
        }
    }
}

