using System;
namespace IDAL.DO
{
    public struct Customer
    {
        public ulong id { get; set; }
        public ulong phoneNum { get; set; }
        public string name { get; set; }
        public int longitude { get; set; }
        public int latitude { get; set; }
        public override string ToString()
        {
            return "Customer: " + id + " " + phoneNum + " " + name + " " + longitude + " " + latitude;
        }
    }
}
