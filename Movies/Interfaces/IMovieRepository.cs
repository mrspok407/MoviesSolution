using Movies.Models;

namespace Movies.Interfaces
{
    public interface IMovieRepository
    {
        ICollection<Movie> GetMovies();

        Movie GetMovie(int id);
        Movie GetMovie(string name);

        decimal GetMovieReviewsRating(int movieId);

        ICollection<Reviewer> GetReviewers(int movieId);
        bool MovieExists(int movieId);

        bool CreateMovie(int genreId, int categoryId, Movie movie);
        bool UpdateMovie(Movie movie);

        bool Save();
    }
}
