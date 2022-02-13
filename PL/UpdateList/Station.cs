using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public class Station : DependencyObject
    {
        public Station(BO.Station station)
        {
            this.ID = station.ID;
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

        public ObjectChangedAction<BO.Station> stationListChanged;
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
}
