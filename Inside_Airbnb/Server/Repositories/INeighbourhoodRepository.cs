namespace Inside_Airbnb.Server.Repositories;

public interface INeighbourhoodRepository
{
    Task<List<Neighbourhood?>> GetAllNeighbourhoods();
    Task<Neighbourhood?> GetNeighbourhoodById(int id);
}