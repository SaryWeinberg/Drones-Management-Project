using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IBL.BO
{
    public class CustomerBL
    {
        private int id;
        private string name;
        private int phoneNum;

        public int ID {
            get { return id; }
            set
            {
                if (value > 100 && value < 999 && BL.BL.ValidateIDNumber(id))
                    id = value;
                else
                    throw new InvalidID();
            }
        }

        public string Name {
            get { return name; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                    name = value;
                else
                    throw new InvalidName();

            }
        }

        public int PhoneNum {
            get { return phoneNum; }
            set
            {
                if (value > 100000 && value < 10000000000)
                    id = value;
                else
                    throw new InvalidPhoneNumber();
            }
        }
        public Location Location { get; set; }

        public List<ParcelsAtTheCustomer> DeliveryToCustomer = new List<ParcelsAtTheCustomer>();

        public List<ParcelsAtTheCustomer> DeliveryFromCustomer = new List<ParcelsAtTheCustomer>();
    }
}

