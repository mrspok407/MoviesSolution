using Movies.Models;

namespace Movies.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        ICollection<Movie> GetMovieByCategoryId(int categoryId);

        Category GetCategory(int id);
        bool CategoryExists(int id);

        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);

        bool DeleteCategory(Category category);

        bool Save();

    }
}