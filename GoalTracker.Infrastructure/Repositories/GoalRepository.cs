using GoalTracker.Application.DTOs;
using GoalTracker.Application.Interfaces;
using GoalTracker.Domain.Models;
using GoalTracker.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoalTracker.Infrastructure.Repositories
{
    public class GoalRepository : IGoalRepository
    {
        private readonly AppDbContext _dbContext;

        public GoalRepository(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
        }

        public async Task AddGoalAsync(Goal goal)
        {
            await _dbContext.Goals.AddAsync(goal);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int dailyID, string username, DateOnly goalDate)
        {
            var goalToDelete = await _dbContext.Goals
                .FirstOrDefaultAsync(g => g.dailyID == dailyID &&
                                   g.Date == goalDate &&
                                   g.GoalUsername.Equals(username));

            if (goalToDelete != null)
            {
                _dbContext.Goals.Remove(goalToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<DailyGoalsDTO>> GetGoalsFromDay(string username, DateOnly day)
        {
            var goalsFromDay = await _dbContext.Goals
                .Where(g => g.Date == day && g.GoalUsername.Equals(username) )
                .Select(g => new DailyGoalsDTO(g.dailyID, g.Text.Value, g.IsDone))
                .ToListAsync();

            return goalsFromDay;
        }
    }
}