using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Inside_Airbnb.Server.Repositories;

public class NeighbourhoodRepository : INeighbourhoodRepository
{
    private readonly inside_airbnbContext _context;
    private readonly IDistributedCache _distributedCache;

    public NeighbourhoodRepository(inside_airbnbContext context, IDistributedCache distributedCache)
    {
        _context = context;
        _distributedCache = distributedCache;
    }

    public async Task<List<Neighbourhood>?> GetAllNeighbourhoods()
    {
        List<Neighbourhood>? neighbourhoods;
        var cachedNeighbourhoods = await _distributedCache.GetStringAsync("_neighbourhoods");

        if (cachedNeighbourhoods != null)
        {
            neighbourhoods = JsonSerializer.Deserialize<List<Neighbourhood>>(cachedNeighbourhoods);
        }
        else
        {
            neighbourhoods = await _context.Neighbourhoods.AsNoTracking().ToListAsync();
            cachedNeighbourhoods = JsonSerializer.Serialize(neighbourhoods);
            var expiryOptions = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
                SlidingExpiration = TimeSpan.FromSeconds(30)
            };
            await _distributedCache.SetStringAsync("_neighbourhoods", cachedNeighbourhoods, expiryOptions);
        }

        return neighbourhoods;
    }
}