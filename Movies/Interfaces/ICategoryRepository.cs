using Movies.Models;

namespace Movies.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        ICollection<Movie> GetMovieByCategoryId(int categoryId);

        Category GetCategory(int id);
        bool CategoryExists(int id);

    }
}