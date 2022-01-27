using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
using System.Runtime.CompilerServices;
namespace Dal
{
    public partial class DalXml : IDal
    {
        /// <summary>
        /// Adding new customer to Database
        /// </summary>
        /// <param name="customer"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer customer)
        {
            List<Customer> customerList = GetCustomers().ToList();
            customerList.Add(customer);
            XmlTools.SaveListToXmlSerializer(customerList, direction + customerFilePath);
        }

        /// <summary>
        /// Update customer in DataBase
        /// </summary>
        /// <param name="customer"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(Customer customer)
        {
            List<Customer> customerList = GetCustomers().ToList();
            customerList[customerList.FindIndex(c => c.ID == customer.ID)] = customer;
            XmlTools.SaveListToXmlSerializer(customerList, direction + customerFilePath);
        }

        /// <summary>
        /// Returns a specific customer by ID number
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> condition = null)
        {
            condition ??= (c => true);
            return from customer in XmlTools.LoadListFromXmlSerializer<Customer>(direction + customerFilePath)
                   where condition(customer)
                   select customer;
        }     
    }
}