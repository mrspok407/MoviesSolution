using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Interfaces;
using Movies.Models;

namespace Movies.Repository
{
    public class CountryRespository : ICountryRepository
    {
        private readonly DataContext _context;
        public CountryRespository(DataContext context)
        {
            _context = context;
        }

        public bool CountryExists(int id)
        {
            return _context.Countries.Any(c => c.Id == id); 
        }


        public ICollection<Country> GetCountries()
        {
            return _context.Countries.ToList();
        }

        public Country GetCountry(int countryId)
        {
            return _context.Countries.Where(c => c.Id == countryId).Include(c => c.Genres).FirstOrDefault();   
        }

        public Country GetCountryByGenre(int genreId)
        {
            return _context.Genres.Where(g => g.Id == genreId).Select(c => c.Country).FirstOrDefault();
        }

        public ICollection<Genre> GetGenresFromACountry(int countryId)
        {
            return _context.Genres.Where(g => g.Country.Id == countryId).ToList();
        }

        public bool CreateCountry(Country country)
        {
            _context.Add(country);
            return Save();
        }

        public bool UpdateCountry(Country country)
        {
            _context.Update(country);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
