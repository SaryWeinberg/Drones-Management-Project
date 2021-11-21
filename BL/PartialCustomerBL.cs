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
            customer.ID = id;
            customer.PhoneNum = phone;
            customer.Name = name;
            customer.Longitude = location.Longitude;
            customer.Latitude = location.Latitude;
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
        public string AddCustomerBL(int id, int phone, string name, Location location)
        {
            CustomerBL customer = new CustomerBL();
            try
            {
                customer.ID = id;
                customer.PhoneNum = phone;
                customer.Name = name;
                customer.Location= location;

            }
            catch (InvalidID e)
            {
                throw e;
            }catch (InvalidName e)
            {
                throw e;
            }
            catch (InvalidPhoneNumber e)
            {
                throw e;
            }
            AddCustomerDal(id, phone, name, location);
            return "Customer added successfully!";
        }

        /// <summary>
        /// Function for update the customer data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phoneNum"></param>
        public string UpdateCustomerData(int id, string name = null, int phoneNum = 0)
        {
            Customer customer = dalObj.GetSpesificCustomer(id);
            if (name != null && name != "")
                customer.Name = name;
            if (phoneNum != 0 && phoneNum != null)
                customer.PhoneNum = phoneNum;

            dalObj.UpdateCustomer(customer);
            return "The update was successful!";
        }

        /// <summary>
        /// Convert from BL customer to DAL customer
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public Customer ConvertBLCustomerToDAL(CustomerBL c)
        {
            return new Customer
            {
                ID = c.ID, 
                Latitude = c.Location.Latitude, 
                Longitude = c.Location.Longitude, 
                Name= c.Name, 
                PhoneNum= c.PhoneNum                
            };
        }

        /// <summary>
        /// Convert from DAL customer to BL customer
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public CustomerBL ConvertDalCustomerToBL(Customer c)
        {
            return new CustomerBL
            {
                ID = c.ID,
                Name = c.Name,
                PhoneNum = c.PhoneNum,
                Location = new Location { Latitude = c.Latitude, Longitude = c.Longitude }
            };
        }

        /// <summary>
        /// Returning a specific customer by ID number
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public CustomerBL GetSpesificCustomerBL(int customerId)
        {


            /*    try
                {*/
            return ConvertDalCustomerToBL(dalObj.GetSpesificCustomer(customerId));
            /* }*/
            /*            catch (ObjectDoesNotExist e)
                        {
                            throw new ObjectNotExist(e.Message);
                        }*/
        }

        /// <summary>
        /// Returning the customer list
        /// </summary>
        /// <returns></returns>
        public List<CustomerBL> GetCustomersBL()
        {
            List<Customer> customersDal = dalObj.GetCustomers();
            List<CustomerBL> customersBL = new List<CustomerBL>();
            customersDal.ForEach(c => customersBL.Add(ConvertDalCustomerToBL(c)));
            return customersBL;
        }
    }
}
