using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using static Domain.Exceptions.Exceptions;

namespace WebApi.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

            _logger.LogError(
                exception,
                "Could not procces a request on machine: {MachineName} with traceId: {traceId}",
                Environment.MachineName,
                traceId
                );

            var (statusCode, title) = MapException(exception);

            await Results.Problem(
                title: title,
                statusCode: statusCode,
                extensions: new Dictionary<string, object?>
                {
                    { "traceId", traceId}
                }
             ).ExecuteAsync(httpContext);

            return true;
        }

        private static (int StatusCode, string Title) MapException(Exception exception)
        {
            return exception switch
            {
                ArgumentOutOfRangeException or ValidationException => (StatusCodes.Status400BadRequest, exception.Message),
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, exception.Message),
                DbUpdateException => (StatusCodes.Status500InternalServerError, exception.Message),
                NotFoundException => (StatusCodes.Status404NotFound, exception.Message),
                ConflictException => (StatusCodes.Status409Conflict, exception.Message),
                ForbiddenException => (StatusCodes.Status403Forbidden, exception.Message),
                TimeoutException => (StatusCodes.Status408RequestTimeout, exception.Message),
                BadRequestException => (StatusCodes.Status400BadRequest, exception.Message),
                NullReferenceException => (StatusCodes.Status500InternalServerError, exception.Message),
                ServiceUnavailableException => (StatusCodes.Status503ServiceUnavailable, exception.Message),
                _ => (StatusCodes.Status500InternalServerError, "An error occurred")
            };
        }
    }
}
