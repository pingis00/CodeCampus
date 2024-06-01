using CodeCampus.Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CodeCampus.Infrastructure.Helpers.MiddleWares;

public class UserSessionValidation(RequestDelegate next, ILogger<UserSessionValidation> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<UserSessionValidation> _logger = logger;

    private static bool IsAjaxRequest(HttpRequest request) => request.Headers.XRequestedWith == "XMLHttpRequest";

    public async Task InvokeAsync(HttpContext context, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
    {
        try
        {
            if (context.User.Identity!.IsAuthenticated)
            {
                var user = await userManager.GetUserAsync(context.User);
                if (user == null)
                {
                    _logger.LogWarning("User not found, signing out.");
                    await signInManager.SignOutAsync();

                    if (!IsAjaxRequest(context.Request) && context.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
                    {
                        context.Response.Redirect("/signin");
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during user session validation.");
            throw;
        }

        await _next(context);
    }
}
