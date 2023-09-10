namespace Movies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int? Rating { get; set; }

        public DateTime? ReleaseDate { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        public ICollection<Review> Reviews { get; set;}
        public ICollection<MovieGenre> MovieGenres { get; set;}
        public ICollection<MovieCategory> MovieCategories { get; set; }
    }
}
