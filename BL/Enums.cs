﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*namespace IBL.BO
{*/
    public enum DroneStatus
    {
        Available, Maintenance, Delivery
    }

    public enum Status
    {
        created, associated, collected, provided
    }

    /*public enum WeightCategories
    {
        Light = 1, Medium, Heavy
    }

    public enum Priorities
    {
        Normal, Medium, Emergency
    }*/

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
        UpdateCustomerData = 1, UpdateDroneName, UpdateStationData, SendDroneToCharge, ReleaseDroneFromCharge, AssignParcelToDrone, CollectParcelByDrone, DeliveryParcelByDrone
    }

    public enum DisplayOptions
    {
        DisplayCustomer = 1, DisplayDrone, DisplayParcel, DisplayStation
    }

    public enum ListDisplayOptions
    {
        DisplayStationsList = 1, DisplayCustomersList, DisplayDronesList, DisplayParcelsList, DisplayParcelsNotYetAssignedDroneList, DisplayAvailableStationsList
    }   
/*}*/
