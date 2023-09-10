using AutoMapper;
using Movies.Dto;
using Movies.Models;

namespace Movies.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Movie, MovieDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<Country, CountryDto>();
            CreateMap<Review, ReviewDto>();
        }
    }
}
