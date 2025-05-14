using FameFindsDAL;
using FameFindsDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                        shopOne.CityName = shop.CityName;
                        shopOne.Pincode = shop.Pincode;
                        shopOne.ContactNumber = shop.ContactNumber;
                        shopOne.FullAddress = shop.FullAddress;
                        shopOne.Latitude = shop.Latitude;
                        shopOne.Longitude = shop.Longitude;
                        shopOne.OpeningTime = shop.OpeningTime;
                        shopOne.ClosingTime = shop.ClosingTime;
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
            bool status = false;

            try
            {
                if (ModelState.IsValid)
                {
                    Shop shopOne = new Shop();
                
                    shopOne.ShopName = shop.ShopName;
                    shopOne.EmailId = shop.EmailId;
                    shopOne.CityName = shop.CityName;
                    shopOne.Pincode = shop.Pincode;
                    shopOne.ContactNumber = shop.ContactNumber;
                    shopOne.FullAddress = shop.FullAddress;
                    shopOne.Latitude = shop.Latitude;
                    shopOne.Longitude = shop.Longitude;
                    shopOne.OpeningTime = shop.OpeningTime;
                    shopOne.ClosingTime = shop.ClosingTime;
                    shopOne.VendorId = shop.VendorId;

                    status = _repository.RegisterShop(shopOne);

                    return Ok("Shop Registered Successfully");
                }
                else
                {
                    return BadRequest("Invalid Data");
                }
            }
            catch (Exception)
            {
                status = false;
                return BadRequest("Registration failed.");
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
                    shopOne.CityName = shop.CityName;
                    shopOne.Pincode = shop.Pincode;
                    shopOne.ContactNumber = shop.ContactNumber;
                    shopOne.FullAddress = shop.FullAddress;
                    shopOne.Latitude = shop.Latitude;
                    shopOne.Longitude = shop.Longitude;
                    shopOne.OpeningTime = shop.OpeningTime;
                    shopOne.ClosingTime = shop.ClosingTime;
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
                        shopOne.CityName = shop.CityName;
                        shopOne.Pincode = shop.Pincode;
                        shopOne.ContactNumber = shop.ContactNumber;
                        shopOne.FullAddress = shop.FullAddress;
                        shopOne.Latitude = shop.Latitude;
                        shopOne.Longitude = shop.Longitude;
                        shopOne.OpeningTime = shop.OpeningTime;
                        shopOne.ClosingTime = shop.ClosingTime;
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
                        shopOne.CityName = shop.CityName;
                        shopOne.Pincode = shop.Pincode;
                        shopOne.ContactNumber = shop.ContactNumber;
                        shopOne.FullAddress = shop.FullAddress;
                        shopOne.Latitude = shop.Latitude;
                        shopOne.Longitude = shop.Longitude;
                        shopOne.OpeningTime = shop.OpeningTime;
                        shopOne.ClosingTime = shop.ClosingTime;
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
                        shopOne.CityName = shop.CityName;
                        shopOne.Pincode = shop.Pincode;
                        shopOne.ContactNumber = shop.ContactNumber;
                        shopOne.FullAddress = shop.FullAddress;
                        shopOne.Latitude = shop.Latitude;
                        shopOne.Longitude = shop.Longitude;
                        shopOne.OpeningTime = shop.OpeningTime;
                        shopOne.ClosingTime = shop.ClosingTime;
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
                if (shopList != null || !shopList.Any())
                {
                    foreach (var shop in shopList)
                    {
                        Shop shopOne = new Shop();
                        shopOne.ShopId = shop.ShopId;
                        shopOne.ShopName = shop.ShopName;
                        shopOne.EmailId = shop.EmailId;
                        shopOne.CityName = shop.CityName;
                        shopOne.Pincode = shop.Pincode;
                        shopOne.ContactNumber = shop.ContactNumber;
                        shopOne.FullAddress = shop.FullAddress;
                        shopOne.Latitude = shop.Latitude;
                        shopOne.Longitude = shop.Longitude;
                        shopOne.OpeningTime = shop.OpeningTime;
                        shopOne.ClosingTime = shop.ClosingTime;
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
                    foreach(var shop in shopList)
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

        [HttpPut("contactNumber")]
        public IActionResult UpdateShopContactNumber(int shopId, string contactNumber)
        {
            try
            {
                var status = _repository.UpdateShopContactNumber(shopId, contactNumber);
                if (status==true)
                {
                    return Ok("Shop Update Shop ContactNumber Successfully");
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


        [HttpPut("emailId")]
        public IActionResult UpdateShopEmailId(int shopId, string emailId)
        {
            try
            {
                var status = _repository.UpdateShopEmailId(shopId, emailId);
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



    }
}
