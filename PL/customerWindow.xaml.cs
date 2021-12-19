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
    /// Interaction logic for customerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        IBL.IBL bl;
        IBL.BO.CustomerBL Customer;
       
        public CustomerWindow(IBL.IBL blMain)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            sendNewCustomer.Visibility = Visibility.Visible;          
        }

        private void AddNewCustomer(object sender, RoutedEventArgs e)
        {           
            string ID = CustomerID.Text;
            string name = CustomerName.Text;
            string phone = CustomerPhone.Text;
            string longitude = CustomerLongitude.Text;
            string latitude = CustomerLatitude.Text;
            try
            {
                MessageBoxResult result =
                    MessageBox.Show(
                    bl.AddCustomerBL(int.Parse(ID), int.Parse(phone), name, new IBL.BO.Location { Longitude = int.Parse(longitude), Latitude = int.Parse(latitude) }),
                    $"Add customer ID - {ID}",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    //new CustomerListWindow(bl).Show();
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }


        public CustomerWindow(IBL.IBL blMain, IBL.BO.CustomerBL customer)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;

            Customer = customer;
            CustomerID.Text = customer.ID.ToString();
            CustomerName.Text = customer.Name.ToString();
            CustomerPhone.Text = customer.PhoneNum.ToString();
            CustomerLongitude.Text = customer.Location.Longitude.ToString();
            CustomerLatitude.Text = customer.Location.Latitude.ToString();
            CustomerID.IsEnabled = false;
            CustomerLongitude.IsEnabled = false;
            CustomerLatitude.IsEnabled = false;

            CustomerName.TextChanged += AddUpdateButton;
            CustomerPhone.TextChanged += AddUpdateButton;        
        }

        private void ClosingWindow(object sender, RoutedEventArgs e) => Close();
        private void AddUpdateButton(object sender, RoutedEventArgs e) => updateCustomer.Visibility = Visibility.Visible;
        private void UpdateCustomer(object sender, RoutedEventArgs e)
        {
            string ID = CustomerID.Text;
            string name = CustomerName.Text;
            try
            {
                MessageBoxResult result =
                    MessageBox.Show(
                    bl.UpdateCustomerData(int.Parse(ID), name),
                    $"Update customer ID - {ID}",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    //new CustomerListWindow(bl).Show();
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }       
    }
}
