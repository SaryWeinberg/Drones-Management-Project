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
        string[] dataArr = { "ID", "phone", "name", "longitude", "latitude" };
        TextBox customerItem;

        public CustomerWindow(IBL.IBL blMain)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            int x = 70;
            foreach (string item in dataArr)
            {
                Label customerItemT = new Label();
                customerItemT.Content = $"Enter customer {item}:";
                customerItemT.VerticalAlignment = VerticalAlignment.Top;
                customerItemT.Margin = new Thickness(43, x, 0, 0);
                CustomerData.Children.Add(customerItemT);
                customerItem = new TextBox();
                customerItem.Name = $"customer{item}";
                customerItem.VerticalAlignment = VerticalAlignment.Top;
                customerItem.Margin = new Thickness(199, x, 0, 0);
                CustomerData.Children.Add(customerItem);
                x += 30;
            }

            Button sendNewCustomer = new Button();
            sendNewCustomer.Content = "Send";
            sendNewCustomer.VerticalAlignment = VerticalAlignment.Top;
            sendNewCustomer.Margin = new Thickness(43, x, 0, 0);
            sendNewCustomer.Click += AddNewCustomer;
            CustomerData.Children.Add(sendNewCustomer);
        }

        private void AddNewCustomer(object sender, RoutedEventArgs e)
        {
            string ID = CustomerData.Children.OfType<TextBox>().First(txt => txt.Name == "customerID").Text;
            string name = CustomerData.Children.OfType<TextBox>().First(txt => txt.Name == "customername").Text;
            string phone = CustomerData.Children.OfType<TextBox>().First(txt => txt.Name == "customerphone").Text;
            string longitude = CustomerData.Children.OfType<TextBox>().First(txt => txt.Name == "customerlongitude").Text;
            string latitude = CustomerData.Children.OfType<TextBox>().First(txt => txt.Name == "customerlatitude").Text;
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
                    new CustomerListWindow(bl).Show();
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
            int x = 70;
            foreach (string item in dataArr)
            {
                Label customerItemT = new Label();
                customerItemT.Content = $"Upadate customer {item}:";
                customerItemT.VerticalAlignment = VerticalAlignment.Top;
                customerItemT.Margin = new Thickness(43, x, 0, 0);
                CustomerData.Children.Add(customerItemT);
                TextBox customerItem = new TextBox();
                customerItem.Name = $"customer{item}";
                customerItem.VerticalAlignment = VerticalAlignment.Top;
                customerItem.Margin = new Thickness(199, x, 0, 0);
                switch (item)
                {
                    case "ID":
                        customerItem.Text = customer.ID.ToString(); customerItem.IsEnabled = false; break;
                    case "phone":
                        customerItem.Text = customer.PhoneNum.ToString(); break;
                    case "name":
                        customerItem.Text = customer.Name.ToString(); break;
                    case "longitude":
                        customerItem.Text = customer.Location.Longitude.ToString(); customerItem.IsEnabled = false; break;
                    case "latitude":
                        customerItem.Text = customer.Location.Latitude.ToString(); customerItem.IsEnabled = false; break;
                }
                CustomerData.Children.Add(customerItem);
                x += 30;
            }

            Button updateCustomer = new Button();
            updateCustomer.Content = "update";
            updateCustomer.VerticalAlignment = VerticalAlignment.Top;
            updateCustomer.Click += UpdateCustomer;
            updateCustomer.Margin = new Thickness(43, x, 0, 0);
            CustomerData.Children.Add(updateCustomer);
        }

        private void DataWindowClosing(object sender, RoutedEventArgs e) => Close();

        private void UpdateCustomer(object sender, RoutedEventArgs e)
        {
            string ID = CustomerData.Children.OfType<TextBox>().First(txt => txt.Name == "customerID").Text;
            string name = CustomerData.Children.OfType<TextBox>().First(txt => txt.Name == "customername").Text;
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
                    new CustomerListWindow(bl).Show();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }       
    }
}
