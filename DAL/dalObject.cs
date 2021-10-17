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

        public static void AddDrone()
        {

        }
        public static void AddCustomer()
        {

        }
        public static void AddParcel()
        {

        }
        public static void Update()
        {


        }

        //Updats//

        public static void AssingParcelToDrone()
        {


        }

        public static void CollectParcelByDrone()
        {


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
