using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private CollectionView view;
        ObservableCollection<BO.StationToList> list = new ObservableCollection<BO.StationToList>();
        public StationListWindow(BLApi.IBL blMain)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            foreach (var item in bl.GetStationsToList())
                list.Add(item);
            DataContext = list;
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);

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
            new MainWindow(bl).Show();
            Close();
        }


        private void GroupByEmptySlots(object sender, RoutedEventArgs e)
        {
            if (view != null && view.CanGroup == true)
            {
                view.GroupDescriptions.Clear();
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("AveChargeSlots");
                view.GroupDescriptions.Add(groupDescription);
            }
        }

        private void ShowAvailbleStations(object sender, RoutedEventArgs e)
        {
            ClearListView();
            StationListView.ItemsSource = bl.GetStationsToList(station => station.AveChargeSlots > 0);
            view = (CollectionView)CollectionViewSource.GetDefaultView(StationListView.ItemsSource);
        }

        private void ClearListView()
        {
            StationListView.ItemsSource = bl.GetStationsToList();
            view = (CollectionView)CollectionViewSource.GetDefaultView(StationListView.ItemsSource);
        }
    }
}
