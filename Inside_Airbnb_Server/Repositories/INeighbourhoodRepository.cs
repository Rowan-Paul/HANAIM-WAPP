namespace Inside_Airbnb_Server;

public interface INeighbourhoodRepository
{
    Task<List<Neighbourhood>> GetAllNeighbourhoods();
    Task<Neighbourhood> GetNeighbourhoodById(int id);
}