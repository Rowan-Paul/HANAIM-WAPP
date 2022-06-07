namespace Inside_Airbnb.Shared
{
    public partial class Review
    {
        public long? ListingId { get; set; }
        public long? Id { get; set; }
        public DateTime? Date { get; set; }
        public int? ReviewerId { get; set; }
        public string? ReviewerName { get; set; }
        public string? Comments { get; set; }
    }
}
