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
    /// Interaction logic for EntranceWindow.xaml
    /// </summary>
    /// 
    public partial class EntranceWindow : Window
    {
        BLApi.IBL bl = BL.BLFactory.GetBL();


        public EntranceWindow()
        {
            InitializeComponent();
        }

        #region Switch between TextBoxes
        /// <summary>
        /// Moves from TextBox CustomerID to TextBox CustomerPhone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownID(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Phone.Focus();
        }
        /// <summary>
        /// Moves from TextBox CustomerPhone to TextBox CustomerName
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownPhone(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Name.Focus();
        }
        /// <summary>
        /// Moves from TextBox CustomerName to TextBox CustomerLongitude
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownName(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Longitude.Focus();
        }
        /// <summary>
        /// Moves from TextBox CustomerLongitude to TextBox CustomerLatitude
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownLongitude(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Latitude.Focus();
        }
        /// <summary>
        /// Moves from TextBox CustomerLatitude to Button sendNewCustomer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownLatitude(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) sendNewCustomer.Focus();
        }
        /// <summary>
        /// Moves from TextBox CustomerID to TextBox CustomerName
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownCustomerID(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) CustomerName.Focus();
        }
        /// <summary>
        /// Moves from TextBox CustomerName to Button sendCustomer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownCustomerName(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) sendCustomer.Focus();
        }
        /// <summary>
        /// Moves from TextBox ManagerID to TextBox ManagerName
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownManagerID(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) ManagerName.Focus();
        }
        /// <summary>
        /// Moves from TextBox ManagerName to Button sendManager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownManagerName(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) sendManager.Focus();
        }
        #endregion

        #region Get Inputs

        /// <summary>
        /// Takes ID input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int IDInput()
        {
            try { return int.Parse(ID.Text); }
            catch (Exception) { throw new InvalidObjException("ID"); }
        }
        /// <summary>
        /// Takes phone number input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int PhoneInput()
        {
            try { return int.Parse(Phone.Text); }
            catch (Exception) { throw new InvalidObjException("Phone"); }
        }
        /// <summary>
        /// Takes name input from the user with tests
        /// </summary>
        /// <returns></returns>
        private string NameInput()
        {
            return Name.Text != "" ? Name.Text : throw new InvalidObjException("Name");
        }
        /// <summary>
        /// Takes longitude input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int LongitudeInput()
        {
            try { return int.Parse(Longitude.Text); }
            catch (Exception) { throw new InvalidObjException("Longitude"); }
        }
        /// <summary>
        /// Takes latitude input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int LatitudeInput()
        {
            try { return int.Parse(Latitude.Text); }
            catch (Exception) { throw new InvalidObjException("Latitude"); }
        }
        /// <summary>
        /// Takes sign ID input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int SignIDInput()
        {
            try { return int.Parse(CustomerID.Text); }
            catch (Exception) { throw new InvalidObjException("Phone"); }
        }
        /// <summary>
        /// Takes sign name input from the user with tests
        /// </summary>
        /// <returns></returns>
        private string SignNameInput()
        {
            return CustomerName.Text != "" ? CustomerName.Text : throw new InvalidObjException("Name");
        }
        /// <summary>
        /// Takes manager ID input from the user with tests
        /// </summary>
        /// <returns></returns>
        private int ManagerIDInput()
        {
            try { return int.Parse(ManagerID.Text); }
            catch (Exception) { throw new InvalidObjException("Phone"); }
        }
        /// <summary>
        /// Takes manager name input from the user with tests
        /// </summary>
        /// <returns></returns>
        private string ManagerNameInput()
        {
            return ManagerName.Text != "" ? ManagerName.Text : throw new InvalidObjException("Name");
        }
        #endregion

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
                    CustomerWindow open = new CustomerWindow(bl, bl.GetSpesificCustomer(IDInput()));

                    open.returnWindow.Visibility = Visibility.Hidden;
                    open.Show();
              
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }


        private void SignIn(object sender, RoutedEventArgs e)
        {
            BO.Customer customer = bl.GetCustomers().ToList().Find(c => c.ID == SignIDInput() && c.Name == SignNameInput());
            if (customer != null)
            {
                CustomerWindow open = new CustomerWindow(bl, customer);
            
                open.returnWindow.Visibility = Visibility.Hidden;
                open.Show();

                Close();
            }
            else
            {
                MessageBoxResult result =
                    MessageBox.Show("The user does not exist in the system",
                    "Error!",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                if (result == MessageBoxResult.OK)
                {
                    tabControl.SelectedItem = SignUp;
                }
            }
        }

        private void Manager(object sender, RoutedEventArgs e)
        {
            if ((ManagerNameInput() == "Gitty" && ManagerIDInput() == 212542385) || (ManagerNameInput() == "Sary" && ManagerIDInput() == 212381743) || (ManagerNameInput() == "1" && ManagerIDInput() == 1))
            {
                new MainWindow(bl).Show();
                Close();
            }
        }
    }
}
