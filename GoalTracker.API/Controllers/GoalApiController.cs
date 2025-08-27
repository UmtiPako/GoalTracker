using GoalTracker.API;
using GoalTracker.Application.DTOs;
using GoalTracker.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GoalTracker.ApiControllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GoalApiController : ControllerBase
    {
        private readonly IGoalService _goalService;
        private readonly Exceptionist _exceptionist;

        public GoalApiController(IGoalService goalService)
        {
            _goalService = goalService;
            _exceptionist = new Exceptionist();
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllGoals()
        {
            try
            {
                var goals = await _goalService.ListAsync();

                if (goals == null || goals.Count == 0)
                    return NotFound(new { message = "No goals found" });

                return Ok(goals);
            }
            catch (Exception ex) 
            {
                return _exceptionist.HandleException(ex);
            }

        }

        [HttpPost("AddOne")]
        public async Task<IActionResult> AddGoal([FromBody] GoalDTO goalDTO)
        {
            try
            {
                await _goalService.AddGoalAsync(goalDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return _exceptionist.HandleException(ex);
            }
        }

        [HttpDelete("DelOne")]
        public async Task<IActionResult> DeleteGoal([FromBody] int dailyID)
        {
            try
            {
                await _goalService.DeleteGoalAsync(dailyID);
                return Ok();
            }
            catch (Exception ex)
            {
                return _exceptionist.HandleException(ex);
            }
        }
    }
}
