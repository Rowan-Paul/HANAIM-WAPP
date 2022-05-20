using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;
using Inside_Airbnb.Server;
using Inside_Airbnb.Server.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inside_Airbnb_Server.Controllers
{
    [Route("api/neighbourhoods")]
    [ApiController]
    public class NeighbourhoodController : ControllerBase
    {
        private INeighbourhoodRepository NeighbourhoodRepository { get; }

        public NeighbourhoodController(INeighbourhoodRepository neighbourhoodRepository)
        {
            NeighbourhoodRepository = neighbourhoodRepository;
        }

        // GET: api/Neighbourhoods
        [HttpGet]
        public async Task<ActionResult<dynamic>> GetNeighbourhoods([FromQuery] bool geojson)
        {
            List<Neighbourhood> neighbourhoods = await NeighbourhoodRepository.GetAllNeighbourhoods();

            if (!geojson) return neighbourhoods;
            var bytes = await System.IO.File.ReadAllBytesAsync(@"wwwroot/neighbourhoods.geojson");

            return File(bytes, "application/octet-stream", "neighbourhoods.json");

        }

        // GET: api/Neighbourhoods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Neighbourhood>> GetNeighbourhood(int id)
        {
            var listing = await NeighbourhoodRepository.GetNeighbourhoodById(id);

            return listing;
        }
    }
}
