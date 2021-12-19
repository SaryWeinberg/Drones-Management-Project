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
            string ID = StationID.Text;
            string name = StationName.Text;
            string chargeSlots = StationChargeSlots.Text;
            string longitude = StationLongitude.Text;
            string latitude = StationLatitude.Text;
            try
            {               
                MessageBoxResult result =
                   MessageBox.Show(
                   bl.AddStationBL(int.Parse(ID), int.Parse(name), new BO.Location { Longitude = int.Parse(longitude), Latitude = int.Parse(latitude) }, int.Parse(chargeSlots)),
                   $"Add station ID - {ID}",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                   /* new StationListWindow(bl).Show();*/
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
                   bl.UpdateStationData(int.Parse(ID), name, chargeSlots),
                   $"Update station ID - {ID}",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    //new StationListWindow(bl).Show();
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void ClosingWindow(object sender, RoutedEventArgs e) => Close();   
    }
    
}
