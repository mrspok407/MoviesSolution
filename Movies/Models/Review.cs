namespace Movies.Models
{
    public class Review
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

        public bool? IsPositive { get; set; } = false;

        public int Rating { get; set; }

        public Reviewer Reviewer { get; set; }
        public Movie Movie { get; set; }
    }
}
