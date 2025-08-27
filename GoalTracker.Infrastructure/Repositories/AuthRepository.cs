using GoalTracker.Application.DTOs;
using GoalTracker.Application.Interfaces;
using GoalTracker.Domain.Entities;
using GoalTracker.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GoalTracker.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AuthRepository(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<bool> LoginAsync(LoginDTO loginDTO)
        {
            var existingUser = await _userManager.FindByNameAsync(loginDTO.Username);

            if (existingUser == null) return false;

            bool result = await _userManager.CheckPasswordAsync(existingUser, loginDTO.Password);
            if (result)
                return true;
            else
                return false;
        }

        public async Task<bool> RegisterAsync(LoginDTO loginDTO)
        {
            AppUser new_user = new AppUser { UserName = loginDTO.Username };

            IdentityResult result = await _userManager.CreateAsync(new_user, loginDTO.Password);

            if (result.Succeeded) return true;
            else return false;
 
        }
    }
}
