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

        public bool AddVendor(Vendor vendor)
        {
            bool status = false;
            try
            {
                context.Vendors.Add(vendor);
                context.SaveChanges();
                status = true;
            }
            catch (Exception)
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
