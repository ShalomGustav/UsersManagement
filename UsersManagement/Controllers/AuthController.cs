using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UsersManagement.Models;
using UsersManagement.Services;

namespace UsersManagement.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserService _userService;
        //[HttpPost("login")]
        //public IActionResult Login(string login, string password)
        //{
            //if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(login))
            //{
            //    return BadRequest("Invalid client request");
            //}

            //if (_userService.GetUserById(id))
            //{

            //}


            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, login.Login)
            //};

            //var token = new JwtSecurityToken(
            //    issuer: AuthOptions.ISSUER,
            //    audience: AuthOptions.AUDIENCE,
            //    claims: claims,
            //    expires: DateTime.UtcNow.AddMinutes(30),
            //    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            //);

            //var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            //return Ok(new { Token = tokenString });
        //}
    }
}
