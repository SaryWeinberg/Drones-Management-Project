using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public class Parcel : DependencyObject
    {
        public Parcel(BO.Parcel parcel)
        {
            ID = parcel.ID;
            Associated = parcel.Associated;
            Created = parcel.Created;
            Delivered = parcel.Delivered;
            Drone = parcel.Drone;
            PickedUp = parcel.PickedUpByDrone;
            Priority = parcel.Priority;
            Sender = parcel.Sender;
            Target = parcel.Target;
            Weight = parcel.Weight;
        }

        public int ID { get { return (int)GetValue(IDProperty); } set { SetValue(IDProperty, value); } }
        public DateTime? Associated { get { return (DateTime?)GetValue(AssociatedProperty); } set { SetValue(AssociatedProperty, value); } }
        public DateTime? Created { get { return (DateTime?)GetValue(CreatedProperty); } set { SetValue(CreatedProperty, value); } }
        public DateTime? Delivered { get { return (DateTime?)GetValue(DeliveredProperty); } set { SetValue(DeliveredProperty, value); } }
        public DateTime? PickedUp { get { return (DateTime?)GetValue(PickedUpProperty); } set { SetValue(PickedUpProperty, value); } }
        public BO.DroneInParcel Drone { get { return (BO.DroneInParcel)GetValue(DroneProperty); } set { SetValue(DroneProperty, value); } }
        public Priorities Priority { get { return (Priorities)GetValue(PriorityProperty); } set { SetValue(PriorityProperty, value); } }
        public WeightCategories Weight { get { return (WeightCategories)GetValue(WeightProperty); } set { SetValue(WeightProperty, value); } }
        public BO.CustomerInParcel Sender { get { return (BO.CustomerInParcel)GetValue(SenderProperty); } set { SetValue(SenderProperty, value); } }
        public BO.CustomerInParcel Target { get { return (BO.CustomerInParcel)GetValue(TargetProperty); } set { SetValue(TargetProperty, value); } }

        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(int), typeof(Parcel), new UIPropertyMetadata());
        public static readonly DependencyProperty AssociatedProperty =
            DependencyProperty.Register("Associated", typeof(DateTime?), typeof(Parcel), new UIPropertyMetadata());
        public static readonly DependencyProperty CreatedProperty =
            DependencyProperty.Register("Created", typeof(DateTime?), typeof(Parcel), new UIPropertyMetadata());
        public static readonly DependencyProperty DeliveredProperty =
            DependencyProperty.Register("Delivered", typeof(DateTime?), typeof(Parcel), new UIPropertyMetadata());
        public static readonly DependencyProperty PickedUpProperty =
            DependencyProperty.Register("PickedUp", typeof(DateTime?), typeof(Parcel), new UIPropertyMetadata());
        public static readonly DependencyProperty DroneProperty =
            DependencyProperty.Register("Drone", typeof(BO.DroneInParcel), typeof(Parcel), new UIPropertyMetadata());
        public static readonly DependencyProperty PriorityProperty =
            DependencyProperty.Register("Priority", typeof(Priorities), typeof(Parcel), new UIPropertyMetadata());
        public static readonly DependencyProperty WeightProperty =
            DependencyProperty.Register("Weight", typeof(WeightCategories), typeof(Parcel), new UIPropertyMetadata());
        public static readonly DependencyProperty SenderProperty =
            DependencyProperty.Register("Sender", typeof(BO.CustomerInParcel), typeof(Parcel), new UIPropertyMetadata());
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(BO.CustomerInParcel), typeof(Parcel), new UIPropertyMetadata());

        public ObjectChanged<BO.Parcel> ParcelListChanged;

        public void updateParcel(BO.Parcel parcel)
        {
            ID = parcel.ID;
            Associated = parcel.Associated;
            Created = parcel.Created;
            Delivered = parcel.Delivered;
            Drone = parcel.Drone;
            PickedUp = parcel.PickedUpByDrone;
            Priority = parcel.Priority;
            Sender = parcel.Sender;
            Target = parcel.Target;
            Weight = parcel.Weight;

            if (ParcelListChanged != null)
                ParcelListChanged(parcel);
        }
    }
}
