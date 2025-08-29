using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GoalTracker.API
{
    // Global exception handler class 
    public class Exceptionist : IExceptionHandler
    {
        private readonly ILogger _logger;
        private readonly IProblemDetailsService _problemDetailsService;

        public Exceptionist(ILogger<Exceptionist> logger, IProblemDetailsService problemDetailsService)
        {
            _logger = logger;
            _problemDetailsService = problemDetailsService;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
 
            _logger.LogError("Unhandled exception: ");

            httpContext.Response.StatusCode = exception switch
                {
                    ArgumentException => StatusCodes.Status400BadRequest,
                    KeyNotFoundException => StatusCodes.Status404NotFound,
                    UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                    _ => StatusCodes.Status500InternalServerError
                };

            await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Type = exception.GetType().Name,
                    Title = exception switch
                    {
                        ArgumentException => "Bad Request",
                        KeyNotFoundException => "Resource Not Found",
                        UnauthorizedAccessException => "Unauthorized",
                        _ => "Internal Server Error"
                    },
                    Detail = exception.Message,
                }
            });

            return true;
        }
    }
}
