using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;


namespace DalObject
{
    public partial class DalObject : IDal
    {
        public DalObject()
        {
            DataSource.Initialize();
        }

        static DalObject instance;

        public static DalObject GetInstance {
            get
            {
                if (instance == null)
                    instance = new DalObject();
                return instance;
            }
        }

        public void AssingParcelToDrone(Parcel parcel)
        {
            /* Drone drone = DataSource.Drones.Find(d =>
             d.Status == DroneStatus.Available && d.MaxWeight >= parcel.Weight);
             int indexP = DataSource.Parcels.IndexOf(parcel);
             int indexD = DataSource.Drones.IndexOf(drone);//
             parcel.DroneId = drone.ID;
             parcel.Scheduled = DateTime.Now;
             DataSource.Parcels[indexP] = parcel;
             drone.Status = DroneStatus.Delivery;
             DataSource.Drones[indexD] = drone;*/
        }

        public Parcel FindParcel(int id)
        {

            return DataSource.Parcels.First(p => p.id == id);

            /* foreach (Parcel parcel in DataSource.Parcels)
             {
                 if (parcel.ID == id)
                 {
                     return parcel;
                 }
             }
             return new Parcel();*/
        }

        public Drone FindDrone(int id)
        {
            return DataSource.Drones.First(d => d.id == id);


            /* foreach (Drone drone in DataSource.Drones)
             {
                 if (drone.ID == id)
                 {
                     return drone;
                 }
             }
             return new Drone();*/
        }

        public void CollectParcelByDrone(Parcel parcel)
        {
            Drone drone = DataSource.Drones.First(d =>
            d.id == parcel.droneId);
            int indexP = DataSource.Parcels.IndexOf(parcel);
            int indexD = DataSource.Drones.IndexOf(drone);
            parcel.droneId = drone.id;
            parcel.pickedUp = DateTime.Now;
            DataSource.Parcels[indexP] = parcel;
            /*         drone.Status = DroneStatus.Delivery;*/
            DataSource.Drones[indexD] = drone;
        }

        public void ProvideParcelToCustomer(Parcel parcel)
        {
            Drone drone = DataSource.Drones.First(d => d.id == parcel.droneId);
            int indexP = DataSource.Parcels.IndexOf(parcel);
            int indexD = DataSource.Drones.IndexOf(drone);
            parcel.droneId = drone.id;
            parcel.delivered = DateTime.Now;
            DataSource.Parcels[indexP] = parcel;
            /*  drone.Status = DroneStatus.Available;*/
            DataSource.Drones[indexD] = drone;
        }


        public void SendDroneToChargeInStation(Drone drone, int stationId)
        {
            DroneCharge droneCharge = new DroneCharge();
            droneCharge.DroneId = drone.id;
            droneCharge.StationId = stationId;
            DataSource.DroneCharges.Add(droneCharge);
            int indexD = DataSource.Drones.IndexOf(drone);
            /*            drone.Status = DroneStatus.Maintenance;*/
            DataSource.Drones[indexD] = drone;
        }

        public void ReleaseDroneFromChargeInStation(Drone drone)
        {
            DroneCharge droneCharge = DataSource.DroneCharges.First(d =>
            d.DroneId == drone.id);
            DataSource.DroneCharges.Remove(droneCharge);
            int indexD = DataSource.Drones.IndexOf(drone);
            /*           drone.Status = DroneStatus.Available;
                       drone.Battery = 100;*/
            DataSource.Drones[indexD] = drone;
        }


        public double[] ElectricalPowerRequest()
        {
            double[] arr = { DataSource.config.Available, DataSource.config.Light, DataSource.config.medium, DataSource.config.heavy ,DataSource.config.chargingRate};
            return arr;
        }
    }
}




