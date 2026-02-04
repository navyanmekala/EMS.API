using EMS1.API.Models;
using EMS1.API.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace EMS1.API.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost("login")]
        public IActionResult Login(User user)
        {                 
            var dbUser = _context.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
                if (dbUser == null)
                {
                    return Unauthorized("Invalid username or password");
                }
    
                var tokenHandler = new JwtSecurityTokenHandler();
               // var keyBytes = Convert.FromBase64String(_configuration["Jwt:Key"]);
                var keyBytes = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

                var signingKey = new SymmetricSecurityKey(keyBytes);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, dbUser.Username),
                        new Claim(ClaimTypes.Role, dbUser.Role)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
    
                return Ok(new { Token = tokenString });
        }
       
    }
}
