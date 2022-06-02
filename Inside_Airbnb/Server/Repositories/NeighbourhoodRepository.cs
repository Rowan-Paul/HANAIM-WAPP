using System.Text.Json;
using Inside_Airbnb.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Inside_Airbnb.Server.Repositories;

public class NeighbourhoodRepository : INeighbourhoodRepository
{
    private readonly InsideAirbnbContext _context;
    private readonly IDistributedCache _distributedCache;

    public NeighbourhoodRepository(InsideAirbnbContext context, IDistributedCache distributedCache)
    {
        _context = context;
        _distributedCache = distributedCache;
    }

    public async Task<List<Neighbourhood>?> GetAllNeighbourhoods()
    {
        try
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
                var expiryOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(120),
                    SlidingExpiration = TimeSpan.FromSeconds(60)
                };
                await _distributedCache.SetStringAsync("_neighbourhoods", cachedNeighbourhoods, expiryOptions);
            }

            return neighbourhoods;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}