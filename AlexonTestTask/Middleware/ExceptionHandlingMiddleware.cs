using AlexonTestTask.API.Exceptions;
using System.Text.Json;
using ValidationException = AlexonTestTask.API.Exceptions.ValidationException;

namespace AlexonTestTask.API.Middleware;

public sealed class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (BaseException e)
        {
            _logger.LogError(e, e.Message);

            await HandleExceptionAsync(context, e);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            await ServerError(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, BaseException exception)
    {
        int statusCode;
        object response;
        switch (exception.StatusCode)
        {
            case 401:
                statusCode = StatusCodes.Status401Unauthorized;
                response = new
                {
                    title = "Unauthorized",
                    status = statusCode,
                    detail = exception.Message
                };
                break;
            case 403:
                statusCode = StatusCodes.Status403Forbidden;
                response = new
                {
                    title = "Forbidden",
                    status = statusCode,
                    detail = exception.Message
                };
                break;
            case 404:
                statusCode = StatusCodes.Status404NotFound;
                response = new
                {
                    title = "Not Found",
                    status = statusCode,
                    detail = exception.Message
                };
                break;
            case 400:
                statusCode = StatusCodes.Status400BadRequest;
                response = new
                {
                    title = "Bad Request",
                    status = statusCode,
                    detail = exception.Message,
                };
                break;
            case 422:
                statusCode = StatusCodes.Status400BadRequest;
                response = new
                {
                    title = "Validation Errors",
                    status = statusCode,
                    detail = exception.Message,
                    errors = GetErrors(exception)
                };
                break;
            default:
                statusCode = StatusCodes.Status500InternalServerError;
                response = new
                {
                    title = "Internal Server Error",
                    status = statusCode,
                    detail = exception.Message,
                };
                break;
        }

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static async Task ServerError(HttpContext httpContext, Exception exception)
    {
        int statusCode;
        object response;
        statusCode = StatusCodes.Status500InternalServerError;
        response = new
        {
            title = "Internal Server Error",
            status = statusCode,
            detail = exception.Message,
        };

        httpContext.Response.ContentType = "application/json; charset=UTF-8";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
    {
        IReadOnlyDictionary<string, string[]> errors = new Dictionary<string, string[]>();

        if (exception is ValidationException validationException)
        {
            errors = validationException.ErrorsDictionary;
        }

        return errors;
    }
}
