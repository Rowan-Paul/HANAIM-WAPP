using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;
using Inside_Airbnb.Server.Repositories;
using Inside_Airbnb.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Inside_Airbnb.Server.Controllers;

[Route("api/listings")]
[ApiController]
public class ListingController : ControllerBase
{
    public ListingController(IListingRepository listingRepository)
    {
        ListingRepository = listingRepository;
    }

    private IListingRepository ListingRepository { get; }

    // GET: api/Listings
    [HttpGet]
    public async Task<ActionResult<dynamic>> GetListings([FromQuery] bool geojson, string? neighbourhood,
        int? priceFrom, int? priceTo, int? reviewsMax, int? reviewsMin)
    {
        List<Listing>? listings;

        if (neighbourhood is {Length: > 0} || priceFrom.HasValue || priceTo.HasValue
            || reviewsMax.HasValue || reviewsMin.HasValue)
            listings = await ListingRepository.GetListingsByParameter(new FilterParameters(neighbourhood, priceFrom,
                priceTo, reviewsMax, reviewsMin));
        else
            listings = await ListingRepository.GetAllListings();

        if (listings == null) return NotFound();

        if (!geojson) return listings;

        List<Feature> features = new();
        FeatureCollection featureCollection = new(features);

        foreach (var listing in listings)
            if (listing.Latitude != null && listing.Longitude != null)
                features.Add(new Feature(new Point(new Position((double) listing.Latitude,
                    (double) listing.Longitude)), new {listing.Id}));

        return featureCollection;
    }

    // GET: api/Listings/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Listing>> GetListing(int id)
    {
        var listing = await ListingRepository.GetListingById(id);

        if (listing == null) return NotFound();

        return listing;
    }
}