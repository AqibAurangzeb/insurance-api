using Insurance.Domain.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Insurance.Service.Unit.Tests.Services
{
    public class ClaimsServiceTests
    {
        private IClaimsService _sut;
        private Mock<IUnitOfWork> _unitOfWorkMock;

        public ClaimsServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _sut = new ClaimsService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GetClaim_WhenIdValidAndResultIsReturnedFromDb_ReturnsClaimDetail()
        {
            // Arrange
            var id = 1;
            var claimData = new Domain.Entities.Claims
            {
                Id = id,
                Ucr = "ABCD",
                ClaimDate = DateTime.UtcNow.AddDays(-10),
                LossDate = DateTime.UtcNow.AddDays(-10),
                AssuredName = "Test",
                IncurredLoss = 5000,
                Closed = true
            };

            _unitOfWorkMock
                .Setup(x => x.Claims.GetById(id))
                .Returns(claimData);

            // Act
            var result = _sut.GetClaim(id);

            // Assert
            Assert.Equal(claimData.Ucr, result.Ucr);
            Assert.Equal(claimData.ClaimDate, result.ClaimDate);
            Assert.Equal(claimData.LossDate, result.LossDate);
            Assert.Equal(claimData.AssuredName, result.AssuredName);
            Assert.Equal(claimData.IncurredLoss, result.IncurredLoss);
            Assert.Equal(claimData.Closed, result.Closed);
            Assert.Equal(10, result.DaysOld);
        }

        [Fact]
        public async Task GetClaim_WhenClaimsGetByIdReturnsNull_ReturnsNull()
        {
            // Arrange
            var id = 1;
            var claimData = new Domain.Entities.Claims
            {
                Id = id,
                Ucr = "ABCD",
                ClaimDate = DateTime.UtcNow.AddDays(-10),
                LossDate = DateTime.UtcNow.AddDays(-10),
                AssuredName = "Test",
                IncurredLoss = 5000,
                Closed = true
            };

            _unitOfWorkMock
                .Setup(x => x.Claims.GetById(id))
                .Returns((Domain.Entities.Claims)null);

            // Act
            var result = _sut.GetClaim(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetClaim_WhenClaimDateInPast_ReturnsDaysOldAsAnAbsoluteValue()
        {
            // Arrange
            var id = 1;
            var claimData = new Domain.Entities.Claims
            {
                Id = id,
                Ucr = "ABCD",
                ClaimDate = DateTime.UtcNow.AddDays(-10),
                LossDate = DateTime.UtcNow.AddDays(-10),
                AssuredName = "Test",
                IncurredLoss = 5000,
                Closed = true
            };

            _unitOfWorkMock
                .Setup(x => x.Claims.GetById(id))
                .Returns(claimData);

            // Act
            var result = _sut.GetClaim(id);

            // Assert
            Assert.Equal(10, result.DaysOld);
        }

        [Fact]
        public async Task GetClaim_WhenClaimDateIsSomehowInTheFuture_DaysOldRemains0()
        {
            // Arrange
            var id = 1;
            var claimData = new Domain.Entities.Claims
            {
                Id = id,
                Ucr = "ABCD",
                ClaimDate = DateTime.UtcNow.AddDays(10),
                LossDate = DateTime.UtcNow.AddDays(10),
                AssuredName = "Test",
                IncurredLoss = 5000,
                Closed = true
            };

            _unitOfWorkMock
                .Setup(x => x.Claims.GetById(id))
                .Returns(claimData);

            // Act
            var result = _sut.GetClaim(id);

            // Assert
            Assert.Equal(0, result.DaysOld);
        }

        [Fact]
        public async Task UpdateClaim_WhenIdValidAndResultIsReturnedFromDb_UpdatesDb()
        {
            // Arrange
            var id = 1;
            var claimUpdateData = new Domain.Models.Claim
            {
                Ucr = "GHIF",
                ClaimDate = DateTime.UtcNow.AddDays(-10),
                LossDate = DateTime.UtcNow.AddDays(-10),
                AssuredName = "Test",
                IncurredLoss = 7000,
                Closed = true
            };
            var claimData = new Domain.Entities.Claims
            {
                Id = id,
                Ucr = "ABCD",
                ClaimDate = DateTime.UtcNow.AddDays(-10),
                LossDate = DateTime.UtcNow.AddDays(-10),
                AssuredName = "Test",
                IncurredLoss = 5000,
                Closed = true
            };

            _unitOfWorkMock
                .Setup(x => x.Claims.GetById(id))
                .Returns(claimData);

            // Act
            var result = _sut.UpdateClaim(id, claimUpdateData);

            // Assert
            Assert.Equal(claimUpdateData.Ucr, result.Ucr);
            Assert.Equal(claimUpdateData.ClaimDate, result.ClaimDate);
            Assert.Equal(claimUpdateData.LossDate, result.LossDate);
            Assert.Equal(claimUpdateData.AssuredName, result.AssuredName);
            Assert.Equal(claimUpdateData.IncurredLoss, result.IncurredLoss);
            Assert.Equal(claimUpdateData.Closed, result.Closed);
            _unitOfWorkMock.Verify(x => x.Complete());
        }

        [Fact]
        public async Task UpdateClaim_WhenClaimsGetByIdReturnsNull_ThrowsException()
        {
            // Arrange
            var id = 1;
            var claimUpdateData = new Domain.Models.Claim
            {
                Ucr = "GHIF",
                ClaimDate = DateTime.UtcNow.AddDays(-10),
                LossDate = DateTime.UtcNow.AddDays(-10),
                AssuredName = "Test",
                IncurredLoss = 7000,
                Closed = true
            };

            _unitOfWorkMock
                .Setup(x => x.Claims.GetById(id))
                .Returns((Domain.Entities.Claims)null);

            // Act
            // Assert
            Assert.Throws<Exception>(() => _sut.UpdateClaim(id, claimUpdateData));            
        }
    }
}
