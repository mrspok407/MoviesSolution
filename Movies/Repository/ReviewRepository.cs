using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Interfaces;
using Movies.Models;

namespace Movies.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }


        public Review GetReview(int reviewId)
        {
            return _context.Reviews.FirstOrDefault(r => r.Id == reviewId);
        }

        public ICollection<Review> GetReviews()
        {
           return _context.Reviews.OrderBy(r => r.Movie.Title).ToList();
        }

        public ICollection<Review> GetReviewsOfAMovie(int movieId)
        {
            return _context.Reviews.Where(r => r.Movie.Id == movieId).ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(r => r.Id == reviewId);
        }

        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _context.RemoveRange(reviews);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

    }
}
