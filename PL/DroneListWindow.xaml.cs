using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        IBL.IBL bl;
       
        public DroneListWindow(IBL.IBL blMain)
        {
            InitializeComponent();
            bl = blMain;
            DroneListView.ItemsSource = bl.GetDronesBL();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatus));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }       

        private void StatusSelectorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (sender)
            {
                case "Available":
                    bl.GetDronesBy(drone => drone.Status == DroneStatus.Available);
                    break;
                case "Maintenance":
                    bl.GetDronesBy(drone => drone.Status == DroneStatus.Maintenance);
                    break;
                case "Delivery":
                    bl.GetDronesBy(drone => drone.Status == DroneStatus.Delivery);
                    break;
            }            
        }

        private void WeightSelectorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (sender)
            {
                case "Light":
                    bl.GetDronesBy(drone => drone.MaxWeight == WeightCategories.Light);
                    break;
                case "Medium":
                    bl.GetDronesBy(drone => drone.MaxWeight == WeightCategories.Medium);
                    break;
                case "Heavy":
                    bl.GetDronesBy(drone => drone.MaxWeight == WeightCategories.Heavy);
                    break;
            }
        }

        private void ViewDroneWindow(object sender, RoutedEventArgs e)
        {
            new DroneWindow(bl).Show();
        }
    }
}
