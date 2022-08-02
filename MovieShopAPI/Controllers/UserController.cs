using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieShopAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UserController : ControllerBase
    {
        //private readonly ICurrentUser _currentUser;

        //public UserController(ICurrentUser currentUser)
        //{
        //    _currentUser = currentUser;
        //}
        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetMoviesPurchasedByUser()
        {
            //get all movies purchased by user, user id
            //httpcontezt.user.claims and then call teh database and get the information to the view
            //var userId = _currentUser.UserId;
            //return View();

            return Ok();
        }
    }
    

}

