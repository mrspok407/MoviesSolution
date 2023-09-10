using Movies.Models;

namespace Movies.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        
        public bool? IsPositive { get; set; } = false;

        public int Rating { get; set; }

    }
}
