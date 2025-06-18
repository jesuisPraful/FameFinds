using FameFindsDAL;
using FameFindsDAL.Models;
using FameFindsWebServices.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace FameFindsWebServicesTests
{
    public class RatingControllerTest
    {
        private readonly Mock<IFameFindsDAL> _mockrepo;
        private readonly RatingController _controller;

        public RatingControllerTest()
        {
            _mockrepo = new Mock<IFameFindsDAL>();
            _controller = new RatingController(_mockrepo.Object);
        }

        [Fact]
        public void GetAllRatings_ReturnsOk_WithRatingsList()
        {
            // Arrange
            var ratings = new List<Rating>
            {
                new Rating { RatingId = 1, RatingValue = 5, Review = "Excellent" },
                new Rating { RatingId = 2, RatingValue = 4, Review = "Good" }
            };

            _mockrepo.Setup(r => r.GetRatings()).Returns(ratings);

            // Act
            var result = _controller.GetAllRatings();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRatings = Assert.IsType<List<Rating>>(okResult.Value);
            Assert.Equal(2, returnedRatings.Count);
        }

        [Fact]
        public void AddRating_ReturnsOk_WhenModelIsValid_AndRatingAdded()
        {
            // Arrange
            var newRating = new FameFindsWebServices.Models.Rating
            {
                CustomerId = 1,
                ShopId = 2,
                RatingValue = 5,
                Review = "Excellent"
            };

            _mockrepo.Setup(r => r.AddRating(It.IsAny<Rating>())).Returns(true);

            // Act
            var result = _controller.AddRating(newRating);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Ratings added successfully", okResult.Value);
        }

        [Fact]
        public void AddRating_ReturnsBadRequest_WhenAddFails()
        {
            // Arrange
            var newRating = new FameFindsWebServices.Models.Rating
            {
                CustomerId = 1,
                ShopId = 2,
                RatingValue = 3,
                Review = "Okay"
            };

            _mockrepo.Setup(r => r.AddRating(It.IsAny<Rating>())).Returns(false);

            // Act
            var result = _controller.AddRating(newRating);

            // Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to add ratings", badResult.Value);
        }

        [Fact]
        public void RemoveRating_ReturnsOk_WhenDeleted()
        {
            // Arrange
            int ratingId = 1;
            _mockrepo.Setup(r => r.RemoveRating(ratingId)).Returns(true);

            // Act
            var result = _controller.RemoveRating(ratingId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Rating deleted successfully", okResult.Value);
        }

        [Fact]
        public void RemoveRating_ReturnsBadRequest_WhenDeleteFails()
        {
            // Arrange
            int ratingId = 2;
            _mockrepo.Setup(r => r.RemoveRating(ratingId)).Returns(false);

            // Act
            var result = _controller.RemoveRating(ratingId);

            // Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to delete rating", badResult.Value);
        }
    }
}
