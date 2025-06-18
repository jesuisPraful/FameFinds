using FameFindsDAL;
using FameFindsDAL.Models;
using FameFindsWebServices.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace FameFindsWebServicesTests
{
    public class ProductControllerTest
    {
        private readonly Mock<IFameFindsDAL> _mockrepo;
        private readonly ProductController _controller;

        public ProductControllerTest()
        {
            _mockrepo = new Mock<IFameFindsDAL>();
            _controller = new ProductController(_mockrepo.Object);
        }

        [Fact]
        public void GetAllProducts_ReturnsOkResult_WithListOfProducts()
        {
            // Arrange
            var products = new List<Product> {
                new Product { ProductId = 1, ProductName = "Laptop", Description = "Dell", CategoryId = 1, CityId = 1 },
                new Product { ProductId = 2, ProductName = "Phone", Description = "Samsung", CategoryId = 2, CityId = 1 }
            };
            _mockrepo.Setup(r => r.GetAllProducts()).Returns(products);

            // Act
            var result = _controller.GetAllProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<List<Product>>(okResult.Value);
            Assert.Equal(2, returned.Count);
        }

        [Fact]
        public void GetProductsByCity_ReturnsOk_WhenProductsExist()
        {
            // Arrange
            var cityName = "Delhi";
            var products = new List<Product> {
                new Product { ProductId = 1, ProductName = "Camera", CityId = 1, CategoryId = 1 }
            };
            _mockrepo.Setup(r => r.GetProductsByCity(cityName)).Returns(products);

            // Act
            var result = _controller.GetProductsByCity(cityName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<List<Product>>(okResult.Value);
            Assert.Single(returned);
        }

        [Fact]
        public void GetProductsByCity_ReturnsNotFound_WhenNoProducts()
        {
            // Arrange
            var cityName = "Unknown";
            _mockrepo.Setup(r => r.GetProductsByCity(cityName)).Returns(new List<Product>());

            // Act
            var result = _controller.GetProductsByCity(cityName);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No products found for the selected city.", notFoundResult.Value);
        }

        [Fact]
        public void AddProduct_ReturnsOk_WhenProductIsValid()
        {
            // Arrange
            var newProduct = new Product
            {
                ProductName = "TV",
                Description = "Smart TV",
                CategoryId = 1,
                CityId = 1
            };

            _mockrepo.Setup(r => r.AddProduct(It.IsAny<Product>())).Returns(true);
            _controller.ModelState.Clear(); // Ensure model is valid

            // Act
            var result = _controller.AddProduct(newProduct);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Product added successfully", okResult.Value);
        }
    }
}
