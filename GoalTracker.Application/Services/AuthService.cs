using GoalTracker.Application.DTOs;
using GoalTracker.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalTracker.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<bool> Login(LoginDTO loginDTO)
        {
            var result = await _authRepository.LoginAsync(loginDTO);

            if (result)
                return true;
            else
                return false;
        }
        
        public async Task<bool> Register(LoginDTO loginDTO)
        {
            var result = await _authRepository.RegisterAsync(loginDTO);

            if (result)
                return true;
            else
                return false;
        }
    }
}
