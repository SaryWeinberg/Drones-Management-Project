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
        string[] dataArr = { "sender_ID", "target_ID", "weight", "priority" };
        public ParcelWindow(BLApi.IBL blMain)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            ParcelWeight.ItemsSource = Enum.GetValues(typeof(WeightCategories));          
            ParcelPriority.ItemsSource = Enum.GetValues(typeof(Priorities));
            sendNewParcel.Visibility = Visibility.Visible;           
        }

        private void AddNewParcel(object sender, RoutedEventArgs e)
        {
            string senderID = ParcelSenderID.Text;
            string targetID = ParcelTargetID.Text;
            int weight = ParcelWeight.SelectedIndex;
            int priority = ParcelPriority.SelectedIndex;
            try
            {              
                MessageBoxResult result =
                   MessageBox.Show(
                   bl.AddParcelBL(int.Parse(senderID), int.Parse(targetID), (WeightCategories)weight, (Priorities)priority),
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

        private void ClosingWindow(object sender, RoutedEventArgs e) => Close();       
    }
}
