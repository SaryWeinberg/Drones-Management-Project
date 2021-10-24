using System;

namespace IDAL.DO

{
    public struct Customer
    {
        public int ID { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }

        public override string ToString()
        {
            return "Customer: " + ID + " " + Phone + " " + Name + " " + longitude + " " + latitude;
        }
    }
}
