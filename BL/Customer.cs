using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IBL.BO
{
    class Customer
    {
        private ulong pid;
        private string pname;
        private ulong pphoneNum;

        public ulong id
        {
            get { return pid; }
            set
            {
                if (value > 100000000 && value < 999999999 && BL.BL.ValidateIDNumber(pid))
                    pid = value;
                else
                    throw new InvalidID();
            }
        }

        public string name
        {
            get { return pname; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                    pname = value;
                else
                    throw new InvalidName();

            }
        }

        public ulong phoneNum
        {
            get { return pphoneNum; }
            set
            {
                if (value > 100000 && value < 10000000000)
                    pid = value;
                else
                    throw new InvalidPhoneNumber();
            }
        }
        public Location location { get; set; }

        public List<ParcelsAtTheCustomer> deliveryToCustomer = new List<ParcelsAtTheCustomer>();

        public List<ParcelsAtTheCustomer> deliveryFromCustomer = new List<ParcelsAtTheCustomer>();
    }
}

