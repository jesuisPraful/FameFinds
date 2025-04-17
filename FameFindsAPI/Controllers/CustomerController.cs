using DAL;
using DAL.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FameFindsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        Repository repository { get; set; }

        public CustomerController()
        {
            repository = new Repository();
        }

        [HttpGet]
        public JsonResult getAllCustomers()
        {
            List<Customer> customers=new List<Customer>();
            try
            {
                customers = repository.getAllCustomers();
            }
            catch (Exception ex)
            {
                customers = null;
            }
            return Json(customers);
        }

    }
}
