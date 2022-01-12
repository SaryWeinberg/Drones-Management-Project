﻿using DO;
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
            customer.Active = true;
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
            if(dalObj.GetCustomers().Any(c => c.ID == id))
                throw new ObjectAlreadyExistException("Customer", id);           

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
        public string UpdateCustomerData(int id, string name = null, int phoneNum = -1)
        {
            DO.Customer customer = dalObj.GetSpesificCustomer(id);

            if (name != null && name != "") customer.Name = name;
            if (phoneNum != null && phoneNum != -1) customer.PhoneNum = phoneNum;

            dalObj.UpdateCustomer(customer);

            return "The update was successful!";
        }

        /// <summary>
        /// Convert from BL customer to DAL customer
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
       /* public DO.Customer ConvertBLCustomerToDAL(BO.Customer c)
        {
            return new DO.Customer
            {
                ID = c.ID,                
                Name = c.Name,
                PhoneNum = c.PhoneNum,
                Latitude = c.location.Latitude,
                Longitude = c.location.Longitude
            };
        }*/

        /// <summary>
        /// Convert from DAL customer to BL customer
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public BO.Customer ConvertDalCustomerToBL(DO.Customer c)
        {
            /* List<ParcelsAtTheCustomer> DeliveryFromCe = (from parcel in GetParcelsBL()
                                                       where parcel.Sender.ID == c.ID
                                                       select new ParcelsAtTheCustomer(parcel)).ToList<ParcelsAtTheCustomer>;*/

            List<ParcelsAtTheCustomer> DeliveryFromC = new List<ParcelsAtTheCustomer>();         
            List<ParcelsAtTheCustomer> DeliveryToC = new List<ParcelsAtTheCustomer>();

            foreach (BO.Parcel parcel in GetParcels())
            {
                if (parcel.Sender.ID == c.ID)
                {
                    DeliveryFromC.Add(new ParcelsAtTheCustomer(parcel));
                }
                else if (parcel.Target.ID == c.ID)
                {
                    DeliveryToC.Add(new ParcelsAtTheCustomer(parcel));
                }
            }


            return new BO.Customer
            {
                ID = c.ID,
                Name = c.Name,
                PhoneNum = c.PhoneNum,
                DeliveryToCustomer = DeliveryToC,
                DeliveryFromCustomer = DeliveryFromC,
                Location = new Location { Latitude = c.Latitude, Longitude = c.Longitude }
            };
        }

        /// <summary>
        /// Returning a specific customer by ID number
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public BO.Customer GetSpesificCustomer(int customerId)
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

        /*public IEnumerable<BO.Customer> GeCustomerByCondition(Predicate<BO.Customer> condition)
        {
            return from customer in GetCustomers()
                   where condition(customer)
                   select customer;
        } */

        /// <summary>
        /// Returning the customer list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.Customer> GetCustomers(Predicate<BO.Customer> condition = null)
        {
            List<DO.Customer> customersDal = dalObj.GetCustomers().ToList();
            List<BO.Customer> customersBL = new List<BO.Customer>();
            customersDal.ForEach(c => customersBL.Add(ConvertDalCustomerToBL(c)));
            /*condition ??= (c => true);
            return from c in dalObj.GetCustomers()
                   where condition(ConvertDalCustomerToBL(c))
                   select ConvertDalCustomerToBL(c);*/
            return customersBL;
        }

        /// <summary>
        /// Returns the customer list with CustomerToList
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.CustomerToList> GetCustomersToList(Predicate<BO.Customer> condition = null)
        {
            /* List<BO.Customer> customers = GetCustomers().ToList();
             List<BO.CustomerToList> customersToList = new List<BO.CustomerToList>();
             foreach (BO.Customer custtomer in customers)
             {
                 customersToList.Add(new BO.CustomerToList(custtomer, dalObj));
             }
             return customersToList;
             condition ??= (c => true);*/
            condition ??= (c => true);
            return from c in GetCustomers()
                   where condition(c)
                   select new CustomerToList(c, dalObj);
        }
    }
}

