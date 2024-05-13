using Carter;
using MediatR;
using SoulProject.Api.Extensions;
using SoulProject.Application.CQRS.Users.Queries.GetAllUsers;

namespace SoulProject.Api.Endpoints;

public class UserEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var apiVersionSet = app.GetApplicationApiVersionSet();
        
        var group = app.MapGroup("api/v{versions:apiVersion}/users")
                       .WithTags(nameof(UserEndpoints).ClearEndpointName())
                       .AuthorizationRequired();
        
        group.MapGet(string.Empty, GetAllUsers)
             .WithName(nameof(GetAllUsers))
             .WithApiVersionSet(apiVersionSet)
             .MapToApiVersion(1);
    }

    private static async Task<IResult> GetAllUsers(ISender sender)
    {
        var result = await sender.Send(new GetAllUsersQuery());

        return Results.Ok(result);
    }
}