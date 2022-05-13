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
    [Route("api/[controller]")]
    [ApiController]
    public class ListingController : ControllerBase
    {
        private readonly inside_airbnbContext _context;

        public ListingController(inside_airbnbContext context)
        {
            _context = context;
        }

        // GET: api/Listing
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Listing>>> GetListings()
        {
          if (_context.Listings == null)
          {
              return NotFound();
          }
          return await _context.Listings.ToListAsync();
        }
        
        // GET: api/Listing/geo
        [HttpGet("geo")]
        public async Task<ActionResult<FeatureCollection>> GetGeoListing()
        {
            if (_context.Listings == null)
            {
                return NotFound();
            }

            List<Listing> listings = await _context.Listings.ToListAsync();
            
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

        // GET: api/Listing/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Listing>> GetListing(int? id)
        {
          if (_context.Listings == null)
          {
              return NotFound();
          }
          var listing = await _context.Listings.FindAsync(id);

          if (listing == null)
          {
              return NotFound();
          }

          return listing;
        }

        // PUT: api/Listing/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListing(int? id, Listing listing)
        {
            if (id != listing.Id)
            {
                return BadRequest();
            }

            _context.Entry(listing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Listing
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Listing>> PostListing(Listing listing)
        {
          if (_context.Listings == null)
          {
              return Problem("Entity set 'inside_airbnbContext.Listings'  is null.");
          }
          _context.Listings.Add(listing);
          await _context.SaveChangesAsync();

          return CreatedAtAction("GetListing", new { id = listing.Id }, listing);
        }

        // DELETE: api/Listing/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListing(int? id)
        {
            if (_context.Listings == null)
            {
                return NotFound();
            }
            var listing = await _context.Listings.FindAsync(id);
            if (listing == null)
            {
                return NotFound();
            }

            _context.Listings.Remove(listing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListingExists(int? id)
        {
            return (_context.Listings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
