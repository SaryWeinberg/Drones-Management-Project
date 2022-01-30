using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StationToList
    {
        public StationToList(Station station)
        {
            ID = station.ID;
            Name = station.Name;
            AveChargeSlots = station.AveChargeSlots - station.DronesInChargelist.Count;
            FullChargeSlots = station.DronesInChargelist.Count;
        }
        public int ID { get; set; }
        public int Name { get; set; }
        public double AveChargeSlots { get; set; }
        public double FullChargeSlots { get; set; }

        public override string ToString()
        {
            return "ID: " + ID + " Name: " + Name + " AveChargeSlots: " + AveChargeSlots + " FullChargeSlots: " + FullChargeSlots;
        }
    }
}
