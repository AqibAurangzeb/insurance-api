namespace Insurance.Domain.Models
{
    public class GetCompanyClaimsResponse
    {
        public string Name { get; set; }

        public List<Claim> Claims { get; set; }
    }
}
