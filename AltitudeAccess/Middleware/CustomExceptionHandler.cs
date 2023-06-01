using AltitudeAccess.ServiceLayer.Common;
using AltitudeAccess.ServiceLayer.Exceptions;
using System.Data.Common;
using System.Text.Json;

namespace AltitudeAccess.Middleware
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandler> _logger;

        private Dictionary<Type, (int StatusCode, string Message)> exceptionDictionary = new Dictionary<Type, (int, string)>
        {
            { typeof(UnauthorizedAccessException), (401, "Unauthorized access.") },
            { typeof(UsernameNotAvailableException), (409, "Username is already taken. Please select a different username.") },
            { typeof(BadHttpRequestException), (400, "Bad HTTP Request") },
            { typeof(UserNotFoundException), (404, "User not found. Make sure your username and password are correct.") },
            { typeof(DbException), (500, "Internal Server Error. Please try again later.") }
        };

        public CustomExceptionHandler(RequestDelegate next, ILogger<CustomExceptionHandler> logger) 
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
                _logger.LogError(ex, "An error occurred while processing the request.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private  async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "text/plain";
            var statusCodeAndMessage = exceptionDictionary.ContainsKey(exception.GetType()) ? exceptionDictionary[exception.GetType()] : (500, "An unexpected error occurred.");
            context.Response.StatusCode = statusCodeAndMessage.Item1;

            var errorDetails = new ErrorDetails
            {
                StatusCode = statusCodeAndMessage.Item1,
                Message = statusCodeAndMessage.Item2
            };

            await WriteErrorDetailsToResponse(context.Response, errorDetails);
        }

        private static async Task WriteErrorDetailsToResponse(HttpResponse response, ErrorDetails errorDetails)
        {
            string jsonString = JsonSerializer.Serialize(errorDetails);
            await response.WriteAsync(jsonString);
        }
    }
}
