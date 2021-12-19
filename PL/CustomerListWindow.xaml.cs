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
            CustomerListView.ItemsSource = bl.GetCustomersBL();
        }

        private void DataWindowClosing(object sender, RoutedEventArgs e) => Close();

        private void ViewCustomerWindow(object sender, RoutedEventArgs e)
        {
            new CustomerWindow(bl).Show();
            Close();
        }

        private void UpdateCustomer(object sender, MouseButtonEventArgs e)
        {
            BO.Customer customer = (sender as ListView).SelectedValue as BO.Customer;
            new CustomerWindow(bl, customer).Show();
            Close();
        }
    }
}
