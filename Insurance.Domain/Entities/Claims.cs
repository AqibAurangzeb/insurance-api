namespace Insurance.Domain.Entities
{
    public class Claims
    {
        public int Id { get; set; }
        public string Ucr { get; set; }
        public int CompanyId { get; set; }
        public DateTime ClaimDate { get; set; }
        public DateTime LossDate { get; set; }
        public string AssuredName { get; set; }
        public decimal IncurredLoss { get; set; }
        public bool Closed { get; set; }
    }
}
