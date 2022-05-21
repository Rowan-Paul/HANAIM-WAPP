using Inside_Airbnb.Server;
using Inside_Airbnb.Shared;
using Microsoft.EntityFrameworkCore;

namespace Inside_Airbnb_Server;

public class ListingRepository : IListingRepository
{
    private readonly inside_airbnbContext _context;

    public ListingRepository(inside_airbnbContext context)
    {
        _context = context;
    }

    public async Task<List<Listing>> GetAllListings()
    {
        List<Listing> list = await _context.Listings.ToListAsync();

        return list;
    }
    
    public async Task<List<Listing>> GetListingsByParameter(FilterParameters parameters)
    {
        
        return await _context.Listings.Where(listing => parameters.Neighbourhood == null || listing.NeighbourhoodCleansed == parameters.Neighbourhood)
            .Where(listing => parameters.PriceFrom == null || listing.Price >= parameters.PriceFrom)
            .Where(listing => parameters.PriceTo == null || listing.Price <= parameters.PriceTo)
            .Where(listing => parameters.ReviewsMax == null || listing.NumberOfReviews <= parameters.ReviewsMax)
            .Where(listing => parameters.ReviewsMin == null || listing.NumberOfReviews >= parameters.ReviewsMin).ToListAsync();
    }

    public async Task<Listing> GetListingById(int id)
    {
        Listing listing = await _context.Listings.FindAsync(Convert.ToInt64(id));

        return listing;
    }
    
    public async Task<int> GetAveragePriceByNeighbourhood(string neighbourhood)
    {
        var averagePrice = _context.Listings.Where(c => c.NeighbourhoodCleansed == neighbourhood && c.Price != null)
            .Average(c => c.Price);

        return (int) averagePrice;
    }
}