﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
/*using IDAL;*/


namespace DalObject
{
    partial class DalObject : IDal
    {
        /// <summary>
        /// Adding new customer to Database
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            DataSource.Customers.Add(customer);
        }

        /// <summary>
        /// Update customer in DataBase
        /// </summary>
        /// <param name="customer"></param>
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
        public Customer GetSpesificCustomer(int customerId)
        {
            try
            {
                return DataSource.Customers.First(customer => customer.ID == customerId);
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
            return from customer in DataSource.Customers
                   select customer;           
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
