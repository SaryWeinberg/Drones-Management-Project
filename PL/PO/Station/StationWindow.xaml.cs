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
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        BLApi.IBL bl;
        Station Station;
        public event ObjectChanged<BO.Station> SomeChangedHappened;
        public StationWindow(BLApi.IBL blMain)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            sendNewStation.Visibility = Visibility.Visible;
            StationID.Focus();
            Station = new Station(new BO.Station());
            Station.stationListChanged +=  UpdateStationList;
        }

        private void AddNewStation(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result =
                   MessageBox.Show(
                   bl.AddStationBL(IDInput(), NameInput(), new BO.Location { Longitude = LongitudeInput(), Latitude = LatitudeInput() }, ChargeSlotsInput()),
                   $"Add station ID - {IDInput()}",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {

                    if (Station.stationListChanged != null)
                        Station.stationListChanged(bl.GetSpesificStation(IDInput()));
                    /* new StationListWindow(bl).Show();
                     Close();*/
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public StationWindow(BLApi.IBL blMain, BO.Station station)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;

            Station = new Station(station);
            Station.stationListChanged += new ObjectChanged<BO.Station>(UpdateStationList);
            AddStation.DataContext = Station;

            /* StationName.Text = station.Name.ToString();
             StationChargeSlots.Text = station.AveChargeSlots.ToString();*/

            UpdateStationGrid.Visibility = Visibility.Visible;

            DronesInChargelist.ItemsSource = station.DronesInChargelist;
            StationID.IsEnabled = false;
            StationLongitude.IsEnabled = false;
            StationLatitude.IsEnabled = false;

            StationName.TextChanged += AddUpdateButton;
            StationChargeSlots.TextChanged += AddUpdateButton;
        }

        private void AddUpdateButton(object sender, RoutedEventArgs e) => updateStation.Visibility = Visibility.Visible;
        public void UpdateStationList(BO.Station station)
        {

            if (SomeChangedHappened != null)
                SomeChangedHappened(station);

        }
        private void UpdateStation(object sender, RoutedEventArgs e)
        {
            string ID = StationID.Text;
            /*          string name = StationName.Text;
                      string chargeSlots = StationChargeSlots.Text;*/
            try
            {
                MessageBoxResult result =
                   MessageBox.Show(
                   bl.UpdateStationData(IDInput(), NameInput(), ChargeSlotsInput()),
                   $"Update station ID - {ID}",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    /*  new StationListWindow(bl).Show();
                      Close();*/
                    Station.UpdateStation(bl.GetSpesificStation(Station.ID));
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void ClosingWindow(object sender, RoutedEventArgs e) => Close();

        private void RefreshWindow(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnWindow(object sender, RoutedEventArgs e)
        {
            new StationListWindow(bl).Show();
     /*       Close();*/
        }

        private void GetDrone(object sender, MouseButtonEventArgs e)
        
       {



   
            
                /*   Close();*/
            
            BO.DroneInCharge drone = (sender as ListView).SelectedValue as BO.DroneInCharge;

            DroneWindow open = new DroneWindow(bl, bl.GetSpesificDrone(drone.ID));

            open.SomeChangedHappened += UpdateObjectInTheList;
            open.Show();
       /*     new DroneWindow(bl, bl.GetSpesificDrone(drone.ID)).Show();
            Close();*/
        }

        public void UpdateObjectInTheList(BO.Drone drone)
        {
            if (SomeChangedHappened != null)

            
                SomeChangedHappened(bl.GetNearestAvailableStation(drone.Location));


        }

        #region Get Inputs
        /// <summary>
        /// Takes ID input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int IDInput()
        {
            try { return int.Parse(StationID.Text); }
            catch (Exception) { throw new InvalidObjException("ID"); }
        }
        /// <summary>
        /// Takes charge slots input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int ChargeSlotsInput()
        {
            try { return int.Parse(StationChargeSlots.Text); }
            catch (Exception) { throw new InvalidObjException("ChargeSlots"); }
        }
        /// <summary>
        /// Takes name input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int NameInput()
        {
            try { return int.Parse(StationName.Text); }
            catch (Exception) { throw new InvalidObjException("Name"); }
        }
        /// <summary>
        /// Takes longitude input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int LongitudeInput()
        {
            try { return int.Parse(StationLongitude.Text); }
            catch (Exception) { throw new InvalidObjException("Longitude"); }
        }
        /// <summary>
        /// Takes latitude input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int LatitudeInput()
        {
            try { return int.Parse(StationLatitude.Text); }
            catch (Exception) { throw new InvalidObjException("Latitude"); }
        }
        #endregion

        #region Switch between TextBoxes
        /// <summary>
        /// Moves from TextBox StationID to TextBox StationName
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownStationID(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) StationName.Focus();
        }
        /// <summary>
        /// Moves from TextBox StationName to TextBox StationChargeSlots
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownStationName(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) StationChargeSlots.Focus();
        }
        /// <summary>
        /// Moves from TextBox StationChargeSlots to TextBox StationLongitude
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownStationChargeSlots(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) StationLongitude.Focus();
        }
        /// <summary>
        /// Moves from TextBox StationLongitude to TextBox StationLatitude
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownStationLongitude(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) StationLatitude.Focus();
        }
        /// <summary>
        /// Moves from TextBox StationLatitude to Button sendNewStation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownStationLatitude(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) sendNewStation.Focus();
        }
        #endregion
    }
}
