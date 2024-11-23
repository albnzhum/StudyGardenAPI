using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using StudyGarden.API.Contracts.Plant;
using StudyGarden.API.Endpoints.Abstractions;
using StudyGarden.Application.Services;
using StudyGarden.Core.Models;

namespace StudyGarden.API.Endpoints;

public static class PlantEndpoint 
{
    public static IEndpointRouteBuilder AddPlantEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("CreatePlant", Create);
        app.MapPut("UpdatePlant", Update);
        app.MapDelete("DeletePlant{id:int}", Delete);
        app.MapGet("GetAllPlants", GetAll);
        app.MapGet("GetPlant", Get);
        app.MapGet("GetPlantsByPlantTypeID/{typeId:int}", GetPlantsByPlantTypeID);
        
        return app;
    }
    private static async Task<IResult> Create([FromBody] PlantRequest model, PlantService service)
    {
        try
        {
            var (plant, error) = Plant.Create(model.Name, model.PlantTypeID);

            if (!string.IsNullOrEmpty(error))
            {
                return Results.BadRequest(error);
            }

            var plantId = await service.Create(plant);

            return Results.Ok(plantId);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> Update([FromBody] PlantRequest model, PlantService service)
    {
        try
        {
            var (plant, error) = Plant.Create(model.Name, model.PlantTypeID);

            if (!string.IsNullOrEmpty(error))
            {
                return Results.BadRequest(error);
            }

            var plantId = await service.Update(plant);
            
            return Results.Ok(plantId);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> Delete(int id, PlantService service)
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

    private static async Task<IResult> GetAll(PlantService service, int userId = default)
    {
        try
        {
            var plants = await service.GetAll(userId);
            
            return Results.Ok(plants);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> Get(PlantService service, int id = default)
    {
        try
        {
            var plant = await service.Get(id);
            
            return Results.Ok(plant);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e);
        }
    }

    private static async Task<IResult> GetPlantsByPlantTypeID(PlantService service, int typeId = default)
    {
        try
        {
            var plants = await service.GetPlantsByPlantTypeID(typeId);
            
            return Results.Ok(plants);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }
}