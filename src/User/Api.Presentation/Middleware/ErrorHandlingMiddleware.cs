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

            // Determinamos el código de estado HTTP dependiendo del tipo de excepción
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
    //public class ErrorHandlingMiddleware
    //{
    //    private readonly RequestDelegate _next;
    //    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    //    private readonly IWebHostEnvironment _environment;

    //    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger, IWebHostEnvironment environment)
    //    {
    //        _next = next;
    //        _logger = logger;
    //        _environment = environment;
    //    }

    //    public async Task Invoke(HttpContext context)
    //    {
    //        try
    //        {
    //            await _next(context);
    //        }
    //        catch (InvalidOperationException ex)
    //        {
    //            _logger.LogWarning("Invalid operation: {Message}", ex.Message);
    //            //await HandleExceptionAsync(context, StatusCodes.Status400BadRequest, ex.Message, ex);
    //            await HandleExceptionAsync(context, ex);
    //        }
    //    }
    //    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    //    {
    //        int statusCode = ex switch
    //        {
    //            EmailAlreadyRegisteredException => StatusCodes.Status409Conflict,
    //            UserNotFoundException => StatusCodes.Status404NotFound,
    //            UserInactiveException => StatusCodes.Status400BadRequest,
    //            InvalidOperationException => StatusCodes.Status400BadRequest,
    //            _ => StatusCodes.Status500InternalServerError
    //        };

    //        _logger.LogError(ex, "An error occurred: {Message}", ex.Message);

    //        var errorResponse = new
    //        {
    //            StatusCode = statusCode,
    //            Message = ex.Message,
    //            Detailed = _environment.IsDevelopment() ? ex.ToString() : null
    //        };

    //        context.Response.StatusCode = statusCode;
    //        context.Response.ContentType = "application/json";
    //        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
    //    }
    //    //private async Task HandleExceptionAsync(HttpContext context, int statusCode, string message, Exception exception)
    //    //{
    //    //    context.Response.StatusCode = statusCode;
    //    //    context.Response.ContentType = "application/json";

    //    //    var errorResponse = new
    //    //    {
    //    //        StatusCode = statusCode,
    //    //        Message = message,
    //    //        Detailed = _environment.IsDevelopment() ? exception.ToString() : null // Solo en desarrollo se envían detalles
    //    //    };

    //    //    var errorJson = JsonConvert.SerializeObject(errorResponse);
    //    //    await context.Response.WriteAsync(errorJson);
    //    //}
    //}

    //public class EmailAlreadyRegisteredException : InvalidOperationException
    //{
    //    public EmailAlreadyRegisteredException() : base("The e-mail address is already registered.") { }
    //}

    //public class UserNotFoundException : KeyNotFoundException
    //{
    //    public UserNotFoundException() : base("No user found.") { }
    //}

    //public class UserInactiveException : InvalidOperationException
    //{
    //    public UserInactiveException() : base("User not found or already inactive.") { }
    //}

    //public static class ErrorHandlingMiddlewareExtensions
    //{
    //    public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    //    }
    //}
}
