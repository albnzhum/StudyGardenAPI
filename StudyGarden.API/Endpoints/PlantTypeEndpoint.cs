using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using StudyGarden.API.Contracts.PlantType;
using StudyGarden.API.Endpoints.Abstractions;
using StudyGarden.Application.Services;
using StudyGarden.Core.Models;

namespace StudyGarden.API.Endpoints;

public static class PlantTypeEndpoint 
{
    public static IEndpointRouteBuilder AddPlantTypeEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("CreatePlantType", Create);
        app.MapPut("UpdatePlantType", Update);
        app.MapDelete("DeletePlantType/{id:int}", Delete);
        app.MapGet("GetAllPlantTypes", GetAll);
        app.MapGet("GetPlantType/{id:int}", Get);
        
        return app;
    }
    private static async Task<IResult> Create([FromBody] PlantTypeRequest model, PlantTypeService service)
    {
        try
        {
            var (plantType, error) = PlantType.Create(model.Name);

            if (!string.IsNullOrEmpty(error))
            {
                return Results.BadRequest(error);
            }

            var plantTypeId = await service.Create(plantType);

            return Results.Ok(plantTypeId);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e);
        }
    }

    private static async Task<IResult> Update([FromBody] PlantTypeRequest model, PlantTypeService service)
    {
        try
        {
            var (plantType, error) = PlantType.Create(model.Name);

            if (!string.IsNullOrEmpty(error))
            {
                return Results.BadRequest(error);
            }

            var plantTypeId = await service.Update(plantType);
            
            return Results.Ok(plantTypeId);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> Delete(int id, PlantTypeService service)
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

    private static async Task<IResult> GetAll(PlantTypeService service, int userId = default)
    {
        try
        {
            var plantTypes = await service.GetAll(userId);
            
            return Results.Ok(plantTypes);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e);
        }
    }

    private static async Task<IResult> Get(PlantTypeService service, int id = default)
    {
        try
        {
            var plantType = await service.Get(id);
            
            return Results.Ok(plantType);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e);
        }
    }
}