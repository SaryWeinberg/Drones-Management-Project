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
        public  ObjectChangedAction<BO.Parcel> SomeChangedHappened;
        public ObjectChangedAction<BO.Parcel> ParcelIsremoved;

        /// <summary>
        /// Ctor of add parcel window
        /// </summary>
        /// <param name="blMain"></param>
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
            Parcel = new Parcel(new BO.Parcel());
            Parcel.ParcelListChanged += new ObjectChangedAction<BO.Parcel>(UpdateParcelList);
        }

        /// <summary>
        /// Ctor of update customer window
        /// </summary>
        /// <param name="blMain"></param>
        /// <param name="parcel"></param>
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

            Parcel.ParcelListChanged += new ObjectChangedAction<BO.Parcel>(UpdateParcelList);
        }

        /// <summary>
        /// Adding new parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                    if (Parcel.ParcelListChanged != null)
                        Parcel.ParcelListChanged(bl.GetSpesificParcel(bl.GetParcels().Count()-1));
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Remove parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveParcel(object sender, RoutedEventArgs e)
        {
            if (Parcel.Associated != null && Parcel.Delivered == null)
            {
                MessageBox.Show("Unable to delete package in shipment");
            }
            else
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
                        ParcelIsremoved(new BO.Parcel() { ID = int.Parse(ParcelID.Text) });
                        Close();
                    }
                }
                catch (Exception exc) { MessageBox.Show(exc.Message); }
            }
        }

        /// <summary>
        /// Closing window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosingWindow(object sender, RoutedEventArgs e) => Close();

        /// <summary>
        /// Enter the customer registered with the parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetCustomer(object sender, MouseButtonEventArgs e)
        {
            string ID = (sender as TextBox).SelectedText;
            new CustomerWindow(bl, bl.GetSpesificCustomer(int.Parse(ID))).Show();
        }

        /// <summary>
        /// Enter the drone registered with the parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetDrone(object sender, MouseButtonEventArgs e)
        {
            string ID = (sender as TextBox).SelectedText;
            new DroneWindow(bl, bl.GetSpesificDrone(int.Parse(ID))).Show();
        }

        /// <summary>
        /// Parcel collection confirmation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApprovePickedUp(object sender, RoutedEventArgs e)
        {
            Parcel.PickedUp = DateTime.Now;
            try
            {
                bl.CollectParcelByDrone(Parcel.Drone.ID);
            }
            catch(Exception exc) { MessageBox.Show(exc.Message); }
            DeliveredChecked.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Confirmation of parcel arrival
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApproveDelivered(object sender, RoutedEventArgs e)
        {
            Parcel.Delivered = DateTime.Now;
            try
            {
                bl.SupplyParcelByDrone(Parcel.Drone.ID);
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }
        }

        /// <summary>
        /// Submit to update the parcel list
        /// </summary>
        /// <param name="parcel"></param>
        public void UpdateParcelList(BO.Parcel parcel)
        {
            if (SomeChangedHappened != null)
                SomeChangedHappened(parcel);
        }

        /// <summary>
        ///  Back to previous window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnWindow(object sender, RoutedEventArgs e)
        {
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
