using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace PL
{
    /// <summary>
    /// Interaction logic for customerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        BLApi.IBL bl;
        BO.Customer Customer;

        public CustomerWindow(BLApi.IBL blMain)
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
                    bl.AddCustomerBL(int.Parse(ID), int.Parse(phone), name, new BO.Location { Longitude = int.Parse(longitude), Latitude = int.Parse(latitude) }),
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


        public CustomerWindow(BLApi.IBL blMain, BO.Customer customer)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;

            Customer = customer;
            CustomerID.Text = customer.ID.ToString();
            CustomerName.Text = customer.Name.ToString();
            CustomerPhone.Text = customer.PhoneNum.ToString();
            CustomerLongitude.Text = customer.location.Longitude.ToString();
            CustomerLatitude.Text = customer.location.Latitude.ToString();
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
