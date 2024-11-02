using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StudyGarden.API.Endpoints;
using StudyGarden.Application.Services;
using StudyGarden.Core.Abstractions;
using StudyGarden.Core.Models;
using StudyGarden.DataAccess.Repositories;
using StudyGarden.Infrastructure;
using Task = StudyGarden.Core.Models.Task;

namespace StudyGarden.API.Extensions;

public static class ApiExtensions
{
    public static void AddMappedEndpoint(this IEndpointRouteBuilder app)
    {
        app.AddUsersEndpoints();
    }

    public static void AddScopedServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<UserService>();
            
        services.AddScoped<IRepository<Achievement>, AchievementRepository>();
        services.AddScoped<AchievementService>();
            
        services.AddScoped<IRepository<UserAchievement>, UserAchievementRepository>();
        services.AddScoped<UserAchievementService>();
            
        services.AddScoped<IRepository<Plant>, PlantRepository>();
        services.AddScoped<PlantService>();
            
        services.AddScoped<PlantTypeRepository>();
        services.AddScoped<PlantTypeService>();
            
        services.AddScoped<IRepository<Task>, TaskRepository>();
        services.AddScoped<TaskService>();
            
        services.AddScoped<IRepository<UserCategory>, UserCategoryRepository>();
        services.AddScoped<UserCategoryService>();
            
        services.AddScoped<IRepository<Garden>, GardenRepository>();
        services.AddScoped<GardenService>();
            
        services.AddScoped<IRepository<Friend>, FriendRepository>();
        services.AddScoped<FriendService>();
    }
    
    public static void AddApiAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

        serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                    };
                }
            );
        
        serviceCollection.AddAuthorization();
    }
}