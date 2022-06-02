namespace Inside_Airbnb.Shared;

public class RoomTypesStats
{
    public RoomTypesStats(List<string> roomTypes, List<int> counts)
    {
        RoomTypes = roomTypes;
        this.counts = counts;
    }

    public List<string> RoomTypes { get; set; }
    public List<int> counts { get; set; }
}