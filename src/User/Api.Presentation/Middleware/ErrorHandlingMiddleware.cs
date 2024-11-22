using Application.Exceptions;
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
            catch (Exception ex)
            {
                _logger.LogError("An error occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode;

            switch (exception)
            {
                case EmailAlreadyRegisteredException:
                    statusCode = StatusCodes.Status409Conflict;
                    break;

                case UserNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    break;

                case UserInactiveException:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;

                case InvalidOperationException:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            _logger.LogError(exception, "An error occurred: {Message}", exception.Message);

            var errorResponse = new
            {
                StatusCode = statusCode,
                Message = exception.Message,
                Detailed = _environment.IsDevelopment() ? exception.ToString() : null
            };

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}
