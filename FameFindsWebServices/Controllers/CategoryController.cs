using FameFindsDAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FameFindsWebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly FameFindsRepository _repository;
        public CategoryController(FameFindsRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            try
            {
                // Assuming you have a method to get all categories from the database
                var categories = _repository.GetAllCategories(); // Replace with actual method
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
