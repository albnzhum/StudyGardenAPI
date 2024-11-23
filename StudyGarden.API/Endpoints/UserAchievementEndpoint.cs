using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using StudyGarden.API.Contracts.UserAchievement;
using StudyGarden.API.Endpoints.Abstractions;
using StudyGarden.Application.Services;
using StudyGarden.Core.Models;

namespace StudyGarden.API.Endpoints;

public static class UserAchievementEndpoint 
{
    public static IEndpointRouteBuilder AddUserAchievementEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("CreateUserAchievement", Create);
        app.MapPut("UpdateUserAchievement", Update);
        app.MapDelete("DeleteUserAchievement/{id:int}", Delete);
        app.MapGet("GetAllUserAchievements/{userId:int}", GetAll);
        app.MapGet("GetUserAchievement/{userId:int}", Get);
        app.MapGet("GetByAchievementId/{achievementId:int}", GetByAchievementId);
        
        return app;
    }
    
    private static async Task<IResult> Create([FromBody] UserAchievementRequest model, UserAchievementService service)
    {
        try
        {
            var (userAchievement, error) = UserAchievement.Create(model.UserID, model.AchievementID, model.DateEarned);

            if (!string.IsNullOrEmpty(error))
            {
                return Results.BadRequest(error);
            }

            var userAchievementId = await service.Create(userAchievement);

            return Results.Ok(userAchievementId);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e);
        }
    }

    private static async Task<IResult> Update([FromBody] UserAchievementRequest model, UserAchievementService service)
    {
        try
        {
            var (userAchievement, error) = UserAchievement.Create(model.UserID, model.AchievementID, model.DateEarned);

            if (!string.IsNullOrEmpty(error))
            {
                return Results.BadRequest(error);
            }

            var userAchievementId = await service.Update(userAchievement);

            return Results.Ok(userAchievementId);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e);
        }
    }

    private static async Task<IResult> Delete(int id, UserAchievementService service)
    {
        try
        {
            await service.Delete(id);

            return Results.Ok(id);
        }
        catch (Exception e)
        {
           return Results.BadRequest(e);
        }
    }

    private static async Task<IResult> GetAll(UserAchievementService service, int userId = default)
    {
        try
        {
            var userAchievements = await service.GetAll(userId);
            
            return Results.Ok(userAchievements);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e);
        }
    }

    private static async Task<IResult> Get(UserAchievementService service, int id = default)
    {
        try
        {
            var userAchievement = await service.Get(id);
            
            return Results.Ok(userAchievement);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e);
        }
    }

    private static async Task<IResult> GetByAchievementId(int achievementId, UserAchievementService service)
    {
        try
        {
            var achievements = await service.GetByAchievementID(achievementId);
            
            return Results.Ok(achievements);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e);
        }
    }
}