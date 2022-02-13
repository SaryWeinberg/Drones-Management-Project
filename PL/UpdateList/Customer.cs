using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public class Customer : DependencyObject
    {
        public ObjectChanged<BO.Customer> CustomerListChanged;
        public Customer(BO.Customer customer)
        {
            ID = customer.ID;
            PhoneNum = customer.PhoneNum;
            Name = customer.Name;
            Location = customer.Location;
            DeliveryToCustomer = customer.DeliveryToCustomer;
            DeliveryFromCustomer = customer.DeliveryFromCustomer;
        }

        public int ID { get { return (int)GetValue(IDProperty); } set { SetValue(IDProperty, value); } }
        public string Name { get { return (string)GetValue(NameProperty); } set { SetValue(NameProperty, value); } }
        public BO.Location Location { get { return (BO.Location)GetValue(LocationProperty); } set { SetValue(LocationProperty, value); } }
        public int PhoneNum { get { return (int)GetValue(PhoneNumProperty); } set { SetValue(PhoneNumProperty, value); } }
        public List<BO.ParcelsAtTheCustomer> DeliveryToCustomer { get { return (List<BO.ParcelsAtTheCustomer>)GetValue(DeliveryToCustomerProperty); } set { SetValue(DeliveryToCustomerProperty, value); } }
        public List<BO.ParcelsAtTheCustomer> DeliveryFromCustomer { get { return (List<BO.ParcelsAtTheCustomer>)GetValue(DeliveryFromCustomerProperty); } set { SetValue(DeliveryFromCustomerProperty, value); } }

        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(int), typeof(Customer), new UIPropertyMetadata());
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(Customer), new UIPropertyMetadata());
        public static readonly DependencyProperty LocationProperty =
            DependencyProperty.Register("Location", typeof(BO.Location), typeof(Customer), new UIPropertyMetadata());
        public static readonly DependencyProperty PhoneNumProperty =
            DependencyProperty.Register("PhoneNum", typeof(int), typeof(Customer), new UIPropertyMetadata());
        public static readonly DependencyProperty DeliveryToCustomerProperty =
            DependencyProperty.Register("DeliveryToCustomer", typeof(List<BO.ParcelsAtTheCustomer>), typeof(Customer), new UIPropertyMetadata());
        public static readonly DependencyProperty DeliveryFromCustomerProperty =
            DependencyProperty.Register("DeliveryFromCustomer", typeof(List<BO.ParcelsAtTheCustomer>), typeof(Customer), new UIPropertyMetadata());
        public void UpdateCustomer(BO.Customer customer)
        {
            ID = customer.ID;
            PhoneNum = customer.PhoneNum;
            Name = customer.Name;
            Location = customer.Location;
            DeliveryToCustomer = customer.DeliveryToCustomer;
            DeliveryFromCustomer = customer.DeliveryFromCustomer;

            if (CustomerListChanged != null)
                CustomerListChanged(customer);
        }
    }
}
