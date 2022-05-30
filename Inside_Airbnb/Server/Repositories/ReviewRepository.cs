using Inside_Airbnb.Shared;
using Microsoft.EntityFrameworkCore;

namespace Inside_Airbnb.Server.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly inside_airbnbContext _context;

    public ReviewRepository(inside_airbnbContext context)
    {
        _context = context;
    }

    public record ReviewRecord(DateTime? Date, int count)
    {
        public override string ToString()
        {
            return $"{{ Date = {Date}, count = {count} }}";
        }
    }

    public async Task<ReviewsPerDateStats> GetReviewsPerDate()
    {
        List<ReviewRecord> amountReviews = await _context.Reviews.GroupBy(p => p.Date)
            .Select(g => new ReviewRecord(g.Key, g.Count())).AsNoTracking().ToListAsync();
        // fetching all reviews but only sending 200 back since sorting errors the linq function
        amountReviews = amountReviews.OrderByDescending(x => x.Date)
            .Take(200).ToList();

        List<DateTime> dates = new();
        List<int> counts = new();

        foreach (var t in amountReviews)
        {
            dates.Add((DateTime)t.Date);
            counts.Add(t.count);
        }

        return new ReviewsPerDateStats(dates, counts);
    }
}