using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class InvalidID : Exception
    {
        public InvalidID() : base(String.Format("Incorrect ID number")) { }
    }

    public class InvalidPhoneNumber : Exception
    {
        public InvalidPhoneNumber() : base(String.Format("The phone number is incorrect")) { }
    }


    public class InvalidName : Exception
    {
        public InvalidName() : base(String.Format("The name is incorrect")) { }
    }   
}
