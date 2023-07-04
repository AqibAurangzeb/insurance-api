namespace Insurance.Domain.Models
{
    public class GetCompanyResponse : Company
    {
        public bool InsuranceActive { get; set; }
    }
}
