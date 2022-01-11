using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using DalApi;
using DO;


namespace DAL
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
                XMLTools.SaveListToXMLSerializer<Customer>(DataSource.Customers, dir + customerFilePath);

            if (!File.Exists(dir + parcelFilePath))
                XMLTools.SaveListToXMLSerializer<Parcel>(DataSource.Parcels, dir + parcelFilePath);

            if (!File.Exists(dir + droneFilePath))
                XMLTools.SaveListToXMLSerializer<Drone>(DataSource.Drones, dir + droneFilePath);

            if (!File.Exists(dir + stationFilePath))
                XMLTools.SaveListToXMLSerializer<Station>(DataSource.Stations, dir + stationFilePath);

            if (!File.Exists(dir + droneChargeFilePath))
                XMLTools.SaveListToXMLSerializer<DroneCharge>(DataSource.DroneCharges, dir + droneChargeFilePath);
        }
              
        public double[] ElectricalPowerRequest()
        {
            throw new Exception();
        }        
    }
}