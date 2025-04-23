using FameFindsDAL;
using FameFindsDAL.Models;
using Microsoft.AspNetCore.Mvc;


namespace FameFindsWebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : Controller
    {
        private readonly FameFindsRepository _repository;
        public RatingController(FameFindsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllRatings()
        {
            try
            {
                var ratings = _repository.GetRatings();
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddRating(Models.Rating rating)
        {
            bool result = false;
            try
            {
                if(ModelState.IsValid)
                {
                    Rating rating1 = new Rating();
                    rating1.RatingId = rating.RatingId;
                    rating1.CustomerId = rating.CustomerId;
                    rating1.ShopId = rating.ShopId;
                    rating1.RatingValue = rating.RatingValue;
                    rating1.Review = rating.Review;
                    rating1.CreatedAt = rating1.CreatedAt;

                    result = _repository.AddRating(rating1);

                    if(result)
                    {
                        return Ok("Ratings added successfully");
                    }
                    else
                    {
                        return BadRequest("Failed to add ratings");
                    }
                }
                else
                {
                    return BadRequest("Model state is not valid");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult RemoveRating(int ratingId)
        {
            bool result = false;
            try
            {
                result = _repository.RemoveRating(ratingId);
                if(result)
                {
                    return Ok("Rating deleted successfully");
                }
                else
                {
                    return BadRequest("Failed to delete rating");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
