using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 
        
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<MovieCategory> MovieCategories { get; set; }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Movie> Movie { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Reviewer> Reviewers { get; set; }

        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieCategory>()
                .HasKey(mc => new { mc.MovieId, mc.CategoryId });
            modelBuilder.Entity<MovieCategory>()
                .HasOne(m => m.Movie)
                .WithMany(mc => mc.MovieCategories)
                .HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<MovieCategory>()
                .HasOne(m => m.Category)
                .WithMany(mc => mc.MovieCategories)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });
            modelBuilder.Entity<MovieGenre>()
                .HasOne(m => m.Movie)
                .WithMany(mg => mg.MovieGenres)
                .HasForeignKey(m => m.MovieId);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(m => m.Genre)
                .WithMany(mg => mg.MovieGenres)
                .HasForeignKey(c => c.GenreId);
        }

    }
}
