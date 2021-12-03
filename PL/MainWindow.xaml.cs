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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL.IBL bl = new BL.BL();
        string[] arrLists = { "Drone", "Customer", "Station", "Parcel" };
        public MainWindow()
        {
            InitializeComponent();
            /*int x = 50;
            foreach (string item in arrLists)
            {
                Button display = new Button();
                display.Content = $"{item} List Window";
                display.VerticalAlignment = VerticalAlignment.Top;
                display.HorizontalAlignment = HorizontalAlignment.Left;
                display.Width = 150;
                switch (item)
                {
                    case "Drone": display.Click += ViewDroneListWindow; break;
                    case "Customer": display.Click += ViewCustomerListWindow; break;
                    case "Station": display.Click += ViewStationListWindow; break;
                    case "Parcel": display.Click +=  ViewParcelListWindow; break;
                }
                display.Margin = new Thickness(x, 50, 0, 0);
                MainData.Children.Add(display);
                x += 170;
            }*/
        }

        private void ViewDroneListWindow(object sender, RoutedEventArgs e)
        {
            new DroneListWindow(bl).Show();
        }

        private void ViewParcelListWindow(object sender, RoutedEventArgs e)
        {
            new ParcelListWindow(bl).Show();
        }

        private void ViewCustomerListWindow(object sender, RoutedEventArgs e)
        {
            new CustomerListWindow(bl).Show();
        }

        private void ViewStationListWindow(object sender, RoutedEventArgs e)
        {
            new StationListWindow(bl).Show();
        }
    }
}
