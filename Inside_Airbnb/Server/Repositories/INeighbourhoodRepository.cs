using Inside_Airbnb.Shared;

namespace Inside_Airbnb.Server.Repositories;

public interface INeighbourhoodRepository
{
    Task<List<Neighbourhood>?> GetAllNeighbourhoods();
}