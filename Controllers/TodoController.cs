using Assignment.Data;
using Assignment.Model;
using Assignment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TaskDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public TodoController(TaskDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public ActionResult Login(Registration user)
        {
            var email = _dbContext.Tasks1.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            if (email == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {
                user.UserMessage = "Login Successfully";
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("UserId",user.Id.ToString()),
                    new Claim("DisplayName", user.FirstName),
                    new Claim("DisplayName", user.LastName),
                    new Claim("UserName", user.FirstName),
                    new Claim("UserName", user.LastName),
                    new Claim("Email",user.Email)
                };

                var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var SignIn = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: SignIn);
                user.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(user);
            }


        }

    }
}
