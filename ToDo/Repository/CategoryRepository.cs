using Movies.Data;
using Movies.Interfaces;
using Movies.Models;

namespace Movies.Repository
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CategoryExists(int categoryId)
        {
            return _context.Categories.Any(c => c.Id == categoryId);
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int categoryId)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == categoryId);
        }

        public ICollection<Movie> GetMovieByCategoryId(int categoryId)
        {
            return _context.MovieCategories.Where(c => c.CategoryId == categoryId).Select(c => c.Movie).ToList();
        }
    }
}
