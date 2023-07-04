using Insurance.Domain.Abstractions;
using Moq;
using Xunit;

namespace Insurance.Service.Unit.Tests.Services
{
    public class CompanyServiceTests
    {
        private ICompanyService _sut;
        private Mock<IUnitOfWork> _unitOfWorkMock;

        public CompanyServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _sut = new CompanyService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GetCompany_WhenIdValidAndResultIsReturnedFromDb_ReturnsCompanyDetails()
        {
            // Arrange
            var id = 1;
            var companyData = new Domain.Entities.Company
            {
                Id = 1,
                Name = "Test",
                Address1 = "Test Address 1",
                Address2 = "Test Address 2",
                Address3 = "Test Address 3",
                Postcode = "LS6 TST",
                Active = true,
                Country = "UK",
                InsuranceEndDate = DateTime.Now.AddDays(1),
            };

            _unitOfWorkMock
                .Setup(x => x.Company.GetById(id))
                .Returns(companyData);

            // Act
            var result = _sut.GetCompany(id);

            // Assert
            Assert.Equal(companyData.Name, result.Name);
            Assert.Equal(companyData.Address1, result.Address1);
            Assert.Equal(companyData.Address2, result.Address2);
            Assert.Equal(companyData.Address3, result.Address3);
            Assert.Equal(companyData.Postcode, result.Postcode);
            Assert.Equal(companyData.Active, result.CompanyActive);
            Assert.Equal(companyData.InsuranceEndDate, result.InsuranceEndDate);
            Assert.True(result.InsuranceActive);
        }

        [Fact]
        public async Task GetCompany_WhenDbCallResultsInNullResponse_ReturnsNull()
        {
            // Arrange
            var id = 1;

            _unitOfWorkMock
                .Setup(x => x.Company.GetById(id))
                .Returns((Domain.Entities.Company)null);

            // Act
            var result = _sut.GetCompany(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetCompany_WhenInsuranceEndDateInPast_InsuranceActiveIsFalse()
        {
            // Arrange
            var id = 1;

            var companyData = new Domain.Entities.Company
            {
                Id = 1,
                Name = "Test",
                Address1 = "Test Address 1",
                Address2 = "Test Address 2",
                Address3 = "Test Address 3",
                Postcode = "LS6 TST",
                Active = true,
                Country = "UK",
                InsuranceEndDate = DateTime.UtcNow.AddHours(-1),
            };

            _unitOfWorkMock
                .Setup(x => x.Company.GetById(id))
                .Returns(companyData);

            // Act
            var result = _sut.GetCompany(id);

            // Assert
            Assert.False(result.InsuranceActive);
        }

        [Fact]
        public async Task GetCompany_WhenInsuranceEndDateInFuture_InsuranceActiveIsTrue()
        {
            // Arrange
            var id = 1;

            var companyData = new Domain.Entities.Company
            {
                Id = 1,
                Name = "Test",
                Address1 = "Test Address 1",
                Address2 = "Test Address 2",
                Address3 = "Test Address 3",
                Postcode = "LS6 TST",
                Active = true,
                Country = "UK",
                InsuranceEndDate = DateTime.UtcNow.AddHours(1),
            };

            _unitOfWorkMock
                .Setup(x => x.Company.GetById(id))
                .Returns(companyData);

            // Act
            var result = _sut.GetCompany(id);

            // Assert
            Assert.True(result.InsuranceActive);
        }

        [Fact]
        public async Task GetCompanyClaims_WhenIdValidAndResultIsReturnedFromDb_ReturnsCompanyClaimDetails()
        {
            // Arrange
            var id = 1;
            var companyData = new Domain.Entities.Company
            {
                Id = 1,
                Name = "Test",
                Address1 = "Test Address 1",
                Address2 = "Test Address 2",
                Address3 = "Test Address 3",
                Postcode = "LS6 TST",
                Active = true,
                Country = "UK",
                InsuranceEndDate = DateTime.Now.AddDays(1),
            };

            var claimData = new List<Domain.Entities.Claims>
            {
                new Domain.Entities.Claims
                {
                    Id = 1,
                    Ucr = "ABCD",
                    ClaimDate = DateTime.UtcNow.AddDays(-10),
                    LossDate = DateTime.UtcNow.AddDays(-10),
                    AssuredName = "Test",
                    IncurredLoss = 5000,
                    Closed = true
                }
            };

            _unitOfWorkMock
                .Setup(x => x.Company.GetById(id))
                .Returns(companyData);

            _unitOfWorkMock
                .Setup(x => x.Claims.GetByCompanyId(id))
                .Returns(claimData);

            // Act
            var result = _sut.GetCompanyClaims(id);

            // Assert
            Assert.Equal(companyData.Name, result.Name);
            Assert.Equal(1, result.Claims.Count());

            var expectedClaim = claimData[0];
            var resultClaim = result.Claims[0];

            Assert.Equal(expectedClaim.Ucr, resultClaim.Ucr);
            Assert.Equal(expectedClaim.ClaimDate, resultClaim.ClaimDate);
            Assert.Equal(expectedClaim.LossDate, resultClaim.LossDate);
            Assert.Equal(expectedClaim.AssuredName, resultClaim.AssuredName);
            Assert.Equal(expectedClaim.IncurredLoss, resultClaim.IncurredLoss);
            Assert.Equal(expectedClaim.Closed, resultClaim.Closed);
        }

        [Fact]
        public async Task GetCompanyClaims_WhenCompanyGetByIdReturnsNull_ThrowsException()
        {
            // Arrange
            var id = 1;

            _unitOfWorkMock
                .Setup(x => x.Company.GetById(id))
                .Returns((Domain.Entities.Company)null);

            // Act
            // Assert
            Assert.Throws<Exception>(() => _sut.GetCompanyClaims(id));
        }

        [Fact]
        public async Task GetCompanyClaims_WhenClaimsGetByCompanyIdReturnsNull_ReturnsNull()
        {
            // Arrange
            var id = 1;
            var companyData = new Domain.Entities.Company
            {
                Id = 1,
                Name = "Test",
                Address1 = "Test Address 1",
                Address2 = "Test Address 2",
                Address3 = "Test Address 3",
                Postcode = "LS6 TST",
                Active = true,
                Country = "UK",
                InsuranceEndDate = DateTime.Now.AddDays(1),
            };

            _unitOfWorkMock
                .Setup(x => x.Company.GetById(id))
                .Returns(companyData);

            _unitOfWorkMock
                .Setup(x => x.Claims.GetByCompanyId(id))
                .Returns((IEnumerable<Domain.Entities.Claims>)null);

            // Act
            var result = _sut.GetCompanyClaims(id);

            // Assert
            Assert.Null(result);
        }


    }
}
