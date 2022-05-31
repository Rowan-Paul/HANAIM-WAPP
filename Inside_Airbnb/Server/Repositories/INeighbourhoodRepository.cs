namespace Inside_Airbnb.Server.Repositories;

public interface INeighbourhoodRepository
{
    Task<List<Neighbourhood>?> GetAllNeighbourhoods();
}