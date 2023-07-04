namespace Insurance.Domain.Models
{
    public class GetClaimResponse : Claim
    {
        public int DaysOld { get; set; }
    }
}
