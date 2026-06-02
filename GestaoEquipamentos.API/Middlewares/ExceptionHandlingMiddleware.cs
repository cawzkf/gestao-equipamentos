using System.Text.Json;
using GestaoEquipamentos.Exceptions;

namespace GestaoEquipamentos.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleAsync(context, ex);
        }
    }

    private async Task HandleAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message, errors) = exception switch
        {
            NotFoundException ex         => (StatusCodes.Status404NotFound,       ex.Message, (object?)null),
            ConflictException ex         => (StatusCodes.Status409Conflict,        ex.Message, (object?)null),
            AuthenticationException ex   => (StatusCodes.Status401Unauthorized,   ex.Message, (object?)null),
            ForbiddenException ex        => (StatusCodes.Status403Forbidden,       ex.Message, (object?)null),
            BusinessRuleException ex     => (StatusCodes.Status422UnprocessableEntity, ex.Message, (object?)null),
            ValidationException ex       => (StatusCodes.Status400BadRequest,      ex.Message, (object?)ex.Errors),
            _                            => (StatusCodes.Status500InternalServerError, "Ocorreu um erro interno.", (object?)null),
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
            _logger.LogError(exception, "Unhandled exception");

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var body = new { message, errors };
        await context.Response.WriteAsync(JsonSerializer.Serialize(body, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        }));
    }
}
