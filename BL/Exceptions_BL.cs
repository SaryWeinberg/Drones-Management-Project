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

    public class NoBatteryToReachChargingStation : Exception
    {
        public NoBatteryToReachChargingStation() : base(String.Format("No battery to reach charging station")) { }
    }

    public class ThereAreNoAvelableChargeSlots : Exception
    {
        public ThereAreNoAvelableChargeSlots() : base(String.Format("There are no avelable charge slots")) { }
    }

    public class TheDroneNotAvailable : Exception
    {
        public TheDroneNotAvailable() : base(String.Format("The drone cannot be charged because it is not available")) { }
    }

    public class TheDroneNotInCharge : Exception
    {
        public TheDroneNotInCharge() : base(String.Format("The drone cannot be released because it is not charged")) { }
    }

}
