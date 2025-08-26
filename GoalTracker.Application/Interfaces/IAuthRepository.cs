using GoalTracker.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalTracker.Application.Interfaces
{
    public interface IAuthRepository
    {
        public Task<bool> LoginAsync(LoginDTO loginDTO);
        public Task<bool> RegisterAsync(LoginDTO loginDTO);

    }
}
