using ApplicationCore.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task< IActionResult> Details(int id)
        {
            // T1
            // go to movie service -> movie repository and get movie details from Movies Table

            // We are making an I/O bound operation //
            // database call, File call, stream, network
            
            // CPU bound operations
            // calculating a loan processing interat rate,
            // Image processing
            // pi number digits
            // 10 ms, 100 ms, 2 sec 10 sec
            // T1 just waiting for I/O bound operation to finish
            // Queued up and will not threads, 503 - Thread starvation, server scalibilty
            // async await  2012 C# 5
            var movieDetails = await _movieService.GetMovieDetails(id);
            return View(movieDetails);
        }

        public async Task<ActionResult> GenreMovies(int id, int pageSize =30, int page = 1)
        {
            var pageMovies = await _movieService.GetMoviesByPagination(id, pageSize, page);
            return View(pageMovies);
        }
    }
}
