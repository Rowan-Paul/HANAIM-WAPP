using Inside_Airbnb.Shared;

namespace Inside_Airbnb.Server.Repositories;

public interface IListingRepository
{
    Task<List<Listing>> GetAllListings();
    Task<List<Listing>> GetListingsByParameter(FilterParameters parameters);
    Task<Listing?> GetListingById(int id);
    Task<int> GetAveragePriceByNeighbourhood(string neighbourhood);
    Task<PropertyTypesStats> GetAmountPropertyTypes();
    Task<RoomTypesStats> GetAmountRoomTypes();
}