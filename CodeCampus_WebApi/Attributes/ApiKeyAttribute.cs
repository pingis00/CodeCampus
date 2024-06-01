using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodeCampus_WebApi.Attrubutes;

[AttributeUsage(AttributeTargets.All)]
public class ApiKeyAttribute : Attribute, IAsyncActionFilter
{
    private const string ApiKeyHeaderName = "X-Api-Key";
    private const string AdminApiKeyHeaderName = "X-Admin-Api-Key";
    private readonly bool _requireAdmin;

    public ApiKeyAttribute(bool requireAdmin = false)
    {
        _requireAdmin = requireAdmin;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var apiKey = configuration["ApiKey"];
        var adminApiKey = configuration["AdminApiKey"];

        if (_requireAdmin)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(AdminApiKeyHeaderName, out var extractedAdminApiKey) || extractedAdminApiKey != adminApiKey)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
        else
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey) || extractedApiKey != apiKey)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }

        await next();
    }
}
