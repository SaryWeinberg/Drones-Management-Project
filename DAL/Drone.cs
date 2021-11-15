using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.DO
{
    public struct Drone
    {
        public int id { get; set; }
        public string model { get; set; }
        public WeightCategories maxWeight { get; set; }
        public int active { get; set; }

        public override string ToString()
        {
            return "Drone: " + id + " " + model + " " + maxWeight ;
        }
    }
}
