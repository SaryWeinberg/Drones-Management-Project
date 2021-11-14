using System;
namespace IDAL.DO
{
    public struct Customer
    {
        public int id { get; set; }
        public int phoneNum { get; set; }
        public string name { get; set; }
        public int longitude { get; set; }
        public int latitude { get; set; }
        public override string ToString()
        {
            return "Customer: " + id + " " + phoneNum + " " + name + " " + longitude + " " + latitude;
        }
    }
}
