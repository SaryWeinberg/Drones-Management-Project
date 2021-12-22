using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    partial class BL : BLApi.IBL
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
            DO.Customer customer = new DO.Customer();
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
            if (dalObj.GetCustomers().Any(c => c.ID == id))
            {
                throw new ObjectAlreadyExistException("Customer", id);
            }
            BO.Customer customer = new BO.Customer();
            try
            {
                customer.ID = id;
                customer.PhoneNum = phone;
                customer.Name = name;
                customer.Location = location;
            }
            catch (InvalidObjException e) { throw e; }
            AddCustomerDal(id, phone, name, location);
            return "Customer added successfully!";
        }

        /// <summary>
        /// Function for update the customer data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phoneNum"></param>
        public string UpdateCustomerData(int id, string name = null, string phoneNum = null)
        {
            DO.Customer customer = dalObj.GetSpesificCustomer(id);
            if (name != null && name != "")
                customer.Name = name;
            if (phoneNum != null && phoneNum != "")
                customer.PhoneNum = int.Parse(phoneNum);

            dalObj.UpdateCustomer(customer);
            return "The update was successful!";
        }

        /// <summary>
        /// Convert from BL customer to DAL customer
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public DO.Customer ConvertBLCustomerToDAL(BO.Customer c)
        {
            return new DO.Customer
            {
                ID = c.ID,
                Latitude = c.Location.Latitude,
                Longitude = c.Location.Longitude,
                Name = c.Name,
                PhoneNum = c.PhoneNum
            };
        }

        /// <summary>
        /// Convert from DAL customer to BL customer
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public BO.Customer ConvertDalCustomerToBL(DO.Customer c)
        {
            return new BO.Customer
            {
                ID = c.ID,
                Name = c.Name,
                PhoneNum = c.PhoneNum,
                Location = new Location { Latitude = c.Latitude, Longitude = c.Longitude },
                DeliveryFromCustomer = dalObj.GetParcelByCondition(parcel => parcel.SenderId == c.ID),
                DeliveryToCustomer = dalObj.GetParcelByCondition(parcel => parcel.SenderId == c.ID)
            };
        }

        /// <summary>
        /// Returning a specific customer by ID number
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public BO.Customer GetSpesificCustomerBL(int customerId)
        {
            try
            {
                return ConvertDalCustomerToBL(dalObj.GetSpesificCustomer(customerId));
            }
            catch (ObjectDoesNotExist e)
            {
                throw new ObjectNotExistException(e.Message);
            }
        }

        /// <summary>
        /// Returning the customer list
        /// </summary>
        /// <returns></returns>
        public List<BO.Customer> GetCustomersBL()
        {
            List<DO.Customer> customersDal = dalObj.GetCustomers();
            List<BO.Customer> customersBL = new List<BO.Customer>();
            customersDal.ForEach(c => customersBL.Add(ConvertDalCustomerToBL(c)));
            return customersBL;
        }

        /// <summary>
        /// Returns the customer list with CustomerToList
        /// </summary>
        /// <returns></returns>
        public List<BO.CustomerToList> GetCustomersListBL()
        {
            List<BO.Customer> customers = GetCustomersBL();
            List<BO.CustomerToList> customersToList = new List<BO.CustomerToList>();
            foreach (BO.Customer custtomer in customers)
            {
                customersToList.Add(new BO.CustomerToList(custtomer, dalObj));
            }
            return customersToList;
        }
    }
}

