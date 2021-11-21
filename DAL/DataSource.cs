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
            public static double Available =0.02, Light =0.3, medium= 0.5, heavy=0.7, chargingRate=0.8;       
        }

        static public void Initialize()
        {
            Random rand = new Random();
            for (int i = 0; i < 2; i++)
            {
                Station station = new Station();
                station.ID = Stations.Count; 
                station.Name = Stations.Count; 
                station.Longitude = rand.Next(1,40);
                station.Latitude = rand.Next(1,40);
                station.ChargeSlots = rand.Next(1,100);
                Stations.Add(station);                
            }

            for (int i = 0; i < 5; i++)
            {
                Drone drone = new Drone();
                drone.ID = Drones.Count;
                drone.Model = $"{Drones.Count}";
                drone.MaxWeight = (WeightCategories)(rand.Next(1, 4));                
                Drones.Add(drone);
            }

            for (int i = 0; i < 11; i++)
            {
                Customer customer = new Customer();
                customer.ID = rand.Next(100000000, 999999999); 
                customer.PhoneNum = rand.Next(111111111, 999999999);
                customer.Name = $"Customer{i}";
                customer.Longitude = rand.Next(1,40);
                customer.Latitude = rand.Next(1,40);                
                Customers.Add(customer);
            }

            for (int i = 0; i < 10; i++)
            {
                Parcel parcel = new Parcel();
                parcel.ID = Parcels.Count;
                parcel.SenderId = Customers[i].ID;
                parcel.TargetId = Customers[i+1].ID;
                parcel.Weight = (WeightCategories)(rand.Next(1, 4));
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
            DateTime start = new DateTime(1995, 1, 1);
            Random rand = new Random();
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rand.Next(range));
        }
    }
}