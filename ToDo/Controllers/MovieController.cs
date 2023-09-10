using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Movies.Dto;
using Movies.Interfaces;
using Movies.Models;
using System.Net;

namespace Movies.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;


        public MovieController(IMovieRepository movieRepository, IMapper mapper)
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

    }
}
