using System;
namespace DO
{
    public struct Customer
    {
        public int ID { get; set; }
        public int PhoneNum { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public bool Active { get; set; }

        public override string ToString()
        {
            return "Customer: " + ID + " " + PhoneNum + " " + Name + " " + Longitude + " " + Latitude;
        }
    }
}
