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
    /// Interaction logic for CustomerListWindow.xaml
    /// </summary>
    public partial class CustomerListWindow : Window
    {        
        BLApi.IBL bl;

        public CustomerListWindow(BLApi.IBL blMain)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            CustomerListView.ItemsSource = bl.GetCustomersToList();
        }

        private void DataWindowClosing(object sender, RoutedEventArgs e) => Close();

        private void RefreshWindow(object sender, RoutedEventArgs e) => CustomerListView.Items.Refresh();

        private void AddCustomer(object sender, RoutedEventArgs e) => new CustomerWindow(bl).Show();

        private void UpdateCustomer(object sender, MouseButtonEventArgs e)
        {
            BO.CustomerToList customerToList = (sender as ListView).SelectedValue as BO.CustomerToList;
            new CustomerWindow(bl, bl.GetSpesificCustomer(customerToList.ID)).Show();
        }

        private void ReturnWindow(object sender, RoutedEventArgs e)
        {
            new MainWindow(bl).Show();
            Close();
        }
    }
}
