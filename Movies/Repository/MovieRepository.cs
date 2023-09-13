using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Interfaces;
using Movies.Models;
using System.Diagnostics.Metrics;
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

        public ICollection<Genre> GetMovieGenres(int movieId)
        {
            return _context.MovieGenres.Where(mg => mg.MovieId == movieId).Select(mg => mg.Genre).ToList();
        }

        public bool MovieHasGenre(int movieId, int genreId)
        {
            return _context.MovieGenres.Any(mg => mg.MovieId == movieId &&  mg.GenreId == genreId);
        }

        public bool AddGenreToMovie(int movieId, int genreId)
        {
            var movie = GetMovie(movieId);
            var genre = _context.Genres.FirstOrDefault(g => g.Id == genreId);

            var movieGenre = new MovieGenre()
            {
                Genre = genre,
                Movie = movie,
            };
            _context.Add(movieGenre);

            return Save();
        }

        public bool RemoveGenreFromMovie(int movieId, int genreId)
        {
            var movieGenreEntity = _context.MovieGenres.FirstOrDefault(mg => mg.MovieId == movieId && mg.GenreId == genreId);
            _context.Remove(movieGenreEntity);

            return Save();
        }

        public bool CreateMovie(int genreId, int categoryId, Movie movie)
        {
            var movieGenreEntity = _context.Genres.FirstOrDefault(g => g.Id == genreId);
            var category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);

            var movieGenre = new MovieGenre()
            {
                Genre = movieGenreEntity,
                Movie = movie,
            };
            _context.Add(movieGenre);

            var movieCategory = new MovieCategory()
            {
                Category = category,
                Movie = movie,
            };
            _context.Add(movieCategory);
            _context.Add(movie);

            return Save();
        }

        public bool UpdateMovie(Movie movie)
        {
            _context.Update(movie);
            return Save();
        }
        public bool DeleteMovie(Movie movie)
        {
            return _context.Movie.Where(p => p.Id == movie.Id).Include(r => r.Reviews).ExecuteDelete() > 0;
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
