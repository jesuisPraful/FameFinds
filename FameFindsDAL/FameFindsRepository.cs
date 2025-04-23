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
        private readonly FameFindsContext _context;
        public FameFindsRepository(FameFindsContext Famecontext)
        {
            _context = Famecontext;
        }

        #region CUSTOMER
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                customers = _context.Customers.ToList();
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
                _context.Customers.Add(customer);
                _context.SaveChanges();
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
                var customerOne = _context.Customers.Find(customer.CustomerId);
                if (customerOne == null)
                {
                    status = false;
                }
                else
                {
                    customerOne.FullName = customer.FullName;
                    customerOne.Email = customer.Email;
                    customerOne.PhoneNumber = customer.PhoneNumber;
                    _context.SaveChanges();
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
                categories = _context.Categories.ToList();
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
                products = _context.Products.ToList();
            }
            catch (Exception ex)
            {
                products = null;
            }
            return products;
        }
        public bool AddProduct(Product product)
        {
          var name= ( from p in _context.Products
            where p.ProductName == product.ProductName
            select p).FirstOrDefault();
            bool status = false;
            if ((name==null))
            { 
                try
                {
                    _context.Products.Add(product);
                    _context.SaveChanges();
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
                var existingProduct = _context.Products.Find(product.ProductId);
                if (existingProduct != null)
                {
                    existingProduct.ProductName = product.ProductName;
                    existingProduct.Description = product.Description;
                    existingProduct.CategoryId = product.CategoryId;
                    _context.SaveChanges();
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
                var existingProduct = _context.Products.Find(productId);
                if (existingProduct != null)
                {
                    _context.Products.Remove(existingProduct);
                    _context.SaveChanges();
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
                product = (from p in _context.Products
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
                products = (from p in _context.Products
                            join c in _context.Categories on p.CategoryId equals c.CategoryId
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

        #region Vendor

        public List<Vendor> GetVendorDetails()
        {
            List<Vendor> vendors = new List<Vendor>();
            try
            {
                vendors = _context.Vendors.ToList();
            }
            catch (Exception ex)
            {
                vendors = null;
            }
            return vendors;
        }
        public bool AddVendor(Vendor vendor)
        {
            bool status = false;
            try
            {
                _context.Vendors.Add(vendor);
                _context.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }


        public bool UpdateVendor(Vendor vendor)
        {
            bool status = false;
            try
            {
                var vendorObj = _context.Vendors.Find(vendor.VendorId);
                if (vendorObj != null)
                {
                    vendorObj.Email = vendor.Email;
                    vendorObj.PhoneNumber = vendor.PhoneNumber;
                    vendorObj.VendorName = vendor.VendorName;
                    _context.Vendors.Update(vendorObj);
                    _context.SaveChanges();
                   status=true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception ex)
            {
                status=false;
            }
            return status;
        }

        public bool RemoveVendorDetails(int vendorId)
        {
            try
            {
                var vendor = _context.Vendors.Find(vendorId);
                if (vendor != null)
                {
                    _context.Vendors.Remove(vendor);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
       

        #endregion

    }
}
