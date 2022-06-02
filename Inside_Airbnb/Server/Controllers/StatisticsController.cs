using Inside_Airbnb.Server.Repositories;
using Inside_Airbnb.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inside_Airbnb.Server.Controllers;

[Route("api/statistics")]
[ApiController]
[Authorize]
[Authorize(Roles = "administrator")]
public class StatisticsController : ControllerBase
{
    public StatisticsController(IListingRepository listingRepository,
        INeighbourhoodRepository neighbourhoodRepository, IReviewRepository reviewRepository)
    {
        ListingRepository = listingRepository;
        NeighbourhoodRepository = neighbourhoodRepository;
        ReviewRepository = reviewRepository;
    }

    private IListingRepository ListingRepository { get; }
    private INeighbourhoodRepository NeighbourhoodRepository { get; }
    private IReviewRepository ReviewRepository { get; }

    // GET: api/statistics/neighbourhoods
    [HttpGet("neighbourhoods")]
    public async Task<ActionResult<NeighbourhoodsStats>> GetNeighbourhoodStats()
    {
        List<Neighbourhood>? neighbourhoods = await NeighbourhoodRepository.GetAllNeighbourhoods();
        var prices = new List<int>();
        var formattedNeighbourhoods = new List<string>();

        if (neighbourhoods == null) return NotFound();
        foreach (var n in neighbourhoods)
        {
            if (n.Neighbourhood1 == null) continue;

            prices.Add((int) await ListingRepository.GetAveragePriceByNeighbourhood(n.Neighbourhood1));
            formattedNeighbourhoods.Add(n.Neighbourhood1);
        }

        return new NeighbourhoodsStats(prices, formattedNeighbourhoods);
    }

    // GET: api/statistics/property-types
    [HttpGet("property-types")]
    public async Task<PropertyTypesStats?> GetPropertyTypesStats()
    {
        var result = await ListingRepository.GetAmountPropertyTypes();

        return result;
    }

    // GET: api/statistics/room-types
    [HttpGet("room-types")]
    public async Task<RoomTypesStats> GetRoomTypesStats()
    {
        var result = await ListingRepository.GetAmountRoomTypes();

        return result;
    }

    // GET: api/statistics/reviews
    [HttpGet("reviews")]
    public async Task<ReviewsPerDateStats?> GetReviewsPerDateStats()
    {
        var result = await ReviewRepository.GetReviewsPerDate();

        return result;
    }
}