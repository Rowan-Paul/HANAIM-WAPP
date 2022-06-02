namespace Inside_Airbnb.Shared;

public class Calendar
{
    public Calendar(long? listingId, DateTime? date, string? available, int? price, int? adjustedPrice, int? minimumNights, int? maximumNights)
    {
        ListingId = listingId;
        Date = date;
        Available = available;
        Price = price;
        AdjustedPrice = adjustedPrice;
        MinimumNights = minimumNights;
        MaximumNights = maximumNights;
    }

    public long? ListingId { get; }
    public DateTime? Date { get; }
    public string? Available { get; }
    public int? Price { get; }
    public int? AdjustedPrice { get; }
    public int? MinimumNights { get; }
    public int? MaximumNights { get; }
}