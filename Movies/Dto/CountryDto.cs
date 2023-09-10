using Movies.Models;

namespace Movies.Dto
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GenreDto> Genres { get; set; }

    }
}
