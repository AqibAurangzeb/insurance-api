using Insurance.Domain.Models;

namespace Insurance.Domain.Abstractions
{
    public interface ICompanyService
    {
        GetCompanyResponse? GetCompany(int id);
        GetCompanyClaimsResponse? GetCompanyClaims(int id);
    }
}
