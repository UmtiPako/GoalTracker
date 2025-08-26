using GoalTracker.Application.DTOs;
using GoalTracker.Application.Interfaces;
using GoalTracker.Domain.Models;
using GoalTracker.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalTracker.Infrastructure.Repositories
{
    public class GoalRepository : IGoalRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GoalRepository(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;        
        }

        public async Task AddGoalAsync(Goal goal)
        {
            goal.GoalUsername = _httpContextAccessor.HttpContext?.User.Identity.Name;

            await _dbContext.AddAsync(goal);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Goal goal)
        {
             _dbContext.Goals.Remove(goal);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<DailyGoalsDTO>?> GetAllDailyGoals()
        {
            var userNameAsking = _httpContextAccessor.HttpContext?.User.Identity.Name;

            var allGoals = await _dbContext.Goals.Where
                (g => g.Date == DateOnly.FromDateTime(DateTime.Today) && 
                g.GoalUsername == userNameAsking)
                .ToListAsync();

            List<DailyGoalsDTO> allGoalDTOs = new List<DailyGoalsDTO>();

            foreach (var goal in allGoals)
            {
                var goalDTO = new DailyGoalsDTO(goal.Id, goal.Text);
                allGoalDTOs.Add(goalDTO);
            }

            return allGoalDTOs;
        }

        public async Task<List<Goal>?> GetGoalsFromDay(DateOnly day)
        {
            var userNameAsking = _httpContextAccessor.HttpContext?.User.Identity.Name;

            var goalsFromDay = await _dbContext.Goals.Where(g => g.Date == day 
            && g.GoalUsername == userNameAsking).ToListAsync();
            return goalsFromDay;
        }
    }
}
