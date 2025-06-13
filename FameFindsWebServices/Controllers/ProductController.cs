using FameFindsDAL;
using FameFindsDAL.Models;
//using FameFindsWebServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FameFindsWebServices.Controllers
{
    [Route("api/[controller]/[Action]")]
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
        //[HttpPost]
        //public IActionResult AddProduct(Models.Product product)
        //{
        //    bool status = false;
        //    try
        //    {
        //        //    if (ModelState.IsValid)
        //        //    {
        //        //        FameFindsDAL.Models.Product productOne = new FameFindsDAL.Models.Product
        //        //        {
        //        //            ProductName = product.ProductName,
        //        //            Description = product.Description,
        //        //            CategoryId = product.CategoryId,
        //        //            CityId = product.CityId
        //        //        };
        //        //        status = _repository.AddProduct(productOne);
        //        //        if (status)
        //        //        {
        //        //            return Ok("Product added successfully");
        //        //        }
        //        //        else
        //        //        {
        //        //            return BadRequest("Failed to add product");
        //        //        }
        //        //        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
        //        //        return BadRequest(string.Join("; ", errors));
        //        //    }
        //        //    else
        //        //    {
        //        //        return BadRequest("Model state is not valid");
        //        //    }
        //        //}
        //        //catch (Exception ex)
        //        //{
        //        //    return StatusCode(500, $"Internal server error: {ex.Message}");
        //        //}
        //        FameFindsDAL.Models.Product dbProduct = new FameFindsDAL.Models.Product
        //        {
        //            ProductName = product.ProductName,
        //            Description = product.Description,
        //            CategoryId = product.CategoryId,
        //            CityId = product.CityId
        //        };

        //        bool status = _repository.AddProduct(dbProduct);

        //        if (status)
        //        {
        //            return Ok("Product added successfully");
        //        }
        //        else
        //        {
        //            return BadRequest("Failed to add product");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}
        [HttpPost]
        public IActionResult AddProduct(FameFindsWebServices.Models.Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // This returns all validation errors in a readable format
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                  .Select(e => e.ErrorMessage);
                    return BadRequest(string.Join("; ", errors));
                }

                // Map from API model to DAL model
                FameFindsDAL.Models.Product dbProduct = new FameFindsDAL.Models.Product
                {
                    ProductName = product.ProductName,
                    Description = product.Description,
                    CategoryId = product.CategoryId,
                    CityId = product.CityId
                };

                bool status = _repository.AddProduct(dbProduct);

                if (status)
                {
                    return Ok("Product added successfully");
                }
                else
                {
                    return BadRequest("Failed to add product");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
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
                    Product productOne = new Product();
                    productOne.ProductId = product.ProductId;
                    productOne.ProductName = product.ProductName;
                    productOne.Description = product.Description;
                    productOne.CategoryId = product.CategoryId;
                    status = _repository.UpdateProduct(productOne);
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
