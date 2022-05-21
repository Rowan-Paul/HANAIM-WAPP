namespace Inside_Airbnb.Shared;

public class NeighbourhoodsStats
{
    public NeighbourhoodsStats(List<int> prices, List<string> neighbourhoods)
    {
        Prices = prices;
        Neighbourhoods = neighbourhoods;
    }

    public List<int> Prices { get; set; }
    public List<string> Neighbourhoods { get; set; }
}