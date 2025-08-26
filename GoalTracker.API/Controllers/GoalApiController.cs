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

        public GoalApiController(IGoalService goalService)
        {
            _goalService = goalService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllGoals()
        {
            var goals = await _goalService.ListAsync();
            return goals.IsNullOrEmpty() ? BadRequest() : Ok(goals);
        }

        [HttpPost("AddOne")]
        public async Task<IActionResult> AddGoal([FromBody] GoalDTO goalDTO)
        {
            try
            {
                await _goalService.AddGoalAsync(goalDTO);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest("There was an error!");
            }
        }
    }
}
