using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public class Drone : DependencyObject
    {
        public Drone(BO.Drone drone)
        {
            ID = drone.ID;
            Model = drone.Model;
            MaxWeight = drone.MaxWeight;
            Battery = drone.Battery;
            Status = drone.Status;
            this.Parcel = drone.Parcel;
            Location = drone.Location;
        }

        public int ID { get { return (int)GetValue(IDProperty); } set { SetValue(IDProperty, value); } }
        public string Model { get { return (string)GetValue(ModelProperty); } set { SetValue(ModelProperty, value); } }
        public WeightCategories MaxWeight { get { return (WeightCategories)GetValue(MaxWeightProperty); } set { SetValue(MaxWeightProperty, value); } }
        public double Battery { get { return (double)GetValue(BatteryProperty); } set { SetValue(BatteryProperty, value); } }
        public DroneStatus Status { get { return (DroneStatus)GetValue(StatusProperty); } set { SetValue(StatusProperty, value); } }
        public BO.ParcelByDelivery Parcel { get { return (BO.ParcelByDelivery)GetValue(ParcelProperty); } set { SetValue(ParcelProperty, value); } }
        public BO.Location Location { get { return (BO.Location)GetValue(LocationProperty); } set { SetValue(LocationProperty, value); } }

        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(int), typeof(Drone), new UIPropertyMetadata());
        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof(string), typeof(Drone), new UIPropertyMetadata());
        public static readonly DependencyProperty MaxWeightProperty =
            DependencyProperty.Register("MaxWeight", typeof(WeightCategories), typeof(Drone), new UIPropertyMetadata());
        public static readonly DependencyProperty BatteryProperty =
            DependencyProperty.Register("Battery", typeof(double), typeof(Drone), new UIPropertyMetadata());
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(DroneStatus), typeof(Drone), new UIPropertyMetadata());
        public static readonly DependencyProperty ParcelProperty =
            DependencyProperty.Register("Parcel", typeof(BO.ParcelByDelivery), typeof(Drone), new UIPropertyMetadata());
        public static readonly DependencyProperty LocationProperty =
            DependencyProperty.Register("Location", typeof(BO.Location), typeof(Drone), new UIPropertyMetadata());
        public ObjectChangedAction<BO.Drone> droneListChanged;
        public void updateDronePO(BO.Drone drone)
        {
            ID = drone.ID;
            Model = drone.Model;
            MaxWeight = drone.MaxWeight;
            Battery = drone.Battery;
            Status = drone.Status;
            Parcel = drone.Parcel;
            Location = drone.Location;

            if (droneListChanged != null)
                droneListChanged(drone);
        }
    }

}
