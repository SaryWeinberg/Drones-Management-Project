using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace BL
{
    partial class BL : IBL.IBL
    {
        /// <summary>
        /// Functions for adding a customer to DAL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="phone"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        public void AddCustomerDal(int id, int phone, string name, Location location)
        {
            Customer customer = new Customer();
            customer.id = id;
            customer.phoneNum = phone;
            customer.name = name;
            customer.longitude = location.longitude;
            customer.latitude = location.latitude;
            dalObj.AddCustomer(customer);
        }

        /// <summary>
        /// Functions for adding a customer to BL,
        /// If no exception are thrown the function will call the function: AddCustomerDal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="phone"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        public void AddCustomerBL(int id, int phone, string name, Location location)
        {
            CustomerBL customer = new CustomerBL();
            try
            {
                customer.id = id;
                customer.phoneNum = phone;
                customer.name = name;
                customer.location.longitude = location.longitude;
                customer.location.latitude = location.latitude;
            }
            catch (InvalidID e)
            {
                throw e;
            }
            catch (InvalidName e)
            {
                throw e;
            }
            catch (InvalidPhoneNumber e)
            {
                throw e;
            }
            AddCustomerDal(id, phone, name, location);
        }

        /// <summary>
        /// Function for update the customer data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phoneNum"></param>
        public void UpdateCustomerData(int id, string name = null, int phoneNum = 0)
        {
            Customer customer = dalObj.GetSpesificCustomer(id);
            if (name != null)
                customer.name = name;
            if (phoneNum != 0)
                customer.phoneNum = phoneNum;
        }

        /// <summary>
        /// Convert from dal customer to BL customer
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public CustomerBL ConvertDalCustomerToBL(Customer c)
        {
            return new CustomerBL
            {
                id = c.id,
                name = c.name,
                phoneNum = c.phoneNum,
                location = new Location { latitude = c.latitude, longitude = c.longitude }
            };
        }

        /// <summary>
        /// Returning a specific customer by ID number
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public CustomerBL GetSpesificCustomerBL(int customerId)
        {
            try
            {
                return ConvertDalCustomerToBL(dalObj.GetSpesificCustomer(customerId));
            }
            catch (ObjectDoesNotExist e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Returning the customer list
        /// </summary>
        /// <returns></returns>
        public List<CustomerBL> GetCustomers()
        {
            List<Customer> customersDal = dalObj.GetCustomers();
            List<CustomerBL> customersBL = new List<CustomerBL>();
            customersDal.ForEach(c => customersBL.Add(ConvertDalCustomerToBL(c)));
            return customersBL;
        }
    }
}
