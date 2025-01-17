﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Infra;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieShopAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICurrentUser _currentUser;
        public UserController(IUserService userService, ICurrentUser currentUser)
        {
            _userService = userService;
            _currentUser = currentUser;
        }
        [HttpGet]
        [Route("details/{id:int}")]
        public async Task<IActionResult> UserDetails(int id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }

        [HttpGet]
        [Route("Purchases/{id:int}")]
        public async Task<IActionResult> GetPurchasesByUser(int id)
        {
            var purchases = await _userService.GetAllPurchasesForUser(id);
            return Ok(purchases);
        }

        [HttpPost]
        [Route("purchase-movie")]
        public async Task<IActionResult> PurchaseMovie([FromBody] PurchaseRequestModel purchase, int userId)
        {
            if (await _userService.PurchaseMovie(purchase, userId))
            {
                return Ok(purchase);
            }
            return Conflict(new { errorMessage = "User has already purchased movie" });

        }

        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> FavoriteMovie([FromBody] FavoriteRequestModel favorite)
        {
            if (await _userService.AddFavorite(favorite))
            {
                return Ok();
            }
            return Conflict(new { errorMessage = "User has already favorited movie" });

        }

        [HttpPost]
        [Route("un-favorite")]
        public async Task<IActionResult> RemoveFavorite([FromBody] FavoriteRequestModel favorite)
        {
            if (await _userService.RemoveFavorite(favorite))
            {
                return Ok();
            }
            return Conflict(new { errorMessage = "User currently does not have this movie favorited" });
        }

        [HttpGet]
        [Route("check-movie-favorite/{movieId:int}")]
        public async Task<IActionResult> CheckFavorite(int movieId)
        {

            if (await _userService.FavoriteExists(_currentUser.UserId, movieId))
            {
                return Ok();
            }
            return NotFound(new { errorMessage = "Favorite not found for this user" });
        }

        [HttpPost]
        [Route("add-review")]
        public async Task<IActionResult> AddReview([FromBody] ReviewRequestModel review)
        {
            if (await _userService.AddMovieReview(review))
            {
                return Ok();
            }
            return Conflict(new { errorMessage = "Review already exists for this movie for this user!" });
        }

        [HttpPut]
        [Route("edit-review")]
        public async Task<IActionResult> EditReview([FromBody] ReviewRequestModel review)
        {
            var updatedReview = await _userService.UpdateMovieReview(review);
            return Ok(updatedReview);
        }

        [HttpDelete]
        [Route("delete-review/{movieId:int}")]
        public async Task<IActionResult> DeleteReview(int movieId)
        {
            if (await _userService.DeleteMovieReview(_currentUser.UserId, movieId))
            {
                return Ok();
            }
            return NotFound(new { errorMessage = "No review found for this movie for this user" });
        }

        [HttpGet]
        [Route("purchase-details/{movieId:int}")]
        public async Task<IActionResult> GetPurchaseDetails(int movieId)
        {
            var purchase = await _userService.GetPurchaseDetails(_currentUser.UserId, movieId);
            if (purchase == null)
            {
                return NotFound(new { errorMessage = "No purchase found for this movie for this user" });
            }
            return Ok(purchase);
        }

        [HttpGet]
        [Route("check-movie-purchased/{movieId:int}")]
        public async Task<IActionResult> IsMoviePurchased(int movieId)
        {
            var isPurchased = await _userService.IsMoviePurchased(new PurchaseRequestModel { MovieId = movieId, PurchaseDateTime = DateTime.Now }, _currentUser.UserId);
            return Ok(isPurchased);
        }

        [HttpGet]
        [Route("favorites")]
        public async Task<IActionResult> GetFavorites()
        {
            var favorites = await _userService.GetAllFavoritesForUser(_currentUser.UserId);
            if (favorites == null || !favorites.Any())
            {
                return NotFound(new { errorMessage = "No favorites found for this user" });
            }
            return Ok(favorites);
        }

        [HttpGet]
        [Route("movie-reviews")]
        public async Task<IActionResult> GetReviews()
        {
            var reviews = await _userService.GetAllReviewsByUser(_currentUser.UserId);
            if (reviews == null || !reviews.Any())
            {
                return NotFound(new { errorMessage = "No reviews found for this user" });
            }
            return Ok(reviews);
        }
    }
    

}

