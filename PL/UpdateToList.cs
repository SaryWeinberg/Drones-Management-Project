using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace PL
{
    public delegate void ObjectChanged<T>(T objectChanged);

    /*  public class UpdateToList

      {
          public  void UpdateList(string listName)
          {
              switch (listName)
              {
                  case "Drone":

                  default:
                      break;
              }
          }


      }*/







  /*  public class ParcelListClass : DependencyObject
    {


    
    }*/


 /*   public class ListClass : DependencyObject
    {*/

/*        public ObservableCollection<BO.DroneToList> DroneList {
            get { return (ObservableCollection<BO.DroneToList>)GetValue(DroneListProperty); }
            set
            {
                SetValue(DroneListProperty, value);
            }
        }

        public static readonly DependencyProperty DroneListProperty =
    DependencyProperty.Register("DroneList", typeof(IEnumerable<BO.DroneToList>), typeof(ListClass), new UIPropertyMetadata());


        public ObservableCollection<BO.ParcelToList> ParcelList {
            get { return (ObservableCollection<BO.ParcelToList>)GetValue(ParcelListProperty); }
            set
            {
                SetValue(ParcelListProperty, value);
            }
        }

        public static readonly DependencyProperty ParcelListProperty =
        DependencyProperty.Register("ParcelList", typeof(ObservableCollection<BO.ParcelToList>), typeof(ParcelListClass), new UIPropertyMetadata());

        public ObservableCollection<BO.StationToList> StationList {
            get { return (ObservableCollection<BO.StationToList>)GetValue(StationListProperty); }
            set
            {
                SetValue(StationListProperty, value);
            }
        }
        public static readonly DependencyProperty StationListProperty =
        DependencyProperty.Register("StationList", typeof(IEnumerable<BO.StationToList>), typeof(ListClass), new UIPropertyMetadata());


        public ObservableCollection<BO.CustomerToList> CustomerList {
            get { return (ObservableCollection<BO.CustomerToList>)GetValue(CustomerListProperty); }
            set
            {
                SetValue(CustomerListProperty, value);
            }
        }




        public static readonly DependencyProperty CustomerListProperty =
    DependencyProperty.Register("CustomerList", typeof(IEnumerable<BO.CustomerToList>), typeof(ListClass), new UIPropertyMetadata());

        public ListClass(ObservableCollection<BO.DroneToList> List)
        {
            DroneList = List;
        }
        public ListClass(ObservableCollection<BO.ParcelToList> List)
        {
            ParcelList = List;
        }
        public ListClass(ObservableCollection<BO.StationToList> List)
        {
            StationList = List;
        }
        public ListClass(ObservableCollection<BO.CustomerToList> List)
        {
            CustomerList = List;
        }

        public void updateDroneList(BO.Drone drone)
        {

            
            foreach (BO.DroneToList droneToList in this.DroneList)
            {
                if(drone.ID == droneToList.ID)
                {

                 
                    DroneList.Remove(droneToList);
                    this.DroneList.Add(new BO.DroneToList(drone));
                    break;
                   
                }

            }



        }

    }*/




    public class Drone : DependencyObject
    {



        public Drone(BO.Drone drone)
        {
            ID = drone.ID;
            Model = drone.Model;
            MaxWeight = drone.MaxWeight;
            Battery = drone.Battery;
            Status = drone.Status;
            Parcel = drone.Parcel;
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
        public ObjectChanged<BO.Drone> droneListChanged;
        public void updateDronePO(BO.Drone drone )
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
      /*  public void AddDrone(BO.Drone drone)
        {
            
            if (droneListChanged != null)
                droneListChanged(drone);

        }*/


    }


    public class Station : DependencyObject
    {

     
        public Station(BO.Station station)
        {
            ID = station.ID;
            Name = station.Name;
            Location = station.Location;
            AveChargeSlots = station.AveChargeSlots;
            DronesInChargelist = station.DronesInChargelist;
        }
        public int ID { get { return (int)GetValue(IDProperty); } set { SetValue(IDProperty, value); } }
        public int Name { get { return (int)GetValue(NameProperty); } set { SetValue(NameProperty, value); } }
        public BO.Location Location { get { return (BO.Location)GetValue(LocationProperty); } set { SetValue(LocationProperty, value); } }
        public double AveChargeSlots { get { return (double)GetValue(AveChargeSlotsProperty); } set { SetValue(AveChargeSlotsProperty, value); } }
        public List<BO.DroneInCharge> DronesInChargelist { get { return (List<BO.DroneInCharge>)GetValue(DronesInChargelistProperty); } set { SetValue(DronesInChargelistProperty, value); } }

        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(int), typeof(Station), new UIPropertyMetadata());
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(int), typeof(Station), new UIPropertyMetadata());
        public static readonly DependencyProperty LocationProperty =
            DependencyProperty.Register("Location", typeof(BO.Location), typeof(Station), new UIPropertyMetadata());
        public static readonly DependencyProperty AveChargeSlotsProperty =
            DependencyProperty.Register("AveChargeSlots", typeof(double), typeof(Station), new UIPropertyMetadata());
        public static readonly DependencyProperty DronesInChargelistProperty =
            DependencyProperty.Register("DronesInChargelist", typeof(List<BO.DroneInCharge>), typeof(Station), new UIPropertyMetadata());


        public  ObjectChanged<BO.Station> stationListChanged;
        public void UpdateStation(BO.Station station)
        {
            ID = station.ID;
            Name = station.Name;
            Location = station.Location;
            AveChargeSlots = station.AveChargeSlots;
            DronesInChargelist = station.DronesInChargelist;

            if (stationListChanged != null)
                stationListChanged(station);

        }
    }

    public class Customer : DependencyObject
    {

        public  ObjectChanged<BO.Customer> CustomerListChanged;
        public Customer(BO.Customer customer)
        {
            ID = customer.ID;
            PhoneNum = customer.PhoneNum;
            Name = customer.Name;
            Location = customer.Location;
            DeliveryToCustomer = customer.DeliveryToCustomer;
            DeliveryFromCustomer = customer.DeliveryFromCustomer;
        }
        public int ID { get { return (int)GetValue(IDProperty); } set { SetValue(IDProperty, value); } }
        public string Name { get { return (string)GetValue(NameProperty); } set { SetValue(NameProperty, value); } }
        public BO.Location Location { get { return (BO.Location)GetValue(LocationProperty); } set { SetValue(LocationProperty, value); } }
        public int PhoneNum { get { return (int)GetValue(PhoneNumProperty); } set { SetValue(PhoneNumProperty, value); } }
        public List<BO.ParcelsAtTheCustomer> DeliveryToCustomer { get { return (List<BO.ParcelsAtTheCustomer>)GetValue(DeliveryToCustomerProperty); } set { SetValue(DeliveryToCustomerProperty, value); } }
        public List<BO.ParcelsAtTheCustomer> DeliveryFromCustomer { get { return (List<BO.ParcelsAtTheCustomer>)GetValue(DeliveryFromCustomerProperty); } set { SetValue(DeliveryFromCustomerProperty, value); } }

        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(int), typeof(Customer), new UIPropertyMetadata());
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(Customer), new UIPropertyMetadata());
        public static readonly DependencyProperty LocationProperty =
            DependencyProperty.Register("Location", typeof(BO.Location), typeof(Customer), new UIPropertyMetadata());
        public static readonly DependencyProperty PhoneNumProperty =
            DependencyProperty.Register("PhoneNum", typeof(int), typeof(Customer), new UIPropertyMetadata());
        public static readonly DependencyProperty DeliveryToCustomerProperty =
            DependencyProperty.Register("DeliveryToCustomer", typeof(List<BO.ParcelsAtTheCustomer>), typeof(Customer), new UIPropertyMetadata());
        public static readonly DependencyProperty DeliveryFromCustomerProperty =
            DependencyProperty.Register("DeliveryFromCustomer", typeof(List<BO.ParcelsAtTheCustomer>), typeof(Customer), new UIPropertyMetadata());

        public void UpdateCustomer(BO.Customer customer)
        {
            ID = customer.ID;
            PhoneNum = customer.PhoneNum;
            Name = customer.Name;
            Location = customer.Location;
            DeliveryToCustomer = customer.DeliveryToCustomer;
            DeliveryFromCustomer = customer.DeliveryFromCustomer;

            if (CustomerListChanged != null)
                CustomerListChanged(customer);
        }
    }


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
        }
    }

    /*public class DroneList : DependencyObject
    {




        public DroneList(IEnumerable<BO.DroneToList> droneList)
        {
            DronesList = droneList;
        }
        public IEnumerable<BO.DroneToList> DronesList { get { return (IEnumerable<BO.DroneToList>)GetValue(DronesListProperty); } set { SetValue(DronesListProperty, value); } }
        public static readonly DependencyProperty DronesListProperty =
            DependencyProperty.Register("DronesList", typeof(IEnumerable<BO.DroneToList>), typeof(DroneList), new UIPropertyMetadata());
    }*/






}
