namespace Insurance.Domain.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IClaimsRepository Claims { get; }
        ICompanyRepository Company { get; }
        int Complete();
    }
}
