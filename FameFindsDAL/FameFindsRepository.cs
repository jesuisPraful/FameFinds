using FameFindsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace FameFindsDAL
{
    public class FameFindsRepository
    {
        private readonly FameFindsContext context;
        public FameFindsRepository(FameFindsContext Famecontext)
        {
            context = Famecontext;
        }

        #region CUSTOMER
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                customers = context.Customers.ToList();
            }
            catch (Exception ex)
            {
                customers = null;
            }
            return customers;
        }
        public bool AddCustomer(Customer customer)
        {
            bool status = false;
            try
            {
                context.Customers.Add(customer);
                context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
        public bool UpdateCustomer(Customer customer)
        {
            bool status = false;
            try
            {
                var customerOne = context.Customers.Find(customer.CustomerId);
                if (customerOne == null)
                {
                    status =  false;
                }
                else
                {
                    customerOne.FullName = customer.FullName;
                    customerOne.Email = customer.Email;
                    customerOne.PhoneNumber = customer.PhoneNumber;
                    context.SaveChanges();
                    status = true;
                }      
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        #region category

        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            try
            {
                categories = context.Categories.ToList();
            }
            catch (Exception ex)
            {
                categories = null;
            }
            return categories;
        }
        #endregion

        #region Products

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                products = context.Products.ToList();
            }
            catch (Exception ex)
            {
                products = null;
            }
            return products;
        }




        #endregion
    }
}
