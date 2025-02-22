﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Drone
    {
        public int ID { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public bool Active { get; set; }

        public override string ToString()
        {
            return "Drone: " + ID + " " + Model + " " + MaxWeight ;
        }
    }
}
