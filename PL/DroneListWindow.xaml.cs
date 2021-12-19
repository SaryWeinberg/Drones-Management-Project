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
        BLApi.IBL bl;

        /// <summary>
        /// Ctor of Drone list window
        /// </summary>
        /// <param name="blMain"></param>
        public DroneListWindow(BLApi.IBL blMain)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            DroneListView.ItemsSource = bl.GetDronesBLList();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatus));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }

        /// <summary>
        /// Filter the list category status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Filter the list category weight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Show drone window with adding ctor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddWindow(object sender, RoutedEventArgs e)
        {
            new DroneWindow(bl).Show();
            //Close();
        }

        /// <summary>
        /// Show drone window with update ctor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateDrone(object sender, MouseButtonEventArgs e)
        {
            BO.Drone dronr1 = (sender as ListView).SelectedValue as BO.Drone;
            new DroneWindow(bl, dronr1).Show();
            Close();
        }

        /// <summary>
        /// Closeing window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseWin(object sender, RoutedEventArgs e) => Close();

        /// <summary>
        /// Show all drones without filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneAllList(object sender, RoutedEventArgs e) => DroneListView.ItemsSource = bl.GetDronesBLList();

        private void RefreshWindow(object sender, RoutedEventArgs e) => DroneListView.Items.Refresh();
    }
}
