using Insurance.API.Controllers;
using Insurance.Domain.Abstractions;
using Insurance.Domain.Entities;
using Insurance.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Insurance.API.Unit.Tests.Controllers
{
    public class ClaimsControllerTests
    {
        private ClaimsController _sut;

        private Mock<IClaimsService> _claimsServiceMock;
        private Mock<ILogger<ClaimsController>> _loggerMock;

        public ClaimsControllerTests()
        {
            _claimsServiceMock = new Mock<IClaimsService>();
            _loggerMock = new Mock<ILogger<ClaimsController>>();

            _sut = new ClaimsController(_loggerMock.Object, _claimsServiceMock.Object);
        }

        [Fact]
        public async Task GetClaim_WhenValidRequest_Returns200OK()
        {
            // Arrange
            var id = 1;
            var claimData = new GetClaimResponse
            {
                Ucr = "ABCD",
                ClaimDate = new DateTime(2023, 07, 03),
                LossDate = new DateTime(2023, 07, 03),
                AssuredName = "Test",
                IncurredLoss = 5000,
                Closed = true,
                DaysOld = 1
            };

            _claimsServiceMock
                .Setup(x => x.GetClaim(id))
                .Returns(claimData);

            // Act
            var result = await _sut.GetClaim(id);

            // Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var response = okObjectResult.Value as GetClaimResponse;
            Assert.Equal(claimData, response);
        }

        [Fact]
        public async Task GetClaim_WhenNullRecievedFromService_Returns404NotFound()
        {
            // Arrange
            var id = 1;

            _claimsServiceMock
                .Setup(x => x.GetClaim(id))
                .Returns((GetClaimResponse)null);

            // Act
            var result = await _sut.GetClaim(id);

            // Assert
            var notFoundResult = result as NotFoundResult;
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public async Task GetClaim_WhenExceptionThrownFromService_Returns500()
        {
            // Arrange
            var id = 1;

            _claimsServiceMock
                .Setup(x => x.GetClaim(id))
                .Throws(new Exception("something went wrong"));

            // Act
            var result = await _sut.GetClaim(id);

            // Assert
            var statusCodeResult = result as StatusCodeResult;
            Assert.Equal(500, statusCodeResult.StatusCode);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString().Equals("error: something went wrong")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateClaim_WhenValidRequest_Returns200OK()
        {
            // Arrange
            var id = 1;
            var claimData = new Claim
            {
                Ucr = "GHIF",
                ClaimDate = new DateTime(2023, 07, 03),
                LossDate = new DateTime(2023, 07, 03),
                AssuredName = "Test",
                IncurredLoss = 5000,
                Closed = true
            };
            var claimResponseData = new Claims
            {
                Id = id,
                Ucr = "GHIF",
                ClaimDate = new DateTime(2023, 07, 03),
                LossDate = new DateTime(2023, 07, 03),
                AssuredName = "Test",
                IncurredLoss = 5000,
                Closed = true
            };

            _claimsServiceMock
                .Setup(x => x.UpdateClaim(id, claimData))
                .Returns(claimResponseData);

            // Act
            var result = await _sut.UpdateClaim(id, claimData);

            // Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var response = okObjectResult.Value as Claims;
            Assert.Equal(claimResponseData, response);
        }

        [Fact]
        public async Task UpdateClaim_WhenNullRecievedFromService_Returns404NotFound()
        {
            // Arrange
            var id = 1;
            var claimData = new Claim
            {
                Ucr = "GHIF",
                ClaimDate = new DateTime(2023, 07, 03),
                LossDate = new DateTime(2023, 07, 03),
                AssuredName = "Test",
                IncurredLoss = 5000,
                Closed = true
            };

            _claimsServiceMock
                .Setup(x => x.UpdateClaim(id, claimData))
                .Returns((Claims)null);

            // Act
            var result = await _sut.UpdateClaim(id, claimData);

            // Assert
            var notFoundResult = result as NotFoundResult;
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public async Task UpdateClaim_WhenExceptionThrownFromServiceHasNotFoundText_Returns404()
        {
            // Arrange
            var id = 1;
            var claimData = new Claim
            {
                Ucr = "GHIF",
                ClaimDate = new DateTime(2023, 07, 03),
                LossDate = new DateTime(2023, 07, 03),
                AssuredName = "Test",
                IncurredLoss = 5000,
                Closed = true
            };

            _claimsServiceMock
                .Setup(x => x.UpdateClaim(id, claimData))
                .Throws(new Exception("claim not found"));

            // Act
            var result = await _sut.UpdateClaim(id, claimData);

            // Assert
            var notFoundResult = result as NotFoundResult;
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public async Task UpdateClaim_WhenExceptionThrownFromService_Returns500()
        {
            // Arrange
            var id = 1;
            var claimData = new Claim
            {
                Ucr = "GHIF",
                ClaimDate = new DateTime(2023, 07, 03),
                LossDate = new DateTime(2023, 07, 03),
                AssuredName = "Test",
                IncurredLoss = 5000,
                Closed = true
            };

            _claimsServiceMock
                .Setup(x => x.UpdateClaim(id, claimData))
                .Throws(new Exception("something went wrong"));

            // Act
            var result = await _sut.UpdateClaim(id, claimData);

            // Assert
            var statusCodeResult = result as StatusCodeResult;
            Assert.Equal(500, statusCodeResult.StatusCode);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString().Equals("error: something went wrong")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}
