using Insurance.API.Controllers;
using Insurance.Domain.Abstractions;
using Insurance.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Insurance.API.Unit.Tests.Controllers
{
    public class CompanyControllerTests
    {
        private CompanyController _sut;

        private Mock<ICompanyService> _companyServiceMock;
        private Mock<ILogger<CompanyController>> _loggerMock;

        public CompanyControllerTests()
        {
            _companyServiceMock = new Mock<ICompanyService>();
            _loggerMock = new Mock<ILogger<CompanyController>>();

            _sut = new CompanyController(_loggerMock.Object, _companyServiceMock.Object);
        }

        [Fact]
        public async Task GetCompany_WhenValidRequest_Returns200OK()
        {
            // Arrange
            var id = 1;
            var companyData = new GetCompanyResponse
            {
                Name = "Aqib LTD",
                Address1 = "Test Address 1",
                Address2 = "Test Address 2",
                Address3 = "Test Address 3",
                Postcode = "LS8 TST",
                Country = "UK",
                CompanyActive = true,
                InsuranceEndDate = new DateTime(2023, 07, 03)
            };

            _companyServiceMock
                .Setup(x => x.GetCompany(id))
                .Returns(companyData);

            // Act
            var result = await _sut.GetCompany(id);

            // Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var response = okObjectResult.Value as GetCompanyResponse;
            Assert.Equal(companyData, response);
        }

        [Fact]
        public async Task GetCompany_WhenNullRecievedFromService_Returns404NotFound()
        {
            // Arrange
            var id = 1;

            _companyServiceMock
                .Setup(x => x.GetCompany(id))
                .Returns((GetCompanyResponse)null);

            // Act
            var result = await _sut.GetCompany(id);

            // Assert
            var notFoundResult = result as NotFoundResult;
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public async Task GetCompany_WhenExceptionThrownFromService_Returns500()
        {
            // Arrange
            var id = 1;

            _companyServiceMock
                .Setup(x => x.GetCompany(id))
                .Throws(new Exception("something went wrong"));

            // Act
            var result = await _sut.GetCompany(id);

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
        public async Task GetCompanyClaims_WhenValidRequest_Returns200OK()
        {
            // Arrange
            var id = 1;
            var companyClaimsData = new GetCompanyClaimsResponse
            {
                Name = "Aqib LTD",
                Claims = new List<Claim> 
                { 
                    new Claim 
                    { 
                        Ucr = "ABCD",
                        ClaimDate = new DateTime(2023, 07, 03),
                        LossDate= new DateTime(2023, 07, 03),
                        AssuredName = "Test",
                        IncurredLoss = 5000,
                        Closed = true
                    }
                }
            };

            _companyServiceMock
                .Setup(x => x.GetCompanyClaims(id))
                .Returns(companyClaimsData);

            // Act
            var result = await _sut.GetCompanyClaims(id);

            // Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var response = okObjectResult.Value as GetCompanyClaimsResponse;
            Assert.Equal(companyClaimsData, response);
        }

        [Fact]
        public async Task GetCompanyClaims_WhenNullRecievedFromService_Returns404NotFound()
        {
            // Arrange
            var id = 1;

            _companyServiceMock
                .Setup(x => x.GetCompanyClaims(id))
                .Returns((GetCompanyClaimsResponse)null);

            // Act
            var result = await _sut.GetCompanyClaims(id);

            // Assert
            var notFoundResult = result as NotFoundResult;
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public async Task GetCompanyClaims_WhenExceptionThrownFromServiceHasMustExistText_Returns400()
        {
            // Arrange
            var id = 1;

            _companyServiceMock
                .Setup(x => x.GetCompanyClaims(id))
                .Throws(new Exception("company must exist"));

            // Act
            var result = await _sut.GetCompanyClaims(id);

            // Assert
            var badRequestObject = result as BadRequestObjectResult;
            Assert.NotNull(badRequestObject);
        }

        [Fact]
        public async Task GetCompanyClaims_WhenExceptionThrownFromService_Returns500()
        {
            // Arrange
            var id = 1;

            _companyServiceMock
                .Setup(x => x.GetCompanyClaims(id))
                .Throws(new Exception("something went wrong"));

            // Act
            var result = await _sut.GetCompanyClaims(id);

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
