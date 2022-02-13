using System;
using System.Collections;
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

        private ObservableCollection<T> Convert<T>(IEnumerable original)
        {
            return new ObservableCollection<T>(original.Cast<T>());
        }
        public  ObjectChangedAction<BO.Station> SomeChangedHappened;
        BLApi.IBL bl;
        private CollectionView view;
        ObservableCollection<BO.StationToList> _myCollection = new ObservableCollection<BO.StationToList>();
        public StationListWindow(BLApi.IBL blMain)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            /*f*//*oreach (var item in bl.GetStationsToList())

                _myCollection.Add(item);*/
            _myCollection = Convert<BO.StationToList>(bl.GetStationsToList());
            DataContext = _myCollection;
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);

        }


       


        private void ClosingWindow(object sender, RoutedEventArgs e) => Close();

        private void RefreshWindow(object sender, RoutedEventArgs e) => StationListView.Items.Refresh();

        private void AddStation(object sender, RoutedEventArgs e)
        {
            StationWindow openWindow = new StationWindow(bl);
            openWindow.SomeChangedHappened += AddStation;
            openWindow.Show();

        }

        private void UpdateStation(object sender, MouseButtonEventArgs e)
        {
            BO.StationToList station = (sender as ListView).SelectedValue as BO.StationToList;
            StationWindow openWindow = new StationWindow(bl, bl.GetSpesificStation(station.ID));
            openWindow.SomeChangedHappened += updateStation;
            openWindow.Show();

        }
        private void updateStation(BO.Station station)
        {
            BO.StationToList stationToList = _myCollection.First(s => s.ID == station.ID);
            int idx = _myCollection.IndexOf(stationToList);
            _myCollection[idx] = new BO.StationToList(station);
        }

        private void AddStation(BO.Station station)
        {
            _myCollection.Add(new BO.StationToList(station));

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
