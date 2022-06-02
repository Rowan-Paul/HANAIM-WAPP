using System.Text.Json;
using Inside_Airbnb.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Inside_Airbnb.Server.Repositories;

public class ListingRepository : IListingRepository
{
    private readonly InsideAirbnbContext _context;
    private readonly IDistributedCache _distributedCache;

    public ListingRepository(InsideAirbnbContext context, IDistributedCache distributedCache)
    {
        _context = context;
        _distributedCache = distributedCache;
    }

    public async Task<List<Listing>?> GetAllListings()
    {
        List<Listing>? listings;
        var cachedListings = await _distributedCache.GetStringAsync("_listings");

        if (cachedListings != null)
        {
            listings = JsonSerializer.Deserialize<List<Listing>>(cachedListings);
        }
        else
        {
            listings = await _context.Listings
                .Select(l => new Listing {Id = l.Id, Latitude = l.Latitude, Longitude = l.Longitude}).AsNoTracking()
                .ToListAsync();
            cachedListings = JsonSerializer.Serialize(listings);
            var expiryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
                SlidingExpiration = TimeSpan.FromSeconds(30)
            };
            await _distributedCache.SetStringAsync("_listings", cachedListings, expiryOptions);
        }

        return listings;
    }

    public async Task<List<Listing>?> GetListingsByParameter(FilterParameters parameters)
    {
        List<Listing>? listings;
        var cachedListings = await _distributedCache.GetStringAsync(
            $"_listings_{parameters.Neighbourhood}_{parameters.PriceFrom}_{parameters.PriceTo}_{parameters.ReviewsMax}_{parameters.ReviewsMin}");

        if (cachedListings != null)
        {
            listings = JsonSerializer.Deserialize<List<Listing>>(cachedListings);
        }
        else
        {
            listings = await _context.Listings.Where(listing =>
                    parameters.Neighbourhood == null || listing.NeighbourhoodCleansed == parameters.Neighbourhood)
                .Where(listing => parameters.PriceFrom == null || listing.Price >= parameters.PriceFrom)
                .Where(listing => parameters.PriceTo == null || listing.Price <= parameters.PriceTo)
                .Where(listing => parameters.ReviewsMax == null || listing.NumberOfReviews <= parameters.ReviewsMax)
                .Where(listing => parameters.ReviewsMin == null || listing.NumberOfReviews >= parameters.ReviewsMin)
                .Select(l => new Listing {Id = l.Id, Latitude = l.Latitude, Longitude = l.Longitude}).AsNoTracking()
                .ToListAsync();
            cachedListings = JsonSerializer.Serialize(listings);
            var expiryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
                SlidingExpiration = TimeSpan.FromSeconds(30)
            };
            await _distributedCache.SetStringAsync(
                $"_listings_{parameters.Neighbourhood}_{parameters.PriceFrom}_{parameters.PriceTo}_{parameters.ReviewsMax}_{parameters.ReviewsMin}",
                cachedListings, expiryOptions);
        }

        return listings;
    }

    public async Task<Listing?> GetListingById(int id)
    {
        Listing? listing;
        var cachedListing = await _distributedCache.GetStringAsync($"_listings_{id}");

        if (cachedListing != null)
        {
            listing = JsonSerializer.Deserialize<Listing>(cachedListing);
        }
        else
        {
            listing = await _context.Listings
                .Select(l => new Listing
                {
                    Id = l.Id, Name = l.Name, HostName = l.HostName, Price = l.Price,
                    NeighbourhoodCleansed = l.NeighbourhoodCleansed, NumberOfReviews = l.NumberOfReviews,
                    NumberOfReviewsL30d = l.NumberOfReviewsL30d, MinimumNights = l.MinimumNights,
                    MaximumNights = l.MaximumNights, Bedrooms = l.Bedrooms, Bathrooms = l.Bathrooms,
                    RoomType = l.RoomType
                }).AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == Convert.ToInt64(id));
            cachedListing = JsonSerializer.Serialize(listing);
            var expiryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
                SlidingExpiration = TimeSpan.FromSeconds(30)
            };
            await _distributedCache.SetStringAsync($"_listings_{id}", cachedListing, expiryOptions);
        }

        return listing;
    }

    public async Task<int?> GetAveragePriceByNeighbourhood(string neighbourhood)
    {
        double? averagePrice;
        var cachedAveragePrice = await _distributedCache.GetStringAsync($"_avg_price_{neighbourhood}");

        if (cachedAveragePrice != null)
        {
            averagePrice = JsonSerializer.Deserialize<double>(cachedAveragePrice);
        }
        else
        {
            averagePrice = _context.Listings.Where(c => c.NeighbourhoodCleansed == neighbourhood && c.Price != null)
                .AsNoTracking().Average(c => c.Price);
            cachedAveragePrice = JsonSerializer.Serialize(averagePrice);
            var expiryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
                SlidingExpiration = TimeSpan.FromSeconds(30)
            };
            await _distributedCache.SetStringAsync($"_avg_price_{neighbourhood}", cachedAveragePrice, expiryOptions);
        }

        if (averagePrice == null) return null;

        return (int) averagePrice;
    }

    public async Task<PropertyTypesStats?> GetAmountPropertyTypes()
    {
        List<PropertyRecord>? amountPropertyTypes;
        var cachedAmountPropertyTypes = await _distributedCache.GetStringAsync("_amountPropertyTypes");

        if (cachedAmountPropertyTypes != null)
        {
            amountPropertyTypes = JsonSerializer.Deserialize<List<PropertyRecord>>(cachedAmountPropertyTypes);
        }
        else
        {
            amountPropertyTypes = await _context.Listings.GroupBy(p => p.PropertyType)
                .Select(g => new PropertyRecord(g.Key, g.Count())).AsNoTracking().ToListAsync();
            // fetching all property types but only sending 20 back since sorting errors the linq function
            amountPropertyTypes = amountPropertyTypes.OrderByDescending(x => x.Count)
                .Take(10).ToList();
            cachedAmountPropertyTypes = JsonSerializer.Serialize(amountPropertyTypes);
            var expiryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
                SlidingExpiration = TimeSpan.FromSeconds(30)
            };
            await _distributedCache.SetStringAsync("_amountPropertyTypes", cachedAmountPropertyTypes, expiryOptions);
        }

        List<string> propertyTypes = new();
        List<int> counts = new();

        if (amountPropertyTypes == null) return null;
        foreach (var t in amountPropertyTypes)
        {
            propertyTypes.Add(t.PropertyType);
            counts.Add(t.Count);
        }

        return new PropertyTypesStats(propertyTypes, counts);
    }

    public async Task<RoomTypesStats> GetAmountRoomTypes()
    {
        List<RoomRecord>? amountRoomTypes;
        var cachedAmountRoomTypes = await _distributedCache.GetStringAsync("_amountRoomTypes");

        if (cachedAmountRoomTypes != null)
        {
            amountRoomTypes = JsonSerializer.Deserialize<List<RoomRecord>>(cachedAmountRoomTypes);
        }
        else
        {
            amountRoomTypes = await _context.Listings.GroupBy(p => p.RoomType)
                .AsNoTracking().Select(g => new RoomRecord(g.Key, g.Count())).AsNoTracking().ToListAsync();
            cachedAmountRoomTypes = JsonSerializer.Serialize(amountRoomTypes);
            var expiryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
                SlidingExpiration = TimeSpan.FromSeconds(30)
            };
            await _distributedCache.SetStringAsync("_amountRoomTypes", cachedAmountRoomTypes, expiryOptions);
        }

        List<string> roomTypes = new();
        List<int> counts = new();

        if (amountRoomTypes == null) return new RoomTypesStats(roomTypes, counts);
        foreach (var t in amountRoomTypes)
        {
            roomTypes.Add(t.RoomType);
            counts.Add(t.Count);
        }

        return new RoomTypesStats(roomTypes, counts);
    }
}

public record PropertyRecord(string PropertyType, int Count)
{
    public override string ToString()
    {
        return $"{{ PropertyType = {PropertyType}, count = {Count} }}";
    }
}

public record RoomRecord(string RoomType, int Count)
{
    public override string ToString()
    {
        return $"{{ RoomType = {RoomType}, count = {Count} }}";
    }
}