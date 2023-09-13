using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Dto;
using Movies.Interfaces;
using Movies.Models;
using Movies.Repository;
using System.Collections.Generic;

namespace Movies.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]

    public class GenreController : Controller
    {
        private readonly IGenreRepository _genreRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public GenreController(IGenreRepository genreRepository, ICountryRepository countryRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _countryRepository = countryRepository;
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

        /// <summary>
        /// fgsdgfdgfdg
        /// </summary>
        /// <param name="genreId"></param>
        /// <returns></returns>
        [HttpGet("{genreId}/country")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountryByGenreId(int genreId)
        {
            if (!_genreRepository.GenreExists(genreId))
                return NotFound();

            var country = _mapper.Map<CountryDto>(_genreRepository.GetCountryByGenreId(genreId));

            Console.WriteLine(country);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(country);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGenre([FromQuery] int countryId, [FromBody] GenreDto genreInput)
        {
            if (genreInput == null)
                return BadRequest(ModelState);

            var countryExists = _countryRepository.CountryExists(countryId);
            var genreExists = _genreRepository.GetGenres()
                .FirstOrDefault(g => g.Type.Trim().ToUpper() == genreInput.Type.Trim().ToUpper());

            if (genreExists != null)
            {
                ModelState.AddModelError("", "Genre with this type already exists");
                return StatusCode(422, ModelState);
            }
            if (!countryExists)
            {
                ModelState.AddModelError("", "Country with this Id does not exists");
                return StatusCode(422, ModelState);
            } 

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genreMap = _mapper.Map<Genre>(genreInput);
            genreMap.Country = _countryRepository.GetCountry(countryId);


            if (!_genreRepository.CreateGenre(genreMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{genreId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult UpdateGenre(int genreId, [FromBody] GenreDto updatedGenre)
        {
            if (updatedGenre == null)
                return BadRequest(ModelState);

            if (genreId != updatedGenre.Id)
                return BadRequest(ModelState);

            if (!_genreRepository.GenreExists(genreId))
                return NotFound();

            var genreWithType = _genreRepository.GetGenres()
                .FirstOrDefault(g => g.Type.Trim().ToUpper() == updatedGenre.Type.Trim().ToUpper());
            if (genreWithType != null)
            {
                ModelState.AddModelError("", "Genre with this type already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genreMap = _mapper.Map<Genre>(updatedGenre);

            if (!_genreRepository.UpdateGenre(genreMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            var moviewWithGenre = _mapper.Map<List<MovieDto>>(_genreRepository.GetMoviesByGenreId(genreId));
            return Ok(moviewWithGenre);
        }

        [HttpDelete("{genreId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult DeleteGenre(int genreId)
        {
            if (!_genreRepository.GenreExists(genreId))
            {
                return NotFound();
            }

            var genreToDelete = _genreRepository.GetGenre(genreId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_genreRepository.DeleteGenre(genreToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting genre");
            }

            return NoContent();
        }
    }

}
