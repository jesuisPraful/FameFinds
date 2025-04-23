using FameFindsDAL;
using FameFindsWebServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
