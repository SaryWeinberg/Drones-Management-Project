using System;

namespace IBL.BO
{
    public class CustomerInParcel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return " id: " + ID + " name: " + Name ;
        }
    }
}
