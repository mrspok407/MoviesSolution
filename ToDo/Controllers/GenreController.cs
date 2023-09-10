using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Dto;
using Movies.Interfaces;
using Movies.Models;

namespace Movies.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]

    public class GenreController : Controller
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreController(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]

        public IActionResult GetGenres()
        {
            var genres = _mapper.Map<List<GenreDto>>(_genreRepository.GetGenres());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(genres);
        }

        [HttpGet("{genreId}")]
        [ProducesResponseType(200, Type = typeof(Genre))]
        [ProducesResponseType(400)]
        public IActionResult GetGenreById(int genreId)
        {
            if (!_genreRepository.GenreExists(genreId))
                return NotFound();

            var genre = _mapper.Map<GenreDto>(_genreRepository.GetGenre(genreId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(genre);
        }

        [HttpGet("{genreId}/movie")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
        [ProducesResponseType(400)]
        public IActionResult GetMovieByGenreId(int genreId)
        {
            if (!_genreRepository.GenreExists(genreId))
                return NotFound();
            
            var movies = _mapper.Map<List<MovieDto>>(_genreRepository.GetMoviesByGenreId(genreId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(movies);
        }
    }

}
