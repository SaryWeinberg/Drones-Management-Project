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
                        int counter = 0;
                            

                      
                            try
                            {
                                blapi.AssignParcelToDrone(DroneId);
                            ViewUpdate(1);
                        }

                            catch (CanNotAssignParcelToDroneException)
                            {
                            if (drone.Battery == 100 && counter < 0)
                            {
                                Thread.Sleep(Delay);
                                counter++;
                            }
                            else
                            {

                                blapi.SendDroneToCharge(DroneId);
                                ViewUpdate(1);

                            }
                            }
                        ViewUpdate(1);

                        break;
                    case DroneStatus.Maintenance:
                        TimeSpan timeInCharge = DateTime.Now - blapi.GetSpecificDroneInCharge(DroneId).DroneEnterToCharge;
                        while (drone.Battery + (timeInCharge.Milliseconds * blapi.ElectricalPowerRequest(4)) < 100)
                        {
                            timeInCharge = DateTime.Now - blapi.GetSpecificDroneInCharge(DroneId).DroneEnterToCharge;
                            drone.Battery += timeInCharge.Milliseconds * blapi.ElectricalPowerRequest(4);
                            ViewUpdate(1);
                            Thread.Sleep(100);
                        }
                        blapi.ReleaseDroneFromCharge(DroneId, timeInCharge.Minutes);
                        ViewUpdate(1);
                        break;
                    case DroneStatus.Delivery:

                        if (drone.Parcel.isWaitingToDelivery)
                        {
                            blapi.CollectParcelByDrone(DroneId);
                            ViewUpdate(1);
                        }
                        else
                        {
                            blapi.SupplyParcelByDrone(DroneId);
                            ViewUpdate(1);

                            Thread.Sleep(Delay);
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
