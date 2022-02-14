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


        /// <summary>
        /// Ctor of StationListWindow
        /// </summary>
        /// <param name="blMain"></param>
        public StationListWindow(BLApi.IBL blMain)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;           
            _myCollection = Convert<BO.StationToList>(bl.GetStationsToList());
            DataContext = _myCollection;
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
        }

        /// <summary>
        /// Closing window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosingWindow(object sender, RoutedEventArgs e) => Close();

        /// <summary>
        /// Show drone window with adding ctor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddStation(object sender, RoutedEventArgs e)
        {
            StationWindow openWindow = new StationWindow(bl);
            openWindow.SomeChangedHappened += AddStation;
            openWindow.Show();
        }

        /// <summary>
        /// Show station window with update ctor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateStation(object sender, MouseButtonEventArgs e)
        {
            BO.StationToList station = (sender as ListView).SelectedValue as BO.StationToList;
            StationWindow openWindow = new StationWindow(bl, bl.GetSpesificStation(station.ID));
            openWindow.SomeChangedHappened += UpdateStationList;
            openWindow.Show();
        }

    
        /// <summary>
        /// Updates objects within the station list
        /// </summary>
        /// <param name="station"></param>
        private void UpdateStationList(BO.Station station)
        {
            BO.StationToList stationToList = _myCollection.First(s => s.ID == station.ID);
            int idx = _myCollection.IndexOf(stationToList);
            _myCollection[idx] = new BO.StationToList(station);
        }

        /// <summary>
        /// Adds a station to the station list
        /// </summary>
        /// <param name="station"></param>
        private void AddStation(BO.Station station)
        {
            _myCollection.Add(new BO.StationToList(station));
        }

        /// <summary>
        /// Back to previous window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnWindow(object sender, RoutedEventArgs e)
        {
            new MainWindow(bl).Show();
            Close();
        }

        /// <summary>
        /// Returns the list divided into groups by empty slots
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupByEmptySlots(object sender, RoutedEventArgs e)
        {
            if (view != null && view.CanGroup == true)
            {
                view.GroupDescriptions.Clear();
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("AveChargeSlots");
                view.GroupDescriptions.Add(groupDescription);
            }
        }

        /// <summary>
        /// Returns the list divided into groups by availble stations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupByAvailbleStations(object sender, RoutedEventArgs e)
        {
            ClearListView();
            StationListView.ItemsSource = bl.GetStationsToList(station => station.AveChargeSlots > 0);
            view = (CollectionView)CollectionViewSource.GetDefaultView(StationListView.ItemsSource);
        }

        /// <summary>
        /// Clears the list to insert a new grouping
        /// </summary>
        private void ClearListView()
        {
            StationListView.ItemsSource = bl.GetStationsToList();
            view = (CollectionView)CollectionViewSource.GetDefaultView(StationListView.ItemsSource);
        }
    }
}
