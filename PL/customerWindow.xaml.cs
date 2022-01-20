using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for customerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        BLApi.IBL bl;
        Customer Customer;

        public CustomerWindow(BLApi.IBL blMain)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            CustomerID.Focus();
  
            sendNewCustomer.Visibility = Visibility.Visible;          
        }

        private void AddNewCustomer(object sender, RoutedEventArgs e)
        {   
            try
            {
                MessageBoxResult result =
                    MessageBox.Show(
                    bl.AddCustomerBL(IDInput(), PhoneInput(), NameInput(), new BO.Location { Longitude = LongitudeInput(), Latitude = LatitudeInput() }),
                    $"Add customer ID - {IDInput()}",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
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


            Customer = new Customer(customer);
            AddCustomer.DataContext = Customer;

            ParcelDeliveryToCustomerLabel.Visibility = Visibility.Visible;

            ParcelDeliveryToCustomerList.ItemsSource = customer.DeliveryToCustomer;
            ParcelDeliveryFromCustomerList.ItemsSource = customer.DeliveryFromCustomer;



            CustomerName.Text = customer.Name.ToString();
            CustomerPhone.Text = customer.PhoneNum.ToString();

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
                    bl.UpdateCustomerData(IDInput(), NameInput(), CustomerPhone.Text),
                    $"Update customer ID - {IDInput()}",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    Customer.UpdateCustomer(bl.GetSpesificCustomer(Customer.ID));
                    /*Close();*/
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }


        private void GetParcel(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            BO.ParcelsAtTheCustomer parcelsAtTheCustomer = (sender as ListView).SelectedValue as BO.ParcelsAtTheCustomer;
            new ParcelWindow(bl, bl.GetSpesificParcelBL(parcelsAtTheCustomer.ID)).Show();
        }


        private void RefreshWindow(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnWindow(object sender, RoutedEventArgs e)
        {
            new CustomerListWindow(bl).Show();
            Close();
        }

        #region Get Inputs

        /// <summary>
        /// Takes ID input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int IDInput()
        {
            try { return int.Parse(CustomerID.Text); }
            catch (Exception) { throw new InvalidObjException("ID"); }
        }
        /// <summary>
        /// Takes phone number input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int PhoneInput()
        {
            try { return int.Parse(CustomerPhone.Text); }
            catch (Exception) { throw new InvalidObjException("Phone"); }
        }
        /// <summary>
        /// Takes name input from the user with tests
        /// </summary>
        /// <returns></returns>
        private string NameInput()
        {
            return CustomerName.Text != "" ? CustomerName.Text : throw new InvalidObjException("Name");
        }
        /// <summary>
        /// Takes longitude input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int LongitudeInput()
        {
            try { return int.Parse(CustomerLongitude.Text); }
            catch (Exception) { throw new InvalidObjException("Longitude"); }
        }
        /// <summary>
        /// Takes latitude input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int LatitudeInput()
        {
            try { return int.Parse(CustomerLatitude.Text); }
            catch (Exception) { throw new InvalidObjException("Latitude"); }
        }

        #endregion

        #region Switch between TextBoxes
        /// <summary>
        /// Moves from TextBox CustomerID to TextBox CustomerPhone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownCustomerID(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) CustomerPhone.Focus();
        }
        /// <summary>
        /// Moves from TextBox CustomerPhone to TextBox CustomerName
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownCustomerPhone(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) CustomerName.Focus();
        }
        /// <summary>
        /// Moves from TextBox CustomerName to TextBox CustomerLongitude
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownCustomerName(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) CustomerLongitude.Focus();
        }
        /// <summary>
        /// Moves from TextBox CustomerLongitude to TextBox CustomerLatitude
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownCustomerLongitude(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) CustomerLatitude.Focus();
        }
        /// <summary>
        /// Moves from TextBox CustomerLatitude to Button sendNewCustomer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownCustomerLatitude(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) sendNewCustomer.Focus();
        }
        #endregion
    }
}
