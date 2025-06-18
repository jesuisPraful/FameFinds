using FameFindsDAL;
using FameFindsDAL.Models;
using FameFindsWebServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProduct = FameFindsWebServices.Models.ShopProduct;
//using ShopProduct = FameFindsWebServices.Models.ShopProduct;

namespace FameFindsWebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopProductController : ControllerBase
    {
        private readonly IFameFindsDAL _repository;
        public ShopProductController(IFameFindsDAL repository)
        {
            _repository = repository;
        }
        [HttpGet("shop/{shopId}")]
        public IActionResult GetProductsByShopId(int shopId)
        {
            try
            {
                var products = _repository.GetProductsByShopId(shopId);
                if (products != null && products.Count > 0)
                    return Ok(products);
                else
                    return NotFound("No products found for this shop.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fetch error: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving data.");
            }
        }
        [HttpPost("add")]
        public IActionResult AddShopProduct([FromBody] ShopProduct shopProduct)
        {
            bool status = false;
            try
            {
                if (ModelState.IsValid)
                {
                    var shopProductOne = new FameFindsDAL.Models.ShopProduct
                    {
                        ShopId = shopProduct.ShopId,
                        ProductId = shopProduct.ProductId,
                        Price = shopProduct.Price,
                        Stock = shopProduct.Stock
                    };
                    status = _repository.AddShopProduct(shopProductOne);

                    if (status)
                        return Ok(new { message = "Product added to shop." });

                    // return Ok(shopProductOne);
                    // return Ok("Product added to shop.");
                    else
                        return BadRequest("Failed to add product");
                }
                else
                    return BadRequest("Invalid Details");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
         [HttpPut("update/{id}")]
        public IActionResult UpdateShopProduct(int id, ShopProduct updatedProduct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool status = _repository.UpdateShopProduct(id, updatedProduct.Price, updatedProduct.Stock);
                    if (status)
                        return Ok("Product updated.");
                    else
                        return NotFound("Product not found.");
                }
                else
                    return BadRequest("Invalid Details");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update error: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpPut("update/price/{id}")]
        public IActionResult UpdateShopProductPrice(int id, [FromBody] decimal price)
        {
            bool status = false;
            try
            {
                status = _repository.UpdateShopProductPrice(id, price);
                if (status)
                    return Ok("Product price updated.");
                else
                    return NotFound("Product not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update price error: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpPut("update/stock/{id}")]
        public IActionResult UpdateShopProductStock(int id, [FromBody] int stock)
        {
            bool status = false;
            try
            {
                status = _repository.UpdateShopProductStock(id, stock);
                if (status)
                    return Ok("Product stock updated.");
                else
                    return NotFound("Product not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update stock error: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteShopProduct(int id)
        {
            bool status = false;
            try
            {
                status = _repository.DeleteShopProduct(id);
                if (status)
                    return Ok(new { message = "Product deleted" });
                else
                    return NotFound("Product not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Delete error: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
