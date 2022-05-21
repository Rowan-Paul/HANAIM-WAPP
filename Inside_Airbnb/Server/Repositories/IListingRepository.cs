
using Inside_Airbnb.Server;
using Inside_Airbnb.Shared;

namespace Inside_Airbnb_Server;

public interface IListingRepository
{
    Task<List<Listing>> GetAllListings();
    Task<List<Listing>> GetListingsByParameter(FilterParameters parameters);
    Task<Listing> GetListingById(int id);
    Task<int> GetAveragePriceByNeighbourhood(string neighbourhood);
    Task<PropertyTypesStats> GetAmountPropertyTypes();
}