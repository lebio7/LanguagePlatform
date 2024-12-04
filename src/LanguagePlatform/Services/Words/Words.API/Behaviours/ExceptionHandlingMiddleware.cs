using FluentValidation;
using System.Net;

namespace Words.API.Behaviours;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandlingMiddleware> logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception has occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode code;
        string result;

        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest; // 400
                var errors = validationException.Errors
                    .Select(e => new { e.PropertyName, e.ErrorMessage })
                    .ToList();
                result = System.Text.Json.JsonSerializer.Serialize(new { message = "Validation failed", errors });
                break;
            case ArgumentNullException:
            case ArgumentException:
            case ResultException:
                code = HttpStatusCode.BadRequest; // 400
                result = System.Text.Json.JsonSerializer.Serialize(new { message = exception.Message });
                break;
            case UnauthorizedAccessException:
                code = HttpStatusCode.Unauthorized; // 401
                result = System.Text.Json.JsonSerializer.Serialize(new { message = exception.Message });
                break;
            case KeyNotFoundException:
                code = HttpStatusCode.NotFound; // 404
                result = System.Text.Json.JsonSerializer.Serialize(new { message = exception.Message });
                break;
            default:
                code = HttpStatusCode.InternalServerError; // 500
                result = System.Text.Json.JsonSerializer.Serialize(new { message = "An error occurred while processing your request.", detailed = exception.Message });
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}
