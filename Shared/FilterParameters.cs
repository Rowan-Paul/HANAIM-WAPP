namespace Shared;

public class FilterParameters
{
    public FilterParameters(string? neighbourhood, int? priceFrom, int? priceTo, int? reviewsMax, int? reviewsMin)
    {
        Neighbourhood = neighbourhood;
        PriceFrom = priceFrom;
        PriceTo = priceTo;
        ReviewsMax = reviewsMax;
        ReviewsMin = reviewsMin;
    }

    public FilterParameters()
    {
    }

    public bool Geojson { get; set; }
    public string? Neighbourhood { get; set; }
    public int? PriceFrom { get; set; }
    public int? PriceTo { get; set; }
    public int? ReviewsMax { get; set; }
    public int? ReviewsMin { get; set; }
}