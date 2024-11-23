using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using StudyGarden.API.Contracts.FriendsRequests;
using StudyGarden.Application.Services;
using StudyGarden.Core.Models;

namespace StudyGarden.API.Endpoints;

public static class FriendEndpoint 
{
    public static IEndpointRouteBuilder AddFriendEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("CreateFriend", Create);
        app.MapPut("UpdateFriend", Update);
        app.MapDelete("DeleteFriend/{id:int}", Delete);
        app.MapGet("GetAllFriends", GetAll);
        app.MapGet("GetFriend/{id:int}", Get);
        return app;
    }
    private static async Task<IResult> Create([FromBody] FriendRequest model, FriendService service)
    {
        try
        {
            var (friend, error) = Friend.Create(model.UserID, model.FriendID, model.IsAccepted);
            
            if (!string.IsNullOrEmpty(error))
            {
                return Results.BadRequest(error);
            }

            var friendId = await service.Create(friend);

            return Results.Ok(friendId);

        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> Update([FromBody] FriendRequest model, FriendService service)
    {
        try
        {
            var (friend, error) = Friend.Create(model.FriendID, model.UserID, model.IsAccepted);
            
            await service.Update(friend);

            return Results.Ok(friend.ID);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> Delete(int id, FriendService service)
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

    private static async Task<IResult> GetAll(FriendService service, int userId = default)
    {
        try
        {
            var friends = await service.GetAll(userId);
            
            return Results.Ok(friends);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    private static async Task<IResult> Get(FriendService service, int id = default)
    {
        try
        {
            var friend = await service.Get(id);
            
            return Results.Ok(friend);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }
}