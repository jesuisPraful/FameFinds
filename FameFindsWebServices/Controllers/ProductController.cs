using FameFindsDAL;
using FameFindsDAL.Models;

//using FameFindsWebServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FameFindsWebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly FameFindsRepository _repository;
        public ProductController(FameFindsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _repository.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult AddProduct(Models.Product product)
        {
            bool status = false;
            try
            {
                if (ModelState.IsValid)
                {
                    Product product1 = new Product();
                    product1.ProductName = product.ProductName;
                    product1.Description = product.Description;
                    product1.CategoryId = product.CategoryId;
                    status = _repository.AddProduct(product1);
                    if (status)
                    {
                        return Ok("Product added successfully");
                    }
                    else
                    {
                        return BadRequest("Failed to add product");
                    }
                }
                else
                {
                    return BadRequest("Model state is not valid");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut]
        public IActionResult UpdateProduct(Models.Product product)
        {
            bool status = false;
            try
            {
                if (ModelState.IsValid)
                {
                    Product product1 = new Product();
                    product1.ProductId = product.ProductId;
                    product1.ProductName = product.ProductName;
                    product1.Description = product.Description;
                    product1.CategoryId = product.CategoryId;
                    status = _repository.UpdateProduct(product1);
                    if (status)
                    {
                        return Ok("Product updated successfully");
                    }
                    else
                    {
                        return BadRequest("Failed to update product");
                    }
                }
                else
                {
                    return BadRequest("Model state is not valid");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete]
        public IActionResult DeleteProduct(int ProductId)
        {
            bool status = false;
            try
            {
                status = _repository.DeleteProduct(ProductId);
                if (status)
                {
                    return Ok("Product deleted successfully");
                }
                else
                {
                    return BadRequest("Failed to delete product");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{ProductName}")]
        public IActionResult GetProductByName(string ProductName)
        {
            try
            {
                var product = _repository.GetProductByName(ProductName);
                if (product != null)
                {
                    return Ok(product);
                }
                else
                {
                    return NotFound("Product not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("GetProducts/{CategoryName}")]
        public IActionResult GetProductByCategory(string CategoryName)
        {
            try
            {
                var products = _repository.GetProductsByCategoryName(CategoryName);
                if (products != null)
                {
                    return Ok(products);
                }
                else
                {
                    return NotFound("Products not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
