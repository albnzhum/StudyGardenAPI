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
        app.MapPost("CreateUserCategory", Create);
        app.MapPut("UpdateUserCategory", Update);
        app.MapDelete("DeleteUserCategory/{id:int}", Delete);
        app.MapGet("GetAllUserCategory", GetAll);
        app.MapGet("GetUserCategory/{id:int}", Get);
        app.MapGet("GetAllByType/{typeId:int}", GetAllByType);

        return app;
    }

    private static async Task<IResult> Create([FromBody] UserCategoryRequest userCategory, UserCategoryService service)
    {
        try
        {
            var (category, error) =
                UserCategory.Create(userCategory.UserID, userCategory.PlantTypeID, userCategory.Title);

            if (!string.IsNullOrEmpty(error))
            {
                return Results.BadRequest(error);
            }

            var categoryId = await service.Create(category);
            
            return Results.Ok(categoryId);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> Update([FromBody] UserCategoryRequest model, UserCategoryService service)
    {
        try
        {
            var userCategory = UserCategory.Create(model.UserID, model.PlantTypeID, model.Title);
            
            await service.Update(userCategory.userCategory);
            
            return Results.Ok(userCategory.userCategory.ID);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> Delete(int id, UserCategoryService service)
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

    private static async Task<IResult> GetAll(UserCategoryService service, int userId = default)
    {
        try
        {
            var categories = await service.GetAll(userId);
            
            return Results.Ok(categories);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> Get(UserCategoryService service, int id = default)
    {
        try
        {
            var category = await service.Get(id);
            return Results.Ok(category);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> GetAllByType(UserCategoryService service, int typeId = default)
    {
        try
        {
            var categories = await service.GetAllByType(typeId);
            
            return Results.Ok(categories);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }
}