using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;


namespace DalObject
{
    public partial class DalObject : IDal
    {
        /// <summary>
        /// Adding new customer to Database
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            DataSource.Customers.Add(customer.Clone());
        }

        /// <summary>
        /// Update customer in DataBase
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateCustomer(Customer customer)
        {
            int index = DataSource.Customers.FindIndex(d => d.id == customer.id);
            DataSource.Customers[index] = customer.Clone();
        }

        /// <summary>
        /// Returns a specific customer by ID number
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Customer GetSpesificCustomer(int customerId)
        {
            try
            {
                return DataSource.Customers.First(customer => customer.id == customerId);
            }
            catch
            {
                throw new ObjectDoesNotExist("Customer", customerId);
            }
        }

        /// <summary>
        /// Returns the list of customers one by one
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomerLists()
        {
            foreach (Customer customer in DataSource.Customers)
            {
                yield return customer;
            }
        }

        /// <summary>
        /// Returns the customer list
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomers()
        {
            return DataSource.Customers;
        }
    }
}
