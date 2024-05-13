using Carter;
using MediatR;
using SoulProject.Api.Endpoints.Models;
using SoulProject.Api.Extensions;
using SoulProject.Application.CQRS.Authentication.Login;

namespace SoulProject.Api.Endpoints;

// ReSharper disable once UnusedType.Global
public class AuthenticationEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var apiVersionSet = app.GetApplicationApiVersionSet();

        var group = app.MapGroup("api/v{versions:apiVersion}/authentication")
                       .WithTags(nameof(AuthenticationEndpoints).ClearEndpointName());

        group.MapPost("login", Login)
             .WithApiVersionSet(apiVersionSet)
             .MapToApiVersion(1);
    }

    private static async Task<IResult> Login(LoginRequest request, ISender sender)
    {
        var result = await sender.Send(new LoginCommand(request.UserName, request.Password));

        return Results.Ok(result);
    }
}