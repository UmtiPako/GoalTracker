using GoalTracker.API;
using GoalTracker.Application.DTOs;
using GoalTracker.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace GoalTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly JwtTokenHelper _jwtTokenHelper;
        private readonly IAuthService _authService;
        private readonly Exceptionist _exceptionist;

        public AuthApiController(JwtTokenHelper jwtTokenHelper, IAuthService authService, Exceptionist exceptionist)
        {
            _jwtTokenHelper = jwtTokenHelper;
            _authService = authService;
            _exceptionist = exceptionist;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                var result = await _authService.Login(loginDTO);

                if (result)
                {
                    var token = _jwtTokenHelper.GenerateToken(loginDTO.Username);
                    return Ok(new { Token = token });
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return _exceptionist.HandleException(ex);
            }

        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginDTO loginDTO)
        {
            try
            {
                var result = await _authService.Register(loginDTO);

                if (result)
                {
                    var token = _jwtTokenHelper.GenerateToken(loginDTO.Username);
                    return Ok(new { Token = token });
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return _exceptionist.HandleException(ex);
            }
        }
    }
}
