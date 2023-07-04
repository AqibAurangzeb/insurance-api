using Insurance.Domain.Entities;
using Insurance.Domain.Models;

namespace Insurance.Domain.Abstractions
{
    public interface IClaimsService
    {
        GetClaimResponse? GetClaim(int id);
        Claims? UpdateClaim(int id, Claim claim);
    }
}
