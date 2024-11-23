using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StudyGarden.API.Endpoints;
using StudyGarden.Application.Interfaces;
using StudyGarden.Application.Services;
using StudyGarden.Core.Abstractions;
using StudyGarden.DataAccess.Repositories;
using StudyGarden.Infrastructure;

namespace StudyGarden.API.Extensions;

public static class ApiExtensions
{
    public static void AddMappedEndpoint(this IEndpointRouteBuilder app)
    {
        app.AddUsersEndpoints();
        app.AddUserCategoryEndpoint();
        app.AddAchievementEndpoint();
        app.AddFriendEndpoint();
        app.AddGardenEndpoint();
        app.AddPlantEndpoint();
        app.AddPlantTypeEndpoint();
        app.AddTaskEndpoint();
        app.AddUserAchievementEndpoint();
    }

    public static void AddScopedServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<UserService>();

        services.AddScoped<IAchievementRepository, AchievementRepository>();
        services.AddScoped<AchievementService>();

        services.AddScoped<IUserAchievementRepository, UserAchievementRepository>(); 
        services.AddScoped<UserAchievementService>();

        services.AddScoped<IPlantRepository, PlantRepository>(); 
        services.AddScoped<PlantService>();

        services.AddScoped<IPlantTypeRepository, PlantTypeRepository>(); 
        services.AddScoped<PlantTypeService>();

        services.AddScoped<ITaskRepository, TaskRepository>(); 
        services.AddScoped<TaskService>();

        services.AddScoped<IUserCategoryRepository, UserCategoryRepository>();
        services.AddScoped<UserCategoryService>();

        services.AddScoped<IGardenRepository, GardenRepository>();
        services.AddScoped<GardenService>();

        services.AddScoped<IFriendRepository, FriendRepository>();
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