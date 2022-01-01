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
        ObservableCollection<BO.ParcelToList> list = new ObservableCollection<BO.ParcelToList>();


        public ParcelListWindow(BLApi.IBL blMain)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
           
            foreach (var item in bl.GetParcelsToList())
                list.Add(item);
            DataContext = list;
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
            ParcelListView.ItemsSource = list;
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
                ParcelListView.ItemsSource = bl.GetParcelsToListByCondition(parcel => parcel.Priority == (Priorities)priority.SelectedItem);
            }
            else
            {
                Sweight = (WeightCategories)WeightSelector.SelectedItem;
                ParcelListView.ItemsSource =
                    bl.GetParcelsToListByCondition(parcel => parcel.Priority == (Priorities)priority.SelectedItem && parcel.Weight == Sweight);
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
                ParcelListView.ItemsSource = bl.GetParcelsToListByCondition(parcel => parcel.Weight == (WeightCategories)weight.SelectedItem);
            }
            else
            {
                Ppriority = (Priorities)PrioritySelector.SelectedItem;
                ParcelListView.ItemsSource = bl.GetParcelsToListByCondition(parcel => parcel.Weight == (WeightCategories)weight.SelectedItem && parcel.Priority == Ppriority);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosingWindow(object sender, RoutedEventArgs e) => Close();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshWindow(object sender, RoutedEventArgs e) => ParcelListView.Items.Refresh();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
/*        private void ParcelAllList(object sender, RoutedEventArgs e) => ParcelListView.ItemsSource = bl.GetParcelsToList();*/

        private void FilterByDateRange(object sender, RoutedEventArgs e)
        {
            
            ParcelListView.ItemsSource = bl.GetParcelsByCondition(parcel=> parcel.Created >= DatePickerFrom.SelectedDate);
            ParcelListView.ItemsSource = from parcels in bl.GetParcelsByCondition(parcel => parcel.Created >= DatePickerFrom.SelectedDate && parcel.Created <= DatePickerTo.SelectedDate)
                                         select (new BO.ParcelToList(parcels));

        }

        /// <summary>
        /// Show parcel window with adding ctor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddParcel(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(bl).Show();
            Close();
        }

        private void UpdateParcel(object sender, MouseButtonEventArgs e)
        {
            BO.ParcelToList parcelToList = (sender as ListView).SelectedValue as BO.ParcelToList;
            new ParcelWindow(bl, bl.GetSpesificParcelBL(parcelToList.ID)).Show();
            Close();
        }

        private void ReturnWindow(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void GroupBySender(object sender, RoutedEventArgs e)
        {
            if (view != null && view.CanGroup == true)
            {
                view.GroupDescriptions.Clear();
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("SenderName");
                view.GroupDescriptions.Add(groupDescription);
            }
        }

        private void GroupByTarget(object sender, RoutedEventArgs e)
        {
            if (view != null && view.CanGroup == true)
            {
                view.GroupDescriptions.Clear();
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("TargetName");
                view.GroupDescriptions.Add(groupDescription);
            }
        }

        private void ParcelListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
