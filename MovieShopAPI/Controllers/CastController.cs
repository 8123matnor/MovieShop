using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : ControllerBase
    {
        private readonly ICastService _castService;

        public CastController(ICastService castService)
        {
            _castService = castService;
        }

        [HttpGet]
        [Route("{castId:int}")]
        public async Task<IActionResult> GetCast(int castId)
        {
            var cast = await _castService.GetCastDetails(castId);
            if (cast == null)
            {
                return NotFound(new { errorMessage = $"No Cast Found for {castId}" });
            }
            return Ok(cast);
        }
    }
}

