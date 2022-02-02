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
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
        BLApi.IBL bl;
        Parcel Parcel;
        public ParcelWindow(BLApi.IBL blMain)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            ParcelSenderID.Focus();
            UpdateParcelGrid.Visibility = Visibility.Hidden;
            ParcelWeight.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            ParcelPriority.ItemsSource = Enum.GetValues(typeof(Priorities));
            sendNewParcel.Visibility = Visibility.Visible;
        }

        public ParcelWindow(BLApi.IBL blMain, BO.Parcel parcel)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            Parcel = new Parcel(parcel);
            AddParcelGrid.Visibility = Visibility.Hidden;
            UpdateParcelGrid.DataContext = Parcel;
            if (Parcel.Associated != null && Parcel.PickedUp == null)
                PickedUpChecked.Visibility = Visibility.Visible;
            if (Parcel.PickedUp != null && Parcel.Delivered == null)
                DeliveredChecked.Visibility = Visibility.Visible;
        }

        private void AddNewParcel(object sender, RoutedEventArgs e)
        {

            try
            {
                MessageBoxResult result =
                   MessageBox.Show(
                   bl.AddParcelBL(SenderIDInput(), TargetIDInput(), (WeightCategories)WeightInput(), (Priorities)PriorityInput()),
                   $"Add parcel",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void RemoveParcel(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result =
                    MessageBox.Show(
                    bl.RemoveParcel(int.Parse(ParcelID.Text)),
                    $"Add parcel",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    new ParcelListWindow(bl).Show();
                    Close();
                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }
        }

/*
        private void UpdateParcel(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result =
                    MessageBox.Show(
                    bl.(int.Parse(ParcelID.Text)),
                    $"Add parcel",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    new ParcelListWindow(bl).Show();
                    Close();
                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }
        }*/

        private void ClosingWindow(object sender, RoutedEventArgs e) => Close();

        private void GetCustomer(object sender, MouseButtonEventArgs e)
        {
            string ID = (sender as TextBox).SelectedText;
            new CustomerWindow(bl, bl.GetSpesificCustomer(int.Parse(ID))).Show();
        }

        private void GetDrone(object sender, MouseButtonEventArgs e)
        {
            string ID = (sender as TextBox).SelectedText;
            new DroneWindow(bl, bl.GetSpesificDrone(int.Parse(ID))).Show();
        }

        private void ApprovePickedUp(object sender, RoutedEventArgs e)
        {
            Parcel.PickedUp = DateTime.Now;
            bl.CollectParcelByDrone(Parcel.Drone.ID);
            DeliveredChecked.Visibility = Visibility.Visible;
        }

        private void ApproveDelivered(object sender, RoutedEventArgs e)
        {
            Parcel.Delivered = DateTime.Now;
            bl.SupplyParcelByDrone(Parcel.Drone.ID);
        }

        private void RefreshWindow(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnWindow(object sender, RoutedEventArgs e)
        {
            new ParcelListWindow(bl).Show();
            Close();
        }      

        #region Get Inputs
        /// <summary>
        /// Takes sander ID input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int SenderIDInput()
        {
            try { return int.Parse(ParcelSenderID.Text); }
            catch (Exception) { throw new InvalidObjException("Sender ID"); }
        }
        /// <summary>
        /// Takes target ID input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int TargetIDInput()
        {
            try { return int.Parse(ParcelTargetID.Text); }
            catch (Exception) { throw new InvalidObjException("Target ID"); }
        }
        /// <summary>
        /// Takes weight input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int WeightInput()
        {
            try { return ParcelWeight.SelectedIndex; }
            catch (Exception) { throw new InvalidObjException("Weight"); }
        }
        /// <summary>
        /// Takes priority input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int PriorityInput()
        {
            try { return ParcelPriority.SelectedIndex; }
            catch (Exception) { throw new InvalidObjException("Priority"); }
        }
        #endregion

        #region Switch between TextBoxes
        /// <summary>
        /// Moves from TextBox ParcelSenderID to TextBox ParcelTargetID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownParcelSenderID(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) ParcelTargetID.Focus();
        }
        /// <summary>
        /// Moves from TextBox ParcelTargetID to ComboBOx ParcelWeight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownParcelTargetID(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) ParcelWeight.Focus();
        }
        /// <summary>
        /// Moves from ComboBOx ParcelWeight to ComboBOx ParcelPriority
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownParcelWeight(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) ParcelPriority.Focus();
        }
        /// <summary>
        /// Moves from ComboBOx ParcelPriority to Button sendNewParcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownParcelPriority(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) sendNewParcel.Focus();
        }
        #endregion        
    }
}
