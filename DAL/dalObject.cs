using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public class DalObject
    {
        public DalObject()
        {
            DataSource.Initialize();
        }

        //Addings//
        public static void AddStation(int id, int name, double longitude, double latitude, int chargeSlots)
        {
            Station station = new Station();
            station.ID = id;
            station.Name = name;
            station.Longitude = longitude;
            station.Latitude = latitude;
            station.ChargeSlots = chargeSlots;
            DataSource.Stations[DataSource.config.StationsIndexer++] = station;
        }

        public static void AddDrone(int id, string model, WeightCategories maxWeight, DroneStatus status, double battery)
        {
            Drone drone = new Drone();
            drone.ID = id;
            drone.Model = model;
            drone.MaxWeight = maxWeight;
            drone.Status = status;
            drone.Battery = battery;
            DataSource.Drones[DataSource.config.DronesIndexer++] = drone;
        }
        public static void AddCustomer(int id, string phone, string name, double Longitude, double Latitude)
        {
            Customer customer = new Customer();
            customer.ID = id;
            customer.Phone = phone;
            customer.Name = name;
            customer.longitude = Longitude;
            customer.latitude = Latitude;
            DataSource.Customers[DataSource.config.customersIndexer++] = customer;
        }
        public static void AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority, DateTime requested, int droneId, DateTime scheduled, DateTime pickedUp, DateTime delivered)
        {
            Parcel parcel = new Parcel();
            parcel.ID = DataSource.config.ParcelsIndexer;
            parcel.SenderId = senderId;
            parcel.TargetId = targetId;
            parcel.Weight = weight;
            parcel.Priority = priority;
            parcel.Requested = requested;
            parcel.DroneId = droneId;
            parcel.Scheduled = scheduled;
            parcel.PickedUp = pickedUp;
            parcel.Delivered = delivered;
            DataSource.Parcels[DataSource.config.ParcelsIndexer++] = parcel;
        }

        //Updats//

        public static void AssingParcelToDrone(Parcel parcel)//שיוך חבליה לרחפן
        {

            foreach (Drone drone in DataSource.Drones)
            {
                if (drone.Status == DroneStatus.Available && drone.MaxWeight >= parcel.Weight)
                {
                    parcel.DroneId = drone.ID;
                    drone.Status = DroneStatus.Delivery;
                    parcel.DroneId = drone.ID;
                    parcel.Scheduled = DateTime.Now;
                    return;
                }
            }
        }

        public static void CollectParcelByDrone(Parcel parcel)//איסוף חבילה ע"י רחפן
        {

            for (int i = 0; i < DataSource.config.DronesIndexer; i++)
            {
                if (DataSource.Drones[i].ID == parcel.DroneId)
                {

                    DataSource.Drones[i].Status = DroneStatus.Delivery;
                    parcel.PickedUp = DateTime.Now;
                    break;
                }
            }
        }

        public static void ProvideParcelToCustomer(Parcel parcel)//אספקת חבילה לקוח
        {
            for (int i = 0; i < DataSource.config.DronesIndexer; i++)
            {
                if (DataSource.Drones[i].ID == parcel.DroneId)
                {
                    DataSource.Drones[i].Status = DroneStatus.Available;

                    break;
                }
            }
            parcel.Delivered = DateTime.Now;
        }


        public static void SendDroneToChargeInStation(Drone drone, int stationId)//שליחת רחפן לטעינה בתחנת בסיס
        {
            drone.Status = DroneStatus.Maintenance;
            DroneCharge droneCharge = new DroneCharge();
            DataSource.droneCharges[DataSource.config.DroneChargeIndexer++] = droneCharge;
            droneCharge.DroneId = drone.ID;
            droneCharge.StationId = stationId;
        }

        public static void ReleaseDroneFromChargeInStation(DroneCharge droneCharge)//שחרור רחפן מטעינה בתחנת בסיס
        {
            for (int i = 0; i < DataSource.config.DronesIndexer; i++)
            {
                if (DataSource.Drones[i].ID == droneCharge.DroneId)
                {
                    DataSource.Drones[i].Status = DroneStatus.Available;
                    DataSource.Drones[i].Battery = 100;
                    break;
                }
            }
        }

        //Display//

        public static Station DisplayStation(int stationId)//תצוגת תחנת בסיס
        {
            foreach (Station station in DataSource.Stations)
            {
                if (station.ID == stationId)
                {

                    return station;
                }
            }
            return new Station();
        }

        public static Drone DisplayDrone(int droneId)//תצוגת רחפן
        {
            foreach (Drone drone in DataSource.Drones)
            {
                if (drone.ID == droneId)
                {
                    return drone;
                }
            }
            return new Drone();
        }

        public static Customer DisplayCustomer(int customerId)//תצוגת לקוח
        {
            foreach (Customer customer in DataSource.Customers)
            {
                if (customer.ID == customerId)
                {
                    return customer;
                }
            }
            return new Customer();
        }

        public static Parcel DisplayParcel(int parcelId)//תצוגת חבילה
        {
            foreach (Parcel parcel in DataSource.Parcels)
            {
                if (parcel.ID == parcelId)
                {
                    return parcel;
                }
            }
            return new Parcel();
        }

        //Display Lists//

        public static IEnumerable<Station> ViewStationLists()//הצגת רשימת תחונת בסיס
        {
            foreach (Station station in DataSource.Stations)
            {
                yield return station;
            }
        }

        public static IEnumerable<Drone> ViewDroneLists()//הצגת שימות הרחפנים
        {
            foreach (Drone drone in DataSource.Drones)
            {
                yield return drone;
            }
        }

        public static IEnumerable<Customer> ViewCustomerLists()//הצגת שימת הלקוחות
        {
            foreach (Customer customer in DataSource.Customers)
            {
                yield return customer;
            }
        }

        public static IEnumerable<Parcel> ViewParcelLists()//הצגת רשימת החבילות
        {
            foreach (Parcel parcel in DataSource.Parcels)
            {
                yield return parcel;
            }

        }

        public static IEnumerable<Parcel> ViewFreeParcelLists()//הצגת רשימת לקוחות שעוד לא שויכו לרחפן
        {
            foreach (Parcel parcel in DataSource.Parcels)
            {
                if (parcel.DroneId == null)
                    yield return parcel;
            }

        }

        public static IEnumerable<Station> ViewAvailableStationLists()//הצגת תחנות בסיס עם עמדות טעינה פנויות
        {
            foreach (Station station in DataSource.Stations)
            {
                int counter = 0;
                foreach (DroneCharge droneCharge in DataSource.droneCharges)
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
    }
}
