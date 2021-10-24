using System;
using IDAL.DO;





namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the delivery system by skimmers:\n1 " +
                "- Insert options.\n2 - Update options.\n3 - Display options.\n4 " +
                "- List view options.\n0 - Exit.");

            int Option = int.Parse(Console.ReadLine());


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
                            break;
                        case (int)AddOptions.AddDrone:
                            break;
                        case (int)AddOptions.AddParcel:
                            break;
                        case (int)AddOptions.AddStation:
                            break;
                        default:
                            Console.WriteLine("ERROR");
                            break;
                    }
                    break;
                //==============================================
                //UpdateOption
                case (int)UserOptions.Update:
                    Console.WriteLine("please select:\n" +
                        "1-Assing parcel to drone\n" +
                        "2-Collect parcel by drone\n" +
                        "3-Provide parcel to customer\n" +
                        "4-Release drone from charge  in station\n" +
                        "5- Send drone to charge in station"
                       );
                    int UpdateOption = int.Parse(Console.ReadLine());
                    switch (UpdateOption)
                    {
                        case (int)UpdateOptions.AssingParcelToDrone:
                            DalObject.DalObject.AssingParcelToDrone();
                            DalObject.DataSource.
                            break;
                        case (int)UpdateOptions.CollectParcelByDrone:
                            break;
                        case (int)UpdateOptions.ProvideParcelToCustomer:
                            break;
                        case (int)UpdateOptions.ReleaseDroneFromChargeInStation:
                            break;
                        case (int)UpdateOptions.SendDroneToChargeInStation:
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
                       "1-Add base-station\n" +
                       "2-Add Drone\n" +
                       "3-Add a new costumer\n" +
                       "4-Add a new Parcel to a costumer"
                      );
                    int DisplayOption = int.Parse(Console.ReadLine());
                    switch (DisplayOption)
                    {
                        case (int)DisplayOptions.DisplayCustomer:
                            break;
                        case (int)DisplayOptions.DisplayDrone:
                            break;
                        case (int)DisplayOptions.DisplayParcel:
                            break;
                        case (int)DisplayOptions.DisplayStation:
                            break;
                        default:
                            Console.WriteLine("ERROR");
                            break;
                    }
                    break;
                //==============================================
                case (int)UserOptions.ListDisplay:
                    Console.WriteLine("please select:\n" +
                       "1-Add base-station\n" +
                       "2-Add Drone\n" +
                       "3-Add a new costumer\n" +
                       "4-Add a new Parcel to a costumer"
                      );
                    int ListDisplayOption = int.Parse(Console.ReadLine());
                    switch (ListDisplayOption)
                    {
                        case (int)ListDisplayOptions.ViewAvailableStationLists:
                            break;
                        case (int)ListDisplayOptions.ViewCustomerLists:
                            break;
                        case (int)ListDisplayOptions.ViewDroneLists:
                            break;
                        case (int)ListDisplayOptions.ViewFreeParcelLists:
                            break;
                        case (int)ListDisplayOptions.ViewParcelLists:
                            break;
                        case (int)ListDisplayOptions.ViewStationLists:
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

        }
    }

}
