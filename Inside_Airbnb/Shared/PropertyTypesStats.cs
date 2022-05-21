namespace Inside_Airbnb.Shared;

public class PropertyTypesStats
{
    public PropertyTypesStats(List<string> propertyTypes, List<int> counts)
    {
        PropertyTypes = propertyTypes;
        this.counts = counts;
    }

    public List<string> PropertyTypes { get; set; }
    public List<int> counts { get; set; }
}