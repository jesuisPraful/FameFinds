using FameFindsDAL;
using FameFindsWebServices.Models;
using FameFindsWebServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResetModel = FameFindsWebServices.Models.ResetPasswordRequest;


namespace FameFindsWebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly FameFindsRepository _repository;
        private readonly AuthenticationService _authService;
        private readonly EmailService _emailService;

        public CustomerController(FameFindsRepository repository, AuthenticationService authService, EmailService emailService)
        {
            _repository = repository;
            _authService = authService;
            _emailService = emailService;
        }


        [HttpGet("GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                var customersList = _repository.GetAllCustomers();
                if (customersList != null)
                {
                    foreach (var customer in customersList)
                    {
                        Customer customerOne = new Customer();

                        customerOne.CustomerId = customer.CustomerId;
                        customerOne.FullName = customer.FullName;
                        customerOne.Email = customer.Email;
                        customerOne.PhoneNumber = customer.PhoneNumber;

                        customers.Add(customerOne);
                    }
                }
            }
            catch (Exception)
            {
                customers = null;
                return BadRequest("Failed to retrieve customers");
            }
            return Ok(customers);
        }

        [HttpGet("customerIdByEmail/{email}")]
        public IActionResult GetCustomerIdByEmail(string email)
        {
            var id = _repository.GetCustomerIdByEmail(email);
            if (id == 0)
                return NotFound("Customer not found");
            return Ok(id);
        }

        [HttpPost("Register")]
        public IActionResult RegisterCustomer([FromBody] CustomerRegister customer)
        {
            bool status = false;
            try
            {
                if (ModelState.IsValid)
                {
                    var customerOne = new Customer
                    {
                        FullName = customer.FullName,
                        Email = customer.Email,
                        PhoneNumber = customer.PhoneNumber,
                        Password = customer.Password
                    };
                    status = _authService.Register(customerOne);
                    if (status)
                        return Ok("User Registered Successfully");
                    else
                        return BadRequest("Registration failed");
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return BadRequest(new { message = "Invalid Data", errors });
                }
            }
            catch (Exception)
            {
                status = false;
                return BadRequest("Registration failed.");
            }
        }
        [HttpPost("login")]
        public IActionResult Login([FromQuery] string email, [FromQuery] string passwordHash)
        {
            try
            {
                var user = _authService.Login(new Models.Customer
                {
                    Email = email,
                    Password = passwordHash
                });

                if (user != null)
                    return Ok(user);

                return Unauthorized("Invalid email or password.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("UpdateUserDetails")]
        public IActionResult UpdateCustomer(Models.Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FameFindsDAL.Models.Customer customerOne = new FameFindsDAL.Models.Customer();

                    customerOne.CustomerId = customer.CustomerId;
                    customerOne.FullName = customer.FullName;
                    customerOne.Email = customer.Email;
                    customerOne.PhoneNumber = customer.PhoneNumber;

                    int status = _repository.UpdateCustomer(customerOne);
                    if (status == 1)
                    {
                        return Ok("Customer Updated Successfully");
                    }
                    else if (status == -1)
                    {
                        return NotFound("Customer Not Found for Update");
                    }
                    else
                    {
                        return BadRequest("Failed to Update Customer");
                    }
                }
                else
                {
                    return BadRequest("Invalid Customer Data");
                }
            }
            catch (Exception)
            {
                return BadRequest("Failed to Update Customer");
            }
        }

        [HttpPut("update-password")]
        public IActionResult UpdatePassword([FromQuery] int userId, [FromQuery] string newPasswordHash)
        {
            var success = _authService.UpdatePassword(userId, newPasswordHash);
            return success ? Ok("Password updated.") : NotFound("User not found.");
        }

        [HttpDelete]
        public IActionResult DeleteCustomer(int customerId)
        {
            try
            {
                int status = _repository.DeleteCustomer(customerId);
                if (status == 1)
                {
                    return Ok("Customer Deleted Successfully");
                }
                else if (status == -1)
                {
                    return NotFound("Customer Not Found for Deletion");
                }
                else
                {
                    return BadRequest("Failed to Delete Customer");
                }
            }
            catch (Exception)
            {
                return BadRequest("Failed to Delete Customer");
            }
        }
        [HttpGet("customerId")]
        public IActionResult GetCustomerById(int customerId)
        {
            try
            {
                var customer = _repository.GetCustomerById(customerId);
                if (customer != null)
                {
                    Models.Customer customerOne = new Models.Customer();

                    customerOne.CustomerId = customer.CustomerId;
                    customerOne.FullName = customer.FullName;
                    customerOne.Email = customer.Email;
                    customerOne.PhoneNumber = customer.PhoneNumber;

                    return Ok(customerOne);
                }
                else
                {
                    return NotFound("Customer Not Found");
                }
            }
            catch (Exception)
            {
                return BadRequest("Failed to Retrieve Customer");
            }
        }

        [HttpGet("check-email")]
        public IActionResult CheckEmailExists([FromQuery] string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return BadRequest("Email must be provided.");

                bool exists = _repository.IsEmailExists(email);
                return Ok(exists);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CheckEmailExists: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        //forget password
        //generating otp, saving in th db, sending to otp to that mail.

        [HttpPost("request-otp")]
        public IActionResult RequestOtp([FromBody] EmailRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest("Email is required.");
            }

            var customer = _repository.GetCustomerByUsername(request.Email);
            if (customer == null)
            {
                return NotFound("Email not found");
            }

            int customerId = customer.CustomerId;

            var otp = _emailService.GenerateOtp();
            _repository.SaveOtp(customerId, otp);

            _emailService.SendOtpEmail(request.Email, otp);

            return Ok("OTP has been sent to your email.");
        }


        //[HttpPost("verify-otp")]
        //public IActionResult VerifyOtp([FromBody] OtpVerificationRequest request)
        //{

        //    var token = _repository.GetOtp(request.CustomerId, request.Otp);
        //    if (token == null || token.IsUsed == true || token.Expiry < DateTime.Now)
        //    {
        //        return BadRequest("Invalid or expired OTP");
        //    }

        //    _repository.MarkOtpAsUsed(request.CustomerId, request.Otp);
        //    return Ok("OTP verified. You may now reset your password.");
        //}

        [HttpPost("verify-otp")]
        public IActionResult VerifyOtp([FromBody] OtpVerificationRequest request)
        {
            var token = _repository.GetOtpByEmail(request.Email, request.Otp);

            if (token == null)
            {
                return BadRequest("Invalid or expired OTP.");
            }

            _repository.MarkOtpAsUsedByEmail(request.Email, request.Otp);

            return Ok("OTP verified. You may now reset your password.");
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetModel request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                return BadRequest("Email is required.");

            if (string.IsNullOrWhiteSpace(request.NewPassword) || string.IsNullOrWhiteSpace(request.ConfirmPassword))
                return BadRequest("All fields are required.");

            if (request.NewPassword != request.ConfirmPassword)
                return BadRequest("Passwords do not match.");

            var token = _repository.GetLatestVerifiedOtp(request.Email);
            if (token == null || token.Expiry < DateTime.Now)
                return BadRequest("OTP not verified or session expired. Please verify your OTP again.");

            var customer = _repository.GetCustomerByUsername(request.Email);
            if (customer == null)
                return NotFound("Customer not found.");

            var result = _authService.UpdatePassword(customer.CustomerId, request.NewPassword);
            if (!result)
                return StatusCode(500, "Error updating password.");

            return Ok("Password reset successful.");
        }

    }
}
