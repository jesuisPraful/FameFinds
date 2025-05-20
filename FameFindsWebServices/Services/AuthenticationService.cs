using FameFindsDAL;
using FameFindsWebServices;
using FameFindsWebServices.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;


namespace FameFindsWebServices.Services
{
    public class AuthenticationService
    {
        private readonly FameFindsRepository _repo;
        private readonly PasswordHasher<Customer> _hasher;

        public AuthenticationService(FameFindsRepository repo)
        {
            _repo = repo;
            _hasher = new PasswordHasher<Customer>();
        }
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
    }
}
