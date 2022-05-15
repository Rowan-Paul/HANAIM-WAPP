using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inside_Airbnb_Server;

namespace Inside_Airbnb_Server.Controllers
{
    [Route("api/listings")]
    [ApiController]
    public class ListingController : ControllerBase
    {
        private IListingRepository ListingRepository { get; }

        public ListingController(IListingRepository listingRepository)
        {
            ListingRepository = listingRepository;
        }

        // GET: api/Listings
        [HttpGet]
        public async Task<ActionResult<dynamic>> GetListings([FromQuery] bool geojson, string? neighbourhood)
        {
            List<Listing> listings;
            Console.WriteLine("neighbourhood: " + neighbourhood);

            if (neighbourhood is { Length: > 0 })
            {
                Console.WriteLine("in");
                listings = await ListingRepository.GetListingsByNeighbourhood(neighbourhood);
            }
            else
            {
                Console.WriteLine("in2");
                listings = await ListingRepository.GetAllListings();
            }

            if (!geojson) return listings;

            List<Feature> features = new();
            FeatureCollection featureCollection = new(features);

            foreach (var listing in listings)
            {
                if (listing.Latitude != null && listing.Longitude != null)
                {
                    features.Add(new Feature(new Point(new Position((double)listing.Latitude,
                        (double)listing.Longitude)), new { listing.Id, listing.Name, listing.HostName }));
                }
            }

            return featureCollection;
        }

        // GET: api/Listings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Listing>> GetListing(int id)
        {
            var listing = await ListingRepository.GetListingById(id);

            return listing;
        }
    }
}