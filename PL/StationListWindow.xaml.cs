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
    /// Interaction logic for StationListWindow.xaml
    /// </summary>
    public partial class StationListWindow : Window
    {
        BLApi.IBL bl;

        public StationListWindow(BLApi.IBL blMain)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            StationListView.ItemsSource = bl.GetStationsListBL();
        }

        private void ClosingWindow(object sender, RoutedEventArgs e) => Close();

        private void RefreshWindow(object sender, RoutedEventArgs e) => StationListView.Items.Refresh();

        private void AddStation(object sender, RoutedEventArgs e)
        {
            new StationWindow(bl).Show();
            Close();
        }

        private void UpdateStation(object sender, MouseButtonEventArgs e)
        {
            BO.StationToList station = (sender as ListView).SelectedValue as BO.StationToList;
            new StationWindow(bl, bl.GetSpesificStation(station.ID)).Show();
            Close();
        }

        private void ReturnWindow(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
