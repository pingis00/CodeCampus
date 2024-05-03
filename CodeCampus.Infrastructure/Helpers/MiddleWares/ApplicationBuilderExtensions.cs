using Microsoft.AspNetCore.Builder;

namespace CodeCampus.Infrastructure.Helpers.MiddleWares;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseUserSessionValidation(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UserSessionValidation>();
    }
}
