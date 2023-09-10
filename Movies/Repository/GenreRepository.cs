using Movies.Data;
using Movies.Interfaces;
using Movies.Models;

namespace Movies.Repository
{
    public class GenreRepository : IGenreRepository
    {

        private readonly DataContext _context;
        public GenreRepository(DataContext context)
        {
            _context = context;
        }
        public bool GenreExists(int genreId)
        {
            return _context.Genres.Any(c => c.Id == genreId);
        }

        public ICollection<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }

        public Genre GetGenre(int genreId)
        {
            return _context.Genres.FirstOrDefault(c => c.Id == genreId);
        }

        public ICollection<Movie> GetMoviesByGenreId(int genreId)
        {
            return _context.MovieGenres.Where(mg => mg.Genre.Id == genreId).Select(mg => mg.Movie).ToList();
        }

        public ICollection<Genre> GetGenresOfAMovie(int movieId)
        {
            return _context.MovieGenres.Where(mg => mg.Movie.Id == movieId).Select(mg => mg.Genre).ToList();
        }
    }
}
