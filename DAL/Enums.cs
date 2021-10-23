

public enum WeightCategories
{
    Light, Medium, Heavy
}

public enum Priorities
{
    Normal, Medium, Emergency
}

public enum DroneStatus
{
    Available, Maintenance, Delivery
}
public enum UserOptions
{
    Exit, Add, Update, Display, ListDisplay
}

public enum AddOptions
{
    AddStation, AddDrone, AddCustomer, AddParcel
}


public enum UpdateOptions
{
    AssingParcelToDrone, CollectParcelByDrone, ProvideParcelToCustomer, SendDroneToChargeInStation, ReleaseDroneFromChargeInStation
}

public enum DisplayOptions
{
    DisplayStation, DisplayDrone, DisplayCustomer, DisplayParcel, ReleaseDroneFromChargeInStation
}

public enum ListDisplayOptions
{
    ViewStationLists, ViewDroneLists, ViewCustomerLists, ViewParcelLists, ViewFreeParcelLists, ViewAvailableStationLists
}