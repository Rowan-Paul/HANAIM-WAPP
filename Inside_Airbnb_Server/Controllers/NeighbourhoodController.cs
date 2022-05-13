using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inside_Airbnb_Server.Controllers
{
    [Route("api/neighbourhoods")]
    [ApiController]
    public class NeighbourhoodController : ControllerBase
    {
        private readonly inside_airbnbContext _context;
        
        public NeighbourhoodController(inside_airbnbContext context)
        {
            _context = context;
        }

        // GET: api/Neighbourhood
        [HttpGet]
        public async Task<ActionResult<dynamic>> GetNeighbourhoods([FromQuery] bool geojson)
        {
            if (geojson)
            {
                var bytes = System.IO.File.ReadAllBytes(@"wwwroot/neighbourhoods.geojson");

                return File(bytes, "application/octet-stream", "neighbourhoods.json");
            }
            
            if (_context.Neighbourhoods == null)
            {
                return NotFound();
            }
            return await _context.Neighbourhoods.ToListAsync();
        }

        // GET: api/Neighbourhood/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Neighbourhood>> GetNeighbourhood(int? id)
        {
            if (_context.Neighbourhoods == null)
            {
                return NotFound();
            }
            var neighbourhood = await _context.Neighbourhoods.FindAsync(id);

            if (neighbourhood == null)
            {
                return NotFound();
            }

            return neighbourhood;
        }

        // POST: api/Neighbourhood
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Neighbourhood/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Neighbourhood/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNeighbourhood(int? id)
        {
            if (_context.Neighbourhoods == null)
            {
                return NotFound();
            }
            var neighbourhood = await _context.Neighbourhoods.FindAsync(id);
            if (neighbourhood == null)
            {
                return NotFound();
            }

            _context.Neighbourhoods.Remove(neighbourhood);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
