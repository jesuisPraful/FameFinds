using Xunit;
using Moq;
using FameFindsWebServices.Controllers;
//using FameFindsWebServices.Models;
using FameFindsWebServices.Services;
using FameFindsDAL;
using FameFindsDAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using FameFindsWebServices.Models;

public class VendorControllerTests
{
    private readonly Mock<IFameFindsDAL> _mockrepo;
    private readonly Mock<IAuthenticationService> _mockAuthService;
    private readonly Mock<IEmailService> _mockEmailService;
    private readonly VendorController _controller;

    public VendorControllerTests()
    {
        _mockrepo = new Mock<IFameFindsDAL>();
        _mockAuthService = new Mock<IAuthenticationService>();
        _mockEmailService = new Mock<IEmailService>();
        _controller = new VendorController(_mockrepo.Object, _mockAuthService.Object as AuthenticationService, _mockEmailService.Object as EmailService);
    }

    [Fact]
    
    public void GetVendorDetails_ReturnsOk_WithVendorList()
    {
        // Arrange
        var vendorsFromRepo = new List<FameFindsDAL.Models.Vendor>
    {
        new FameFindsDAL.Models.Vendor
        {
            VendorId = 1,
            VendorName = "John",
            Email = "john@example.com",
            PasswordHash = "hash123",
            PhoneNumber = "1234567890"
        },
        new FameFindsDAL.Models.Vendor
        {
            VendorId = 2,
            VendorName = "Jane",
            Email = "jane@example.com",
            PasswordHash = "hash456",
            PhoneNumber = "9876543210"
        }
    };

        _mockrepo.Setup(r => r.GetVendorDetails()).Returns(vendorsFromRepo);

        // Act
        var result = _controller.GetVendorDetails();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedVendors = Assert.IsType<List<FameFindsWebServices.Models.Vendor>>(okResult.Value);

        Assert.Equal(2, returnedVendors.Count);
        Assert.Equal("John", returnedVendors[0].VendorName);
        Assert.Equal("Jane", returnedVendors[1].VendorName);
    }


    [Fact]
    public void CheckEmailExists_ReturnsOk_WhenEmailExists()
    {
        // Arrange
        string email = "vendor@example.com";
        _mockrepo.Setup(repo => repo.IsVendorEmailExists(email)).Returns(true);

        var controller = new VendorController(_mockrepo.Object, _mockAuthService.Object as AuthenticationService, _mockEmailService.Object as EmailService);

        // Act
        var result = controller.CheckEmailExists(email);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.True((bool)okResult.Value); // since `exists` is true
    }
    [Fact]
    public void CheckEmailExists_ReturnsBadRequest_WhenEmailIsEmpty()
    {
        // Arrange
        var controller = new VendorController(_mockrepo.Object, _mockAuthService.Object as AuthenticationService, _mockEmailService.Object as EmailService);

        // Act
        var result = controller.CheckEmailExists("");

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Email must be provided.", badRequest.Value);
    }

    [Fact]
    public void CheckEmailExists_Returns500_OnException()
    {
        // Arrange
        string email = "vendor@example.com";
        _mockrepo.Setup(repo => repo.IsVendorEmailExists(email)).Throws(new Exception("DB error"));

        var controller = new VendorController(_mockrepo.Object, _mockAuthService.Object as AuthenticationService, _mockEmailService.Object as EmailService);

        // Act
        var result = controller.CheckEmailExists(email);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
        Assert.Equal("Internal Server Error", objectResult.Value);
    }


    [Fact]
    public void UpdateVendor_ReturnsOk_WhenUpdateIsSuccessful()
    {
        // Arrange
        var vendorToUpdate = new FameFindsWebServices.Models.Vendor
        {
            VendorId = 1,
            Email = "vendor@example.com",
            PhoneNumber = "1234567890",
            PasswordHash = "hashedpassword",
            VendorName = "UpdatedVendor"
        };

        _mockrepo.Setup(r => r.UpdateVendor(It.IsAny<FameFindsDAL.Models.Vendor>())).Returns(true);

        var controller = new VendorController(_mockrepo.Object, _mockAuthService.Object as AuthenticationService, _mockEmailService.Object as EmailService);

        // Act
        var result = controller.UpdateVendor(vendorToUpdate);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Vendor updated successfully", okResult.Value);
    }

    [Fact]
    public void DeleteVendor_ReturnsOk_WhenVendorIsDeleted()
    {
        int vendorId = 1;
        _mockrepo.Setup(r => r.RemoveVendorDetails(vendorId)).Returns(true);

        var controller = new VendorController(_mockrepo.Object, _mockAuthService.Object as AuthenticationService, _mockEmailService.Object as EmailService);

        var result = controller.DeleteVendor(vendorId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Vendor deleted successfully", okResult.Value);
    }

    [Fact]
    public void GetVendorByEmail_ReturnsOk_WhenVendorExists()
    {
        string email = "vendor@example.com";
        var vendor = new FameFindsDAL.Models.Vendor
        {
            VendorId = 1,
            VendorName = "John",
            Email = email,
            PhoneNumber = "1234567890",
            PasswordHash = "hashedpass"
        };

        _mockrepo.Setup(r => r.GetVendorByUsername(email)).Returns(vendor);

        var controller = new VendorController(_mockrepo.Object, _mockAuthService.Object as AuthenticationService, _mockEmailService.Object as EmailService);

        var result = controller.GetVendorByEmail(email);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(vendor, okResult.Value);
    }

}
