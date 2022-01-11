﻿using System;
namespace DO
{
    public struct Customer
    {
        public int ID { get; set; }
        public int PhoneNum { get; set; }
        public string Name { get; set; }
        public int Longitude { get; set; }
        public int Latitude { get; set; }
        public bool Active { get; set; }

        public override string ToString()
        {
            return "Customer: " + ID + " " + PhoneNum + " " + Name + " " + Longitude + " " + Latitude;
        }
    }
}
