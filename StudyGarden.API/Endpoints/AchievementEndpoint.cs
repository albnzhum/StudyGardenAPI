using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using StudyGarden.API.Contracts.AchievementsRequests;
using StudyGarden.API.Endpoints.Abstractions;
using StudyGarden.Application.Services;
using StudyGarden.Core.Models;

namespace StudyGarden.API.Endpoints;

public static class AchievementEndpoint 
{
    public static IEndpointRouteBuilder AddAchievementEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("CreateAchievement", Create);
        app.MapPut("UpdateAchievement", Update);
        app.MapDelete("DeleteAchievement/{id:int}", Delete);
        app.MapGet("GetAllAchievement", GetAll);
        app.MapGet("GetAchievement/{id:int}", Get);
        app.MapGet("GetAchievementPlant/{id:int}", GetPlant);
        
        return app;
    }

    private static async Task<IResult> Create([FromBody] AchievementRequest model, AchievementService service)
    {
        try
        {
            var (achievement, error) = Achievement.Create(model.Title, model.PlantID);

            if (!string.IsNullOrEmpty(error))
            {
                return Results.BadRequest(error);
            }

            var achievementId = await service.Create(achievement);

            return Results.Ok(achievementId);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> Update([FromBody] AchievementRequest model, AchievementService service)
    {
        try
        {
            var (achievement, error) = Achievement.Create(model.Title, model.PlantID);
            
            await service.Update(achievement);
            
            return Results.Ok(achievement.ID);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> Delete(int id, AchievementService service)
    {
        try
        {
            await service.Delete(id);

            return Results.Ok(id);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> GetAll(AchievementService service, int userId = default)
    {
        try
        {
            var achievements = await service.GetAll(userId);
            
            return Results.Ok(achievements);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> Get(AchievementService service, int id = default)
    {
        try
        {
            var achievement = await service.Get(id);
            
            return Results.Ok(achievement);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> GetPlant(AchievementService service, int id = default)
    {
        try
        {
            var plant = await service.GetPlant(id);
            
            return Results.Ok(plant);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }
    
}