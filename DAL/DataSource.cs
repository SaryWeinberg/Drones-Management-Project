using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    class DataSource
    {
        /// <arrays>
        static internal Drone[] Drones = new Drone[10];
        static internal Station[] Stations = new Station[5];
        static internal Customer[] customers = new Customer[100];
        static internal Parcel[] Parcels = new Parcel[1000];
        /// </arrays>

        internal class config
        {
            static internal int DronesIndexer = 0;
            static internal int StationsIndexer = 0;
            static internal int customersIndexer = 0;
            static internal int ParcelsIndexer = 0;
        }

        static public void Initialize()
        {
            Random rand = new Random();
            for(int i = 0; i < 2; i++)
            {
                Station station = Stations[config.StationsIndexer++];
                station.ID = config.StationsIndexer;
                station.Name = config.StationsIndexer;
                station.Longitude = rand.Next();
                station.Latitude = rand.Next();
                station.ChargeSlots = rand.Next(10); //כמה רחפנים יש?
            }

            for(int i = 0; i < 5; i++)
            {
                Drone drone = Drones[config.DronesIndexer++];
                drone.ID = config.DronesIndexer;
                drone.Model = $"{config.DronesIndexer}";
                drone.MaxWeight = (WeightCategories)(rand.Next(0, 2));
                drone.Status = (DroneStatus)(rand.Next(0,2));
                drone.Battery = rand.Next(100);
            }

            for(int i = 0; i < 10; i++)
            {
                Customer customer = customers[config.customersIndexer++];
                customer.ID = config.customersIndexer;
                customer.Phone = $"{rand.Next(111111111, 999999999)}";
                customer.Name = $"Customer{i}";
                customer.longitude =  rand.Next();
                customer.latitude =  rand.Next();
            }

            for (int i = 0; i < 10; i++)
            {
                Parcel parcel = Parcels[config.ParcelsIndexer++];
                parcel.ID = config.ParcelsIndexer;
                parcel.SenderId = rand.Next() % config.customersIndexer;
                parcel.TargetId = rand.Next() % config.customersIndexer;
                parcel.Weight = (WeightCategories)(rand.Next(0, 2));
                parcel.Priority = (Priorities)(rand.Next(0, 2));
                parcel.Requested = new DateTime(rand.Next(2000, 2021), rand.Next(12), rand.Next(31), rand.Next(60), rand.Next(60), rand.Next(60));
                parcel.DroneId = rand.Next() % config.DronesIndexer;
                parcel.Scheduled = new DateTime(rand.Next(2000, 2021), rand.Next(12), rand.Next(31), rand.Next(60), rand.Next(60), rand.Next(60));
                parcel.PickedUp = new DateTime(rand.Next(2000, 2021), rand.Next(12), rand.Next(31), rand.Next(60), rand.Next(60), rand.Next(60));
                parcel.Delivered = new DateTime(rand.Next(2000, 2021), rand.Next(12), rand.Next(31), rand.Next(60), rand.Next(60), rand.Next(60));
            }
        }
    }
}
