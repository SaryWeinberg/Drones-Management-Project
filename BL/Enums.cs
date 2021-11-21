using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public enum DroneStatus
    {
        Available, Maintenance, Delivery
    }

    public enum Status
    {
        created, associated, collected, provided
    }

    public enum UserOptions
    {
        Exit, Add, Update, Display, ListDisplay
    }

    public enum AddOptions
    {
        AddStation = 1, AddDrone, AddCustomer, AddParcel
    }

    public enum UpdateOptions
    {
        AssingParcelToDrone = 1, CollectParcelByDrone, ProvideParcelToCustomer, SendDroneToChargeInStation, ReleaseDroneFromChargeInStation
    }

    public enum DisplayOptions
    {
        DisplayCustomer = 1, DisplayDrone, DisplayParcel, DisplayStation
    }

    public enum ListDisplayOptions
    {
        ViewStationLists = 1, ViewCustomerLists, ViewDroneLists, ViewParcelLists, ViewFreeParcelLists, ViewAvailableStationLists
    }
}
