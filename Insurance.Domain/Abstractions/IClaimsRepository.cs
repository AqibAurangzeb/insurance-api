using Insurance.Domain.Entities;

namespace Insurance.Domain.Abstractions
{
    public interface IClaimsRepository : IGenericRepository<Entities.Claims>
    {
        IEnumerable<Claims> GetByCompanyId(int id);
    }
}
