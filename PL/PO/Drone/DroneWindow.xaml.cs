using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

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

        public ObjectChangedAction<BO.Drone> SomeChangedHappened;
        /// <summary>
        /// Ctor of Add drone window
        /// </summary>
        /// <param name="blMain"></param>
        public DroneWindow(BLApi.IBL blMain)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;            
            StationIDLabel.Visibility = Visibility.Visible;
            batteryStatusLabel.Visibility = Visibility.Hidden;
            MaxWeight.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            DroneID.Focus();
            Drone = new Drone(new BO.Drone());
            Drone.droneListChanged += new ObjectChangedAction<BO.Drone>(UpdateDroneList);
        }

        /// <summary>
        /// Ctor of update drone window
        /// </summary>
        /// <param name="blMain"></param>
        /// <param name="drone"></param>
        public DroneWindow(BLApi.IBL blMain, BO.Drone drone)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;

            Drone = new Drone(drone);
            Drone.droneListChanged += new ObjectChangedAction<BO.Drone>(UpdateDroneList);

            Drone_bl = drone;
            DataContext = Drone;

            StationIDLabel.Visibility = Visibility.Hidden;
            batteryStatusLabel.Visibility = Visibility.Visible;

            DroneID.IsEnabled = false;
            DroneModel.TextChanged += AddUpdateBTN;

            MaxWeight.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            MaxWeight.IsEnabled = false;
            MaxWeight.IsEditable = true;

            Status.ItemsSource = Enum.GetValues(typeof(Status));
            Status.IsEnabled = false;
            Status.IsEditable = true;

            DisplayBTN();
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

                    if (Drone.droneListChanged != null)
                        Drone.droneListChanged(bl.GetSpesificDrone(IDInput()));
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Displays the buttons according to the status of the drone
        /// </summary>
        public void DisplayBTN()
        {
            sendDroneToCharge.IsEnabled = false;
            assignParcelToDrone.IsEnabled = false;
            releaseDronefromCharge.IsEnabled = false;
            collectParcelByDrone.IsEnabled = false;
            deliveryParcelByDrone.IsEnabled = false;

            if (Drone.Battery < 10) battery.Foreground = new SolidColorBrush(Colors.Red);
            else if (Drone.Battery > 10 && Drone.Battery < 30) battery.Foreground = new SolidColorBrush(Colors.Orange);
            else if (Drone.Battery > 30 && Drone.Battery < 50) battery.Foreground = new SolidColorBrush(Colors.Yellow);
            else if (Drone.Battery > 50 && Drone.Battery < 90) battery.Foreground = new SolidColorBrush(Colors.YellowGreen);
            else battery.Foreground = new SolidColorBrush(Colors.Green);

            if (Drone.Parcel != null) 
            {
                parcelLabel.Visibility = Visibility.Visible;
                parcel.Visibility = Visibility.Visible;
            }

            if (Drone.Status == DroneStatus.Available)
            {
                sendDroneToCharge.IsEnabled = true;
                assignParcelToDrone.IsEnabled =true;
            }
            else if (Drone.Status == DroneStatus.Maintenance)
            {
                releaseDronefromCharge.IsEnabled = true;
            }
            else if (Drone.Status == DroneStatus.Delivery)
            {
                if (bl.GetSpesificParcel(Drone.Parcel.ID).PickedUpByDrone == null)
                {
                    deliveryParcelByDrone.IsEnabled = false;
                    collectParcelByDrone.IsEnabled = true;
                }
                else
                {
                    collectParcelByDrone.IsEnabled = false;
                    deliveryParcelByDrone.IsEnabled = true;
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
            update.IsEnabled = true;
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
                  $"Send drone ID - {IDInput()} to charge",
                  MessageBoxButton.OK,
                  MessageBoxImage.Information);

                if (result == MessageBoxResult.OK)
                {
                    Drone.updateDronePO(bl.GetSpesificDrone(Drone.ID));                   
                    DisplayBTN();
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
                    DisplayBTN();
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
                    DisplayBTN();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Submit to update the drone list
        /// </summary>
        /// <param name="drone"></param>
        public void UpdateDroneList(BO.Drone drone)
        {
            DisplayBTN();

            if (SomeChangedHappened != null)
                SomeChangedHappened(drone);
        }

        /// <summary>
        /// Entrance to the window of the parcel associated with the drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetParcel(object sender, MouseButtonEventArgs e)
        {
            new ParcelWindow(bl, bl.GetSpesificParcel(Drone.Parcel.ID)).Show();
        }

        /// <summary>
        /// Back to previous window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnWindow(object sender, RoutedEventArgs e)
        {
            new DroneListWindow(bl).Show();
            Close();
        }

        BackgroundWorker Worker = new BackgroundWorker();

        /// <summary>
        /// Start of the Simulatiom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SimulationClick(object sender, RoutedEventArgs e)
        {
            Worker.DoWork += (object? sender, DoWorkEventArgs e) => {
                bl.StartSimulation(
                   Drone_bl.ID,
                     (i) => { Worker.ReportProgress(i); },
                     () => Worker.CancellationPending);
            };

            Worker.WorkerReportsProgress = true;
            Worker.ProgressChanged += (object? sender, ProgressChangedEventArgs e) => {
                Drone.updateDronePO(bl.GetSpesificDrone(Drone_bl.ID));               
            };
           
            Worker.WorkerSupportsCancellation = true;
            Worker.RunWorkerAsync();
        }

        /// <summary>
        /// Stopping the Simulatiom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SimulationStopClick(object sender, RoutedEventArgs e)
        {
            Worker.CancelAsync();
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

        private void Simulatiom_Click(object sender, RoutedEventArgs e)
        {

            Worker.DoWork += (object? sender, DoWorkEventArgs e) => {
                bl.StartSimulation(
                   Drone_bl.ID,
                     (i) => { Worker.ReportProgress(i); },
                     () => Worker.CancellationPending);

            };
            Worker.WorkerReportsProgress = true;
            Worker.ProgressChanged += (object? sender, ProgressChangedEventArgs e) => {
                Drone.updateDronePO(bl.GetSpesificDrone(Drone_bl.ID));

            };

            Worker.WorkerSupportsCancellation = true;
            Worker.RunWorkerAsync();
        }

        private void Simulatiom_Stop_Click(object sender, RoutedEventArgs e)
        {
            Worker.CancelAsync();
        }       
    }
}