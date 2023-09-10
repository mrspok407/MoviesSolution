namespace Movies.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public required string Type { get; set; }

        public Country Country { get; set; }

        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
