using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public class DataSource
    {
        /// <arrays>
        /// 
        static internal List<Drone> Drones = new List<Drone>();
        static internal List<Station> Stations = new List<Station>();
        static internal List<Customer> Customers = new List<Customer>();
        static internal List<Parcel> Parcels = new List<Parcel>();
        static internal List<DroneCharge> DroneCharges = new List<DroneCharge>();

        //static internal Drone[] Drones = new Drone[10];
        //static internal Station[] Stations = new Station[5];
        //static internal Customer[] Customers = new Customer[100];
        //static internal Parcel[] Parcels = new Parcel[100];
        //static internal DroneCharge[] droneCharges = new DroneCharge[0];
        /// </arrays>

        internal class config
        {
            public static double Available, Light, medium, heavy, chargingRate;
        //    static internal int DronesIndexer = 0;
        //    static internal int StationsIndexer = 0;
        //    static internal int customersIndexer = 0;
        //    static internal int ParcelsIndexer = 0;
        //    static internal int DroneChargeIndexer = 0;
        }

        static public void Initialize()
        {
            Random rand = new Random();
            for (int i = 0; i < 2; i++)
            {
                Station station = new Station();
                station.id = Stations.Count; 
                station.name = Stations.Count; 
                station.longitude = rand.Next();
                station.latitude = rand.Next();
                station.chargeSlots = rand.Next(10); //כמה רחפנים יש?

                //======================================================
                Stations.Add(station);
                //Stations[config.StationsIndexer] = station;
                //config.StationsIndexer++;
            }

            for (int i = 0; i < 5; i++)
            {
                Drone drone = new Drone();
                drone.id = Drones.Count;
                drone.model = $"{Drones.Count}";
                drone.maxWeight = (WeightCategories)(rand.Next(0, 2));
/*                drone.Status = (DroneStatus)(rand.Next(0, 2));
                drone.Battery = rand.Next(100);*/
                //Drones[config.DronesIndexer] = drone;
                //config.DronesIndexer++;
                Drones.Add(drone);
            }

            for (int i = 0; i < 10; i++)
            {
                Customer customer = new Customer();
                customer.id = Customers.Count;
                customer.phoneNum = rand.Next(111111111, 999999999);
                customer.name = $"Customer{i}";
                customer.longitude = rand.Next();
                customer.latitude = rand.Next();
                //Customers[config.customersIndexer] = customer;
                //config.customersIndexer++;
                Customers.Add(customer);
            }

            for (int i = 0; i < 10; i++)
            {
                Parcel parcel = new Parcel();
                parcel.id = Parcels.Count;

                //parcel.ID = config.ParcelsIndexer;
                //------------------------------------------------
                parcel.senderId = rand.Next() % Customers.Count;
                parcel.targetId = rand.Next() % Customers.Count;


                parcel.weight = (WeightCategories)(rand.Next(0, 2));
                parcel.priority = (Priorities)(rand.Next(0, 2));
                parcel.requested = RandomDate();
                //---------------------------------------------------------
                parcel.droneId = rand.Next() % Drones.Count;
                parcel.scheduled = RandomDate();
                parcel.pickedUp = RandomDate();
                parcel.delivered = RandomDate();
                //Parcels[config.ParcelsIndexer] = parcel;
                //config.ParcelsIndexer++;
                Parcels.Add(parcel);
            }
        }
        public static DateTime RandomDate()
        {
            Random rand = new Random();
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Seconds;
            return start.AddDays(rand.Next(range));
        }
    }
}