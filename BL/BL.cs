using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    class BL : IBL
    {


        public BL()
        {
            IDal dalObj = new DalObject.DalObject();
            double[] ElectricUse = dalObj.ElectricalPowerRequest();
            double Available = ElectricUse[0];
            double Light = ElectricUse[1];
            double medium = ElectricUse[2];
            double heavy = ElectricUse[3];
            double chargingRate = ElectricUse[4];
            List<Drone> DroneList = dalObj.getDrones();
            List<Station> StationList = dalObj.getStations();
            List<Parcel> ParcelList = dalObj.getParcels();
            List<Customer> CustomerList = dalObj.getCustomers();
        }

        public object ParcelList { get; private set; }

        /* void CheackDrones(List<Drone> DroneList)
         {
             DroneList.
         }*/

        /*  bool CheckExistParcelId(int id)
          {
              ParcelList.Find(p=> id == p.id);
              return true;
          }*/
        public bool validateIDNumber(ulong id)
        {
            int sum = 0, digit;
            for (int i = 0; i < 9; i++)
            {
                digit = (int)(id % 10);
                if ((i % 2) == 0) 
                    sum += digit * 2;
                else 
                    sum += digit;
                id /= 10;
            }
            if ((sum % 10) == 0)
                return true;
            return false;
        }
    }
}
