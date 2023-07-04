namespace Insurance.Domain.Models
{
    public class Claim
    {
        public string Ucr { get; set; }
        public DateTime ClaimDate { get; set; }
        public DateTime LossDate { get; set; }
        public string AssuredName { get; set; }
        public decimal IncurredLoss { get; set; }
        public bool Closed { get; set; }
    }
}
