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
            IDAL.DO.Customer customer = new IDAL.DO.Customer();
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
        public void AddCustomer(int id, int phone, string name, Location location)
        {
            IBL.BO.Customer customer = new IBL.BO.Customer();
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
            IDAL.DO.Customer customer = dalObj.GetSpesificCustomer(id);
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
        public IBL.BO.Customer ConvertDalCustomerToBL(IDAL.DO.Customer c)
        {
            return new IBL.BO.Customer
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
        public IBL.BO.Customer GetSpesificCustomerBL(int customerId)
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
        public List<IBL.BO.Customer> GetCustomers()
        {
            List<IDAL.DO.Customer> customersDal = dalObj.GetCustomers();
            List<IBL.BO.Customer> customersBL = new List<IBL.BO.Customer>();
            customersDal.ForEach(c => customersBL.Add(ConvertDalCustomerToBL(c)));
            return customersBL;
        }
    }
}
