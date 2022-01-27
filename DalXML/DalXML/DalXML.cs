using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using DalApi;
using DO;
using System.Runtime.CompilerServices;

namespace Dal
{
    public partial class DalXml : IDal
    {
        internal static DalXml instance;

        public static DalXml GetInstance {
            get
            {
                if (instance == null)
                    instance = new DalXml();
                return instance;
            }
        }

        static string direction = @"..\..\..\..\XmlData\";
        static DalXml()
        {
            if (!Directory.Exists(direction))
                Directory.CreateDirectory(direction);
        }

        string stationFilePath = @"StationList.xml";
        string customerFilePath = @"CustomerList.xml";
        string parcelFilePath = @"ParcelList.xml";
        string droneFilePath = @"DroneList.xml";
        string droneChargeFilePath = @"DroneChargeList.xml";

        public DalXml()
        {
            DataSource.Initialize();
            if (!File.Exists(direction + customerFilePath))
                XmlTools.SaveListToXmlSerializer<Customer>(DataSource.Customers, direction + customerFilePath);

            if (!File.Exists(direction + parcelFilePath))
                XmlTools.SaveListToXmlSerializer<Parcel>(DataSource.Parcels, direction + parcelFilePath);

            if (!File.Exists(direction + droneFilePath))
                XmlTools.SaveListToXmlSerializer<Drone>(DataSource.Drones, direction + droneFilePath);

            if (!File.Exists(direction + stationFilePath))
                XmlTools.SaveListToXmlSerializer<Station>(DataSource.Stations, direction + stationFilePath);

            if (!File.Exists(direction + droneChargeFilePath))
                XmlTools.SaveListToXmlSerializer<DroneCharge>(DataSource.DroneCharges, direction + droneChargeFilePath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double[] ElectricalPowerRequest()
        {
            double[] arr = { DataSource.config.Available, DataSource.config.Light, DataSource.config.medium, DataSource.config.heavy, DataSource.config.chargingRate };
            return arr;
        }     
    }
}