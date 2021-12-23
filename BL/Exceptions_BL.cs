﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class InvalidObjException : Exception
    {
        public InvalidObjException(string obj) : base(String.Format($"Incorrect {obj}")) { }
    }

    public class NoBatteryToReachChargingStationException : Exception
    {
        public NoBatteryToReachChargingStationException() : base(String.Format("No battery to reach charging station")) { }
    }

    public class ThereAreNoAvelableChargeSlotsException : Exception
    {
        public ThereAreNoAvelableChargeSlotsException() : base(String.Format("There are no avelable charge slots")) { }
    }

    public class TheDroneNotAvailableException : Exception
    {
        public TheDroneNotAvailableException() : base(String.Format("The drone is not available")) { }
    }

    public class TheDroneNotInChargeException : Exception
    {
        public TheDroneNotInChargeException() : base(String.Format("The drone cannot be released because it is not charged")) { }
    }

    public class CanNotAssignParcelToDroneException : Exception
    {
        public CanNotAssignParcelToDroneException() : base(String.Format("The drone can not take any parcel")) { }
    }

    public class TheParcelCouldNotCollectedOrDeliveredException : Exception
    {
        public TheParcelCouldNotCollectedOrDeliveredException(string str) : base(String.Format($"The parcel could not be {str}")) { }
    }

    public class ObjectNotExistException : Exception
    {
        public ObjectNotExistException(string msg) : base(String.Format($"{msg}")) { }
    }

    public class ObjectAlreadyExistException : Exception
    {
        public ObjectAlreadyExistException(string obj, int id) : base(String.Format($"{obj}, ID - {id} already exist")) { }
    }
}
