using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IBL.BO
{
    class Customer
    {
        private ulong id;
        private string name;
        private ulong phoneNum;

        public ulong ID
        {
            get { return id; }
            set
            {
                if (value > 100000000 && value < 999999999 && BL.BL.ValidateIDNumber(id))
                    id = value;
                else
                    throw new InvalidID();
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                    name = value;
                else
                    throw new InvalidName();

            }
        }

        public ulong PhoneNum
        {
            get { return phoneNum; }
            set
            {
                if (value > 100000 && value < 10000000000)
                    id = value;
                else
                    throw new InvalidPhoneNumber();
            }
        }
        public Location location { get; set; }

        public List<ParcelsAtTheCustomer> DeliveryToCustomer = new List<ParcelsAtTheCustomer>();

        public List<ParcelsAtTheCustomer> DeliveryFromCustomer = new List<ParcelsAtTheCustomer>();
    }
}

