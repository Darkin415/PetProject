using PetProject.Contracts;
using PetProject.Domain.Shared.ValueObject;
using static PetProject.Contracts.Envelope;

namespace PetProject.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // вызов всего приложения 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            var error = PetProject.Domain.Shared.ValueObject.Error.Failure("server.internal", ex.Message);

            var envelope = Envelope.Error(error);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(envelope);
        }

    }
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}