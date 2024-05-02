using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Admin.Application.Commands;
using SchoolApp.Admin.Infrastructure;
using SchoolApp.Admin.Services.Services;

namespace SchoolApp.Admin.WebAPI.Endpoints.Admin;

public static class AdminEndpoints
{
    public static RouteGroupBuilder MapAdminEndpoints(this RouteGroupBuilder app)
    {
        app.MapPost("/admin/roles", ManageUserRolesAsync)
           .RequireAuthorization("Admin")
           .WithName("ManageUserRoles")
           .Produces(StatusCodes.Status200OK)
           .Produces(StatusCodes.Status400BadRequest);

        return app;
    }

    private static async Task<IResult> ManageUserRolesAsync([FromBody] UserRoleModificationRequest request, [FromServices] IIdentityService identityService)
    {
        if (request == null || string.IsNullOrEmpty(request.UserId) || request.Roles == null || !request.Roles.Any())
        {
            return Results.BadRequest("Invalid user role modification request.");
        }

        var result = await identityService.ModifyUserRolesAsync(request.UserId, request.Roles);

        if (result)
        {
            return Results.Ok($"User roles successfully modified for user ID: {request.UserId}.");
        }
        else
        {
            return Results.BadRequest($"Failed to modify user roles for user ID: {request.UserId}.");
        }
    }
}

public class UserRoleModificationRequest
{
    public string UserId { get; set; }
    public List<string> Roles { get; set; }
}
