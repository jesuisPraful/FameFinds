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
                    status = false;
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
        #endregion

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
        public bool AddProduct(Product product)
        {
          var name= ( from p in context.Products
            where p.ProductName == product.ProductName
            select p).FirstOrDefault();
            bool status = false;
            if ((name==null))
            { 
                try
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                    status = true;
                }
                catch (Exception ex)
                {
                    status = false;
                }
                return status;
            }
            else
            {
                Console.WriteLine("Product already exists");
                status = false;
                return status;
            }
        }
        public bool UpdateProduct(Product product)
        {
            bool status = false;
            try
            {
                var existingProduct = context.Products.Find(product.ProductId);
                if (existingProduct != null)
                {
                    existingProduct.ProductName = product.ProductName;
                    existingProduct.Description = product.Description;
                    existingProduct.CategoryId = product.CategoryId;
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
        public bool DeleteProduct(int productId)
        {
            bool status = false;
            try
            {
                var existingProduct = context.Products.Find(productId);
                if (existingProduct != null)
                {
                    context.Products.Remove(existingProduct);
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
        public Product GetProductByName(string ProductName)
        {
            Product product = new Product();
            try
            {
                product = (from p in context.Products
                          where p.ProductName == ProductName
                          select p).FirstOrDefault();
            }
            catch (Exception ex)
            {
                product = null;
            }
            return product;
        }
        public List<Product> GetProductsByCategoryName(string categoryName)
        {
            List<Product> products = new List<Product>();
            try
            {
                products = (from p in context.Products
                            join c in context.Categories on p.CategoryId equals c.CategoryId
                            where c.CategoryName == categoryName
                            select p).ToList();
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
