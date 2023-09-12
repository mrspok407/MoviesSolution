using Movies.Data;
using Movies.Interfaces;
using Movies.Models;
using System.Diagnostics.Metrics;

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

        public Country GetCountryByGenreId(int genreId)
        {
            /*return GetGenre(genreId).Country;*/
            return _context.Genres.Where(g => g.Id == genreId).Select(g => g.Country).FirstOrDefault();

        }

        public ICollection<Genre> GetGenresOfAMovie(int movieId)
        {
            return _context.MovieGenres.Where(mg => mg.Movie.Id == movieId).Select(mg => mg.Genre).ToList();
        }

        public bool CreateGenre(Genre genre)
        {
            _context.Add(genre);
            return Save();
        }

        public bool UpdateGenre(Genre genre)
        {
            _context.Update(genre);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

    }
}
