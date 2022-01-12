using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using DalApi;
using DO;


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

        static string dir = @"..\..\..\..\xmlData\";
        static DalXml()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        string stationFilePath = @"StationList.xml";
        string customerFilePath = @"CustomerList.xml";
        string parcelFilePath = @"ParcelList.xml";
        string droneFilePath = @"DroneList.xml";
        string droneChargeFilePath = @"DroneChargeList.xml";

        public DalXml()
        {
            DataSource.Initialize();
            if (!File.Exists(dir + customerFilePath))
                XmlTools.SaveListToXmlSerializer<Customer>(DataSource.Customers, dir + customerFilePath);

            if (!File.Exists(dir + parcelFilePath))
                XmlTools.SaveListToXmlSerializer<Parcel>(DataSource.Parcels, dir + parcelFilePath);

            if (!File.Exists(dir + droneFilePath))
                XmlTools.SaveListToXmlSerializer<Drone>(DataSource.Drones, dir + droneFilePath);

            if (!File.Exists(dir + stationFilePath))
                XmlTools.SaveListToXmlSerializer<Station>(DataSource.Stations, dir + stationFilePath);

            if (!File.Exists(dir + droneChargeFilePath))
                XmlTools.SaveListToXmlSerializer<DroneCharge>(DataSource.DroneCharges, dir + droneChargeFilePath);
        }
              
        public double[] ElectricalPowerRequest()
        {
            double[] arr = { DataSource.config.Available, DataSource.config.Light, DataSource.config.medium, DataSource.config.heavy, DataSource.config.chargingRate };
            return arr;
        }      
    }
}