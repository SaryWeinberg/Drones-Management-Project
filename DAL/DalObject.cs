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

        public static void AddDrone(int id, string model, WeightCategories maxWeight, DroneStatus status ,double battery)
        {
            Drone drone = new Drone();
            drone.ID = id;
            drone.Model = model;
            drone.MaxWeight = maxWeight;
            drone.Status = status;
            drone.Battery = battery;
            DataSource.Drones[DataSource.config.DronesIndexer++] = drone;


        }
        public static void AddCustomer(int id,string phone,string name,double Longitude,double Latitude)
        {
            Customer customer = new Customer();
            customer.ID = id;
            customer.Phone = phone;
            customer.Name = name;
            customer.longitude = Longitude;
            customer.latitude = Latitude;
            DataSource.customers[DataSource.config.customersIndexer++] = customer;

        }
        public static void AddParcel( int id,int senderId ,int targetId ,WeightCategories weight,Priorities priority,DateTime requested,int droneId ,DateTime scheduled,DateTime pickedUp ,DateTime delivered )
        {
            Parcel parcel = new Parcel();
            parcel.ID = id;
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
        //public static void Update()
        //{


        //}

        //Updats//

        public static void AssingParcelToDrone(Parcel parcel)
        {
            foreach (Drone drone in DataSource.Drones)
            {
                if (drone.Status == DroneStatus.Available && drone.MaxWeight >= parcel.Weight)
                {
                    parcel.DroneId = drone.ID;
                    return;
                }
            }

        }

        public static void CollectParcelByDrone(Parcel parcel)//איסוף חבילה ע"י רחפן
        {
            foreach (Drone drone in DataSource.Drones)
            {
                if (drone.ID == parcel.DroneId)
                {
                    parcel.DroneId = drone.ID;
                    return;
                }
            }


        }
        public static void ProvideParcelToCustomer()
        {


        }
        public static void SendDroneToChargeInStation()
        {


        }
        public static void ReleaseDroneFromChargeInStation()
        {


        }

        //Display//

        public static void DisplayStation()
        {


        }
        public static void DisplayDrone()
        {


        }
        public static void DisplayCustomer()
        {


        }
        public static void DisplayParcel()
        {


        }

        //Display Lists//

        public static void ViewStationLists()
        {


        }
        public static void ViewDroneLists()
        {

            
        }
        public static void ViewCustomerLists()
        {


        }
        public static void ViewParcelLists()
        {


        }
        public static void ViewFreeParcelLists()
        {


        }
        public static void ViewAvailableStationLists()
        {


        }

        //Exit//
        public static void Exit()
        {


        }
    }
}
