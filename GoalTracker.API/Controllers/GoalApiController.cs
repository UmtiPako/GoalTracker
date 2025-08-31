using GoalTracker.Application.DTOs;
using GoalTracker.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoalTracker.ApiControllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GoalApiController : ControllerBase
    {
        private readonly IGoalService _goalService;

        public GoalApiController(IGoalService goalService)
        {
            _goalService = goalService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllGoals()
        {
            var goals = await _goalService.ListAsync();

            if (goals == null || goals.Count == 0)
                return NotFound(new { message = "No goals found" });

            return Ok(goals);
        }

        [HttpGet("GetFrom")]
        public async Task<IActionResult> GetFromDay(DateOnly day)
        {
            var goalsFrom = await _goalService.ListFromDayAsync(day);
            return Ok(goalsFrom);
        }

        [HttpPost("AddOne")]
        public async Task<IActionResult> AddGoal([FromBody] GoalDTO goalDTO)
        {
            await _goalService.AddGoalAsync(goalDTO);
            return Ok();
        }

        [HttpDelete("DelOne")]
        public async Task<IActionResult> DeleteGoal([FromBody] int dailyID)
        {
            await _goalService.DeleteGoalAsync(dailyID);
            return Ok();
        }
    }
}
