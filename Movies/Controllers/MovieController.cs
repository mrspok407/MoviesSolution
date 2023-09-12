﻿using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Movies.Dto;
using Movies.Interfaces;
using Movies.Models;
using Movies.Repository;
using System.Net;

namespace Movies.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;


        public MovieController(IMovieRepository movieRepository, IGenreRepository _genreRepository, IReviewRepository _reviewRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
        public IActionResult GetMovies()
        {
            var movies = _mapper.Map<List<MovieDto>>(_movieRepository.GetMovies());


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(movies);
        }

        [HttpGet("{movieId}")]
        [ProducesResponseType(200, Type = typeof(Movie))]
        [ProducesResponseType(400)]
        public IActionResult GetMovieById(int movieId)
        {
            if (!_movieRepository.MovieExists(movieId))
                return NotFound();

            var movie = _mapper.Map<MovieDto>(_movieRepository.GetMovie(movieId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(movie);
        }

        [HttpGet("{movieId}/reviewsRating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]

        public IActionResult GetMovieRating(int movieId)
        {
            if (!_movieRepository.MovieExists(movieId))
                return NotFound();
            var rating = _movieRepository.GetMovieReviewsRating(movieId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(rating);
        }

        [HttpGet("{movieId}/reviewers")]
        [ProducesResponseType(200, Type = typeof(ICollection<Reviewer>))]
        [ProducesResponseType(400)]

        public IActionResult GetReviewers(int movieId)
        {
            if (!_movieRepository.MovieExists(movieId))
                return NotFound();
            var reviewers = _mapper.Map<List<ReviewerDto>>(_movieRepository.GetReviewers(movieId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(reviewers);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMovie([FromQuery] int genreId, [FromQuery] int categoryId, [FromBody] MovieDto movieInput)
        {
            if (movieInput == null)
                return BadRequest(ModelState);


            var movie = _movieRepository.GetMovies()
                .FirstOrDefault(g => g.Title.Trim().ToUpper() == movieInput.Title.Trim().ToUpper());

            if (movie != null)
            {
                ModelState.AddModelError("", "Movie allready exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movieMap = _mapper.Map<Movie>(movieInput);
            

            if (!_movieRepository.CreateMovie(genreId, categoryId, movieMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{movieId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult UpdateMovie(int movieId, [FromBody] MovieDto updatedMovie)
        {
            if (updatedMovie == null)
                return BadRequest(ModelState);

            if (movieId != updatedMovie.Id)
                return BadRequest(ModelState);

            if (!_movieRepository.MovieExists(movieId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movieMap = _mapper.Map<Movie>(updatedMovie);

            if (!_movieRepository.UpdateMovie(movieMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }

    }
}
