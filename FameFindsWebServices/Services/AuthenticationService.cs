using FameFindsDAL;
using FameFindsWebServices;
using FameFindsWebServices.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;


namespace FameFindsWebServices.Services
{
    public interface IAuthenticationService
    {
        bool Register(Customer customer);
        bool Login(Customer customer);
        bool UpdatePassword(int userId, string newPassword);
        bool AddVendor(Vendor vendor);
        bool LoginVendor(Vendor vendor);
        bool UpdatePasswordVendor(int vendorId, string newPassword);
    }
    public class AuthenticationService:IAuthenticationService
    {
        private readonly IFameFindsDAL _repo;
        private readonly PasswordHasher<Customer> _hasher;
        private readonly PasswordHasher<Vendor> _hasherV;

        public AuthenticationService(IFameFindsDAL repo)
        {
            _repo = repo;
            _hasher = new PasswordHasher<Customer>();
            _hasherV = new PasswordHasher<Vendor>();
        }

        #region customer
        public bool Register(Customer customer)
        {
            bool status = false;
            customer.PasswordHash = _hasher.HashPassword(customer, customer.Password);
            customer.Password = null; // Clear raw password before storing
            FameFindsDAL.Models.Customer customerOne = new FameFindsDAL.Models.Customer()
            {
                FullName = customer.FullName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                PasswordHash = customer.PasswordHash
            };
            status = _repo.RegisterCustomer(customerOne);
            return status;
        }

        public bool Login(Customer customer)
        {
            var storedUser = _repo.GetCustomerByUsername(customer.Email);
            if (storedUser == null)
                return false;

            var result = _hasher.VerifyHashedPassword(customer, storedUser.PasswordHash, customer.Password);
            return result == PasswordVerificationResult.Success;
        }

        public bool UpdatePassword(int userId, string newPassword)
        {
            var customer = _repo.GetCustomerById(userId);
            if (customer == null)
                return false;

            var tempCustomer = new Customer();
            var hashedPassword = _hasher.HashPassword(tempCustomer, newPassword);

            return _repo.UpdateUserPassword(userId, hashedPassword);
        }
        #endregion

        #region vendor

        public bool AddVendor(Vendor vendor)
        {
            bool status = false;
            string hashedPassword = _hasherV.HashPassword(vendor, vendor.PasswordHash); // Hash password
                                                                                       // Do NOT nullify vendor.PasswordHash here

            FameFindsDAL.Models.Vendor vendor1 = new FameFindsDAL.Models.Vendor()
            {
                VendorName = vendor.VendorName,
                Email = vendor.Email,
                PasswordHash = hashedPassword, // Use the hashed password
                PhoneNumber = vendor.PhoneNumber
            };
            status = _repo.AddVendor(vendor1);
            return status;
        }

         public bool LoginVendor(Vendor vendor)
        {
            var storedVendor = _repo.GetVendorByUsername(vendor.Email);
            if (storedVendor == null)
                return false;

            var result = _hasherV.VerifyHashedPassword(vendor, storedVendor.PasswordHash, vendor.PasswordHash);
            return result == PasswordVerificationResult.Success;
        }

        public bool UpdatePasswordVendor(int vendorId, string newPassword)
        {
            var vendor = _repo.GetVendorById(vendorId);
            if (vendor == null)
                return false;

            var tempvendor = new Vendor();
            var hashedPassword = _hasherV.HashPassword(tempvendor, newPassword);

            return _repo.UpdateUserPassword(vendorId, hashedPassword);
        }

        #endregion
    }
}
