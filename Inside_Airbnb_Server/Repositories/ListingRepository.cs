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

    public async Task<Listing> GetListingById(int id)
    {
        Listing listing = await _context.Listings.FindAsync(id);

        return listing;
    }
}