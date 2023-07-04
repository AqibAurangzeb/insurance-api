using Insurance.Domain.Abstractions;
using Insurance.Domain.Models;

namespace Insurance.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public GetCompanyResponse? GetCompany(int id)
        {
            var result = _unitOfWork.Company.GetById(id);

            if (result != null)
            {
                var response = new GetCompanyResponse
                {
                    Name = result.Name,
                    Address1 = result.Address1,
                    Address2 = result.Address2,
                    Address3 = result.Address3,
                    Postcode = result.Postcode,
                    Country = result.Country,
                    CompanyActive = result.Active,
                    InsuranceActive = result.InsuranceEndDate > DateTime.UtcNow,
                    InsuranceEndDate = result.InsuranceEndDate,
                };

                return response;
            }
            else
            {
                return null;
            }
        }

        public GetCompanyClaimsResponse? GetCompanyClaims(int id)
        {
            var company = _unitOfWork.Company.GetById(id);

            if (company == null)
            {
                throw new Exception("company must exist");
            }

            var claims = _unitOfWork.Claims.GetByCompanyId(id);

            if (claims != null)
            {
                var claimList = new List<Domain.Models.Claim>();

                foreach (var claim in claims)
                {
                    claimList.Add(new Domain.Models.Claim
                    {
                        Ucr = claim.Ucr,
                        ClaimDate = claim.ClaimDate,
                        LossDate = claim.LossDate,
                        AssuredName = claim.AssuredName,
                        IncurredLoss = claim.IncurredLoss,
                        Closed = claim.Closed
                    });
                }

                var response = new GetCompanyClaimsResponse
                {
                    Name = company.Name,
                    Claims = claimList
                };

                return response;

            }
            else
            {
                return null;
            }
        }
    }
}
