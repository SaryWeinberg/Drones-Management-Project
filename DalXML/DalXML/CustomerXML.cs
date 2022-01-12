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
        /// <summary>
        /// Adding new customer to Database
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            IEnumerable<Customer> customerList = GetCustomers();
            customerList.ToList().Add(customer);
            XMLTools.SaveListToXMLSerializer(customerList, dir + customerFilePath);
        }

        /// <summary>
        /// Update customer in DataBase
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateCustomer(Customer customer)
        {
            List<Customer> customerList = GetCustomers().ToList();
            customerList[customerList.FindIndex(c => c.ID == customer.ID)] = customer;
            XMLTools.SaveListToXMLSerializer(customerList, dir + customerFilePath);
        }

        /// <summary>
        /// Returns a specific customer by ID number
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Customer GetSpesificCustomer(int customerId)
        {
            try { return GetCustomers(c => c.ID == customerId).First(); }
            catch (Exception) { throw new ObjectDoesNotExist("Customer", customerId); }
        }

        /// <summary>
        /// Returns the customer list
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> condition = null)
        {
            return from customer in XMLTools.LoadListFromXMLSerializer<Customer>(dir + customerFilePath)
                   where condition(customer)
                   select customer;
        }     
    }
}
