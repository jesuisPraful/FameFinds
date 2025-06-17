using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FameFindsDAL;
using FameFindsWebServices.Controllers;
using FameFindsWebServices.Services;
using Microsoft.AspNetCore.Mvc;
using FameFindsDAL.Models;
using FameFindsWebServices.Models;

namespace FameFindsWebServicesTests
{
    public class CustomerControllerTest
    {
        private readonly Mock<IFameFindsDAL> _mockrepo;
        private readonly CustomerController _controller;
        private readonly Mock<IAuthenticationService> _mockAuthService;
        private readonly Mock<IEmailService> _mockEmailService;
        public CustomerControllerTest()
        {
            _mockrepo = new Mock<IFameFindsDAL>();
            _mockAuthService = new Mock<IAuthenticationService>();
            _mockEmailService = new Mock<IEmailService>();
            _controller = new CustomerController(_mockrepo.Object, _mockAuthService.Object, _mockEmailService.Object);
        }

        [Fact]
        public void TestGetAllCustomers_Success()
        {
            // Arrange
            var customerList = new List<FameFindsDAL.Models.Customer>
    {
        new FameFindsDAL.Models.Customer { CustomerId = 1, FullName = "John Doe", Email = "john@example.com", PhoneNumber = "1234567890" },
        new FameFindsDAL.Models.Customer { CustomerId = 2, FullName = "Jane Smith", Email = "jane@example.com", PhoneNumber = "0987654321" }
    };

            _mockrepo
                .Setup(repo => repo.GetAllCustomers())
                .Returns(customerList);

            // Act
            var result = _controller.GetAllCustomers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);

            var customerResult = okResult.Value as List<FameFindsDAL.Models.Customer>;
            Assert.NotNull(customerResult);
            Assert.Equal(2, customerResult.Count);
            Assert.Equal("John Doe", customerResult[0].FullName);
        }

        [Fact]
        public void TestGetAllCustomers_NullList()
        {
            // Arrange
            _mockrepo
                .Setup(repo => repo.GetAllCustomers())
                .Returns((List<FameFindsDAL.Models.Customer>)null);

            // Act
            var result = _controller.GetAllCustomers();

            // Assert
            var okResult = Xunit.Assert.IsType<OkObjectResult>(result);
            var customers = Xunit.Assert.IsType<List<FameFindsDAL.Models.Customer>>(okResult.Value);
            Xunit.Assert.Empty(customers); // since the list is not populated if null, returns empty
        }

        [Fact]
        public void TestGetAllCustomers_Exception()
        {
            // Arrange
            _mockrepo
                .Setup(repo => repo.GetAllCustomers())
                .Throws(new Exception("DB error"));

            // Act
            var result = _controller.GetAllCustomers();

            // Assert
            var badRequestResult = Xunit.Assert.IsType<BadRequestObjectResult>(result);
            Xunit.Assert.Equal("Failed to retrieve customers", badRequestResult.Value);
        }


        [Fact]
        public void TestRegisterCustomer_Success()
        {
            // Arrange
            var customerRegister = new CustomerRegister
            {
                FullName = "Ammu",
                Email = "ammu@example.com",
                PhoneNumber = "9876543210",
                Password = "password123"
            };

            _mockAuthService.Setup(service => service.Register(It.IsAny<FameFindsWebServices.Models.Customer>()))
                            .Returns(true);

            // Act
            var result = _controller.RegisterCustomer(customerRegister);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User Registered Successfully", okResult.Value);
        }
        [Fact]
        public void TestRegisterCustomer_FailedFromService()
        {
            // Arrange
            var customerRegister = new CustomerRegister
            {
                FullName = "Ammu",
                Email = "ammu@example.com",
                PhoneNumber = "9876543210",
                Password = "password123"
            };

            _mockAuthService.Setup(service => service.Register(It.IsAny<FameFindsWebServices.Models.Customer>()))
                            .Returns(false);

            // Act
            var result = _controller.RegisterCustomer(customerRegister);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Registration failed", badRequest.Value);
        }
        [Fact]
        public void TestRegisterCustomer_InvalidModelState()
        {
            // Arrange
            _controller.ModelState.AddModelError("Email", "Email is required");

            var customer = new CustomerRegister
            {
                FullName = "Test User",
                PhoneNumber = "9876543210",
                Password = "pass123"
                // Email is intentionally missing
            };

            // Act
            var result = _controller.RegisterCustomer(customer);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            // Get the returned anonymous object
            var response = badRequestResult.Value;

            // Use reflection or dynamic to inspect anonymous object
            var messageProp = response.GetType().GetProperty("message")?.GetValue(response, null)?.ToString();
            var errorsProp = response.GetType().GetProperty("errors")?.GetValue(response, null) as IEnumerable<string>;

            Assert.Equal("Invalid Data", messageProp);
            Assert.Contains("Email is required", errorsProp);
        }
        [Fact]
        public void TestRegisterCustomer_ExceptionThrown()
        {
            // Arrange
            var customerRegister = new CustomerRegister
            {
                FullName = "Ammu",
                Email = "ammu@example.com",
                PhoneNumber = "9876543210",
                Password = "password123"
            };

            _mockAuthService.Setup(service => service.Register(It.IsAny<FameFindsWebServices.Models.Customer>()))
                            .Throws(new Exception("DB Error"));

            // Act
            var result = _controller.RegisterCustomer(customerRegister);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Registration failed.", badRequest.Value);
        }

        [Fact]
        public void TestLogin_Success()
        {

            _mockAuthService
                .Setup(service => service.Login(It.IsAny<FameFindsWebServices.Models.Customer>()))
                .Returns(true); // Login is successful

            // Act
            var result = _controller.Login("test@example.com", "hashedpassword");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(true, okResult.Value);
        }
        [Fact]
        public void TestLogin_InvalidCredentials_ReturnsUnauthorized()
        {

            _mockAuthService
                .Setup(service => service.Login(It.IsAny<FameFindsWebServices.Models.Customer>()))
                .Returns(false); // login failed

            // Act
            var result = _controller.Login("invalid@example.com", "wrongpassword");

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid email or password.", unauthorizedResult.Value);
        }

        [Fact]
        public void TestLogin_Exception()
        {
            _mockAuthService
                .Setup(service => service.Login(It.IsAny<FameFindsWebServices.Models.Customer>()))
                .Throws(new Exception("DB failure"));

            // Act
            var result = _controller.Login("test@example.com", "pass");

            // Assert
            var statusResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusResult.StatusCode);
            Assert.Equal("An error occurred while processing your request.", statusResult.Value);
        }

        [Fact]
        public void UpdateCustomer_ValidModel_ReturnsOk()
        {
            var customer = new FameFindsWebServices.Models.Customer { CustomerId = 1, FullName = "John", Email = "john@example.com", PhoneNumber = "1234567890" };
            _controller.ModelState.Clear();

            _mockrepo.Setup(r => r.UpdateCustomer(It.IsAny<FameFindsDAL.Models.Customer>())).Returns(1);

            var result = _controller.UpdateCustomer(customer);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Customer Updated Successfully", okResult.Value);
        }
        [Fact]
        public void UpdateCustomer_NotFound()
        {
            var customer = new FameFindsWebServices.Models.Customer { CustomerId = 999, FullName = "Ghost", Email = "ghost@example.com", PhoneNumber = "123" };
            _controller.ModelState.Clear();

            _mockrepo.Setup(r => r.UpdateCustomer(It.IsAny<FameFindsDAL.Models.Customer>())).Returns(-1);

            var result = _controller.UpdateCustomer(customer);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Customer Not Found for Update", notFoundResult.Value);
        }
        [Fact]
        public void UpdateCustomer_Failure()
        {
            var customer = new FameFindsWebServices.Models.Customer { CustomerId = 1, FullName = "Fail", Email = "fail@example.com", PhoneNumber = "999" };
            _controller.ModelState.Clear();

            _mockrepo.Setup(r => r.UpdateCustomer(It.IsAny<FameFindsDAL.Models.Customer>())).Returns(0);

            var result = _controller.UpdateCustomer(customer);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to Update Customer", badRequest.Value);
        }

        [Fact]
        public void UpdateCustomer_InvalidModel()
        {
            _controller.ModelState.AddModelError("FullName", "Required");

            var customer = new FameFindsWebServices.Models.Customer();

            var result = _controller.UpdateCustomer(customer);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid Customer Data", badRequest.Value);
        }
        [Fact]
        public void UpdatePassword_Success_ReturnsOk()
        {
            _mockAuthService.Setup(s => s.UpdatePassword(1, "newhash")).Returns(true);

            var controller = new CustomerController(null, _mockAuthService.Object, null);

            var result = controller.UpdatePassword(1, "newhash");

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Password updated.", okResult.Value);
        }

        [Fact]
        public void UpdatePassword_UserNotFound_ReturnsNotFound()
        {
            _mockAuthService.Setup(s => s.UpdatePassword(999, "wronghash")).Returns(false);

            var controller = new CustomerController(null, _mockAuthService.Object, null);

            var result = controller.UpdatePassword(999, "wronghash");

            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found.", notFound.Value);
        }
        [Fact]
        public void GetCustomerById_ExistingCustomer_ReturnsOk()
        {
            // Arrange
            var dbCustomer = new FameFindsDAL.Models.Customer
            {
                CustomerId = 1,
                FullName = "John Doe",
                Email = "john@example.com",
                PhoneNumber = "1234567890"
            };

            _mockrepo.Setup(r => r.GetCustomerById(1)).Returns(dbCustomer);

            // Act
            var result = _controller.GetCustomerById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCustomer = Assert.IsType<FameFindsWebServices.Models.Customer>(okResult.Value);
            Assert.Equal(dbCustomer.CustomerId, returnedCustomer.CustomerId);
            Assert.Equal(dbCustomer.Email, returnedCustomer.Email);
        }

        [Fact]
        public void GetCustomerById_NonExistingCustomer()
        {
            _mockrepo.Setup(r => r.GetCustomerById(999)).Returns((FameFindsDAL.Models.Customer)null);

            var result = _controller.GetCustomerById(999);

            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Customer Not Found", notFound.Value);
        }

        [Fact]
        public void GetCustomerById_Exception()
        {
            _mockrepo.Setup(r => r.GetCustomerById(It.IsAny<int>())).Throws(new Exception("DB error"));

            var result = _controller.GetCustomerById(1);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to Retrieve Customer", badRequest.Value);
        }
        [Fact]
        public void RequestOtp_ValidEmail_SendsOtp_ReturnsOk()
        {
            // Arrange
            var request = new EmailRequest { Email = "test@example.com" };

            var customer = new FameFindsDAL.Models.Customer
            {
                CustomerId = 1,
                Email = "test@example.com"
            };

            _mockrepo.Setup(r => r.GetCustomerByUsername(request.Email)).Returns(customer);
            _mockEmailService.Setup(e => e.GenerateOtp()).Returns("123456");
            _mockEmailService.Setup(e => e.SendOtpEmail(request.Email, "123456"));

            // Act
            var result = _controller.RequestOtp(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OTP has been sent to your email.", okResult.Value);

            _mockrepo.Verify(r => r.SaveOtp(customer.CustomerId, "123456"), Times.Once);
            _mockEmailService.Verify(e => e.SendOtpEmail(request.Email, "123456"), Times.Once);
        }

        [Fact]
        public void RequestOtp_EmailMissing_ReturnsBadRequest()
        {
            // Arrange
            var request = new EmailRequest { Email = "" };

            // Act
            var result = _controller.RequestOtp(request);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Email is required.", badRequest.Value);
        }

        [Fact]
        public void RequestOtp_EmailNotFound_ReturnsNotFound()
        {
            // Arrange
            var request = new EmailRequest { Email = "unknown@example.com" };

            _mockrepo.Setup(r => r.GetCustomerByUsername(request.Email)).Returns((FameFindsDAL.Models.Customer)null);

            // Act
            var result = _controller.RequestOtp(request);

            // Assert
            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Email not found", notFound.Value);
        }
       
    }
}
