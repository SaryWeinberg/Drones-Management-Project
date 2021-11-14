

public enum WeightCategories
{
    Light, Medium, Heavy
}

public enum Priorities
{
    Normal, Medium, Emergency
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