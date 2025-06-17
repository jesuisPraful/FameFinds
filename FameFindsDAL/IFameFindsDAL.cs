using FameFindsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FameFindsDAL
{
    public interface IFameFindsDAL
    {
        #region CUSTOMER
        bool RegisterCustomer(Customer customer);
        Customer LoginCustomer(string email, string passwordHash);
        Customer GetCustomerById(int customerId);
        Customer GetCustomerByUsername(string username);
        List<Customer> GetAllCustomers();
        int UpdateCustomer(Customer customer);
        int DeleteCustomer(int customerId);
        bool UpdateUserPassword(int customerId, string newPasswordHash);
        bool IsEmailExists(string email);
        void SaveOtp(int customerId, string otp);
        CustomerPasswordResetToken GetOtp(int customerId, string otp);
        CustomerPasswordResetToken? GetOtpByEmail(string email, string otp);
        void MarkOtpAsUsedByEmail(string email, string otp);
        void MarkOtpAsUsed(int customerId, string otp);
        CustomerPasswordResetToken? GetLatestVerifiedOtp(string email);
        #endregion

        #region category

        List<Category> GetAllCategories();

        #endregion

        #region Products

        List<Product> GetAllProducts();
        List<Product> GetProductsByCity(string cityName);
        bool AddProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(int productId);
        Product GetProductByName(string ProductName);
        List<Product> GetProductsByCategoryName(string categoryName);

        #endregion

        #region vendor

        List<Vendor> GetVendorDetails();
        bool AddVendor(Vendor vendor);
        bool IsVendorEmailExists(string email);
        Vendor LoginVendor(string email, string passwordHash);
        Vendor GetVendorById(int vendorId);
        Vendor GetVendorByUsername(string username);
        bool UpdateVendor(Vendor vendor);
        bool RemoveVendorDetails(int vendorId);
        Vendor GetVendorByName(string VendorName);
        void SaveVendorOtp(int vendorId, string otp);
        VendorPasswordResetToken GetVendorOtp(int vendorId, string otp);
        VendorPasswordResetToken? GetVendorOtpByEmail(string email, string otp);
        void MarkVendorOtpAsUsedByEmail(string email, string otp);
        VendorPasswordResetToken? GetVendorLatestVerifiedOtp(string email);

        #endregion

        #region Ratings
        bool AddRating(Rating rating);
        List<Rating> GetRatings();
        bool RemoveRating(int ratingId);
        List<ShopWithRatingDto> GetShopsSortedByRating();
        #endregion

        #region shop

        bool RegisterShop(Shop shop);
        List<Shop> GetAllShops();
        Shop GetShopsByShopId(int shopId);
        List<Shop> GetShopsByVendorId(int vendorId);
        List<Shop> GetShopsByShopName(string shopName);
        List<Shop> GetShopsByCityName(string cityName);
        List<Shop> GetShopsByProduct(string productName);
        List<Shop> GetShopsByProductAndCity(string productName, string cityName);
        List<Shop> GetShopByCategoryName(string categoryName);
        int RemoveShop(int shopId);
        bool UpdateShopName(int ShopId, string shopName, string nshopName);
        bool UpdateShopContactNumber(int shopId, string nContactNumber, string contactNumber);
        bool UpdateShopEmailId(int ShopId, string nemailId, string emailId);
        bool UpdateShopIsOpen(int ShopId, bool isOpen);

        #endregion

        #region city

        List<City> GetAllCities();
        bool RegisterCity(City city);
        bool DeleteCity(int cityId);
        City GetCityById(int cityId);
        City GetCityByName(string cityName);

        #endregion

        #region shopProduct

        bool AddShopProduct(ShopProduct shopProduct);
        bool UpdateShopProductPrice(int shopProductId, decimal price);
        bool UpdateShopProductStock(int shopProductId, int stock);
        bool UpdateShopProduct(int shopProductId, decimal? price = null, int? stock = null);
        bool DeleteShopProduct(int shopProductId);
        List<ShopProduct> GetProductsByShopId(int shopId);

        #endregion
    }
}
