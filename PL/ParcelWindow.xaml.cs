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
        public ParcelWindow(BLApi.IBL blMain)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            UpdateParcelGrid.Visibility = Visibility.Hidden;
            ParcelWeight.ItemsSource = Enum.GetValues(typeof(WeightCategories));          
            ParcelPriority.ItemsSource = Enum.GetValues(typeof(Priorities));
            sendNewParcel.Visibility = Visibility.Visible;           
        }

        public ParcelWindow(BLApi.IBL blMain, BO.Parcel parcel)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            AddParcelGrid.Visibility = Visibility.Hidden;              
                                 
            ParcelID.Text = parcel.ID.ToString();
            ParcelSender.Text = parcel.Sender.ToString();
            ParcelTarget.Text = parcel.Target.ToString();
            ParcelWeigh.Text = parcel.Weight.ToString();
            ParcelPriorit.Text = parcel.Priority.ToString();
            ParcelDrone.Text = parcel.Drone.ToString();
            ParcelCreated.Text = parcel.Created.ToString();
            ParcelAssociated.Text = parcel.Associated.ToString();
            ParcelPickedUp.Text = parcel.PickedUp.ToString();
            ParcelDelivered.Text = parcel.Delivered.ToString();
        }

        private void AddNewParcel(object sender, RoutedEventArgs e)
        {
            
            try
            {              
                MessageBoxResult result =
                   MessageBox.Show(
                   bl.AddParcelBL(GetSenderID(), GetTargetID(), (WeightCategories)GetWeight(), (Priorities)GetPriority()),
                   $"Add parcel",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    //new ParcelListWindow(bl).Show();
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        //===========Get Inputs===========

        private int GetSenderID()
        {
            try { return int.Parse(ParcelSenderID.Text); }
            catch (Exception) { throw new InvalidObjException("Sender ID"); }
        }
        private int GetTargetID()
        {
            try { return int.Parse(ParcelTargetID.Text); }
            catch (Exception) { throw new InvalidObjException("Target ID"); }
        }        
        private int GetWeight()
        {
            try { return int.Parse(ParcelWeight.Text); }
            catch (Exception) { throw new InvalidObjException("Weight"); }
        }
        private int GetPriority()
        {
            try { return int.Parse(ParcelPriority.Text); }
            catch (Exception) { throw new InvalidObjException("Priority"); }
        }

        private void ClosingWindow(object sender, RoutedEventArgs e) => Close();       
    }
}
