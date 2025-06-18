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
using Newtonsoft.Json;
using Xunit;


namespace FameFindsWebServicesTests
{
    public class ShopControllerTest
    {
        private readonly Mock<IFameFindsDAL> _mockrepo;
        private readonly ShopController _controller;

        public ShopControllerTest()
        {
            _mockrepo = new Mock<IFameFindsDAL>();
            _controller = new ShopController(_mockrepo.Object);
        }



        [Fact]
        public void GetAllShops_ReturnsListOfShops_WhenShopsExist()
        {
            // Arrange
            var expectedShops = new List<Shop>
        {
            new Shop { ShopId = 1, ShopName = "Shop 1" },
            new Shop { ShopId = 2, ShopName = "Shop 2" }
        };

            _mockrepo
                .Setup(repo => repo.GetAllShops())
                .Returns(expectedShops);

            // Act
            var result = _controller.GetAllShops();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedShops = Assert.IsType<List<Shop>>(okResult.Value);
            Assert.Equal(expectedShops.Count, returnedShops.Count);
            Assert.Equal(expectedShops[0].ShopName, returnedShops[0].ShopName);
        }

        [Fact]
        public void GetAllShops_ReturnsEmptyList_WhenNoShopsExist()
        {
            // Arrange
            var emptyShopList = new List<Shop>();

            _mockrepo
                .Setup(repo => repo.GetAllShops())
                .Returns(emptyShopList);

            // Act
            var result = _controller.GetAllShops();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedShops = Assert.IsType<List<Shop>>(okResult.Value);
            Assert.Empty(returnedShops);
        }

        [Fact]
        public void GetAllShops_ReturnsBadRequest_WhenRepositoryThrowsException()
        {
            // Arrange
            _mockrepo
                .Setup(repo => repo.GetAllShops())
                .Throws(new Exception());

            // Act
            var result = _controller.GetAllShops();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to retrieve shops", badRequestResult.Value);
        }

        [Fact]
        public void RegisterShop_ReturnsOk_WhenRegistrationSuccessful()
        {
            // Arrange
            var inputShop = new FameFindsWebServices.Models.Shop
            {
                ShopName = "Test Shop",
                EmailId = "test@example.com",
                CityId = 1,
                Pincode = "123456",
                ContactNumber = "9876543210",
                FullAddress = "123 Test Street",
                Latitude = 12,
                Longitude = 56,
                IsOpen = true,
                VendorId = 2
            };

            _mockrepo.Setup(repo => repo.RegisterShop(It.IsAny<FameFindsDAL.Models.Shop>()))
                     .Returns(true);

            // Act
            var result = _controller.RegisterShop(inputShop);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Convert anonymous object to JSON, then back to dynamic
            var json = JsonConvert.SerializeObject(okResult.Value);
            dynamic response = JsonConvert.DeserializeObject<dynamic>(json);

            Assert.Equal("Shop Registered Successfully", (string)response.message);
        }
 

        [Fact]
        public void RegisterShop_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var inputShop = new FameFindsWebServices.Models.Shop
            {
                ShopName = null, // Invalid field to trigger ModelState error
                EmailId = "invalid@example.com"
            };

            _controller.ModelState.AddModelError("ShopName", "Required");

            // Act
            var result = _controller.RegisterShop(inputShop);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            var json = JsonConvert.SerializeObject(badRequestResult.Value);
            dynamic response = JsonConvert.DeserializeObject<dynamic>(json);

            Assert.Equal("Invalid Data", (string)response.message);
        }




    [Fact]
        public void RegisterShop_ReturnsBadRequest_WhenRegistrationFails()
        {
            // Arrange
            var inputShop = new FameFindsWebServices.Models.Shop
            {
                ShopName = "Test Shop",
                EmailId = "test@example.com",
                CityId = 1,
                Pincode = "123456",
                ContactNumber = "9876543210",
                FullAddress = "123 Test Street",
                Latitude = 12,
                Longitude = 56,
                IsOpen = true,
                VendorId = 2
            };

            _mockrepo.Setup(repo => repo.RegisterShop(It.IsAny<FameFindsDAL.Models.Shop>()))
                     .Returns(false); // simulate failure

            // Act
            var result = _controller.RegisterShop(inputShop);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            var json = JsonConvert.SerializeObject(badRequestResult.Value);
            dynamic response = JsonConvert.DeserializeObject<dynamic>(json);

            Assert.Equal("Failed to register shop.", (string)response.message);
        }


        [Fact]
        public void RegisterShop_ReturnsBadRequest_WhenExceptionThrown()
        {
            // Arrange
            var inputShop = new FameFindsWebServices.Models.Shop
            {
                ShopName = "Test Shop",
                EmailId = "test@example.com",
                CityId = 1,
                Pincode = "123456",
                ContactNumber = "9876543210",
                FullAddress = "123 Test Street",
                Latitude = 12,
                Longitude = 56,
                IsOpen = true,
                VendorId = 2
            };

            _mockrepo.Setup(repo => repo.RegisterShop(It.IsAny<FameFindsDAL.Models.Shop>()))
                     .Throws(new Exception("Some exception"));

            // Act
            var result = _controller.RegisterShop(inputShop);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            var json = JsonConvert.SerializeObject(badRequestResult.Value);
            dynamic response = JsonConvert.DeserializeObject<dynamic>(json);

            Assert.Equal("Registration failed.", (string)response.message);
            Assert.Equal("Some exception", (string)response.error);
        }



        [Fact]
        public void GetShopsByShopId_ReturnsOk_WhenShopExists()
        {
            // Arrange
            int shopId = 1;
            var dalShop = new FameFindsDAL.Models.Shop
            {
                ShopId = shopId,
                ShopName = "Test Shop",
                EmailId = "test@example.com",
                CityId = 10,
                Pincode = "123456",
                ContactNumber = "9876543210",
                FullAddress = "Test Address",
                Latitude = 12,
                Longitude = 56,
                VendorId = 2
            };

            _mockrepo.Setup(repo => repo.GetShopsByShopId(shopId)).Returns(dalShop);

            // Act
            var result = _controller.GetShopsByShopId(shopId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<FameFindsDAL.Models.Shop>(okResult.Value);

            Assert.Equal(dalShop.ShopId, returnValue.ShopId);
            Assert.Equal(dalShop.ShopName, returnValue.ShopName);
        }

        [Fact]
        public void GetShopsByShopId_ReturnsNotFound_WhenShopDoesNotExist()
        {
            // Arrange
            int shopId = 2;
            _mockrepo.Setup(repo => repo.GetShopsByShopId(shopId)).Returns((FameFindsDAL.Models.Shop)null);

            // Act
            var result = _controller.GetShopsByShopId(shopId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Shop Not Found", notFoundResult.Value);
        }


        [Fact]
       
        public void GetShopsByVendorId_ReturnsOk_WithSingleShop()
        {
            // Arrange
            int vendorId = 1;

            var dalShops = new List<FameFindsDAL.Models.Shop>
            {
                new FameFindsDAL.Models.Shop
                {
                    ShopId = 1,
                    ShopName = "Test Shop",
                    EmailId = "test@example.com",
                    CityId = 10,
                    Pincode = "123456",
                    ContactNumber = "9000000001",
                    FullAddress = "123 Test Street",
                    Latitude = 12,
                    Longitude = 56,
                    VendorId = vendorId
                }
            };

            _mockrepo.Setup(repo => repo.GetShopsByVendorId(vendorId)).Returns(dalShops);

            // Act
            var result = _controller.GetShopsByVendorId(vendorId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnList = okResult.Value as IEnumerable<dynamic>;

            Assert.NotNull(returnList);
            var shopList = returnList.ToList();

            Assert.Single(shopList);
            Assert.Equal("Test Shop", (string)shopList[0].ShopName);
        }

        [Fact]
        public void GetShopsByVendorId_ReturnsBadRequest_OnException()
        {
            // Arrange
            int vendorId = 1;
            _mockrepo.Setup(repo => repo.GetShopsByVendorId(vendorId))
                     .Throws(new Exception("Database error"));

            // Act
            var result = _controller.GetShopsByVendorId(vendorId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to retrieve shops", badRequestResult.Value?.ToString());
        }

        [Fact]
        public void GetShopIdsByVendorId_ReturnsOk_WithShopIds()
        {
            // Arrange
            int vendorId = 1;
            var expectedShopIds = new List<int> { 101, 102 };

            _mockrepo.Setup(repo => repo.GetShopIdsByVendorId(vendorId)).Returns(expectedShopIds);

            // Act
            var result = _controller.GetShopIdsByVendorId(vendorId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualShopIds = Assert.IsType<List<int>>(okResult.Value);
            Assert.Equal(expectedShopIds.Count, actualShopIds.Count);
            Assert.Equal(expectedShopIds, actualShopIds);
        }

        [Fact]
        public void GetShopIdsByVendorId_ReturnsNotFound_WhenNoShopsExist()
        {
            // Arrange
            int vendorId = 1;
            _mockrepo.Setup(repo => repo.GetShopIdsByVendorId(vendorId)).Returns(new List<int>());

            // Act
            var result = _controller.GetShopIdsByVendorId(vendorId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No shops found for the given vendor.", notFoundResult.Value);
        }

        [Fact]
        public void GetShopIdsByVendorId_ReturnsBadRequest_OnException()
        {
            // Arrange
            int vendorId = 1;
            _mockrepo.Setup(repo => repo.GetShopIdsByVendorId(vendorId)).Throws(new Exception("Database error"));

            // Act
            var result = _controller.GetShopIdsByVendorId(vendorId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to retrieve shop IDs.", badRequestResult.Value);
        }


        [Fact]
        public void GetShopsByShopName_ReturnsOk_WithShops()
        {
            // Arrange
            string shopName = "Test Shop";
            var dalShops = new List<FameFindsDAL.Models.Shop>
    {
            new FameFindsDAL.Models.Shop
            {
                ShopId = 1,
                ShopName = shopName,
                EmailId = "test@example.com",
                CityId = 10,
                Pincode = "123456",
                ContactNumber = "9876543210",
                FullAddress = "Test Address",
                Latitude = 12,
                Longitude = 56,
                VendorId = 2
            }
        };

            _mockrepo.Setup(repo => repo.GetShopsByShopName(shopName)).Returns(dalShops);

            // Act
            var result = _controller.GetShopsByShopName(shopName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Use dynamic to safely inspect list contents
            var returnList = okResult.Value as IEnumerable<object>;
            Assert.NotNull(returnList);

            var shop = returnList.FirstOrDefault() as dynamic;
            Assert.NotNull(shop);
            Assert.Equal(shopName, (string)shop.ShopName);
        }




        [Fact]
        public void GetShopsByShopName_ReturnsBadRequest_OnException()
        {
            // Arrange
            string shopName = "ErrorShop";
            _mockrepo.Setup(repo => repo.GetShopsByShopName(shopName)).Throws(new Exception("DB error"));

            // Act
            var result = _controller.GetShopsByShopName(shopName);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to retrieve shops", badRequestResult.Value);
        }



        [Fact]
        public void GetShopsByCityName_ReturnsOk_WithShops()
        {
            // Arrange
            string cityName = "Delhi";
            var dalShops = new List<FameFindsDAL.Models.Shop>
    {
        new FameFindsDAL.Models.Shop
        {
            ShopId = 1,
            ShopName = "Test Shop",
            EmailId = "test@example.com",
            CityId = 10,
            Pincode = "123456",
            ContactNumber = "9876543210",
            FullAddress = "Some Address",
            Latitude = 12,
            Longitude = 34,
            VendorId = 2
        }
    };

            _mockrepo.Setup(repo => repo.GetShopsByCityName(cityName)).Returns(dalShops);

            // Act
            var result = _controller.GetShopsByCityName(cityName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnList = okResult.Value as IEnumerable<object>;
            Assert.NotNull(returnList);

            var shop = returnList.FirstOrDefault() as dynamic;
            Assert.NotNull(shop);
            Assert.Equal("Test Shop", (string)shop.ShopName);
        }


        [Fact]
        public void GetShopsByCityName_ReturnsBadRequest_OnException()
        {
            // Arrange
            string cityName = "Delhi";
            _mockrepo.Setup(repo => repo.GetShopsByCityName(cityName))
                     .Throws(new Exception("Database error"));

            // Act
            var result = _controller.GetShopsByCityName(cityName);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to retrieve shops", badRequestResult.Value);
        }

        [Fact]
        public void GetShopsByProduct_ReturnsBadRequest_OnException()
        {
            // Arrange
            string productName = "Shoes";
            _mockrepo.Setup(repo => repo.GetShopsByProduct(productName)).Throws(new Exception("Database failure"));

            // Act
            var result = _controller.GetShopsByProduct(productName);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to retrieve shops", badRequest.Value);
        }




        [Fact]
        public void GetShopByCategoryName_ReturnsOk_WithShops()
        {
            // Arrange
            string categoryName = "Electronics";
            var dalShops = new List<FameFindsDAL.Models.Shop>
    {
        new FameFindsDAL.Models.Shop
        {
            ShopId = 101,
            ShopName = "ElectroWorld",
            EmailId = "electro@example.com",
            CityId = 1,
            Pincode = "123456",
            ContactNumber = "9876543210",
            FullAddress = "Sector 21, Gurgaon",
            Latitude = 28,
            Longitude = 77,
            VendorId = 9
        }
    };

            _mockrepo.Setup(repo => repo.GetShopByCategoryName(categoryName)).Returns(dalShops);

            // Act
            var result = _controller.GetShopByCategoryName(categoryName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnList = okResult.Value as IEnumerable<object>;
            Assert.NotNull(returnList);
            var list = returnList.ToList();
            Assert.Single(list);

            dynamic shop = list[0];
            Assert.Equal("ElectroWorld", (string)shop.ShopName);
        }


        [Fact]
        public void UpdateShopName_ReturnsOk_WhenUpdated()
        {
            // Arrange
            int shopId = 1;
            string oldName = "Old Shop";
            string newName = "New Shop";
            _mockrepo.Setup(r => r.UpdateShopName(shopId, oldName, newName)).Returns(true);

            // Act
            var result = _controller.UpdateShopName(shopId, oldName, newName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Shop Name Updated Successfully", okResult.Value);
        }

        [Fact]
        public void UpdateShopName_ReturnsNotFound_WhenUpdateFails()
        {
            // Arrange
            int shopId = 1;
            string oldName = "Old Shop";
            string newName = "New Shop";
            _mockrepo.Setup(r => r.UpdateShopName(shopId, oldName, newName)).Returns(false);

            // Act
            var result = _controller.UpdateShopName(shopId, oldName, newName);

            // Assert
            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("ShopName Not Updated", notFound.Value);
        }


        [Fact]
        public void UpdateShopContactNumber_ReturnsOk_WhenUpdated()
        {
            // Arrange
            int shopId = 2;
            string current = "9999999999";
            string updated = "8888888888";
            _mockrepo.Setup(r => r.UpdateShopContactNumber(shopId, updated, current)).Returns(true);

            // Act
            var result = _controller.UpdateShopContactNumber(shopId, current, updated);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Shop Contact Number Updated Successfully", ok.Value);
        }

        [Fact]
        public void UpdateShopContactNumber_ReturnsNotFound_WhenUpdateFails()
        {
            // Arrange
            int shopId = 2;
            string current = "9999999999";
            string updated = "8888888888";
            _mockrepo.Setup(r => r.UpdateShopContactNumber(shopId, updated, current)).Returns(false);

            // Act
            var result = _controller.UpdateShopContactNumber(shopId, current, updated);

            // Assert
            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Shop Not Updated - Existing Contact Number Mismatch or Shop Not Found", notFound.Value);
        }


        [Fact]
        public void UpdateShopEmailId_ReturnsOk_WhenUpdated()
        {
            // Arrange
            int shopId = 3;
            string oldEmail = "old@example.com";
            string newEmail = "new@example.com";
            _mockrepo.Setup(r => r.UpdateShopEmailId(shopId, newEmail, oldEmail)).Returns(true);

            // Act
            var result = _controller.UpdateShopEmailId(shopId, newEmail, oldEmail);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Shop Update Shop EmailId Successfully", ok.Value);
        }

        [Fact]
        public void UpdateShopEmailId_ReturnsNotFound_WhenUpdateFails()
        {
            // Arrange
            int shopId = 3;
            string oldEmail = "old@example.com";
            string newEmail = "new@example.com";
            _mockrepo.Setup(r => r.UpdateShopEmailId(shopId, newEmail, oldEmail)).Returns(false);

            // Act
            var result = _controller.UpdateShopEmailId(shopId, newEmail, oldEmail);

            // Assert
            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Shop Not Update", notFound.Value);
        }


        [Fact]
        public void UpdateShopIsOpen_ReturnsOk_WhenUpdateIsSuccessful()
        {
            // Arrange
            int shopId = 1;
            bool isOpen = true;

            _mockrepo.Setup(r => r.UpdateShopIsOpen(shopId, isOpen)).Returns(true);

            // Act
            var result = _controller.UpdateShopIsOpen(shopId, isOpen);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("oOpeningClosing Updated", okResult.Value);
        }

        [Fact]
        public void UpdateShopIsOpen_ReturnsNotFound_WhenUpdateFails()
        {
            // Arrange
            int shopId = 1;
            bool isOpen = false;

            _mockrepo.Setup(r => r.UpdateShopIsOpen(shopId, isOpen)).Returns(false);

            // Act
            var result = _controller.UpdateShopIsOpen(shopId, isOpen);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Not Update", notFoundResult.Value);
        }

        [Fact]
        public void GetAverageRating_ReturnsOk_WithValidAverage()
        {
            // Arrange
            int shopId = 1;
            double expectedAverage = 4.2;

            _mockrepo.Setup(r => r.GetAverageRatingByShopId(shopId)).Returns(expectedAverage);

            // Act
            var result = _controller.GetAverageRating(shopId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedAverage, okResult.Value);
        }


    }

}
