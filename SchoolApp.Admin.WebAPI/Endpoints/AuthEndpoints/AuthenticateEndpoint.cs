using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using SchoolApp.Admin.Domain.SeedWork;
using SchoolApp.Admin.Infrastructure.Identity;


namespace SchoolApp.Admin.WebAPI.Endpoints.AuthEndpoints;

public static class AuthEndpoints
{
    public static SignInManager<ApplicationUser>? InManager { get; }
    public static ITokenClaimsService? TokenClaimsService { get; }


    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {

        app.MapPost("/authenticate", HandleAsync);

            return app;
    }

    private static async Task<Ok<AuthenticateResponse>> HandleAsync(IMapper mapper, AuthenticateRequest request)
    {
        var response = new AuthenticateResponse(request.CorrelationId());

        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        //var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
        var result = await InManager.PasswordSignInAsync(request.Username, request.Password, false, true);

        response.Result = result.Succeeded;
        response.IsLockedOut = result.IsLockedOut;
        response.IsNotAllowed = result.IsNotAllowed;
        response.RequiresTwoFactor = result.RequiresTwoFactor;
        response.Username = request.Username;
        if (result.Succeeded)
        {
            response.Token = await TokenClaimsService.GetTokenAsync(request.Username);
        }

        return TypedResults.Ok(response);
    }


    

    
}
