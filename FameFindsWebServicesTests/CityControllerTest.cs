using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FameFindsDAL;
using FameFindsDAL.Models;
using FameFindsWebServices.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FameFindsWebServicesTests
{
    public class CityControllerTest
    {
        private readonly Mock<IFameFindsDAL> _mockrepo;
        private readonly CityController _controller;
        public CityControllerTest()
        {
            _mockrepo = new Mock<IFameFindsDAL>();
            _controller = new CityController(_mockrepo.Object);
        }

        [Fact]
        public void GetAllCities_ReturnsOk_WithCityList()
        {
            // Arrange
            var cities = new List<City>
        {
            new City { CityId = 1, CityName = "Mumbai" },
            new City { CityId = 2, CityName = "Delhi" }
        };

            _mockrepo.Setup(repo => repo.GetAllCities()).Returns(cities);

            // Act
            var result = _controller.GetAllCities();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<City>>(okResult.Value);
            Xunit.Assert.Equal(2, returnValue.Count);
        }
        [Fact]
        public void GetAllCities_Exception()
        {
            // Arrange
            _mockrepo.Setup(repo => repo.GetAllCities()).Throws(new Exception());

            // Act
            var result = _controller.GetAllCities();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Equal("Internal server error", objectResult.Value);
        }


        [Fact]
        public void TestRegisterCity_Success()
        {
            FameFindsWebServices.Models.City city = new FameFindsWebServices.Models.City { CityId = 1, CityName = "Pune" };
            bool status = true;

            _mockrepo
                .Setup(repo => repo.RegisterCity(It.IsAny<City>()))
                .Returns(status);

            _controller.ModelState.Clear(); // Ensure valid model state

            var result = _controller.RegisterCity(city);

            var okResult = Xunit.Assert.IsType<OkObjectResult>(result);
            Xunit.Assert.Equal("City Registered Successfully", okResult.Value);
        }

        [Fact]
        public void TestRegisterCity_Failure()
        {
            FameFindsWebServices.Models.City city = new FameFindsWebServices.Models.City { CityId = 1, CityName = "Pune" };
            bool status = false;

            _mockrepo
                .Setup(repo => repo.RegisterCity(It.IsAny<City>()))
                .Returns(status);

            _controller.ModelState.Clear();

            var result = _controller.RegisterCity(city);

            var badRequestResult = Xunit.Assert.IsType<BadRequestObjectResult>(result);
            Xunit.Assert.Equal("Failed to register city.", badRequestResult.Value);
        }
        [Fact]
        public void TestRegisterCity_ModelInvalid()
        {
            FameFindsWebServices.Models.City city = new FameFindsWebServices.Models.City { CityId = 1, CityName = "" };

            _controller.ModelState.AddModelError("CityName", "Required");

            var result = _controller.RegisterCity(city);

            var badRequestResult = Xunit.Assert.IsType<BadRequestObjectResult>(result);
            Xunit.Assert.Equal("Invalid data.", badRequestResult.Value);
        }
        [Fact]
        public void TestRegisterCity_Exception()
        {
            FameFindsWebServices.Models.City city = new FameFindsWebServices.Models.City { CityId = 1, CityName = "Pune" };

            _mockrepo
                .Setup(repo => repo.RegisterCity(It.IsAny<City>()))
                .Throws(new Exception("DB Error"));

            _controller.ModelState.Clear();

            var result = _controller.RegisterCity(city);

            var badRequestResult = Xunit.Assert.IsType<BadRequestObjectResult>(result);
            Xunit.Assert.Contains("Registration failed", badRequestResult.Value.ToString());
        }


        [Fact]
        public void TestGetCityById_Success()
        {
            // Arrange
            var city = new City { CityId = 1, CityName = "Pune" };

            _mockrepo
                .Setup(repo => repo.GetCityById(It.IsAny<int>()))
                .Returns(city);

            // Act
            var result = _controller.GetCityById(1);

            // Assert
            var okResult = Xunit.Assert.IsType<OkObjectResult>(result);
            Xunit.Assert.Equal(city, okResult.Value);
        }
        [Fact]
        public void TestGetCityById_NotFound()
        {
            // Arrange
            _mockrepo
                .Setup(repo => repo.GetCityById(It.IsAny<int>()))
                .Returns((City)null);

            // Act
            var result = _controller.GetCityById(999);

            // Assert
            var notFoundResult = Xunit.Assert.IsType<NotFoundObjectResult>(result);
            Xunit.Assert.Equal("City not found", notFoundResult.Value);
        }

        [Fact]
        public void TestGetCityById_Exception()
        {
            // Arrange
            _mockrepo
                .Setup(repo => repo.GetCityById(It.IsAny<int>()))
                .Throws(new Exception("Database error"));

            // Act
            var result = _controller.GetCityById(1);

            // Assert
            var objectResult = Xunit.Assert.IsType<ObjectResult>(result);
            Xunit.Assert.Equal(500, objectResult.StatusCode);
            Xunit.Assert.Equal("Internal server error", objectResult.Value);
        }

        [Fact]
        public void TestGetCityByName_Success()
        {
            // Arrange
            var city = new City { CityId = 1, CityName = "Mumbai" };

            _mockrepo
                .Setup(repo => repo.GetCityByName(It.IsAny<string>()))
                .Returns(city);

            // Act
            var result = _controller.GetCityByName("Mumbai");

            // Assert
            var okResult = Xunit.Assert.IsType<OkObjectResult>(result);
            Xunit.Assert.Equal(city, okResult.Value);
        }
        [Fact]
        public void TestGetCityByName_NotFound()
        {
            // Arrange
            _mockrepo
                .Setup(repo => repo.GetCityByName(It.IsAny<string>()))
                .Returns((City)null);

            // Act
            var result = _controller.GetCityByName("UnknownCity");

            // Assert
            var notFoundResult = Xunit.Assert.IsType<NotFoundObjectResult>(result);
            Xunit.Assert.Equal("City not found", notFoundResult.Value);
        }
        [Fact]
        public void TestGetCityByName_Exception()
        {
            // Arrange
            _mockrepo
                .Setup(repo => repo.GetCityByName(It.IsAny<string>()))
                .Throws(new Exception("DB Failure"));

            // Act
            var result = _controller.GetCityByName("Hyderabad");

            // Assert
            var objectResult = Xunit.Assert.IsType<ObjectResult>(result);
            Xunit.Assert.Equal(500, objectResult.StatusCode);
            Xunit.Assert.Equal("Internal server error", objectResult.Value);
        }

        [Fact]
        public void TestDeleteCity_Success()
        {
            // Arrange
            _mockrepo
                .Setup(repo => repo.DeleteCity(It.IsAny<int>()))
                .Returns(true);

            // Act
            var result = _controller.DeleteCity(1);

            // Assert
            var okResult = Xunit.Assert.IsType<OkObjectResult>(result);
            Xunit.Assert.Equal("City deleted successfully.", okResult.Value);
        }
        [Fact]
        public void TestDeleteCity_NotFound()
        {
            // Arrange
            _mockrepo
                .Setup(repo => repo.DeleteCity(It.IsAny<int>()))
                .Returns(false);

            // Act
            var result = _controller.DeleteCity(999);

            // Assert
            var notFoundResult = Xunit.Assert.IsType<NotFoundObjectResult>(result);
            Xunit.Assert.Equal("City not found or could not be deleted.", notFoundResult.Value);
        }
        [Fact]
        public void TestDeleteCity_Exception()
        {
            // Arrange
            _mockrepo
                .Setup(repo => repo.DeleteCity(It.IsAny<int>()))
                .Throws(new Exception("Database failure"));

            // Act
            var result = _controller.DeleteCity(1);

            // Assert
            var objectResult = Xunit.Assert.IsType<ObjectResult>(result);
            Xunit.Assert.Equal(500, objectResult.StatusCode);
            Xunit.Assert.Contains("Error while deleting city", objectResult.Value.ToString());
        }

    }
}
