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
        }

        private void AddNewStation(object sender, RoutedEventArgs e)
        {  
            try
            {               
                MessageBoxResult result =
                   MessageBox.Show(
                   bl.AddStationBL(GetID(), GetName(), new BO.Location { Longitude = GetLongitude(), Latitude = GetLatitude() }, GetChargeSlots()),
                   $"Add station ID - {GetID()}",
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
            StationID.Text = station.ID.ToString();
            StationName.Text = station.Name.ToString();
            StationChargeSlots.Text = station.AveChargeSlots.ToString();
            StationLongitude.Text = station.Location.Longitude.ToString();
            StationLatitude.Text = station.Location.Latitude.ToString();
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
                   bl.UpdateStationData(GetID(), GetName(), GetChargeSlots()),
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
        private int GetID()
        {
            try { return int.Parse(StationID.Text); }
            catch (Exception) { throw new InvalidObjException("ID"); }
        }
        private int GetChargeSlots()
        {
            try { return int.Parse(StationChargeSlots.Text); }
            catch (Exception) { throw new InvalidObjException("ChargeSlots"); }
        }
        private int GetName()
        {
            try { return int.Parse(StationName.Text); }
            catch (Exception) { throw new InvalidObjException("Name"); }
        }
        private int GetLongitude()
        {
            try { return int.Parse(StationLongitude.Text); }
            catch (Exception) { throw new InvalidObjException("Longitude"); }
        }
        private int GetLatitude()
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
    }   
}
