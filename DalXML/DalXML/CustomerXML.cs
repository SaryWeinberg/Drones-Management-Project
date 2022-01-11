using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace DAL
{
    public partial class DalXml : IDal
    {
        public void AddCustomer(Customer customer)
        {
            IEnumerable<Customer> customerList = GetCustomers();
            customerList.ToList().Add(customer);
            XMLTools.SaveListToXMLSerializer(customerList, dir + customerFilePath);
        }
        public Customer GetSpesificCustomer(int customerId)
        {
            try { return GetCustomers(c => c.ID == customerId).First(); }
            catch (Exception) { throw new ObjectDoesNotExist("Customer", customerId); }
        }
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> condition = null)
        {
            return from customer in XMLTools.LoadListFromXMLSerializer<Customer>(dir + customerFilePath)
                   where condition(customer)
                   select customer;
        }
        public void UpdateCustomer(Customer customer)
        {
            List<Customer> customerList = GetCustomers().ToList();
            customerList[customerList.FindIndex(c => c.ID == customer.ID)] = customer;
            XMLTools.SaveListToXMLSerializer(customerList, dir + customerFilePath);
        }
    }
}
