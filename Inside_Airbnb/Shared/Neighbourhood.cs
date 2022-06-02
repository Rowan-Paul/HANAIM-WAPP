namespace Inside_Airbnb.Shared;

public class Neighbourhood
{
    public Neighbourhood(string? neighbourhoodGroup, string? neighbourhood1)
    {
        NeighbourhoodGroup = neighbourhoodGroup;
        Neighbourhood1 = neighbourhood1;
    }

    public string? NeighbourhoodGroup { get; }
    public string? Neighbourhood1 { get; }
}