using FameFindsDAL.Models;
using Microsoft.EntityFrameworkCore;
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
        public bool RegisterCustomer(Customer customer)
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
        public Customer LoginCustomer(string email, string passwordHash)
        {
            try
            {
                var user = _context.Customers.FirstOrDefault(u => u.Email == email && u.PasswordHash == passwordHash);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login failed: {ex.Message}");
                return null;
            }
        }

        public Customer GetCustomerById(int customerId)
        {
            Customer customer = new Customer();
            try
            {
                customer = _context.Customers.Find(customerId);
            }
            catch (Exception ex)
            {
                customer = null;
            }
            return customer;
        }

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
                Console.WriteLine(ex.Message);
            }
            return customers;
        }
        public int UpdateCustomer(Customer customer)
        {
            int status = 0;
            try
            {
                var customerOne = _context.Customers.Find(customer.CustomerId);
                if (customerOne == null)
                {
                    status = -1;
                }
                else
                {
                    customerOne.FullName = customer.FullName;
                    customerOne.Email = customer.Email;
                    customerOne.PhoneNumber = customer.PhoneNumber;
                    _context.SaveChanges();
                    status = 1;
                }
            }
            catch (Exception ex)
            {
                status = -99;
            }
            return status;
        }
        public int DeleteCustomer(int customerId)
        {
            int status = 0;
            try
            {
                var customerOne = _context.Customers.Find(customerId);
                if (customerOne == null)
                {
                    status = -1;
                }
                else
                {
                    _context.Customers.Remove(customerOne);
                    _context.SaveChanges();
                    status = 1;
                }
            }
            catch (Exception ex)
            {
                status = -99;
            }
            return status;
        }
        public bool UpdateUserPassword(int customerId, string newPasswordHash)
        {
            bool status = false;
            try
            {
                var user = _context.Customers.Find(customerId);
                if (user != null)
                {
                    user.PasswordHash = newPasswordHash;
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

        public bool IsEmailExists(string email)
        {
            try
            {
                return _context.Customers.Any(c => c.Email == email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in IsEmailExists: {ex.Message}");
                return false;
            }
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
            var name = (from p in _context.Products
                        where p.ProductName == product.ProductName
                        select p).FirstOrDefault();
            bool status = false;
            if ((name == null))
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
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception ex)
            {
                status = false;
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


        #region Ratings

        public bool AddRating(Rating rating)
        {
            bool status = false;
            try
            {
                var customerObj = _context.Customers.Find(rating.CustomerId);
                if (customerObj != null)
                {
                    _context.Ratings.Add(rating);
                    _context.SaveChanges();
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        //public bool UpdateRating(Rating rating)
        //{
        //    bool status = false;
        //    try
        //    {
        //        var ratingObj = _context.Ratings.Find(rating.RatingId);
        //        if (ratingObj != null && rating.CustomerId != null)
        //        {
        //            ratingObj.RatingValue = rating.RatingValue;
        //            ratingObj.Comment = rating.Comment;
        //            _context.SaveChanges();
        //            status = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        status = false;
        //    }
        //    return status;
        //}

        public List<Rating> GetRatings()
        {
            List<Rating> ratings = new List<Rating>();
            try
            {
                ratings = (from r in _context.Ratings
                           select r).ToList();
            }
            catch (Exception ex)
            {
                ratings = null;
            }
            return ratings;
        }

        public bool RemoveRating(int ratingId)
        {
            bool status = false;
            try
            {
                var removeObj = _context.Ratings.Find(ratingId);
                //if (removeObj != null && removeObj.CustomerId == rating.CustomerId)
                if (removeObj != null && removeObj.CustomerId != null)
                {
                    _context.Ratings.Remove(removeObj);
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

    }
}
