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
            try
            {
                MessageBoxResult result =
                    MessageBox.Show(
                    bl.AddCustomerBL(GetID(), GetPhone(), GetName(), new BO.Location { Longitude = GetLongitude(), Latitude = GetLatitude() }),
                    $"Add customer ID - {GetID()}",
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
            ParcelDeliveryToCustomerLabel.Visibility = Visibility.Visible;
            ParcelDeliveryToCustomerList.Visibility = Visibility.Visible;
            ParcelDeliveryFromCustomerLabel.Visibility = Visibility.Visible;
            ParcelDeliveryFromCustomerList.Visibility = Visibility.Visible;
            ParcelDeliveryToCustomerList.ItemsSource = customer.DeliveryToCustomer;
            ParcelDeliveryFromCustomerList.ItemsSource = customer.DeliveryFromCustomer;

            Customer = customer;
            CustomerID.Text = customer.ID.ToString();
            CustomerName.Text = customer.Name.ToString();
            CustomerPhone.Text = customer.PhoneNum.ToString();
            CustomerLongitude.Text = customer.location.Longitude.ToString();


            CustomerList.ItemsSource = customer.DeliveryToCustomer;
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
            try
            {
                MessageBoxResult result =
                    MessageBox.Show(
                    bl.UpdateCustomerData(GetID(), GetName()),
                    $"Update customer ID - {GetID()}",
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

        //===========Get Inputs===========

        private int GetID()
        {
            try { return int.Parse(CustomerID.Text); }
            catch (Exception) { throw new InvalidObjException("ID"); }
        }
        private int GetPhone()
        {
            try { return int.Parse(CustomerPhone.Text); }
            catch (Exception) { throw new InvalidObjException("Phone"); }
        }
        private string GetName()
        {
            return CustomerName.Text != "" ? CustomerName.Text : throw new InvalidObjException("Name");
        }
        private int GetLongitude()
        {
            try { return int.Parse(CustomerLongitude.Text); }
            catch (Exception) { throw new InvalidObjException("Longitude"); }
        }
        private int GetLatitude()
        {
            try { return int.Parse(CustomerLatitude.Text); }
            catch (Exception) { throw new InvalidObjException("Latitude"); }
        }
    }
}
