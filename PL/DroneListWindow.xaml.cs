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
            WindowStyle = WindowStyle.None;
            bl = blMain;
            DroneListView.ItemsSource = bl.GetDronesBLList();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatus));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }

        private void StatusSelectorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox status = sender as ComboBox;
            WeightCategories Sweight = 0;
            if (WeightSelector.SelectedItem == null)
            {
                DroneListView.ItemsSource = bl.GetDronesBy(drone => drone.Status == (DroneStatus)status.SelectedItem);
            }
            else
            {
                Sweight = (WeightCategories)WeightSelector.SelectedItem;
                DroneListView.ItemsSource = bl.GetDronesBy(drone => drone.Status == (DroneStatus)status.SelectedItem && drone.MaxWeight == Sweight);
            }
        }

        private void WeightSelectorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox weight = sender as ComboBox;
            DroneStatus Sstatus = 0;
            if (StatusSelector.SelectedItem == null)
            {
                DroneListView.ItemsSource = bl.GetDronesBy(drone => drone.MaxWeight == (WeightCategories)weight.SelectedItem);
            }
            else
            {
                Sstatus = (DroneStatus)StatusSelector.SelectedItem;
                DroneListView.ItemsSource = bl.GetDronesBy(drone => drone.MaxWeight == (WeightCategories)weight.SelectedItem && drone.Status == Sstatus);
            }
        }

        private void AddWindow(object sender, RoutedEventArgs e)
        {
            new DroneWindow(bl).Show();
            Close();
        }

        private void UpdateDrone(object sender, MouseButtonEventArgs e)
        {
            IBL.BO.DroneBL dronr1 = (sender as ListView).SelectedValue as IBL.BO.DroneBL;
            new DroneWindow(bl, dronr1).Show();
            Close();
        }

        private void CloseWin(object sender, RoutedEventArgs e) => Close();

        private void DroneAllList(object sender, RoutedEventArgs e) => DroneListView.ItemsSource = bl.GetDronesBLList();
        
    }
}
