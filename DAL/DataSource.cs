using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    class DataSource
    {
        /// <arrays>
        static internal Drone[] Drones = new Drone[10];
        static internal Station[] BaseStations = new Station[5];
        static internal Customer[] customers = new Customer[100];
        static internal Parcel[] Parcels = new Parcel[1000];
        /// </arrays>

        internal class config
        {
            static internal int DronesIndexer = 0;
            static internal int BaseStationsIndexer = 0;
            static internal int customersIndexer = 0;
            static internal int ParcelsIndexer = 0;

        }
        static public void Initialize()
        {


        }
    }
}
