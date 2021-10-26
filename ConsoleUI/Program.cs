using System;
using IDAL.DO;
using DalObject;
using System.Collections.Generic;

namespace ConsoleUI
{
    class Program
    {
        static public void Main(string[] args)
        {
            int Option;
            do
            {
                Console.WriteLine("Welcome to the delivery system by drones:\n" +
                    "1 - Insert options.\n" +
                    "2 - Update options.\n" +
                    "3 - Display options.\n" +
                    "4 - List view options.\n" +
                    "0 - Exit.");

                Option = int.Parse(Console.ReadLine());

                int id;
                string phone;
                string name;
                double Longitude;
                double Latitude;
                string Model;
                string MaxWeight;
                string Status;
                int Battery;
                int SenderId;
                int TargetId;
                string Weight;
                string Priority;
                int DroneId;
                int ChargeSlots;
                int ParcelId;
                        DalObject.DalObject d = new DalObject.DalObject();
                switch (Option)
                {
                    case (int)UserOptions.Exit:
                        break;
                    //==============================================
                    //AddOption
                    case (int)UserOptions.Add:
                        Console.WriteLine("please select:\n" +
                            "1-Add base-station\n" +
                            "2-Add Drone\n" +
                            "3-Add a new costumer\n" +
                            "4-Add a new Parcel to a costumer"
                           );
                        int AddOption = int.Parse(Console.ReadLine());
                        switch (AddOption)
                        {
                            case (int)AddOptions.AddCustomer:
                                Console.WriteLine("Enter id");
                                id = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter phone");
                                phone = (Console.ReadLine());
                                Console.WriteLine("Enter name");
                                name = (Console.ReadLine());
                                Console.WriteLine("Enter Longitude");
                                Longitude = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter Latitude");
                                Latitude = int.Parse(Console.ReadLine());
                                DalObject.DalObject.AddCustomer(id, phone, name, Longitude, Latitude);
                                break;
                            case (int)AddOptions.AddDrone:
                                Console.WriteLine("Enter Model");
                                Model = (Console.ReadLine());
                                Console.WriteLine("Enter Max Weight");
                                MaxWeight = (Console.ReadLine());
                                WeightCategories maxWeight = (WeightCategories)Enum.Parse(typeof(WeightCategories), MaxWeight);
                                Console.WriteLine("Enter Status");
                                Status = (Console.ReadLine());
                                DroneStatus status = (DroneStatus)Enum.Parse(typeof(DroneStatus), Status);
                                Console.WriteLine("Enter Battery");
                                Battery = int.Parse(Console.ReadLine());
                                DalObject.DalObject.AddDrone(Model, maxWeight, status, Battery);
                                break;
                            case (int)AddOptions.AddParcel:
                                Console.WriteLine("Enter Sender Id");
                                SenderId = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter Target Id");
                                TargetId = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter Weight");
                                Weight = (Console.ReadLine());
                                WeightCategories weight = (WeightCategories)Enum.Parse(typeof(WeightCategories), Weight);
                                Console.WriteLine("Enter Priority");
                                Priority = (Console.ReadLine());
                                Priorities priority = (Priorities)Enum.Parse(typeof(Priorities), Priority);
                                Console.WriteLine("Enter Drone Id");
                                DroneId = int.Parse(Console.ReadLine());
                                DalObject.DalObject.AddParcel(SenderId, TargetId, weight, priority, DateTime.Now, DroneId, DateTime.Now, DateTime.Now, DateTime.Now);
                                break;
                            case (int)AddOptions.AddStation:
                                Console.WriteLine("Enter Longitude");
                                Longitude = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter Latitude");
                                Latitude = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter Charge Slots");
                                ChargeSlots = int.Parse(Console.ReadLine());
                                DalObject.DalObject.AddStation(Longitude, Latitude, ChargeSlots);
                                break;
                            default:
                                Console.WriteLine("ERROR");
                                break;
                        }
                        break;
                    //==============================================
                    //UpddateOption
                    case (int)UserOptions.Update:
                        Console.WriteLine("please select:\n" +
                            "1-Assing a parcel to a drone\n" +
                            "2-Collect a parcel by a drone\n" +
                            "3-Supply a parcel to a customer\n" +
                            "4-Send a drone to charge in a station"
                       );
                        int UpddateOption = int.Parse(Console.ReadLine());
                        switch (UpddateOption)
                        {
                            case (int)UpdateOptions.AssingParcelToDrone:
                                DalObject.DalObject.AssingParcelToDrone(DalObject.DalObject.FindParcel());
                                break;
                            case (int)UpdateOptions.CollectParcelByDrone:
                                DalObject.DalObject.CollectParcelByDrone(DalObject.DalObject.FindParcel());
                                break;
                            case (int)UpdateOptions.ProvideParcelToCustomer:
                                DalObject.DalObject.ProvideParcelToCustomer(DalObject.DalObject.FindParcel());
                                break;
                            case (int)UpdateOptions.SendDroneToChargeInStation:
                                Console.WriteLine("Enter station id:");
                                int StationId = int.Parse(Console.ReadLine());
                                DalObject.DalObject.SendDroneToChargeInStation(DalObject.DalObject.FindDrone(), StationId);
                                break;
                            case (int)UpdateOptions.ReleaseDroneFromChargeInStation:
                                DalObject.DalObject.ReleaseDroneFromChargeInStation(DalObject.DalObject.FindDrone());
                                break;
                            default:
                                Console.WriteLine("ERROR");
                                break;
                        }
                        break;
                    //==============================================
                    //DisplayOptions
                    case (int)UserOptions.Display:
                        Console.WriteLine("please select:\n" +
                           "1-Display specific customer\n" +
                           "2-Display specific drone\n" +
                           "3-Display specific parcel\n" +
                           "4-Display specific station"
                          );
                        int DisplayOption = int.Parse(Console.ReadLine());
                        switch (DisplayOption)
                        {
                            case (int)DisplayOptions.DisplayCustomer:
                                Console.WriteLine("Enter Customer ID");
                                id = int.Parse(Console.ReadLine());
                                Console.WriteLine(DalObject.DalObject.DisplayCustomer(id));
                                break;
                            case (int)DisplayOptions.DisplayDrone:
                                Console.WriteLine("Enter Drone ID");
                                id = int.Parse(Console.ReadLine());
                                Console.WriteLine(DalObject.DalObject.DisplayDrone(id));
                                break;
                            case (int)DisplayOptions.DisplayParcel:
                                Console.WriteLine("Enter Parcel ID");
                                id = int.Parse(Console.ReadLine());
                                Console.WriteLine(DalObject.DalObject.DisplayParcel(id));
                                break;
                            case (int)DisplayOptions.DisplayStation:
                                Console.WriteLine("Enter Station ID");
                                id = int.Parse(Console.ReadLine());
                                Console.WriteLine(DalObject.DalObject.DisplayStation(id));
                                break;
                            default:
                                Console.WriteLine("ERROR");
                                break;
                        }
                        break;
                    //==============================================
                    //ListDisplay
                    case (int)UserOptions.ListDisplay:
                        Console.WriteLine("please select:\n" +
                           "1-View Available Station Lists\n" +
                           "2-View Customer Lists\n" +
                           "3-View Free Parcel Lists\n" +
                           "4-View Parcel Lists\n" +
                           "5-View Station Lists"
                          );
                        int ListDisplayOption = int.Parse(Console.ReadLine());
                        switch (ListDisplayOption)
                        {
                            case (int)ListDisplayOptions.ViewAvailableStationLists:
                                IEnumerable<Station> stations = DalObject.DalObject.ViewAvailableStationLists();
                                foreach (Station item in stations)
                                {
                                    Console.WriteLine(item);
                                }
                                break;
                            case (int)ListDisplayOptions.ViewCustomerLists:
                                IEnumerable<Customer> customers = DalObject.DalObject.ViewCustomerLists();
                                foreach (Customer item in customers)
                                {
                                    Console.WriteLine(item);
                                }
                                break;
                            case (int)ListDisplayOptions.ViewDroneLists:
                                IEnumerable<Drone> drones = DalObject.DalObject.ViewDroneLists();
                                foreach (Drone item in drones)
                                {
                                    Console.WriteLine(item);
                                }
                                break;
                            case (int)ListDisplayOptions.ViewFreeParcelLists:
                                IEnumerable<Parcel> parcels = DalObject.DalObject.ViewFreeParcelLists();
                                foreach (Parcel item in parcels)
                                {
                                    Console.WriteLine(item);
                                }
                                break;
                            case (int)ListDisplayOptions.ViewParcelLists:
                                parcels = DalObject.DalObject.ViewParcelLists();
                                foreach (Parcel item in parcels)
                                {
                                    Console.WriteLine(item);
                                }
                                break;
                            case (int)ListDisplayOptions.ViewStationLists:
                                stations = DalObject.DalObject.ViewStationLists();
                                foreach (Station item in stations)
                                {
                                    Console.WriteLine(item);
                                }
                                break;
                            default:
                                Console.WriteLine("ERROR");
                                break;
                        }
                        break;
                    //==============================================
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
            } while (Option != 0);
        }
    }
}