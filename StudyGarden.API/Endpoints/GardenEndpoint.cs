using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using StudyGarden.API.Contracts.GardenRequests;
using StudyGarden.Application.Services;
using StudyGarden.Core.Models;

namespace StudyGarden.API.Endpoints;

public static class GardenEndpoint 
{
    public static IEndpointRouteBuilder AddGardenEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("CreateGarden", Create);
        app.MapPut("UpdateGarden", Update);
        app.MapDelete("DeleteGarden/{id:int}", Delete);
        app.MapGet("GetAllGardens/{userId:int}", GetAll);
        app.MapGet("GetGarden/{id:int}", Get);
        app.MapGet("GetByUserId/{userId:int}", GetByUserId);
        
        return app;
    }
    private static async Task<IResult> Create([FromBody] GardenRequest model, GardenService service)
    {
        try
        {
            var (garden, error) = Garden.Create(model.UserID, model.TaskID, model.PlantID, 
                model.GrowthStage, model.PositionX, model.PositionY, model.PositionZ);

            if (!string.IsNullOrEmpty(error))
            {
                return Results.BadRequest(error);
            }

            var gardenId = await service.Create(garden);

            return Results.Ok(gardenId);

        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> Update([FromBody] GardenRequest model, GardenService service)
    {
        try
        {
            var (garden, error) = Garden.Create(model.UserID, model.TaskID, model.PlantID, 
                model.GrowthStage, model.PositionX, model.PositionY, model.PositionZ);
            
            var gardenId = await service.Update(garden);
            
            return Results.Ok(gardenId);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> Delete(int id, GardenService service)
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

    private static async Task<IResult> GetAll(GardenService service, int userId = default)
    {
        try
        {
            var gardens = await service.GetAll(userId);

            return Results.Ok(gardens);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e);
        }
    }

    private static async Task<IResult> Get(GardenService service, int id = default)
    {
        try
        {
            var garden = await service.Get(id);

            return Results.Ok(garden);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e);
        }
    }

    private static async Task<IResult> GetByUserId(int userId, GardenService service)
    {
        try
        {
            var userAchievement = await service.GetByUserId(userId);
            
            return Results.Ok(userAchievement);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e);
        }
    }
}