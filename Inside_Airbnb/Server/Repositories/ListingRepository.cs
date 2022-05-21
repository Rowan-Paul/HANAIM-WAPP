﻿using Inside_Airbnb.Server;
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

    public record PropertyRecord(string PropertyType, int count)
    {
        public override string ToString()
        {
            return $"{{ PropertyType = {PropertyType}, count = {count} }}";
        }
    }
    public record RoomRecord(string RoomType, int count)
    {
        public override string ToString()
        {
            return $"{{ RoomType = {RoomType}, count = {count} }}";
        }
    }

    public async Task<PropertyTypesStats> GetAmountPropertyTypes()
    {
        List<PropertyRecord> amountPropertyTypes = await _context.Listings.GroupBy(p => p.PropertyType)
            .Select(g => new PropertyRecord(g.Key, g.Count())).ToListAsync();
        // fetching all property types but only sending 20 back since sorting errors the linq function
        amountPropertyTypes = amountPropertyTypes.OrderByDescending(x => x.count)
            .Take(20).ToList();

        List<string> propertyTypes = new();
        List<int> counts = new();

        foreach (var t in amountPropertyTypes)
        {
            propertyTypes.Add(t.PropertyType);
            counts.Add(t.count);
        }

        return new PropertyTypesStats(propertyTypes, counts);
    }
    
    public async Task<RoomTypesStats> GetAmountRoomTypes()
    {
        List<RoomRecord> amountRoomTypes = await _context.Listings.GroupBy(p => p.RoomType)
            .Select(g => new RoomRecord(g.Key, g.Count())).ToListAsync();

        List<string> roomTypes = new();
        List<int> counts = new();

        foreach (var t in amountRoomTypes)
        {
            roomTypes.Add(t.RoomType);
            counts.Add(t.count);
        }

        return new RoomTypesStats(roomTypes, counts);
    }
}