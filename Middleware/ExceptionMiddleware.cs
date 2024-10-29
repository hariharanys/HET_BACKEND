using HET_BACKEND.Helper;
using HET_BACKEND.Models;
using HET_BACKEND.Utilities.ExceptionUtility;

namespace HET_BACKEND.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly JWTHelper _jwthelper;
        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,JWTHelper jWTHelper) {
            _next = next;
            _logger = logger;
            _jwthelper = jWTHelper;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                bool valid = false;
                valid = _jwthelper.ValidToken(token!);
                if (!valid)
                {
                    await HandleSessionExpiredException(httpContext);
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context,Exception exception)
        {
            _logger.LogError($"Something went wrong: {exception}");

            ErrorMessage errorMessage = new ErrorMessage();
            var statusCode = errorMessage.GetStatusCode(exception);
            var customMessage = errorMessage.GetErrorMessage(statusCode);

            var errorDetails = new ExceptionMiddlewareModel()
            {
                statusCode = statusCode,
                message = customMessage,
                messageTree = exception.Message.ToString()  
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorDetails.statusCode ?? 520;
            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorDetails));
        }

        private Task HandleSessionExpiredException(HttpContext context)
        {
            _logger.LogError($"Something went wrong: Session Expired");
            ErrorMessage errorMessage = new ErrorMessage();
            var statusCode = 419;
            var customMessage = errorMessage.GetErrorMessage(statusCode);
            var errorDetails = new ExceptionMiddlewareModel()
            {
                statusCode = statusCode,
                message = customMessage,
                messageTree = "Session Got Expired. Login Again"
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorDetails.statusCode ?? 520;
            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorDetails));
        }
    }
}
