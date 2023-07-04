using Insurance.Domain.Abstractions;
using Insurance.Domain.Entities;
using Insurance.Domain.Models;

namespace Insurance.Service
{
    public class ClaimsService : IClaimsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClaimsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public GetClaimResponse? GetClaim(int id)
        {
            var result = _unitOfWork.Claims.GetById(id);

            if (result != null)
            {
                var daysOld = result.ClaimDate < DateTime.UtcNow 
                    ? (result.ClaimDate - DateTime.UtcNow).Duration().Days 
                    : 0;

                var response = new GetClaimResponse
                {
                    Ucr = result.Ucr,
                    ClaimDate = result.ClaimDate,
                    LossDate = result.LossDate,
                    AssuredName = result.AssuredName,
                    IncurredLoss = result.IncurredLoss,
                    Closed = result.Closed,
                    DaysOld = daysOld
                };

                return response;
            }
            else
            {
                return null;
            }
        }

        public Claims? UpdateClaim(int id, Claim claim)
        {
            var result = _unitOfWork.Claims.GetById(id);

            if (result == null)
            {
                throw new Exception("claim not found");
            }

            result.Ucr = claim.Ucr;
            result.ClaimDate = claim.ClaimDate;
            result.LossDate = claim.LossDate;
            result.AssuredName = claim.AssuredName;
            result.IncurredLoss = claim.IncurredLoss;
            result.Closed = claim.Closed;

            _unitOfWork.Complete();

            return result;
        }
    }
}
