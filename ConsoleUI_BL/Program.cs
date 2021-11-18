using System;
using System.Collections.Generic;
using System.Linq;
using IBL.BO;
using IBL;
using BL;

namespace ConsoleUI
{
    class Program
    {
        static public void Main(string[] args)
        {
            IBL.IBL bl = new BL.BL();

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
                            case (int)AddOptions.AddStation:
                                try { addStation(bl); }
                                catch (InvalidID e) { Console.WriteLine(e); }
                                break;
                            case (int)AddOptions.AddDrone:
                                try { addDrone(bl); }
                                catch (InvalidID e) { Console.WriteLine(e); }
                                break;
                            case (int)AddOptions.AddCustomer:
                                try { addCustomer(bl); }
                                catch (InvalidID e) { Console.WriteLine(e); }
                                catch (InvalidName e) { Console.WriteLine(e); }
                                break;
                            case (int)AddOptions.AddParcel:
                                try { addParcel(bl); }
                                catch (InvalidID e) { Console.WriteLine(e); }
                                break;
                            default:
                                Error(); break;

                        }
                        break;
                    //==============================================
                    //UpddateOption
                    case (int)UserOptions.Update:
                        Console.WriteLine("please select:\n" +
                            "1-Update customer data\n" +
                            "2-Update drone name\n" +
                            "3-Update station data\n" +
                            "4-Send a drone to charge in a station\n" +
                            "5-Release drone from charge in station" +
                            "6-Assing a parcel to a drone\n" +
                            "7-Collect a parcel by a drone\n" +
                            "8-Delivery parcel by drone"
                       );
                        int UpddateOption = int.Parse(Console.ReadLine());
                        switch (UpddateOption)                        {

                            case (int)UpdateOptions.UpdateCustomerData:
                                Console.WriteLine(bl.UpdateCustomerData(GetInt("customer", "ID")));
                                break;
                            case (int)UpdateOptions.UpdateDroneName:
                                Console.WriteLine(bl.UpdateDroneName(GetInt("drone", "ID"), GetString("drone", "name")));
                                break;
                            case (int)UpdateOptions.UpdateStationData:
                                Console.WriteLine(bl.UpdateStationData(GetInt("station", "ID")));
                                break;
                            case (int)UpdateOptions.SendDroneToCharge:
                                Console.WriteLine(bl.SendDroneToCharge(GetInt("drone", "ID")));
                                break;
                            case (int)UpdateOptions.ReleaseDroneFromCharge:
                                Console.WriteLine(bl.ReleaseDroneFromCharge(GetInt("drone", "ID"), GetInt("charging", "time")));
                                break;
                            case (int)UpdateOptions.AssignParcelToDrone:
                                Console.WriteLine(bl.AssignParcelToDrone(GetInt("drone", "ID")));
                                break;
                            case (int)UpdateOptions.CollectParcelByDrone:
                                Console.WriteLine(bl.CollectParcelByDrone(GetInt("drone", "ID")));
                                break;
                            case (int)UpdateOptions.DeliveryParcelByDrone:
                                Console.WriteLine(bl.DeliveryParcelByDrone(GetInt("drone", "ID")));
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
                                Console.WriteLine(bl.GetSpesificCustomerBL(GetInt("customer", "ID")));
                                break;
                            case (int)DisplayOptions.DisplayDrone:
                                Console.WriteLine(bl.GetSpesificDroneBL(GetInt("drone", "ID")));
                                break;
                            case (int)DisplayOptions.DisplayParcel:
                                Console.WriteLine(bl.GetSpesificParcelBL(GetInt("parcel", "ID")));
                                break;
                            case (int)DisplayOptions.DisplayStation:
                                Console.WriteLine(bl.GetSpesificStationBL(GetInt("station", "ID")));
                                break;
                            default: Error(); break;
                        }
                        break;
                    //==============================================
                    //ListDisplay
                    case (int)UserOptions.ListDisplay:
                        Console.WriteLine("please select:\n" +
                           "1-Display Stations List\n" +
                           "2-Display Customers List\n" +
                           "3-Display Drones List\n" +
                           "4-Display Parcels List\n" +
                           "5-Display Parcels Not Yet Assigned Drone List\n" +
                           "6-Display Available Stations List"
                          );
                        int ListDisplayOption = int.Parse(Console.ReadLine());
                        switch (ListDisplayOption)
                        {
                            case (int)ListDisplayOptions.DisplayStationsList: 
                                foreach(StationBL station in bl.GetStationsBL())
                                {
                                    Console.WriteLine(station);
                                }
                                break;
                            case (int)ListDisplayOptions.DisplayCustomersList:
                                foreach (CustomerBL customer in bl.GetCustomersBL())
                                {
                                    Console.WriteLine(customer);
                                }
                                break;
                            case (int)ListDisplayOptions.DisplayDronesList:
                                foreach (DroneBL drone in bl.GetDronesBL())
                                {
                                    Console.WriteLine(drone);
                                }
                                break;
                            case (int)ListDisplayOptions.DisplayParcelsList:
                                foreach (ParcelBL parcel in bl.GetParcelsBL())
                                {
                                    Console.WriteLine(parcel);
                                }
                                break;
                            case (int)ListDisplayOptions.DisplayParcelsNotYetAssignedDroneList:
                                foreach (ParcelBL parcel in bl.GetParcelsNotYetAssignedDroneList())
                                {
                                    Console.WriteLine(parcel);
                                }
                                break;
                            case (int)ListDisplayOptions.DisplayAvailableStationsList:
                                foreach (StationBL station in bl.GEtAvailableStationsList())
                                {
                                    Console.WriteLine(station);
                                }
                                break;
                            default: Error(); break;
                        }
                        break;
                    //==============================================
                    default: Error(); break;
                }

            } while (Option != 0);


            //Functions for code readings

            static void addStation(IBL.IBL bl)
            {
                int ID = GetInt("station", "ID");
                int Name = GetInt("station", "name");
                int Longitude = GetInt("station", "longitude");
                int Latitude = GetInt("station", "latitude");
                int ChargeSlots = GetInt("station", "charge slots");
                Console.WriteLine(bl.AddStationBL(ID, Name, new Location { Longitude = Longitude, Latitude = Latitude }, ChargeSlots));
            }

            static void addDrone(IBL.IBL bl)
            {
                int ID = GetInt("drone", "ID");
                string Model = GetString("drone", "model");
                string MaxWeight = GetString("drone", "max weight");
                WeightCategories maxWeight = (WeightCategories)Enum.Parse(typeof(WeightCategories), MaxWeight);
                int stationID = GetInt("station", "ID");
                Console.WriteLine(bl.AddDroneBL(ID, Model, maxWeight, stationID));
            }

            static void addCustomer(IBL.IBL bl)
            {
                int id = GetInt("customer", "ID");
                int phone = GetInt("customer", "phone");
                string name = GetString("customer", "name");
                int Longitude = GetInt("customer", "longitude");
                int Latitude = GetInt("customer", "latitude");
                Console.WriteLine(bl.AddCustomerBL(id, phone, name, new Location { Longitude = Longitude, Latitude = Latitude }));
            }

            static void addParcel(IBL.IBL bl)
            {
                int SenderId = GetInt("sender", "ID");
                int TargetId = GetInt("target", "ID");
                string Weight = GetString("parcel", "weight");
                WeightCategories weight = (WeightCategories)Enum.Parse(typeof(WeightCategories), Weight);
                string Priority = GetString("parcel", "priority");
                Priorities priority = (Priorities)Enum.Parse(typeof(Priorities), Priority);
                Console.WriteLine(bl.AddParcelBL(SenderId, TargetId, weight, priority));
            }

            static int GetInt(string typeOfInt, string item)
            {
                Console.WriteLine($"Enter {typeOfInt} {item}");
                int integer = int.Parse(Console.ReadLine());
                return integer;
            }

            static string GetString(string typeOfString, string item)
            {
                Console.WriteLine($"Enter {typeOfString} {item}");
                string str = Console.ReadLine();
                return str;
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