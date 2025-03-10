using ExperimentsDemo.API.Exceptions;
using System.Text.Json;

namespace ExperimentsDemo.API.Extensions;

public static class HttpContextAccessorExtensions
{
    public static async Task SendBadRequestAndAbort(this IHttpContextAccessor? accessor, string message,
        Dictionary<string, string[]>? errors = default)
    {
        var response = new
        {
            title = "Bad request",
            status = StatusCodes.Status400BadRequest,
            detail = message,
            errors
        };

        if (accessor?.HttpContext == null || accessor.HttpContext.Response.HasStarted)
            throw new ValidationException(errors ?? []);

        await accessor.HttpContext.SetResponseAndAbort(StatusCodes.Status400BadRequest,
            JsonSerializer.Serialize(response));
    }

    private static async Task SetResponseAndAbort(this HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(message, context.RequestAborted);
    }
}
