using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Dto;
using Movies.Interfaces;
using Movies.Models;
using Movies.Repository;

namespace Movies.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;


        public ReviewController(IReviewRepository reviewRepository, IMapper mapper, IMovieRepository movieRepository)
        {
            _reviewRepository = reviewRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewById(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(review);
        }

        [HttpGet("movie/{movieId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]

        public IActionResult GetReviewsOfAMovie(int movieId)
        {
            if (!_movieRepository.MovieExists(movieId))
                 return NotFound();

            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfAMovie(movieId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(reviews);
        }
    }
}
