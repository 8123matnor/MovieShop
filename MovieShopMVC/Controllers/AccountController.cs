using System.Security.Claims;
using ApplicationCore.Models;
using ApplicationCore.ServiceContracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        // account/register
        [HttpGet]
        public IActionResult Register()
        {
            // show the View so that user can enter info and click on register button
            return View();
        }

        //OLD REGISTER POST
        //[HttpPost]
        //public async Task<IActionResult> Register(UserRegisterModel model)
        //{
        //    // service, hash the password and save in database
        //    var user = await _accountService.CreateUser(model);
        //    return RedirectToAction("Login");
        //}

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            var user = await _accountService.CreateUser(model);
            return Ok(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        //USING COOKIES
        //[HttpPost]
        //public async Task<IActionResult> Login(UserLoginModel model)
        //{
        //    var user = await _accountService.ValidateUser(model);
        //    if (user == null)
        //    {
        //        return View(model);
        //    }

        //    //after successful auth, create claims
        //    //need user id, email, firstname, lastname
        //    //
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Email, user.Email),
        //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //        new Claim(ClaimTypes.Surname, user.LastName),
        //        new Claim(ClaimTypes.GivenName, user.FirstName),
        //        new Claim("language", "english")
        //    };

        //    //identity object
        //    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //    //create cookie with some expiration time
        //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));



        //    return LocalRedirect("~/");
        //}

        //USING TWC

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var user = await _accountService.ValidateUser(model);

            if (user != null)
            {
                var jwtToken = CreateJwtToken(user);
                return Ok(new { token = jwtToken });
            }

            return Unauthorized(new { errorMessage = "Please check email and password" });
        }

        private string CreateJwtToken(UserInfoResponseModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim("Country", "USA"),
                new Claim("language", "english")
            };

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["secretKey"]));

            //specify algorithm
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            //specify expiration of the token
            var tokenExpiration = DateTime.UtcNow.AddHours(2);

            //create token
            var tokenDetails = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = tokenExpiration,
                SigningCredentials = credentials,
                Issuer = "MovieShop, Inc",
                Audience = "MovieShop Clients"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedJwt = tokenHandler.CreateToken(tokenDetails);

            return tokenHandler.WriteToken(encodedJwt);
        }
    }
}
