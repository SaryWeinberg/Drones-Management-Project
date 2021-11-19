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
        static internal List<Drone> Drones = new List<Drone>();
        static internal List<Station> Stations = new List<Station>();
        static internal List<Customer> Customers = new List<Customer>();
        static internal List<Parcel> Parcels = new List<Parcel>();
        static internal List<DroneCharge> DroneCharges = new List<DroneCharge>();

        internal class config
        {
            public static double Available, Light, medium, heavy, chargingRate;       
        }

        static public void Initialize()
        {
            Random rand = new Random();
            for (int i = 0; i < 2; i++)
            {
                Station station = new Station();
                station.ID = Stations.Count; 
                station.Name = Stations.Count; 
                station.Longitude = rand.Next();
                station.Latitude = rand.Next();
                station.ChargeSlots = rand.Next(100);
                Stations.Add(station);                
            }

            for (int i = 0; i < 5; i++)
            {
                Drone drone = new Drone();
                drone.ID = Drones.Count;
                drone.Model = $"{Drones.Count}";
                drone.MaxWeight = (WeightCategories)(rand.Next(0, 2));                
                Drones.Add(drone);
            }

            for (int i = 0; i < 11; i++)
            {
                Customer customer = new Customer();
                customer.ID = rand.Next(100000000, 999999999); 
                customer.PhoneNum = rand.Next(111111111, 999999999);
                customer.Name = $"Customer{i}";
                customer.Longitude = rand.Next();
                customer.Latitude = rand.Next();                
                Customers.Add(customer);
            }

            for (int i = 0; i < 10; i++)
            {
                Parcel parcel = new Parcel();
                parcel.ID = Parcels.Count;
                parcel.SenderId = Customers[i].ID;
                parcel.TargetId = Customers[i+1].ID;
                parcel.Weight = (WeightCategories)(rand.Next(0, 2));
                parcel.Priority = (Priorities)(rand.Next(0, 2));
                parcel.Created = RandomDate();
                parcel.DroneId = rand.Next() % Drones.Count;
                parcel.Associated = RandomDate();
                parcel.PickedUp = RandomDate();
                parcel.Delivered = RandomDate();                
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