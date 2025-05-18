using ECommerceBackendSystem.Application.Abstractions.Dtos.ServiceResponse;
using System.Net;
using System.Text.Json;

namespace ECommerceBackendSystem.API.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var correlationId = context.Items["CorrelationId"]?.ToString();
            this._logger.LogError(ex, "Unhandled exception occurred. Path: {Path}, CorrelationId: {CorrelationId}", context.Request.Path, correlationId);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var response = new ServiceResponse()
            {
                IsSuccessful = false,
                StatusCode = (HttpStatusCode)context.Response.StatusCode,
                Message = $"{ex.Message}"
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
