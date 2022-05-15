namespace Inside_Airbnb_Server;

public interface IListingRepository
{
    Task<List<Listing>> GetAllListings();
    public Task<List<Listing>> GetListingsByNeighbourhood(string neighbourhood);
    Task<Listing> GetListingById(int id);
}