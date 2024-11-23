using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using StudyGarden.API.Contracts.Task;
using StudyGarden.API.Endpoints.Abstractions;
using StudyGarden.Application.Services;
using Task = StudyGarden.Core.Models.Task;

namespace StudyGarden.API.Endpoints;

public static class TaskEndpoint 
{
    public static IEndpointRouteBuilder AddTaskEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("CreateTask", Create);
        app.MapPut("UpdateTask", Update);
        app.MapGet("GetAllTasks/{userId:int}", GetAll);
        app.MapGet("GetTask/{id:int}", Get);
        
        return app;
    }
    
    private static async Task<IResult> Create([FromBody] TaskRequest model, TaskService service)
    {
        try
        {
            var (task, error) = Task.Create(model.Title, model.Description, model.UserID, model.PlantID, 
                model.CategoryID, model.CreatedDate, model.DueDate, model.Status, model.Priority);

            if (!string.IsNullOrEmpty(error))
            {
                return Results.BadRequest(error);
            }

            var taskId = await service.Create(task);

            return Results.Ok(taskId);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> Update([FromBody] TaskRequest model, TaskService service)
    {
        try
        {
            var (task, error) = Task.Create(model.Title, model.Description, model.UserID, model.PlantID,
                model.CategoryID, model.CreatedDate, model.DueDate, model.Status, model.Priority);

            if (!string.IsNullOrEmpty(error))
            {
                return Results.BadRequest(error);
            }
            
            var taskId = await service.Update(task);
            
            return Results.Ok(taskId);
            
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> Delete(int id, TaskService service)
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

    private static async Task<IResult> GetAll(TaskService service, int userId = default)
    {
        try
        {
            var tasks = await service.GetAll(userId);
            
            return Results.Ok(tasks);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> Get(TaskService service, int id = default)
    {
        try
        {
            var task = await service.Get(id);
            
            return Results.Ok(task);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }
}