namespace Inside_Airbnb_Server;

public interface IListingRepository
{
    Task<List<Listing>> GetAllListings();
    Task<Listing> GetListingById(int id);
}