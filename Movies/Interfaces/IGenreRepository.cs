using Movies.Models;

namespace Movies.Interfaces
{
    public interface IGenreRepository
    {
        ICollection<Genre> GetGenres();
        Genre GetGenre(int genreId);

        ICollection<Genre> GetGenresOfAMovie(int movieId);

        ICollection<Movie> GetMoviesByGenreId(int genreId);

        Country GetCountryByGenreId(int genreId);


        bool GenreExists(int id);
        bool CreateGenre(Genre genre);
        bool UpdateGenre(Genre genre);

        bool Save();
    }
}
