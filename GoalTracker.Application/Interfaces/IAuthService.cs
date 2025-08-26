using GoalTracker.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalTracker.Application.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> Login(LoginDTO loginDTO);
        public Task<bool> Register(LoginDTO loginDTO);

    }
}
