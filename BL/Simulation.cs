using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BO;


namespace BL
{
    class Simulation
    {
        int Delay = 1000;
        int DroneSpeed = 2000;

        public Simulation(BL blapi, int DroneId, Action<int> ViewUpdate, Func<bool> ToStop)
        {
            while (!ToStop())
            {
                BO.Drone drone = blapi.GetSpesificDrone(DroneId);
                switch (drone.Status)
                {
                    case DroneStatus.Available:
                        Thread.Sleep(Delay);

                        try
                        {
                            blapi.AssignParcelToDrone(DroneId);
                            ViewUpdate(1);
                            Thread.Sleep(Delay);
                        }
                        catch (Exception ex) when (ex is CanNotAssignParcelToDroneException || ex is ObjectNotExistException)
                        {
                            if (drone.Battery == 100)
                            {
                                Thread.Sleep(3000);
                            }
                            else
                            {
                                progressDroneLocation(ref drone, ViewUpdate, blapi, blapi.GetNearestAvailableStation(drone.Location).Location, 0);
                                blapi.SendDroneToCharge(DroneId);
                                ViewUpdate(1);
                                Thread.Sleep(Delay);

                            }
                        }

                        ViewUpdate(1);
                        Thread.Sleep(Delay);

                        break;
                    case DroneStatus.Maintenance:
                        TimeSpan timeInCharge = DateTime.Now - blapi.GetSpecificDroneInCharge(DroneId).DroneEnterToCharge;
                        while (drone.Battery + (timeInCharge.Minutes * blapi.ElectricalPowerRequest(4)) < 100)
                        {
                            timeInCharge = DateTime.Now - blapi.GetSpecificDroneInCharge(DroneId).DroneEnterToCharge;
                            drone.Battery += timeInCharge.Minutes * blapi.ElectricalPowerRequest(4);
                            ViewUpdate(1);
                            Thread.Sleep(100);
                        }
                        blapi.ReleaseDroneFromCharge(DroneId, timeInCharge.Minutes);
                        ViewUpdate(1);
                        break;
                    case DroneStatus.Delivery:

                        if (drone.Parcel.isWaitingToDelivery)
                        {

                            Location droneLocation = new Location() { Latitude = drone.Location.Latitude, Longitude = drone.Location.Longitude };
                            progressDroneLocation(ref drone, ViewUpdate, blapi, drone.Parcel.PickUpLocation, 0);
                            Thread.Sleep(Delay);
                            blapi.CollectParcelByDrone(DroneId);

                            ViewUpdate(1);
                            Thread.Sleep(Delay);

                        }
                        else
                        {
                            progressDroneLocation(ref drone, ViewUpdate, blapi, drone.Parcel.TargetLocation, drone.Parcel.Weight);
                            ViewUpdate(1);
                            blapi.SupplyParcelByDrone(DroneId);

                            ViewUpdate(1);
                            Thread.Sleep(Delay);
                        }
                        ViewUpdate(1);
                        break;
                    default:
                        break;
                }
            }

        }


        internal void progressDroneLocation(ref Drone drone, Action<int> ViewUpdate, BL blapi, Location targetLocation, WeightCategories weight)
        {

            Location droneLocation = new Location() { Latitude = drone.Location.Latitude, Longitude = drone.Location.Longitude };

            double battery = drone.Battery;
            double batteryRequest = blapi.TotalBatteryToDestination(drone.Location, targetLocation, (int)weight);

            for (int i = 100; i >= 0; i--)
            {
                drone.Battery -= batteryRequest / 100;
                drone.Location = blapi.Totalprogress(drone.Location, targetLocation, droneLocation);
                ViewUpdate(1);
                Thread.Sleep((int)(DroneSpeed / 100));
            }


            drone.Battery = battery;
            drone.Location = droneLocation;
        }

    }


}
