using Insurance.Domain.Abstractions;
using Insurance.Domain.Entities;

namespace Insurance.DataAccess
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(InsuranceDbContext context) : base(context)
        {
        }
    }
}
