using Insurance.Domain.Abstractions;
using Insurance.Domain.Entities;

namespace Insurance.DataAccess
{
    public class ClaimsRepository : GenericRepository<Claims>, IClaimsRepository
    {
        public ClaimsRepository(InsuranceDbContext context) : base(context)
        {
        }

        public IEnumerable<Claims> GetByCompanyId(int id)
        {
            return _context.Claims.Where(x => x.CompanyId == id).ToList();
        }

    }
}
