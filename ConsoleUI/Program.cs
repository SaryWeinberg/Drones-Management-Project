using System;
using IDAL.DO;
using DalObject;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleUI
{
    class Program
    {
        static public void Main(string[] args)
        {
/*            IDal DalObj = new IDal();
*/            DalObject.DalObject DalObj = new DalObject.DalObject();

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
                            case (int)AddOptions.AddStation: addStation(DalObj); break;
                            case (int)AddOptions.AddDrone: addDrone(DalObj); break;
                            case (int)AddOptions.AddCustomer: addCustomer(DalObj); break;
                            case (int)AddOptions.AddParcel: addParcel(DalObj); break;
                            default: Error(); break;
                        }
                        break;
                    //==============================================
                    //UpddateOption
                    case (int)UserOptions.Update:
                        Console.WriteLine("please select:\n" +
                            "1-Assing a parcel to a drone\n" +
                            "2-Collect a parcel by a drone\n" +
                            "3-Provide parcel to customer\n" +
                            "4-Send a drone to charge in a station\n" +
                            "5-Release drone from charge in station" 
                       );
                        int UpddateOption = int.Parse(Console.ReadLine());
                        switch (UpddateOption)
                        {
                            case (int)UpdateOptions.AssingParcelToDrone:
                                DalObj.AssingParcelToDrone(DalObj.FindParcel(GetByID("parcel")));
                                break;
                            case (int)UpdateOptions.CollectParcelByDrone:
                                DalObj.CollectParcelByDrone(DalObj.FindParcel(GetByID("parcel"))) ;
                                break;
                            case (int)UpdateOptions.ProvideParcelToCustomer:
                                DalObj.ProvideParcelToCustomer(DalObj.FindParcel(GetByID("parcel")));
                                break;
                            case (int)UpdateOptions.SendDroneToChargeInStation:
                                DalObj.SendDroneToChargeInStation(DalObj.FindDrone(GetByID("drone")), GetByID("Station"));
                                break;
                            case (int)UpdateOptions.ReleaseDroneFromChargeInStation:
                                DalObj.ReleaseDroneFromChargeInStation(DalObj.FindDrone(GetByID("drone")));
                                break;
                            default: Error(); break;
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
                                Console.WriteLine(DalObj.GetSpesificCustomer((ulong)GetByID("Customer")));
                                break;
                            case (int)DisplayOptions.DisplayDrone:
                                Console.WriteLine(DalObj.GetSpesificDrone(GetByID("Drone")));
                                break;
                            case (int)DisplayOptions.DisplayParcel:                      
                                Console.WriteLine(DalObj.GetSpesificParcel(GetByID("Parcel")));
                                break;
                            case (int)DisplayOptions.DisplayStation:                              
                                Console.WriteLine(DalObj.GetSpesificStation(GetByID("Station")));
                                break;
                            default: Error(); break;
                        }
                        break;
                    //==============================================
                    //ListDisplay
                    case (int)UserOptions.ListDisplay:
                        Console.WriteLine("please select:\n" +
                            "1-View Station Lists\n"+
                           "2-View Customer Lists\n" +
                           "3-View Drone Lists\n"+
                           "4-View Parcel Lists\n" +
                           "5-View Free Parcel Lists\n" +
                           "6-View Available Station Lists"
                          );
                        int ListDisplayOption = int.Parse(Console.ReadLine());
                        switch (ListDisplayOption)
                        {
                            case (int)ListDisplayOptions.ViewStationLists:
                                IEnumerable<Station> stations = DalObj.GetStationLists();
                                Station[] stationList = stations.Cast<Station>().ToArray();
                                PrintLists(stationList); 
                                break;
                            case (int)ListDisplayOptions.ViewCustomerLists:
                                IEnumerable<Customer> customers = DalObj.GetCustomerLists();
                                Customer[] customerList = customers.Cast<Customer>().ToArray();
                                PrintLists(customerList); 
                                break;
                            case (int)ListDisplayOptions.ViewDroneLists:
                                IEnumerable<Drone> drones = DalObj.GetDroneLists();
                                Drone[] droneList = drones.Cast<Drone>().ToArray();
                                PrintLists(droneList); 
                                break;
                            case (int)ListDisplayOptions.ViewParcelLists:
                                IEnumerable<Parcel> parcels = DalObj.GetParcelLists();
                                Parcel[] parcelList = parcels.Cast<Parcel>().ToArray();
                                PrintLists(parcelList); 
                                break;
                            case (int)ListDisplayOptions.ViewFreeParcelLists:
                                parcels = DalObj.GetFreeParcelLists();
                                parcelList = parcels.Cast<Parcel>().ToArray();
                                PrintLists(parcelList); 
                                break;
                            case (int)ListDisplayOptions.ViewAvailableStationLists:
                                stations = DalObj.GatAvailableStationLists();
                                stationList = stations.Cast<Station>().ToArray();
                                PrintLists(stationList); 
                                break;
                            default: Error(); break;
                        }
                        break;
                    //==============================================
                    default: Error(); break;
                }

            } while (Option != 0);


            //Functions for code readings

            static void addStation(DalObject.DalObject DalObj)
            {
                Console.WriteLine("Enter Longitude");
                int Longitude = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Latitude");
                int Latitude = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Charge Slots");
                int ChargeSlots = int.Parse(Console.ReadLine());
                DalObj.AddStation(Longitude, Latitude, ChargeSlots);
            }

            static void addDrone(DalObject.DalObject DalObj)
            {
                Console.WriteLine("Enter Model");
                string Model = (Console.ReadLine());
                Console.WriteLine("Enter Max Weight");
                string MaxWeight = (Console.ReadLine());
                WeightCategories maxWeight = (WeightCategories)Enum.Parse(typeof(WeightCategories), MaxWeight);
                Console.WriteLine("Enter Status");
                string Status = (Console.ReadLine());
                DroneStatus status = (DroneStatus)Enum.Parse(typeof(DroneStatus), Status);
                Console.WriteLine("Enter Battery");
                int Battery = int.Parse(Console.ReadLine());
                DalObj.AddDrone(Model, maxWeight, status, Battery);
            }

            static void addCustomer(DalObject.DalObject DalObj)
            {
                Console.WriteLine("Enter id");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter phone");
                string phone = (Console.ReadLine());
                Console.WriteLine("Enter name");
                string name = (Console.ReadLine());
                Console.WriteLine("Enter Longitude");
                int Longitude = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Latitude");
                int Latitude = int.Parse(Console.ReadLine());
                DalObj.AddCustomer(id, phone, name, Longitude, Latitude);
            }

            static void addParcel(DalObject.DalObject DalObj)
            {
                Console.WriteLine("Enter Sender Id");
                int SenderId = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Target Id");
                int TargetId = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Weight");
                string Weight = (Console.ReadLine());
                WeightCategories weight = (WeightCategories)Enum.Parse(typeof(WeightCategories), Weight);
                Console.WriteLine("Enter Priority");
                string Priority = (Console.ReadLine());
                Priorities priority = (Priorities)Enum.Parse(typeof(Priorities), Priority);
                Console.WriteLine("Enter Drone Id");
                int DroneId = int.Parse(Console.ReadLine());
                DalObj.AddParcel(SenderId, TargetId, weight, priority, DateTime.Now, DroneId, DateTime.Now, DateTime.Now, DateTime.Now);
            }
            
            static int GetByID(string typeOfID)
            {
                Console.WriteLine($"Enter {typeOfID} ID");
                int id = int.Parse(Console.ReadLine());
                return id;
            }

            static void PrintLists<T>(T[] list)
            {
                foreach (T item in list)
                {
                    Console.WriteLine(item);
                }
            }

            static void Error()
            {
                Console.WriteLine("ERROR");
            }

           

 
        }       
    }
}