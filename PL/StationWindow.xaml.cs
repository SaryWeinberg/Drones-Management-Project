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
        BO.Station Station;

        public StationWindow(BLApi.IBL blMain)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            sendNewStation.Visibility = Visibility.Visible;
            StationID.Focus();
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
                    new StationListWindow(bl).Show();
                    Close();
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

            Station = station;
            AddStation.DataContext = Station;
            /*StationID.Text = station.ID.ToString();
            StationName.Text = station.Name.ToString();
            StationChargeSlots.Text = station.AveChargeSlots.ToString();
            StationLongitude.Text = station.Location.Longitude.ToString();
            StationLatitude.Text = station.Location.Latitude.ToString();*/

            StationName.Text = station.Name.ToString();
            StationChargeSlots.Text = station.AveChargeSlots.ToString();

            UpdateStationGrid.Visibility = Visibility.Visible;
           /* DronesInChargelistLabel.Visibility = Visibility.Visible;
            DronesInChargelist.Visibility = Visibility.Visible;*/
            DronesInChargelist.ItemsSource = station.DronesInChargelist;
            StationID.IsEnabled = false;
            StationLongitude.IsEnabled = false;
            StationLatitude.IsEnabled = false;

            StationName.TextChanged += AddUpdateButton;
            StationChargeSlots.TextChanged += AddUpdateButton;          
        }

        private void AddUpdateButton(object sender, RoutedEventArgs e) => updateStation.Visibility = Visibility.Visible;

        private void UpdateStation(object sender, RoutedEventArgs e)
        {
            string ID = StationID.Text;
            string name = StationName.Text;
            string chargeSlots = StationChargeSlots.Text;
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
                    new StationListWindow(bl).Show();
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void ClosingWindow(object sender, RoutedEventArgs e) => Close();

        //===========Get Inputs===========
        private int IDInput()
        {
            try { return int.Parse(StationID.Text); }
            catch (Exception) { throw new InvalidObjException("ID"); }
        }
        private int ChargeSlotsInput()
        {
            try { return int.Parse(StationChargeSlots.Text); }
            catch (Exception) { throw new InvalidObjException("ChargeSlots"); }
        }
        private int NameInput()
        {
            try { return int.Parse(StationName.Text); }
            catch (Exception) { throw new InvalidObjException("Name"); }
        }
        private int LongitudeInput()
        {
            try { return int.Parse(StationLongitude.Text); }
            catch (Exception) { throw new InvalidObjException("Longitude"); }
        }
        private int LatitudeInput()
        {
            try { return int.Parse(StationLatitude.Text); }
            catch (Exception) { throw new InvalidObjException("Latitude"); }
        }

        private void RefreshWindow(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnWindow(object sender, RoutedEventArgs e)
        {
            new StationListWindow(bl).Show();
            Close();
        }

        private void GetDrone(object sender, MouseButtonEventArgs e)
        {
            BO.DroneInCharge drone = (sender as ListView).SelectedValue as BO.DroneInCharge;
            new DroneWindow(bl, bl.GetSpesificDrone(drone.ID)).Show();
            Close();
        }

        private void OnKeyDownStationID(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) StationName.Focus();
        }

        private void OnKeyDownStationName(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) StationChargeSlots.Focus();
        }

        private void OnKeyDownStationChargeSlots(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) StationLongitude.Focus();
        }

        private void OnKeyDownStationLongitude(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) StationLatitude.Focus();
        }

        private void OnKeyDownStationLatitude(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) sendNewStation.Focus();
        }
    }   
}
