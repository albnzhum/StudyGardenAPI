using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using StudyGarden.API.Contracts.UserCategory;
using StudyGarden.Application.Services;
using StudyGarden.Core.Models;

namespace StudyGarden.API.Endpoints;

public static class UserCategoryEndpoint 
{
    public static IEndpointRouteBuilder AddUserCategoryEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("Create", Create);
        app.MapPut("Update", Update);
        app.MapDelete("Delete/{id:int}", Delete);
        app.MapGet("GetAll", GetAll);
        app.MapGet("Get/{id:int}", Get);

        return app;
    }

    private static async Task<IResult> Create([FromBody] UserCategoryRequest userCategory, UserCategoryService service)
    {
        var (category, error) = UserCategory.Create(userCategory.UserID, userCategory.PlantTypeID, userCategory.Title);

        if (!string.IsNullOrEmpty(error))
        {
            return Results.BadRequest(error);
        }

        var categoryId = await service.Create(category);
        return Results.Ok(categoryId);
    }

    private static async Task<IResult> Update([FromBody] UserCategoryRequest model, UserCategoryService service)
    {
        var userCategory = UserCategory.Create(model.UserID, model.PlantTypeID, model.Title);
        await service.Update(userCategory.userCategory);
        return Results.Ok("Update successful");
    }

    private static async Task<IResult> Delete(int id, UserCategoryService service)
    {
        await service.Delete(id);
        return Results.Ok("Delete successful");
    }

    private static async Task<IResult> GetAll(UserCategoryService service, int userId = default)
    {
        var categories = await service.GetAll(userId);
        return Results.Ok(categories);
    }

    private static async Task<IResult> Get(UserCategoryService service, int id = default)
    {
        var category = await service.Get(id);
        return Results.Ok(category);
    }
}