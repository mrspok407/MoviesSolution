using Movies.Models;

namespace Movies.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountryByGenre(int genreId);
        ICollection<Genre> GetGenresFromACountry(int countryId);
        bool CountryExists(int id);
    }
}
