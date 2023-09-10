namespace Movies.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Genre> Genres { get; set;}
    }
}
