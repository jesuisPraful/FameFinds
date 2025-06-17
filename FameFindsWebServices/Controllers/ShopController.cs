using FameFindsDAL;
using FameFindsDAL.Models;
//using FameFindsWebServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FameFindsWebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : Controller
    {
        private readonly FameFindsRepository _repository;
        public ShopController(FameFindsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllShops()
        {
            List<Shop> shops = new List<Shop>();

            try
            {
                var shopList = _repository.GetAllShops();
                if (shopList != null)
                {
                    foreach (var shop in shopList)
                    {
                        Shop shopOne = new Shop();
                        shopOne.ShopId = shop.ShopId;
                        shopOne.ShopName = shop.ShopName;
                        shopOne.EmailId = shop.EmailId;
                        shopOne.CityId = shop.CityId;
                        shopOne.Pincode = shop.Pincode;
                        shopOne.ContactNumber = shop.ContactNumber;
                        shopOne.FullAddress = shop.FullAddress;
                        shopOne.Latitude = shop.Latitude;
                        shopOne.Longitude = shop.Longitude;
                        shopOne.VendorId = shop.VendorId;

                        shops.Add(shopOne);
                    }
                }

            }
            catch (Exception)
            {
                shops = null;
                return BadRequest("Failed to retrieve shops");
            }
            return Ok(shops);
        }

        [HttpPost("Register")]
        public IActionResult RegisterShop(Models.Shop shop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Shop shopOne = new Shop
                    {
                        ShopName = shop.ShopName,
                        EmailId = shop.EmailId,
                        CityId = shop.CityId,
                        Pincode = shop.Pincode,
                        ContactNumber = shop.ContactNumber,
                        FullAddress = shop.FullAddress,
                        Latitude = shop.Latitude,
                        Longitude = shop.Longitude,
                        IsOpen = shop.IsOpen ?? true,
                        CreatedAt = DateTime.Now,
                        VendorId = shop.VendorId
                    };

                    bool status = _repository.RegisterShop(shopOne);

                    if (status)
                        return Ok(new { message = "Shop Registered Successfully" });
                    else
                        return BadRequest(new { message = "Failed to register shop." });
                }
                else
                {
                    return BadRequest(new { message = "Invalid Data" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Registration failed.", error = ex.Message });
            }
        }






        [HttpGet("shopId")]
        public IActionResult GetShopsByShopId(int shopId)
        {
            
            try
            {
                 
                var shop = _repository.GetShopsByShopId(shopId);

                if (shop != null)
                {
                    Shop shopOne = new Shop();

                    shopOne.ShopId = shop.ShopId;
                    shopOne.ShopName = shop.ShopName;
                    shopOne.EmailId = shop.EmailId;
                    shopOne.CityId = shop.CityId;
                    shopOne.Pincode = shop.Pincode;
                    shopOne.ContactNumber = shop.ContactNumber;
                    shopOne.FullAddress = shop.FullAddress;
                    shopOne.Latitude = shop.Latitude;
                    shopOne.Longitude = shop.Longitude;
                    shopOne.VendorId = shop.VendorId;


                    return Ok(shopOne);

                }
                else
                {
                    return NotFound("Shop Not Found");
                }

            }
            catch (Exception)
            {
                return BadRequest("Failed to retrieve Shop");
            }
        }


        [HttpGet("vendorId")]

        public IActionResult GetShopsByVendorId(int vendorId)
        {
            List<Shop> shops = new List<Shop>();
            try
            {
                var shopList = _repository.GetShopsByVendorId(vendorId);
                if (shopList != null)
                {
                    foreach (var shop in shopList)
                    {
                        Shop shopOne = new Shop();
                        shopOne.ShopId = shop.ShopId;
                        shopOne.ShopName = shop.ShopName;
                        shopOne.EmailId = shop.EmailId;
                        shopOne.CityId = shop.CityId;
                        shopOne.Pincode = shop.Pincode;
                        shopOne.ContactNumber = shop.ContactNumber;
                        shopOne.FullAddress = shop.FullAddress;
                        shopOne.Latitude = shop.Latitude;
                        shopOne.Longitude = shop.Longitude;
                        shopOne.VendorId = shop.VendorId;

                        shops.Add(shopOne);
                    }
                }

            }
            catch (Exception)
            {
                shops = null;
                return BadRequest("Failed to retrieve shops");
            }
            return Ok(shops);

        }


        [HttpGet("shopName")]
        
        public IActionResult GetShopsByShopName(string shopName)
        {
            List<Shop> shops = new List<Shop>();
            try
            {
                var shopList = _repository.GetShopsByShopName(shopName);
                if (shopList != null)
                {
                    foreach (var shop in shopList)
                    {
                        Shop shopOne = new Shop();
                        shopOne.ShopId = shop.ShopId;
                        shopOne.ShopName = shop.ShopName;
                        shopOne.EmailId = shop.EmailId;
                        shopOne.CityId = shop.CityId;
                        shopOne.Pincode = shop.Pincode;
                        shopOne.ContactNumber = shop.ContactNumber;
                        shopOne.FullAddress = shop.FullAddress;
                        shopOne.Latitude = shop.Latitude;
                        shopOne.Longitude = shop.Longitude;
                        shopOne.VendorId = shop.VendorId;

                        shops.Add(shopOne);
                    }
                }

            }
            catch (Exception)
            {
                shops = null;
                return BadRequest("Failed to retrieve shops");
            }
            return Ok(shops);

        }


        [HttpGet("cityName")]

        public IActionResult GetShopsByCityName(string cityName)
        {
            List<Shop> shops = new List<Shop>();
            try
            {
                var shopList = _repository.GetShopsByCityName(cityName);
                if (shopList != null)
                {
                    foreach (var shop in shopList)
                    {
                        Shop shopOne = new Shop();
                        shopOne.ShopId = shop.ShopId;
                        shopOne.ShopName = shop.ShopName;
                        shopOne.EmailId = shop.EmailId;
                        shopOne.CityId = shop.CityId;
                        shopOne.Pincode = shop.Pincode;
                        shopOne.ContactNumber = shop.ContactNumber;
                        shopOne.FullAddress = shop.FullAddress;
                        shopOne.Latitude = shop.Latitude;
                        shopOne.Longitude = shop.Longitude;
                        shopOne.VendorId = shop.VendorId;

                        shops.Add(shopOne);
                    }
                }

            }
            catch (Exception)
            {
                shops = null;
                return BadRequest("Failed to retrieve shops");
            }
            return Ok(shops);
        }


        [HttpGet("productName")]
        public IActionResult GetShopsByProduct(string productName)
        {
            List<Shop> shops = new List<Shop>();
            try
            {
                var shopList = _repository.GetShopsByProduct(productName);
                if (shopList != null || shopList.Any())
                {
                    foreach (var shop in shopList)
                    {
                        Shop shopOne = new Shop();
                        shopOne.ShopId = shop.ShopId;
                        shopOne.ShopName = shop.ShopName;
                        shopOne.EmailId = shop.EmailId;
                        shopOne.CityId = shop.CityId;
                        shopOne.Pincode = shop.Pincode;
                        shopOne.ContactNumber = shop.ContactNumber;
                        shopOne.FullAddress = shop.FullAddress;
                        shopOne.Latitude = shop.Latitude;
                        shopOne.Longitude = shop.Longitude;
                        shopOne.VendorId = shop.VendorId;

                        shops.Add(shopOne);
                    }
                    return Ok(shops);
                }
                else
                {
                    return NotFound("Shops not Found");
                }

            }
            catch (Exception)
            {
                return BadRequest("Failed to retrieve shops");
            }
             
        }


        [HttpGet("categoryName")]
        public IActionResult GetShopByCategoryName(string categoryName)
        {
            List<Shop> shops = new List<Shop>();
            try
            {
                var shopList = _repository.GetShopByCategoryName(categoryName);
                if (shopList != null || !shopList.Any())
                {
                    foreach (var shop in shopList)
                    {
                        shops.Add(shop);

                    }
                    return Ok(shops);
                }
                else
                {
                    return NotFound("Shops not Found");
                }

            }
            catch (Exception)
            {
                return BadRequest("Failed to retrieve shops");
            }
        }

        [HttpGet("GetShops")]
        public IActionResult GetShopsByProductAndCity([FromQuery] string productName, [FromQuery] string cityName)
        {
            try
            {
                var shops = _repository.GetShopsByProductAndCity(productName, cityName);

                if (shops == null || shops.Count == 0)
                {
                    return NotFound("No shops found for the given product and city.");
                }

                // Map to DTOs
                var shopDTOs = shops.Select(shop => new Models.ShopDTO
                {
                    ShopId = shop.ShopId,
                    ShopName = shop.ShopName,
                    EmailId = shop.EmailId,
                    CityId = shop.CityId,
                    Pincode = shop.Pincode,
                    ContactNumber = shop.ContactNumber,
                    FullAddress = shop.FullAddress,
                    Latitude = shop.Latitude,
                    Longitude = shop.Longitude,
                    IsOpen = shop.IsOpen,
                    CreatedAt = shop.CreatedAt,
                    VendorId = shop.VendorId
                }).ToList();

                return Ok(shopDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete]
        public IActionResult RemoveShop(int shopId)
        {
            try
            {
                int status = _repository.RemoveShop(shopId);
                if (status == 1)
                {
                    return Ok("Shop Removed Successfully");
                }
                else if (status == -1)
                {
                    return NotFound("Shop Not Found for Deletion");
                }
                else
                {
                    return BadRequest("Failed to Remove Shop");
                }
            }
            catch (Exception)
            {
                return BadRequest("Failed to Remove Shop");
            }
        }

        [HttpPut("UpdateShopName")]
        public IActionResult UpdateShopName(int shopId, string shopName, string nshopName)
        {
            try
            {
                var status = _repository.UpdateShopName(shopId, shopName, nshopName);
                if (status)
                {
                    return Ok("Shop Name Updated Successfully");
                }
                else
                {
                    return NotFound("ShopName Not Updated");
                }
            }
            catch (Exception)
            {
                return BadRequest("Failed to Update Shop Name");
            }
            
        }



        [HttpPut("contactNumber")]
        public IActionResult UpdateShopContactNumber(int shopId, string contactNumber, string nContactNumber)
        {
            try
            {
                var status = _repository.UpdateShopContactNumber(shopId, nContactNumber, contactNumber);
                if (status)
                {
                    return Ok("Shop Contact Number Updated Successfully");
                }
                else
                {
                    return NotFound("Shop Not Updated - Existing Contact Number Mismatch or Shop Not Found");
                }
            }
            catch (Exception)
            {
                return BadRequest("Failed to Update Shop Contact Number");
            }
        }



        [HttpPut("emailId")]
        public IActionResult UpdateShopEmailId(int shopId, string nemailId,string emailId)
        {
            try
            {
                var status = _repository.UpdateShopEmailId(shopId, nemailId,emailId);
                if (status == true)
                {
                    return Ok("Shop Update Shop EmailId Successfully");
                }
                else
                {
                    return NotFound("Shop Not Update");
                }

            }
            catch (Exception)
            {
                return BadRequest("Failed to Update Shop");
            }
        }


        [HttpPut("isOpen")]
        public IActionResult UpdateShopIsOpen(int shopId, bool isOpen)
        {
            try
            {
                var status = _repository.UpdateShopIsOpen(shopId, isOpen);
                if (status == true)
                {
                    return Ok("oOpeningClosing Updated");
                }
                else
                {
                    return NotFound("Not Update");
                }

            }
            catch (Exception)
            {
                return BadRequest("Failed to Update");
            }
        }

        // For Ratings

        //[HttpGet("sorted-by-rating")]
        //public IActionResult GetShopsSortedByRating()
        //{
        //    try
        //    {
        //        var sortedShops = _repository.GetShopsSortedByRating();
        //        return Ok(sortedShops);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}


    }
}
