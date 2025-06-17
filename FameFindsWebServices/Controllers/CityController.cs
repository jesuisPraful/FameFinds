using FameFindsDAL;
using FameFindsDAL.Models;
using FameFindsWebServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics.Eventing.Reader;

namespace FameFindsWebServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly IFameFindsDAL _repository;
        public CityController(IFameFindsDAL repository)
        {
            _repository = repository;
        }


        [HttpPost("getCityByShop")]
        public IActionResult GetCityByShop(FameFindsDAL.Models.Shop shop)
        {
            try
            {
                if (shop == null)
                {
                    return BadRequest("Invalid shop data.");
                }

                var city = _repository.CityByShop(shop);
                if (city != null)
                {
                    return Ok(city);
                }
                else
                {
                    return BadRequest("City Not Found");
                }

            }
            catch (Exception)
            {

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getCityByShopId/{shopId}")]
        public IActionResult GetCityByShopId(int shopId)
        {
            try
            {
                var city = _repository.CityByShoIdp(shopId);
                if (city != null)
                {
                    return Ok(city);
                }
                else
                {
                    return BadRequest("City Not Found");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet]
        public IActionResult GetAllCities()
        {

            try
            {
                var cities = _repository.GetAllCities();
                return Ok(cities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult RegisterCity(Models.City city)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var status = _repository.RegisterCity(new FameFindsDAL.Models.City
                    {
                        CityId = city.CityId,
                        CityName = city.CityName
                    });

                    if (status)
                        return Ok("City Registered Successfully");
                    else
                        return BadRequest("Failed to register city.");
                }

                return BadRequest("Invalid data.");
            }
            catch (Exception ex)
            {
                return BadRequest("Registration failed: " + ex.Message);
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
        [HttpDelete("{cityId}")]
        public IActionResult DeleteCity(int cityId)
        {
            try
            {
                var status = _repository.DeleteCity(cityId);
                if (status)
                    return Ok("City deleted successfully.");
                else
                    return NotFound("City not found or could not be deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error while deleting city: " + ex.Message);
            }
        }
    }
}
