using Inside_Airbnb.Server.Repositories;
using Inside_Airbnb.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Inside_Airbnb.Server.Controllers;

[Route("api/neighbourhoods")]
[ApiController]
public class NeighbourhoodController : ControllerBase
{
    public NeighbourhoodController(INeighbourhoodRepository neighbourhoodRepository)
    {
        NeighbourhoodRepository = neighbourhoodRepository;
    }

    private INeighbourhoodRepository NeighbourhoodRepository { get; }

    // GET: api/Neighbourhoods
    [HttpGet]
    public async Task<ActionResult<dynamic>> GetNeighbourhoods([FromQuery] bool geojson)
    {
        List<Neighbourhood>? neighbourhoods = await NeighbourhoodRepository.GetAllNeighbourhoods();

        if (neighbourhoods == null) return NotFound();

        if (!geojson) return neighbourhoods;
        var bytes = await System.IO.File.ReadAllBytesAsync(@"wwwroot/neighbourhoods.geojson");

        return File(bytes, "application/octet-stream", "neighbourhoods.json");
    }
}