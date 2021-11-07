using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    class BL: IBL
    {
       

        public BL()
        {
            IDal dalObj = new DalObject.DalObject();
            double [] ElectricUse = dalObj.ElectricalPowerRequest();
            double Available = ElectricUse[0];
            double Light = ElectricUse[1];
            double medium = ElectricUse[2];
            double heavy = ElectricUse[3];
            double chargingRate = ElectricUse[4];
            List<Drone> DroneList = dalObj.getDrones();

        }

        void CheackDrones(List<Drone> DroneList)
        {
            DroneList.
        }
    }
}
