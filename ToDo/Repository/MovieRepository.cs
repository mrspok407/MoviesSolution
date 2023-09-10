using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Interfaces;
using Movies.Models;
using System.Linq;

namespace Movies.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DataContext _context;
        public MovieRepository(DataContext context)
        {
            _context = context;
        }

        public Movie GetMovie(int id)
        {
            return _context.Movie.Include(m => m.Reviews).FirstOrDefault(m => m.Id == id);
        }

        public Movie GetMovie(string name)
        {
            return _context.Movie.FirstOrDefault(m => m.Title == name);
        }

        public decimal GetMovieReviewsRating(int movieId)
        {
            var review = _context.Reviews.Where(r => r.Movie.Id == movieId);

            if (review.Count() <= 0)
            {
                return 0;
            }

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Reviewer> GetReviewers(int movieId)
        {
            var reviews = _context.Reviews.Where(r => r.Movie.Id == movieId);
            return reviews.Select(r => r.Reviewer).ToList();
        }

        public bool MovieExists(int movieId)
        {
            return _context.Movie.Any(m => m.Id == movieId);
        }

        public ICollection<Movie> GetMovies()
        {
            return _context.Movie.OrderBy(x => x.Rating).ToList();
        }
    }
}
