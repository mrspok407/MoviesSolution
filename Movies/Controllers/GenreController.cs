﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Dto;
using Movies.Interfaces;
using Movies.Models;
using Movies.Repository;

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
    }

}
