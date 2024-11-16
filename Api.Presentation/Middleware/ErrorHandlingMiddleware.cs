using Newtonsoft.Json;

namespace Api.Presentation.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Invalid operation: {Message}", ex.Message);
                await HandleExceptionAsync(context, StatusCodes.Status400BadRequest, ex.Message, ex);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Token not found: {Message}", ex.Message);
                await HandleExceptionAsync(context, StatusCodes.Status404NotFound, ex.Message, ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning("Unauthorized access: {Message}", ex.Message);
                await HandleExceptionAsync(context, StatusCodes.Status401Unauthorized, "Unauthorized access", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, "An unexpected error occurred", ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, int statusCode, string message, Exception exception)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                StatusCode = statusCode,
                Message = message,
                Detailed = _environment.IsDevelopment() ? exception.ToString() : null // Solo en desarrollo se envían detalles
            };

            var errorJson = JsonConvert.SerializeObject(errorResponse);
            await context.Response.WriteAsync(errorJson);
        }
    }

    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
