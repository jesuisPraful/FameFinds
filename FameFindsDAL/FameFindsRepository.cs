using FameFindsDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static FameFindsDAL.FameFindsRepository;
using static System.Formats.Asn1.AsnWriter;

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
        public Customer GetCustomerByUsername(string username)
        {
            Customer customer = new Customer();
            try
            {
                customer = _context.Customers.FirstOrDefault(u => u.Email == username);
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

        //public bool IsEmailRegistered(string email)
        //{
        //    return _context.Customers.Any(c => c.Email == email);
        //}
        
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
        //to save otp in data base
        public void SaveOtp(int customerId, string otp)
        {
            var entry = new CustomerPasswordResetToken
            {
                CustomerId = customerId,
                Token = otp,
                Expiry = DateTime.Now.AddMinutes(5),
                IsUsed = false,
                RequestedAt = DateTime.Now
            };

            _context.CustomerPasswordResetTokens.Add(entry);
            _context.SaveChanges();
        }
        public CustomerPasswordResetToken GetOtp(int customerId, string otp)
        {
            try
            {
                return _context.CustomerPasswordResetTokens
                    .FirstOrDefault(t =>
                        t.CustomerId == customerId &&
                        t.Token == otp &&
                        t.IsUsed != true); 
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public CustomerPasswordResetToken? GetOtpByEmail(string email, string otp)
        {
            try
            {
                var customer = _context.Customers.FirstOrDefault(c => c.Email == email);
                if (customer == null) return null;

                return _context.CustomerPasswordResetTokens.FirstOrDefault(t =>
                    t.CustomerId == customer.CustomerId &&
                    t.Token == otp &&
                    (t.IsUsed == false || t.IsUsed == null) && 
                    t.Expiry > DateTime.Now);
            }
            catch
            {
                return null;
            }
        }

        public void MarkOtpAsUsedByEmail(string email, string otp)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Email == email);
            if (customer == null) return;

            var token = _context.CustomerPasswordResetTokens.FirstOrDefault(t =>
                t.CustomerId == customer.CustomerId &&
                t.Token == otp &&
                t.IsUsed != true &&
                t.Expiry > DateTime.Now);

            if (token != null)
            {
                token.IsUsed = true;
                _context.SaveChanges();
            }
        }
        //did not use since id cannot be used in frontend
        public void MarkOtpAsUsed(int customerId, string otp)
        {
            var token = _context.CustomerPasswordResetTokens
        .FirstOrDefault(t =>
            t.CustomerId == customerId &&
            t.Token == otp &&
            (t.IsUsed == false || t.IsUsed == null) &&
            t.Expiry > DateTime.Now);

            if (token != null)
            {
                token.IsUsed = true;
                _context.SaveChanges();
            }
        }
        //reset password only with in 5 minutes and only after otp verification.
        public CustomerPasswordResetToken? GetLatestVerifiedOtp(string email)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Email == email);
            if (customer == null) return null;

            return _context.CustomerPasswordResetTokens
                .Where(t => t.CustomerId == customer.CustomerId && t.IsUsed == true && t.Expiry > DateTime.Now)
                .OrderByDescending(t => t.RequestedAt)
                .FirstOrDefault();
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
        public List<Product> GetProductsByCity(string cityName)
        {
            List<Product> products = new List<Product>();
            try
            {
                products = (from p in _context.Products
                            join c in _context.Cities on p.CityId equals c.CityId
                            where c.CityName == cityName
                            select p).ToList();

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
                        where p.ProductName == product.ProductName && p.CityId == product.CityId
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

        public List<Product> GetProductByCity(string cityName)
        {
            City city = _context.Cities.Where(c => c.CityName == cityName).FirstOrDefault();
            List<Product> product = new List<Product>();

            try
            {
                product = _context.Products
                    .Where(p => p.CityId == city.CityId)
                    .ToList();
            }
            catch (Exception)
            {

                city = null;
            }
            return product;
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
        //public bool IsVendorEmailRegistered(string email)
        //{
        //    return _context.Vendors.Any(c => c.Email == email);
        //}
        public bool IsVendorEmailExists(string email)
        {
            try
            {
                return _context.Vendors.Any(c => c.Email == email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in IsEmailExists: {ex.Message}");
                return false;
            }
        }
        public Vendor LoginVendor(string email, string passwordHash)
        {
            try
            {
                var vendor = _context.Vendors.FirstOrDefault(u => u.Email == email && u.PasswordHash == passwordHash);
                return vendor;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login failed: {ex.Message}");
                return null;
            }
        }
        public Vendor GetVendorById(int vendorId)
        {
            Vendor vendor = new Vendor();
            try
            {
                vendor = _context.Vendors.Find(vendorId);
            }
            catch (Exception ex)
            {
                vendor = null;
            }
            return vendor;
        }
        public Vendor GetVendorByUsername(string username)
        {
            Vendor vendor=new Vendor();
            try
            {
                vendor = _context.Vendors.FirstOrDefault(u => u.Email == username);
            }
            catch (Exception ex)
            {
                vendor = null;
            }
            return vendor;
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
        public Vendor GetVendorByName(string VendorName)
        {
            Vendor vendor = new Vendor();
            try
            {
                vendor = (from V in _context.Vendors
                          where V.VendorName == VendorName
                          select V).FirstOrDefault();
            }
            catch (Exception ex)
            {
                vendor = null;
            }
            return vendor;
        }

        //to save otp in data base
        public void SaveVendorOtp(int vendorId, string otp)
        {
            var entry = new VendorPasswordResetToken
            {
                VendorId = vendorId,
                Token = otp,
                Expiry = DateTime.Now.AddMinutes(5),
                IsUsed = false,
                RequestedAt = DateTime.Now
            };

            _context.VendorPasswordResetTokens.Add(entry);
            _context.SaveChanges();
        }

        public VendorPasswordResetToken GetVendorOtp(int vendorId, string otp)
        {
            try
            {
                return _context.VendorPasswordResetTokens
                    .FirstOrDefault(t =>
                        t.VendorId == vendorId &&
                        t.Token == otp &&
                        t.IsUsed != true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public VendorPasswordResetToken? GetVendorOtpByEmail(string email, string otp)
        {
            try
            {
                var vendor = _context.Vendors.FirstOrDefault(c => c.Email == email);
                if (vendor == null) return null;

                return _context.VendorPasswordResetTokens.FirstOrDefault(t =>
                    t.VendorId == vendor.VendorId &&
                    t.Token == otp &&
                    (t.IsUsed == false || t.IsUsed == null) &&
                    t.Expiry > DateTime.Now);
            }
            catch
            {
                return null;
            }
        }

        public void MarkVendorOtpAsUsedByEmail(string email, string otp)
        {
            var vendor = _context.Vendors.FirstOrDefault(c => c.Email == email);
            if (vendor == null) return;

            var token = _context.VendorPasswordResetTokens.FirstOrDefault(t =>
                t.VendorId == vendor.VendorId &&
                t.Token == otp &&
                t.IsUsed != true &&
                t.Expiry > DateTime.Now);

            if (token != null)
            {
                token.IsUsed = true;
                _context.SaveChanges();
            }
        }
        //reset password only with in 5 minutes and only after otp verification.
        public VendorPasswordResetToken? GetVendorLatestVerifiedOtp(string email)
        {
            var Vendor = _context.Vendors.FirstOrDefault(c => c.Email == email);
            if (Vendor == null) return null;

            return _context.VendorPasswordResetTokens
                .Where(t => t.VendorId == Vendor.VendorId && t.IsUsed == true && t.Expiry > DateTime.Now)
                .OrderByDescending(t => t.RequestedAt)
                .FirstOrDefault();
        }
        #endregion

        #region Ratings

        public bool AddRating(Rating rating)
        {
            bool status = false;
            try
            {
                var shopObj = _context.Shops.Find(rating.ShopId);
                var customerObj = _context.Customers.Find(rating.CustomerId);
                if (shopObj != null && customerObj != null)
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

        #region shop

        //Register Shops
        public bool RegisterShop(Shop shop)
        {
            bool status = false;
            try
            {
                _context.Shops.Add(shop);
                _context.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }



        //Get All Shops 
        public List<Shop> GetAllShops()
        {

            List<Shop> shops = new List<Shop>();
            try
            {
                shops = _context.Shops.ToList();
            }

            catch (Exception ex)
            {
                shops = null;

            }
            return shops;
        }


        //Get Shop By ShopId
        public Shop GetShopsByShopId(int shopId)
        {

            Shop shops = new Shop();
            try
            {
                shops = _context.Shops
                    .Where(s => s.ShopId == shopId).Select(s => s)
                    .FirstOrDefault();
            }

            catch (Exception ex)
            {
                shops = null;

            }
            return shops;
        }


        //Get Shop by Vendor Id
        public List<Shop> GetShopsByVendorId(int vendorId)
        {

            List<Shop> shops = new List<Shop>();
            try
            {
                shops = _context.Shops
                    .Where(s => s.VendorId == vendorId).Select(s => s)
                    .ToList();
            }

            catch (Exception ex)
            {
                shops = null;

            }
            return shops;
        }



        //Get Shop by ShopName
        public List<Shop> GetShopsByShopName(string shopName)
        {

            List<Shop> shops = new List<Shop>();
            try
            {
                shops = _context.Shops
                    .Where(s => s.ShopName == shopName).Select(s => s)
                    .ToList();
            }

            catch (Exception ex)
            {
                shops = null;

            }
            return shops;
        }



        //Get Shop By City Name
        public List<Shop> GetShopsByCityName(string cityName)
        {
            City city = _context.Cities.Where(c => c.CityName == cityName).FirstOrDefault();
            List<Shop> shops = new List<Shop>();
            try
            {
                shops = _context.Shops
                    .Where(s => s.CityId == city.CityId).Select(s => s)
                    .ToList();
            }

            catch (Exception ex)
            {
                shops = null;

            }
            return shops;
        }



        //Get Shop By ProductName
        public List<Shop> GetShopsByProduct(string productName)
        {
            List<Shop> shops = new List<Shop>();
            try
            {
                Product product = _context.Products.Where(P => P.ProductName == productName).FirstOrDefault();

                List<ShopProduct> ListShopsProducts = new List<ShopProduct>();
                ListShopsProducts = _context.ShopProducts
                                        .Where(sp => sp.ProductId == product.ProductId)
                                        .GroupBy(sp => sp.ShopId)
                                        .Select(g => g.First())
                                        .ToList();




                foreach (var shopProduct in ListShopsProducts)
                {
                    Shop shop = new Shop();
                    shop = _context.Shops.Find(shopProduct.ShopId);
                    if (shop != null)
                    {
                        shops.Add(shop);
                    }
                }

            }
            catch (Exception)
            {
                shops = null;
            }
            return shops;
        }




        public List<Shop> GetShopByCategoryName(string categoryName)
        {

            try
            {
                var category = _context.Categories.Where(c => c.CategoryName == categoryName).FirstOrDefault();

                var productIds = _context.Products.Where(p => p.CategoryId == category.CategoryId).Select(p => p.ProductId).ToList();

                List<int> shopsId = new List<int>();
                foreach (var productId in productIds)
                {
                    var shopId = Convert.ToInt32(_context.ShopProducts.Where(sp => sp.ProductId == productId).Select(sp => sp.ShopId).FirstOrDefault());
                    shopsId.Add(shopId);
                }

                List<Shop> shops = new List<Shop>();

                foreach (var shopId in shopsId)
                {
                    var shop = _context.Shops.Where(sp => sp.ShopId == shopId).FirstOrDefault();
                    shops.Add(shop);
                }
                return shops;
            }
            catch (Exception)
            {
                return null;
            }
        }


        //Remove Shops By id
        public int RemoveShop(int shopId)
        {
            int status = 0;
            try
            {
                Shop shop = _context.Shops.Find(shopId);
                if (shop != null)
                {
                    _context.Shops.Remove(shop);
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


        //Update Operations on Shops
        public bool UpdateShopContactNumber(int shopId, string contactNumber)
        {
            bool status = false;
            try
            {
                Shop shop = _context.Shops.Find(shopId);
                if (shop != null)
                {
                    shop.ContactNumber = contactNumber;
                    _context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;

        }

        //Change EmailId
        public bool UpdateShopEmailId(int ShopId, string emailId)
        {
            bool status = false;
            try
            {
                Shop shop = _context.Shops.Find(ShopId);
                if (shop != null)
                {
                    shop.EmailId = emailId;
                    _context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        //Shop is open or not
        public bool UpdateShopIsOpen(int ShopId, bool isOpen)
        {
            bool status = false;
            try
            {
                Shop shop = _context.Shops.Find(ShopId);
                if (shop != null)
                {
                    shop.IsOpen = isOpen;
                    _context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        #endregion



        #region city
        public List<City> GetAllCities()
        {
            List<City> cities = new List<City>();
            try
            {
                cities = _context.Cities.ToList();
            }
            catch (Exception ex)
            {
                cities = null;
            }
            return cities;
        }


        public bool RegisterCity(City city)
        {
            bool status = false;
            try
            {
                _context.Cities.Add(city);
                _context.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public bool DeleteCity(int cityId)
        {
            bool status = false;
            try
            {
                City cityToDelete = _context.Cities.Find(cityId);
                if (cityToDelete != null)
                {
                    _context.Cities.Remove(cityToDelete);
                    _context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }


        public City GetCityById(int cityId)
        {
            City city = new City();
            try
            {
                city = _context.Cities.Find(cityId);
            }
            catch (Exception ex)
            {
                city = null;
            }
            return city;
        }

        public City GetCityByName(string cityName)
        {
            City city = new City();
            try
            {
                city = _context.Cities.FirstOrDefault(c => c.CityName.ToLower() == cityName.ToLower());
            }
            catch (Exception ex)
            {
                city = null;
            }
            return city;
        }

        

        #endregion
    }

}

