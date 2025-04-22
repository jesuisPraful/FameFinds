using FameFindsDAL;
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

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
