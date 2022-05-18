using Shared;

namespace Inside_Airbnb_Server;

public interface IListingRepository
{
    Task<List<Listing>> GetAllListings();
    public Task<List<Listing>> GetListingsByParameter(FilterParameters parameters);
    Task<Listing> GetListingById(int id);
}