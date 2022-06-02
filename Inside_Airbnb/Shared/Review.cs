namespace Inside_Airbnb.Shared;

public class Review
{
    public Review(long? listingId, long? id, DateTime? date, int? reviewerId, string? reviewerName, string? comments)
    {
        ListingId = listingId;
        Id = id;
        Date = date;
        ReviewerId = reviewerId;
        ReviewerName = reviewerName;
        Comments = comments;
    }

    public long? ListingId { get; }
    public long? Id { get; }
    public DateTime? Date { get; }
    public int? ReviewerId { get; }
    public string? ReviewerName { get; }
    public string? Comments { get; set; }
}