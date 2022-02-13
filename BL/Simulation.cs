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
                int counter = 0;
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

                        catch (CanNotAssignParcelToDroneException)
                        {
                            if (drone.Battery == 100)
                            {
                                Thread.Sleep(3000);
                                counter++;
                            }
                            else
                            {

                                blapi.SendDroneToCharge(DroneId);
                                ViewUpdate(1);
                                Thread.Sleep(Delay);

                            }
                        }

                        catch (ObjectNotExistException)
                        {
                            ToStop();

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

                            for (int i = 100; i > 0; i--)
                            {
                                drone.Location = blapi.Totalprogress(drone.Location, drone.Parcel.PickUpLocation, droneLocation);
                     /*           drone.Battery -= 0.01* blapi.ElectricalPowerRequest((int)drone.Parcel.Weight);*/
                                ViewUpdate(1);
                                Thread.Sleep((int)(DroneSpeed / 100));
                            }
                            Thread.Sleep(Delay);
                            blapi.CollectParcelByDrone(DroneId);
                            ViewUpdate(1);
                            Thread.Sleep(Delay);

                        }
                        else
                        {

                      

                            Location droneLocation = new Location() { Latitude = drone.Location.Latitude, Longitude = drone.Location.Longitude };
                       
                            for (int i = 100; i > 0; i--)
                            {
                                drone.Location = blapi.Totalprogress(drone.Location, drone.Parcel.TargetLocation, droneLocation);
                                ViewUpdate(1);
                                Thread.Sleep((int)(DroneSpeed / 100));
                            }
                            blapi.SupplyParcelByDrone(DroneId);

                            ViewUpdate(1);
                            Thread.Sleep(Delay);

                            /*         Thread.sl(blapi.TotalTimeUsage(drone.Location, DroneSpeed, drone.Parcel.TargetLocation);*/
                        }
                        ViewUpdate(1);
                        break;
                    default:
                        break;
                }
                /*      blapi.GetSpesificDrone(DroneId);*/
            }

        }

    }
}
