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

        [HttpPost("GetUserID")]
        public async Task<IActionResult> GetUserID([FromBody] LoginModel model)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Username == model.Username);
            if (user != null && PasswordHasher.VerifyPassword(model.Password, user.HashedPassword))
            {
                var id = user.ID;
                return Ok(new { id });
            }
            return Unauthorized();
        }
        
        [HttpGet("CheckToken")]
        public IActionResult CheckToken([FromHeader] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { Message = "Токен не предоставлен" });
            }

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("nVc0GHPlKcPpMnJE4GHxN6NsvF4oyKSy")), // Замените на ваш ключ
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero // Для уменьшения возможного дрейфа времени
            };

            try
            {
                // Проверяем и валидируем токен
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                var identity = principal.Identity as System.Security.Claims.ClaimsIdentity;
                if (identity != null)
                {
                    var userClaims = identity.Claims;

                    var userId = userClaims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                    if (!string.IsNullOrEmpty(userId))
                    {
                        return Ok(new
                        {
                            Message = $"Токен действителен + {userId}",
                            UserId = userId,
                        });
                    }
                }
            }
            catch (SecurityTokenException)
            {
                // Токен недействителен
                return Unauthorized(new { Message = "Токен недействителен или истек" });
            }

            return Unauthorized(new { Message = "Токен недействителен или истек" });
        }


        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString())
            };
            
            Console.WriteLine("UserID " + user.ID.ToString());

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