using Microsoft.AspNetCore.Mvc;

namespace GoalTracker.API
{
    public class Exceptionist : ControllerBase
    {
        public IActionResult HandleException(Exception ex)
        {
            return ex switch
            {
                ArgumentException => BadRequest(new { message = ex.Message }),
                KeyNotFoundException => NotFound(new { message = ex.Message }),
                UnauthorizedAccessException => Unauthorized(new { message = "Unauthorized" }),
                _ => StatusCode(500, new { message = "Unexpected error occurred" })
            };
        }
    }
}
