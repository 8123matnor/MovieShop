using ApplicationCore.ServiceContracts;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;
using System.Diagnostics;

namespace MovieShopMVC.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
       
        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
         
        }

        // Action methods inside the controller

        [HttpGet]
        public async Task< IActionResult> Index()
        {
            // Home Page 
            // Top 30 Highest grossing Movies
            // go to database and get 30 movies
            // List<Movie>
            // we need to send the model data to the View
            // Controllers will call Services, which are gonna call Repositories
            // passing data from Controller/action methods to Views, through C# Models
             
            var movieCards = await _movieService.GetTopRevenueMovies();
            return View(movieCards);
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TopRatedMovies()
        {
            return View();
        } 


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}