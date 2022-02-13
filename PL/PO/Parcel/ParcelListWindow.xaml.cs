using System;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ParcelListWindow.xaml
    /// </summary>
    public partial class ParcelListWindow : Window
    {
        BLApi.IBL bl;
        private CollectionView view;
        ObservableCollection<BO.ParcelToList> _myCollection = new ObservableCollection<BO.ParcelToList>();

        /// <summary>
        /// Ctor of parcel list window
        /// </summary>
        /// <param name="blMain"></param>
        public ParcelListWindow(BLApi.IBL blMain)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            foreach (var item in bl.GetParcelsToList())
                _myCollection.Add(item);
            DataContext = _myCollection;
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
            PrioritySelector.ItemsSource = Enum.GetValues(typeof(Priorities));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }       

        /// <summary>
        /// Filter the list category priority
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrioritySelectorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox priority = sender as ComboBox;
            WeightCategories Sweight = 0;
            if (WeightSelector.SelectedItem == null)
            {
                ParcelListView.ItemsSource = bl.GetParcelsToList(parcel => parcel.Priority == (Priorities)priority.SelectedItem);
            }
            else
            {
                Sweight = (WeightCategories)WeightSelector.SelectedItem;
                ParcelListView.ItemsSource =
                    bl.GetParcelsToList(parcel => parcel.Priority == (Priorities)priority.SelectedItem && parcel.Weight == Sweight);
            }
        }

        /// <summary>
        /// Filter the list category weight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeightSelectorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox weight = sender as ComboBox;
            Priorities Ppriority = 0;
            if (PrioritySelector.SelectedItem == null)
            {
                ParcelListView.ItemsSource = bl.GetParcelsToList(parcel => parcel.Weight == (WeightCategories)weight.SelectedItem);
            }
            else
            {
                Ppriority = (Priorities)PrioritySelector.SelectedItem;
                ParcelListView.ItemsSource = bl.GetParcelsToList(parcel => parcel.Weight == (WeightCategories)weight.SelectedItem && parcel.Priority == Ppriority);
            }
        }

        /// <summary>
        /// Closing the Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosingWindow(object sender, RoutedEventArgs e) => Close();

        /// <summary>
        /// Filter the list by date range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterByDateRange(object sender, RoutedEventArgs e)
        {            
            ParcelListView.ItemsSource = bl.GetParcels(parcel=> parcel.Created >= DatePickerFrom.SelectedDate);
            ParcelListView.ItemsSource = from parcels in bl.GetParcels(parcel => parcel.Created >= DatePickerFrom.SelectedDate && parcel.Created <= DatePickerTo.SelectedDate)
                                         select (new BO.ParcelToList(parcels));      
        }

        /// <summary>
        /// Show parcel window with adding ctor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>       
        private void AddParcel(object sender, RoutedEventArgs e)
        {
            ParcelWindow openWindow = new ParcelWindow(bl);
            openWindow.SomeChangedHappened += addParcel;
            openWindow.returnWindow.Click += SonReturnWindow;
            openWindow.Show();
        }

        /// <summary>
        /// Add parcel to the list
        /// </summary>
        /// <param name="parcel"></param>
        private void addParcel(BO.Parcel parcel)
        {
            _myCollection.Add(new BO.ParcelToList(parcel));
        }

        /// <summary>
        /// Sending to the window of update parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateParcel(object sender, MouseButtonEventArgs e)
        {
            BO.ParcelToList parcelToList = (sender as ListView).SelectedValue as BO.ParcelToList;
            ParcelWindow openWindow = new ParcelWindow(bl, bl.GetSpesificParcel(parcelToList.ID));
            openWindow.SomeChangedHappened += UpdateParcel;
            openWindow.Show();
        }

        /// <summary>
        /// Update parcel in the list
        /// </summary>
        /// <param name="parcel"></param>
        private void UpdateParcel(BO.Parcel parcel)
        {
            BO.ParcelToList parcelToList = _myCollection.First(s => s.ID == parcel.ID);
            int idx = _myCollection.IndexOf(parcelToList);
            _myCollection[idx] = new BO.ParcelToList(parcel);
        }

        /// <summary>
        /// Back to this window through the son
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SonReturnWindow(object sender, RoutedEventArgs e)
        {
           this.Show();            
        }

        /// <summary>
        ///  Back to previous window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnWindow(object sender, RoutedEventArgs e)
        {
            new MainWindow(bl).Show();
            Close();
        }

        /// <summary>
        /// Sorting into groups by sender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupBySender(object sender, RoutedEventArgs e)
        {
            if (view != null && view.CanGroup == true)
            {
                view.GroupDescriptions.Clear();
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("SenderName");
                view.GroupDescriptions.Add(groupDescription);
            }
        }

        /// <summary>
        /// Sorting into groups by target
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupByTarget(object sender, RoutedEventArgs e)
        {
            if (view != null && view.CanGroup == true)
            {
                view.GroupDescriptions.Clear();
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("TargetName");
                view.GroupDescriptions.Add(groupDescription);
            }
        }    
    }
}
