using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using System.Runtime.CompilerServices;
namespace Dal
{
    internal partial class DalObject : IDal
    {
        DalObject()
        {
            DataSource.Initialize();
        }

        internal static DalObject instance;

        public static DalObject GetInstance {
            get
            {
                if (instance == null)
                    instance = new DalObject();
                return instance;
            }        
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double[] ElectricalPowerRequest()
        {
            double[] arr = { DataSource.config.Available, DataSource.config.Light, DataSource.config.medium, DataSource.config.heavy, DataSource.config.chargingRate };
            return arr;
        }      
    }
}