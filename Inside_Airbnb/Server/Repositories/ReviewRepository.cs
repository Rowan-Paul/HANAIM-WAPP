using System.Text.Json;
using Inside_Airbnb.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Inside_Airbnb.Server.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly InsideAirbnbContext _context;
    private readonly IDistributedCache _distributedCache;

    public ReviewRepository(InsideAirbnbContext context, IDistributedCache distributedCache)
    {
        _context = context;
        _distributedCache = distributedCache;
    }

    public async Task<ReviewsPerDateStats?> GetReviewsPerDate()
    {
        try
        {
            List<ReviewRecord>? amountReviews;
            var cachedAmountReviews = await _distributedCache.GetStringAsync("_reviewsPerDate");

            if (cachedAmountReviews != null)
            {
                amountReviews = JsonSerializer.Deserialize<List<ReviewRecord>>(cachedAmountReviews);
            }
            else
            {
                amountReviews = await _context.Reviews.GroupBy(p => p.Date)
                    .Select(g => new ReviewRecord(g.Key, g.Count())).AsNoTracking().ToListAsync();
                // fetching all reviews but only sending 200 back since sorting errors the linq function
                amountReviews = amountReviews.OrderByDescending(x => x.Date)
                    .Take(200).ToList();
                cachedAmountReviews = JsonSerializer.Serialize(amountReviews);
                var expiryOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
                    SlidingExpiration = TimeSpan.FromSeconds(30)
                };
                await _distributedCache.SetStringAsync("_reviewsPerDate", cachedAmountReviews, expiryOptions);
            }

            List<DateTime> dates = new();
            List<int> counts = new();

            if (amountReviews == null) return new ReviewsPerDateStats(dates, counts);
            foreach (var t in amountReviews)
            {
                if (t.Date == null) return null;

                dates.Add((DateTime) t.Date);
                counts.Add(t.Count);
            }

            return new ReviewsPerDateStats(dates, counts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    private record ReviewRecord(DateTime? Date, int Count)
    {
        public override string ToString()
        {
            return $"{{ Date = {Date}, count = {Count} }}";
        }
    }
}