using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using System.Runtime.CompilerServices;
/*using IDAL;*/


namespace Dal
{
    internal partial class DalObject : IDal
    {
        /// <summary>
        /// Adding new customer to Database
        /// </summary>
        /// <param name="customer"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer customer)
        {
            DataSource.Customers.Add(customer);
        }

        /// <summary>
        /// Update customer in DataBase
        /// </summary>
        /// <param name="customer"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(Customer customer)
        {
            int index = DataSource.Customers.FindIndex(d => d.ID == customer.ID);
            DataSource.Customers[index] = customer;
        }

        /// <summary>
        /// Returns a specific customer by ID number
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        
        public Customer GetSpesificCustomer(int customerId)
        {
            try
            {
                return GetCustomers(customer => customer.ID == customerId).First();
            }
            catch
            {
                throw new ObjectDoesNotExist("Customer", customerId);
            }
        }

        /// <summary>
        /// Returns the customer list
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> condition = null)
        {
            condition ??= (c => true);
            return from customer in DataSource.Customers
                   where condition(customer)
                   select customer;
        }      
    }
}
