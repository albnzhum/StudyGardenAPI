using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using StudyGarden.Application.Services;
using StudyGarden.Contracts.UserRequests;

namespace StudyGarden.API.Endpoints;

public static class UsersEndpoint
{
    public static IEndpointRouteBuilder AddUsersEndpoints(this
        IEndpointRouteBuilder app)
    {
        app.MapPost("Register", Register);
        app.MapPost("Login", Login);
        app.MapDelete("Delete", Delete);

        return app;
    }

    private static async Task<IResult> Register(
        [FromBody]RegisterUserRequest request,
        UserService userService)
    {
        await userService.Register(request.Login, request.Password);

        return Results.Ok("Register");
    }

    private static async Task<IResult> Login(
        [FromBody]LoginUserRequest request,
        UserService userService,
        HttpContext context)
    {
        var token = await userService.Login(request.Login, request.Password);

        context.Response.Cookies.Append("tasty-cookies", token);

        return Results.Ok(token);
    }

    private static async Task<IResult> Delete(int id, UserService userService, HttpContext context)
    {
        return Results.Ok(await userService.DeleteUser(id));
    }
}