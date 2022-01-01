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
            /*            ParcelID.Text = parcel.ID.ToString();
                        ParcelSender.Text = parcel.Sender.ID.ToString();
                        ParcelTarget.Text = parcel.Target.ID.ToString();
                        ParcelWeigh.Text = parcel.Weight.ToString();
                        ParcelPriorit.Text = parcel.Priority.ToString();
                        ParcelDrone.Text = parcel.Drone.ID.ToString();
                        ParcelCreated.Text = parcel.Created.ToString();
                        ParcelAssociated.Text = parcel.Associated.ToString();
                        ParcelPickedUp.Text = parcel.PickedUp.ToString();
                        ParcelDelivered.Text = parcel.Delivered.ToString();*/
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
                    //new ParcelListWindow(bl).Show();
                    /*  Close();*/
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        //===========Get Inputs===========

        private int SenderIDInput()
        {
            try { return int.Parse(ParcelSenderID.Text); }
            catch (Exception) { throw new InvalidObjException("Sender ID"); }
        }
        private int TargetIDInput()
        {
            try { return int.Parse(ParcelTargetID.Text); }
            catch (Exception) { throw new InvalidObjException("Target ID"); }
        }
        private int WeightInput()
        {
            try
            {
                return ParcelWeight.SelectedIndex;
                /*    return int.Parse(ParcelWeight.Text);*/

            }
            catch (Exception) { throw new InvalidObjException("Weight"); }
        }
        private int PriorityInput()
        {
            try
            {
                return ParcelPriority.SelectedIndex;
                /*  return int.Parse(ParcelPriority.Text);*/
            }
            catch (Exception) { throw new InvalidObjException("Priority"); }
        }

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

        private void RefreshWindow(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnWindow(object sender, RoutedEventArgs e)
        {
            new ParcelListWindow(bl).Show();
            Close();
        }

        

        private void OnKeyDownParcelSenderID(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) ParcelTargetID.Focus();

        }

        private void OnKeyDownParcelTargetID(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) ParcelWeight.Focus();

        }

        private void OnKeyDownParcelWeight(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) ParcelPriority.Focus();

        }

        private void OnKeyDownParcelPriority(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) sendNewParcel.Focus();

        }
    }
}
