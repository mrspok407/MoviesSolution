using Movies.Models;

namespace Movies.Interfaces
{
    public interface IGenreRepository
    {
        ICollection<Genre> GetGenres();
        Genre GetGenre(int genreId);

        ICollection<Genre> GetGenresOfAMovie(int movieId);

        ICollection<Movie> GetMoviesByGenreId(int genreId);
        
        bool GenreExists(int id);
    }
}
