using Inside_Airbnb.Shared;

namespace Inside_Airbnb.Server.Repositories;

public interface IReviewRepository
{
    Task<ReviewsPerDateStats?> GetReviewsPerDate();
}