using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        IBL.IBL bl;
        IBL.BO.DroneBL Drone;

        /// <summary>
        /// Ctor of Add drone window
        /// </summary>
        /// <param name="blMain"></param>
        public DroneWindow(IBL.IBL blMain)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            StationIdLabel.Visibility = Visibility.Visible;
            batteryStatusLabel.Visibility = Visibility.Hidden;
            max_weight.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }



        /// <summary>
        /// Adding drone 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewDrone(object sender, RoutedEventArgs e)
        {
            string ID = DroneID.Text;
            int weight = max_weight.SelectedIndex + 1;
            string model = DroneModel.Text;
            string stationID = station_ID.Text;
            try
            {
                int id = int.Parse(ID);
                int station = int.Parse(stationID);
                MessageBoxResult result =
                 MessageBox.Show(
                   bl.AddDroneBL(id, model, (WeightCategories)weight, station),
                   $"Add drone ID - {ID}",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    //new DroneListWindow(bl).Show();
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private bool available = false;

        /// <summary>
        /// Ctor of update drone window
        /// </summary>
        /// <param name="blMain"></param>
        /// <param name="drone"></param>
        public DroneWindow(IBL.IBL blMain, IBL.BO.DroneBL drone)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            Drone = drone;
            StationIdLabel.Visibility = Visibility.Hidden;
            batteryStatusLabel.Visibility = Visibility.Visible;
            
            DroneModelLabel.Content = "drone model:";
            DroneModel.Text = drone.Model.ToString();
            DroneModel.TextChanged += AddUpdateBTN;
            DroneID.IsEnabled = false;
            DroneIDLabel.Content = " drone ID:";
            MaxWeightLabel.Content = "max weight";

            max_weight.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            max_weight.Text = drone.MaxWeight.ToString();
            max_weight.IsEnabled = false;
            max_weight.IsEditable = true;

            Status.ItemsSource = Enum.GetValues(typeof(Status));
            Status.Text = drone.Status.ToString();
            Status.IsEnabled = false;
            Status.IsEditable = true;

            DroneID.Text = drone.ID.ToString();
            
            batteryStatus.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            batteryStatus.Text = drone.BatteryStatus.ToString();
            batteryStatus.IsEditable = true;
            longitude.Text = drone.Location.Longitude.ToString();
            latitude.Text = drone.Location.Latitude.ToString();

            if (drone.Status == DroneStatus.Available)
            {
                available = true;
                sendDroneToCharge.Visibility = Visibility.Visible;
                sendDroneToCharge.Margin = new Thickness(0, 370, 245, 0);
                assignParcelToDrone.Visibility = Visibility.Visible;
                assignParcelToDrone.Margin = new Thickness(0, 370, 670, 0);
            }
            else if (drone.Status == DroneStatus.Maintenance)
            {
                timecharge.Visibility = Visibility.Visible;
                timechargeLabel.Visibility = Visibility.Visible;
                timecharge.TextChanged += releseDrone;
            }
            else if (drone.Status == DroneStatus.Delivery)
            {
                if (bl.GetSpesificParcelBL(drone.Parcel.ID).PickedUp == null)
                {
                    collectParcelByDrone.Visibility = Visibility.Visible;
                    collectParcelByDrone.Margin = new Thickness(0, 370, 245, 0);
                }
                else
                {
                    deliveryParcelByDrone.Visibility = Visibility.Visible;
                    deliveryParcelByDrone.Margin = new Thickness(0, 370, 245, 0);
                }
            }
        }

        /// <summary>
        /// Adding update button when the content changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddUpdateBTN(object sender, RoutedEventArgs e)
        {
            update.Visibility = Visibility.Visible;
            if(available)
                update.Margin = new Thickness(0, 285, 450, 0);
            else update.Margin = new Thickness(0, 370, 670, 0);
        }

        /// <summary>
        /// Adding relese button when the status is maintains and the user fill in the time of charge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void releseDrone(object sender, RoutedEventArgs e)
        {
            releaseDronefromCharge.Visibility = Visibility.Visible;
            releaseDronefromCharge.Margin = new Thickness(0, 370, 245, 0);
        }

        /// <summary>
        /// closing the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosingWindow(object sender, RoutedEventArgs e) => Close();

        /// <summary>
        /// Update drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateDrone(object sender, RoutedEventArgs e)
        {
            string model = DroneModel.Text;
            try
            {
                MessageBoxResult result =
                  MessageBox.Show(
                    bl.UpdateDroneData(Drone.ID, model),
                    $"Update drone - {Drone.ID} model",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    new DroneListWindow(bl).Show();
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Send drone to charge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendDroneToCharge(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result =
                MessageBox.Show(
                  bl.SendDroneToCharge(Drone.ID),
                  $"Send drone ID - {Drone.ID} to charge",
                  MessageBoxButton.OK,
                  MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    new DroneListWindow(bl).Show();
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Relese dron from charge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReleaseDronefromCharge(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result =
                MessageBox.Show(
                  bl.ReleaseDroneFromCharge(Drone.ID, int.Parse(timecharge.Text)),
                  $"Release drone ID - {Drone.ID} from charge",
                  MessageBoxButton.OK,
                  MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    new DroneListWindow(bl).Show();
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Assing parcel to drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssignParcelToDrone(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result =
                 MessageBox.Show(
                 bl.AssignParcelToDrone(Drone.ID),
                 $"Assign parcel",
                 MessageBoxButton.OK,
                 MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    new DroneListWindow(bl).Show();
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Collect parcel by drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CollectParcelByDrone(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result =
                MessageBox.Show(
                bl.CollectParcelByDrone(Drone.ID),
                $"Collect parcel",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    new DroneListWindow(bl).Show();
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Delivery parcel by drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeliveryParcelByDrone(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result =
               MessageBox.Show(
               bl.DeliveryParcelByDrone(Drone.ID),
               $"Delivery parcel",
               MessageBoxButton.OK,
               MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    new DroneListWindow(bl).Show();
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
