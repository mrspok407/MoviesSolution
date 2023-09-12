using Movies.Models;

namespace Movies.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int reviewId);

        ICollection<Review> GetReviewsOfAMovie(int movieId);

        bool ReviewExists(int reviewId);
        bool CreateReview(Review review);

        bool UpdateReview(Review review);
        bool Save();
    }
}
