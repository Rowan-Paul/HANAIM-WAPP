using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inside_Airbnb.Server.Repositories;
using Inside_Airbnb.Shared;
using Inside_Airbnb_Server;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inside_Airbnb.Server.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    [Authorize]
    [Authorize(Roles = "administrator")]
    public class StatisticsController : ControllerBase
    {
        private IListingRepository ListingRepository { get; }
        private INeighbourhoodRepository NeighbourhoodRepository { get; }
        private IReviewRepository ReviewRepository { get; }

        public StatisticsController(IListingRepository listingRepository,
            INeighbourhoodRepository neighbourhoodRepository, IReviewRepository reviewRepository)
        {
            ListingRepository = listingRepository;
            NeighbourhoodRepository = neighbourhoodRepository;
            ReviewRepository = reviewRepository;
        }

        // GET: api/statistics/neighbourhoods
        [HttpGet("neighbourhoods")]
        public async Task<NeighbourhoodsStats> GetNeighbourhoodStats()
        {
            List<Neighbourhood?> neighbourhoods = await NeighbourhoodRepository.GetAllNeighbourhoods();
            List<int> prices = new List<int>();
            List<string> formattedNeighbourhoods = new List<string>();

            foreach (var n in neighbourhoods)
            {
                prices.Add(await ListingRepository.GetAveragePriceByNeighbourhood(n.Neighbourhood1));
                formattedNeighbourhoods.Add(n.Neighbourhood1);
            }

            return new NeighbourhoodsStats(prices, formattedNeighbourhoods);
        }

        // GET: api/statistics/property-types
        [HttpGet("property-types")]
        public async Task<PropertyTypesStats> GetPropertyTypesStats()
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
        public async Task<ReviewsPerDateStats> GetReviewsPerDateStats()
        {
            var result = await ReviewRepository.GetReviewsPerDate();

            return result;
        }
    }
}