using FameFindsDAL;
using FameFindsDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FameFindsWebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly FameFindsRepository _repository;
        public CityController(FameFindsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllCities()
        {

            try
            {
                var cities = _repository.GetAllCities();
                return Ok(cities);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult RegisterCity(Models.City city)
        {
            bool status = false;

            try
            {
                if (ModelState.IsValid)
                {
                    City cityOne = new City
                    {
                        CityId=city.CityId,
                        CityName = city.CityName
                    };

                    status = _repository.RegisterCity(cityOne);

                    if (status)
                        return Ok("City Registered Successfully");
                    else
                        return BadRequest("Failed to register shop.");
                }
                else
                {
                    return BadRequest("Invalid Data");
                }
            }
            catch (Exception)
            {
                return BadRequest("Registration failed.");
            }
        }

        // GET: api/City/id/5
        [HttpGet("id/{cityId}")]
        public IActionResult GetCityById(int cityId)
        {
            try
            {
                var city = _repository.GetCityById(cityId);
                if (city == null)
                    return NotFound("City not found");

                return Ok(city);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/City/name/Delhi
        [HttpGet("name/{cityName}")]
        public IActionResult GetCityByName(string cityName)
        {
            try
            {
                var city = _repository.GetCityByName(cityName);
                if (city == null)
                    return NotFound("City not found");

                return Ok(city);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
