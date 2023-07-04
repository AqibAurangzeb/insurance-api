using Insurance.Domain.Abstractions;

namespace Insurance.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InsuranceDbContext _context;

        public UnitOfWork(InsuranceDbContext context)
        {
            _context = context;
            Claims = new ClaimsRepository(_context);
            Company = new CompanyRepository(_context);
        }

        public IClaimsRepository Claims { get; }

        public ICompanyRepository Company { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
