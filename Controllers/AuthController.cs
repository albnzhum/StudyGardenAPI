using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using StudyGarden.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudyGarden.Common;
using StudyGarden.Model;

namespace StudyGarden.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
        {
            var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Username == model.Username);
            if (existingUser != null)
            {
                return BadRequest("Username is already taken.");
            }

            var hashedPassword = PasswordHasher.HashPassword(model.Password);

            var newUser = new User
            {
                Username = model.Username,
                HashedPassword = hashedPassword
            };

            await _context.User.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Username == model.Username);
            if (user != null && PasswordHasher.VerifyPassword(model.Password, user.HashedPassword))
            {
                var token = GenerateJwtToken(user);
                return Ok(new { token });
            }
            return Unauthorized();
        }
        
        [HttpGet("GetCurrentUser")]
        public ActionResult<User> GetCurrentUser()
        {
            // Извлекаем UserID из токена
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = int.Parse(userIdClaim);
    
            var user = _context.User.Find(userId);
    
            if (user == null)
            {
                return NotFound();
            }
            

            return Ok(user);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer"),
                audience: _configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Jwt:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"] ?? "60")),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
    }
}