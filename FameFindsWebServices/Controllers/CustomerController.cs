using FameFindsDAL;
using FameFindsWebServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FameFindsWebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly FameFindsRepository _repository;
        public CustomerController(FameFindsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
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

        [HttpPost("Register")]
        public IActionResult RegisterCustomer(Models.Customer customer)
        {
            bool status = false;
            try
            {
                if (ModelState.IsValid)
                {
                    FameFindsDAL.Models.Customer customerOne = new FameFindsDAL.Models.Customer();

                    customerOne.FullName = customer.FullName;
                    customerOne.Email = customer.Email;
                    customerOne.PhoneNumber = customer.PhoneNumber;
                    //customerOne.CustomerId = customer.CustomerId;
                    customerOne.PasswordHash = customer.PasswordHash;

                    status = _repository.RegisterCustomer(customerOne);
                    return Ok("User Registered Successfully ");
                }
                else
                {
                    return BadRequest("Invalid Data");
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
                var user = _repository.LoginCustomer(email, passwordHash);

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
            var success = _repository.UpdateUserPassword(userId, newPasswordHash);
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
        [HttpGet("{customerId}")]
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
                return Ok(new { emailExists = exists });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CheckEmailExists: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
