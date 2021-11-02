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
        }
    }
}
