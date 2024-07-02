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
        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            if (login == null || string.IsNullOrEmpty(login.Login) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Invalid client request");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login.Name)
            };

            var token = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { Token = tokenString });
        }
    }
}
