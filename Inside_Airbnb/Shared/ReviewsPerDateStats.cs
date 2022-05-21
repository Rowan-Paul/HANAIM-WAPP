namespace Inside_Airbnb.Shared;

public class ReviewsPerDateStats
{
    public ReviewsPerDateStats(List<DateTime> dates, List<int> counts)
    {
        Dates = dates;
        this.counts = counts;
    }

    public List<DateTime> Dates { get; set; }
    public List<int> counts { get; set; }
}