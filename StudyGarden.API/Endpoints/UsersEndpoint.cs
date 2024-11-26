using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudyGarden.Application.Services;
using StudyGarden.Contracts.UserRequests;
using StudyGarden.Infrastructure;

namespace StudyGarden.API.Endpoints;

public static class UsersEndpoint
{
    public static IEndpointRouteBuilder AddUsersEndpoints(this
        IEndpointRouteBuilder app)
    {
        app.MapPost("Register", Register);
        app.MapPost("Login", Login);
        app.MapDelete("Delete", Delete);
        app.MapGet("GetUserByID/{id:int}", GetByID);
        app.MapGet("CheckToken/{token}", CheckToken);

        return app;
    }

    private static async Task<IResult> GetByID(int id, UserService service)
    {
        try
        {
            var user = await service.Get(id);
            
            return user == null ? Results.NotFound() : Results.Ok(user);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> CheckToken(string token, IConfiguration configuration)
    {
        if (string.IsNullOrEmpty(token))
        {
            return Results.BadRequest(new { Message = "Токен не предоставлен" });
        }
        
        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

        var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)), 
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };

        try
        {
            // Проверяем и валидируем токен
            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

            // Распарсим токен, чтобы вручную извлечь userId
            var jwtToken = validatedToken as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;
            if (jwtToken == null)
            {
                return Results.BadRequest(new { Message = "Невалидный формат токена" });
            }

            // Получаем userId из полезной нагрузки токена (claim 'sub' или другой)
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                return Results.Ok(new
                {
                    Message = $"Токен действителен + {userId}",
                    UserId = userId,
                });
            }

            return Results.BadRequest(new { Message = "userId отсутствует в токене" });
        }
        catch (SecurityTokenException)
        {
            // Токен недействителен
            return Results.BadRequest(new { Message = "Токен недействителен или истек" });
        }
    }

    private static async Task<IResult> Register(
        [FromBody]RegisterUserRequest request,
        UserService userService)
    {
        await userService.Register(request.Login, request.Password);

        return Results.Ok("Register");
    }

    private static async Task<IResult> Login(
        [FromBody]LoginUserRequest request,
        UserService userService,
        HttpContext context)
    {
        var token = await userService.Login(request.Login, request.Password);

        context.Response.Cookies.Append("tasty-cookies", token);

        return Results.Ok(token);
    }

    private static async Task<IResult> Delete(int id, UserService userService, HttpContext context)
    {
        return Results.Ok(await userService.Delete(id));
    }
}