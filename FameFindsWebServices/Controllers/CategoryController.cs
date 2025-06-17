using FameFindsDAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FameFindsWebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly IFameFindsDAL _repository;
        public CategoryController(IFameFindsDAL repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            try
            {
                var categories = _repository.GetAllCategories(); 
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
