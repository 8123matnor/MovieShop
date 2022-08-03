using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            if (movie == null)
            {
                return NotFound(new { errorMessage = $"No Movie Found for {id}" });
            }
            return Ok(movie);
        }

        [HttpGet]
        [Route("top-grossing")]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTopRevenueMovies();

            if (movies == null || !movies.Any())
            {
                return NotFound(new { errorMessage = "No Movies Found" });
            }

            return Ok(movies);
        }

       

        [HttpGet]
        [Route("genre/{genreId:int}")]
        public async Task<IActionResult> GetGenre(int genreId)
        {
            var movie = await _movieService.GetMoviesByPagination(genreId, 30, 1);
            if (movie == null)
            {
                return NotFound(new { errorMessage = $"No Movie Found for {genreId}" });
            }
            return Ok(movie);
        }


    }
}

