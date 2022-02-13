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

        /// <summary>
        /// Ctor of CustomerListWindow
        /// </summary>
        /// <param name="blMain"></param>
        public CustomerListWindow(BLApi.IBL blMain)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            CustomerListView.ItemsSource = bl.GetCustomersToList();
        }

        /// <summary>
        /// Close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataWindowClosing(object sender, RoutedEventArgs e) => Close();

        /// <summary>
        /// Sending to the window of adding a new customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCustomer(object sender, RoutedEventArgs e)
        {
            new CustomerWindow(bl).Show();
        }

        /// <summary>
        /// Sending to the window of update customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateCustomer(object sender, MouseButtonEventArgs e)
        {
            BO.CustomerToList customerToList = (sender as ListView).SelectedValue as BO.CustomerToList;
            new CustomerWindow(bl, bl.GetSpesificCustomer(customerToList.ID)).Show();
        }

        /// <summary>
        /// Back to previous window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnWindow(object sender, RoutedEventArgs e)
        {
            new MainWindow(bl).Show();
            Close();
        }
    }
}
