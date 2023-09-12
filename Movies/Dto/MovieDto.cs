using Movies.Models;

namespace Movies.Dto
{
    public class MovieDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int? Rating { get; set; }

        public DateTime? ReleaseDate { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
}
