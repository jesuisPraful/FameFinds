using FameFindsDAL;
using FameFindsDAL.Models;
using FameFindsWebServices.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FameFindsWebServicesTests
{
    public class ShopProductControllerTest
    {
        private readonly Mock<IFameFindsDAL> _mockrepo;
        private readonly ShopProductController _controller;

        public ShopProductControllerTest()
        {
            _mockrepo = new Mock<IFameFindsDAL>();
            _controller = new ShopProductController(_mockrepo.Object);
        }


        [Fact]
        public void GetProductsByShopId_ReturnsOk_WhenProductsExist()
        {
            // Arrange
            int shopId = 1;
            var products = new List<ShopProduct>
            {
            new ShopProduct { ShopId = 1, ProductId = 101, Price = 299.99m, Stock = 50 }
            };

            _mockrepo.Setup(r => r.GetProductsByShopId(shopId)).Returns(products);

            // Act
            var result = _controller.GetProductsByShopId(shopId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProducts = Assert.IsAssignableFrom<List<ShopProduct>>(okResult.Value);
            Assert.Single(returnedProducts);
        }

        [Fact]
        public void GetProductsByShopId_ReturnsNotFound_WhenNoProducts()
        {
            // Arrange
            int shopId = 2;
            _mockrepo.Setup(r => r.GetProductsByShopId(shopId)).Returns(new List<ShopProduct>());

            // Act
            var result = _controller.GetProductsByShopId(shopId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No products found for this shop.", notFoundResult.Value);
        }

       
    }

}
