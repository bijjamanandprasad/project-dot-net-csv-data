using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ClientApi.Models;

namespace ClientApi.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(ApiKey.HeaderName, out var potentialApiKey))
        {
            context.Result = new UnauthorizedObjectResult(new { message = "API Key is missing" });
            return;
        }

        var providedApiKey = potentialApiKey.ToString();

        if (string.IsNullOrWhiteSpace(providedApiKey))
        {
            context.Result = new UnauthorizedObjectResult(new { message = "API Key is empty" });
            return;
        }

        // Check if the token starts with "Bearer " prefix
        if (providedApiKey.StartsWith(ApiKey.BearerPrefix))
        {
            providedApiKey = providedApiKey.Substring(ApiKey.BearerPrefix.Length);
        }

        if (!ApiKey.ValidToken.Equals(providedApiKey, StringComparison.OrdinalIgnoreCase))
        {
            context.Result = new UnauthorizedObjectResult(new { message = "Invalid API Key" });
            return;
        }

        await next();
    }
} 