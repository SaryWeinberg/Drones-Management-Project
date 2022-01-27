using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        BLApi.IBL bl;
        Drone Drone;
        BO.Drone Drone_bl;
        DroneListWindow ExistDroneListWindow;

        /// <summary>
        /// Ctor of Add drone window
        /// </summary>
        /// <param name="blMain"></param>
        public DroneWindow(BLApi.IBL blMain, DroneListWindow droneListWindow)
        {
            ExistDroneListWindow = droneListWindow;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            /*Drone = new Drone();
            Drone.droneListChanged += new ObjectChanged(droneListWindow.AddDrone);*/
            StationIDLabel.Visibility = Visibility.Visible;
            batteryStatusLabel.Visibility = Visibility.Hidden;
            MaxWeight.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            DroneID.Focus();
            
            /*Drone.AddDroneOrRemove();*/
        }

        /// <summary>
        /// Adding drone 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewDrone(object sender, RoutedEventArgs e)
        {
            
           
            try
            {
                MessageBoxResult result =
                 MessageBox.Show(
                   bl.AddDroneBL(IDInput(), ModelInput(), (WeightCategories)WeightInput(), StationIDInput()),
                   $"Add drone ID - {IDInput()}",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {

                    ExistDroneListWindow.AddDrone(bl.GetSpesificDrone(IDInput()));

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
        public DroneWindow(BLApi.IBL blMain, BO.Drone drone, DroneListWindow droneListWindow)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            Drone = new Drone(drone);
            Drone.droneListChanged += new ObjectChanged(droneListWindow.update);


            Drone_bl = drone;
            AddDroneGrid.DataContext = Drone;
            StationIDLabel.Visibility = Visibility.Hidden;
            batteryStatusLabel.Visibility = Visibility.Visible;

            /*           DroneModel.Text = drone.Model.ToString();*/
            DroneModel.TextChanged += AddUpdateBTN;
            /*            DroneID.IsEnabled = false;*/

            MaxWeight.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            MaxWeight.IsEnabled = false;
            MaxWeight.IsEditable = true;

            Status.ItemsSource = Enum.GetValues(typeof(Status));
            Status.IsEnabled = false;
            Status.IsEditable = true;


            DisplayBTN();
        }

        public void DisplayBTN()
        {

            sendDroneToCharge.Visibility = Visibility.Hidden;
            assignParcelToDrone.Visibility = Visibility.Hidden;
            releaseDronefromCharge.Visibility = Visibility.Hidden;
            collectParcelByDrone.Visibility = Visibility.Hidden;
            deliveryParcelByDrone.Visibility = Visibility.Hidden;
 
       

            if (Drone.Battery < 10) battery.Foreground = new SolidColorBrush(Colors.Red);
            else if (Drone.Battery > 95) battery.Foreground = new SolidColorBrush(Colors.Green);
            else battery.Foreground = new SolidColorBrush(Colors.Yellow);

            if (Drone.Parcel != null)
            {
                parcelLabel.Visibility = Visibility.Visible;
                parcel.Visibility = Visibility.Visible;
            }


            if (Drone.Status == DroneStatus.Available)
            {
                available = true;
                sendDroneToCharge.Visibility = Visibility.Visible;
                sendDroneToCharge.Margin = new Thickness(0, 370, 245, 0);
                assignParcelToDrone.Visibility = Visibility.Visible;
                assignParcelToDrone.Margin = new Thickness(0, 370, 670, 0);
            }
            else if (Drone.Status == DroneStatus.Maintenance)
            {
                releaseDronefromCharge.Visibility = Visibility.Visible;
                releaseDronefromCharge.Margin = new Thickness(0, 370, 245, 0);
            }
            else if (Drone.Status == DroneStatus.Delivery)
            {
                if (bl.GetSpesificParcel(Drone.Parcel.ID).PickedUp == null)
                {
                    deliveryParcelByDrone.Visibility = Visibility.Hidden;
                    collectParcelByDrone.Visibility = Visibility.Visible;
                    collectParcelByDrone.Margin = new Thickness(0, 370, 245, 0);
                }
                else
                {
                    collectParcelByDrone.Visibility = Visibility.Hidden;
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
            if (available)
            {

                update.Margin = new Thickness(0, 285, 450, 0);
            }
            else update.Margin = new Thickness(0, 370, 670, 0);
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
            try
            {
                MessageBoxResult result =
                  MessageBox.Show(
                    bl.UpdateDroneData(Drone.ID, ModelInput()),
                    $"Update drone - {Drone.ID} model",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                Drone.Model = ModelInput();
                if (result == MessageBoxResult.OK)
                {
                   Drone.updateDronePO(bl.GetSpesificDrone(Drone.ID));
          /*          AddDroneGrid.DataContext = Drone;*/
                    /*        new DroneListWindow(bl).Show();*/
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
                    Drone.updateDronePO(bl.GetSpesificDrone(Drone.ID));
              /*      AddDroneGrid.DataContext = Drone;*/

                    DisplayBTN();
                    /*   new DroneListWindow(bl).Show();*/
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
            TimeSpan timeInCharge = DateTime.Now - bl.GetSpecificDroneInCharge(IDInput()).DroneEnterToCharge;
            try
            {
                MessageBoxResult result =
                MessageBox.Show(
                  bl.ReleaseDroneFromCharge(IDInput(), timeInCharge.Minutes),
                  $"Release drone ID - {IDInput()} from charge",
                  MessageBoxButton.OK,
                  MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                   Drone.updateDronePO(bl.GetSpesificDrone(Drone.ID));
                    AddDroneGrid.DataContext = Drone;
                    DisplayBTN();
                    /*          new DroneListWindow(bl).Show();*/
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
                 bl.AssignParcelToDrone(IDInput()),
                 $"Assign parcel",
                 MessageBoxButton.OK,
                 MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {

                     Drone.updateDronePO(bl.GetSpesificDrone(Drone.ID));
/*                    AddDroneGrid.DataContext = Drone;*/
                    DisplayBTN();
                    /*         new DroneListWindow(bl).Show();*/
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
                bl.CollectParcelByDrone(IDInput()),
                $"Collect parcel",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                   Drone.updateDronePO(bl.GetSpesificDrone(Drone.ID));
/*                    AddDroneGrid.DataContext = Drone;*/
                    DisplayBTN();
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
               bl.SupplyParcelByDrone(IDInput()),
               $"Delivery parcel",
               MessageBoxButton.OK,
               MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
               Drone.updateDronePO(bl.GetSpesificDrone(Drone.ID));
         /*           AddDroneGrid.DataContext = Drone;*/
                    DisplayBTN();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }



        private void GetParcel(object sender, MouseButtonEventArgs e)
        {
            string ID = (sender as TextBox).SelectedText;
            new ParcelWindow(bl, bl.GetSpesificParcel(int.Parse(ID))).Show();
        }

        private void RefreshWindow(object sender, RoutedEventArgs e)
        {


        }

        private void ReturnWindow(object sender, RoutedEventArgs e)
        {
            new DroneListWindow(bl).Show();
            Close();
        }

        #region Get Inputs
        /// <summary>
        /// Takes ID input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int IDInput()
        {
            try { return int.Parse(DroneID.Text); }
            catch (Exception) { throw new InvalidObjException("ID"); }
        }
        /// <summary>
        /// Takes weight input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int WeightInput()
        {
            return MaxWeight.SelectedIndex != -1 ? MaxWeight.SelectedIndex + 1 : throw new InvalidObjException("Weight");
        }
        /// <summary>
        /// Takes model input from the user with tests
        /// </summary>
        /// <returns></returns>
        private string ModelInput()
        {
            return DroneModel.Text != "" ? DroneModel.Text : throw new InvalidObjException("Model");
        }
        /// <summary>
        /// Takes station input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int StationIDInput()
        {
            try { return int.Parse(stationID.Text); }
            catch (Exception) { throw new InvalidObjException("Station ID"); }
        }

        #endregion

        #region Switch between TextBoxes
        /// <summary>
        /// Moves from TextBox DroneID to ComboBox MaxWeight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownDroneID(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) MaxWeight.Focus();
        }
        /// <summary>
        /// Moves from ComboBox MaxWeight to TextBox DroneModel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownMaxWeight(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) DroneModel.Focus();
        }
        /// <summary>
        /// Moves from TextBox DroneModel to TextBox stationID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownDroneModel(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) stationID.Focus();
        }
        /// <summary>
        /// Moves from TextBox stationID to Button sendNewDrone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownStationID(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) sendNewDrone.Focus();
        }
        #endregion

        /*        private void Button_Click(object sender, RoutedEventArgs e)
                {
                    Drone.ID = 100;
     
        
        
        
        
        }*/



    }
}