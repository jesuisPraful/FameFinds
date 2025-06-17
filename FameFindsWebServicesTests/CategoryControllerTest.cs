using FameFindsDAL;
using FameFindsDAL.Models;
using FameFindsWebServices.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace FameFindsWebServicesTests
{
    public class CategoryControllerTest
    {
        private readonly Mock<IFameFindsDAL> _mockrepo;
        private readonly CategoryController _controller;
        public CategoryControllerTest()
        {
            _mockrepo = new Mock<IFameFindsDAL>();
            _controller = new CategoryController(_mockrepo.Object);
        }
        [Fact]
        public void GetAllCategories_Success()
        {
            // Arrange
            var mockCategories = new List<Category>
        {
            new Category { CategoryId = 1, CategoryName = "Electronics" },
            new Category { CategoryId = 2, CategoryName = "Clothing" }
        };
            _mockrepo.Setup(repo => repo.GetAllCategories()).Returns(mockCategories);

            // Act
            var result = _controller.GetAllCategories();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Category>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }
        [Fact]
        public void GetAllCategories_Success_WithEmptyList()
        {
            // Arrange
            var mockCategories = new List<Category>();
            _mockrepo.Setup(repo => repo.GetAllCategories()).Returns(mockCategories);

            // Act
            var result = _controller.GetAllCategories();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Category>>(okResult.Value);
            Assert.Empty(returnValue);
        }
        [Fact]
        public void GetAllCategories_Exception()
        {
            // Arrange
            _mockrepo.Setup(repo => repo.GetAllCategories()).Throws(new Exception("DB error"));

            // Act
            var result = _controller.GetAllCategories();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Equal("Internal server error", objectResult.Value);
        }

    }
}