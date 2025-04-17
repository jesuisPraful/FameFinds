using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FameFindsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        Repository repository {  get; set; }
        public ProductController()
        {
            repository = new Repository();
        }

        [HttpGet]
        public JsonResult getAllProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                products=repository.getAllProducts();
            }
            catch (Exception ex)
            {
                products = null;
            }
            return Json(products);
        }
    }
}
