using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class Customer
    {
        private int id;
        private string name;
        private int phoneNum;

        public int ID {
            get { return id; }
            set
            {
                if (value > 100000000 && value < 999999999 && BL.BL.ValidateIDNumber(id))
                    id = value;
                else
                    throw new InvalidObjException("ID");
            }
        }

        public string Name {
            get { return name; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                    name = value;
                else
                    throw new InvalidObjException("name");

            }
        }

        public int PhoneNum {
            get { return phoneNum; }
            set
            {
                if (value > 100000 && value < 1000000000)
                  phoneNum = value;
                else
                    throw new InvalidObjException("phone number");
            }
        }
        public Location Location { get; set; }

        public List<ParcelsAtTheCustomer> DeliveryToCustomer = new List<ParcelsAtTheCustomer>();

        public List<ParcelsAtTheCustomer> DeliveryFromCustomer = new List<ParcelsAtTheCustomer>();

        public override string ToString()
        {
            return "Customer: id: " + ID + " Phone number: " + PhoneNum + " name: " + Name + " location: " + Location;
        }
    }
}

