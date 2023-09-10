using Movies.Data;
using Movies.Models;
using System.Diagnostics.Metrics;

namespace Movies
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.Movie.Any())
            {
                var movies = new List<Movie>()
                {
                    new Movie()
                    {
                        Title = string.Format("Batman"),
                        CreatedDate = DateTime.Now,
                        Rating = 3,
                    },
                     new Movie()
                    {
                        Title = string.Format("Inception"),
                        CreatedDate = DateTime.Now,
                        Rating = 1,
                    },
                     new Movie()
                    {
                        Title = string.Format("Lord"),
                        CreatedDate = DateTime.Now,
                        Rating = 5,
                    },
                     new Movie()
                    {
                        Title = string.Format("Dogs"),
                        CreatedDate = DateTime.Now,
                        Rating = 10,
                    },
                };
                dataContext.Movie.AddRange(movies);
                dataContext.SaveChanges();
            }
        }
    }
}