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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GoalRepository(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddGoalAsync(Goal goal)
        {
            var username = GetCurrentUsername();
            if (string.IsNullOrEmpty(username))
                throw new UnauthorizedAccessException("User is not authenticated");

            goal.GoalUsername = username;

            var today = DateOnly.FromDateTime(DateTime.Today);

            var dailyIDs = await _dbContext.Goals
                .Where(g => g.Date == today && g.GoalUsername == username)
                .Select(g => g.dailyID).ToListAsync();

            var maxDailyId = 0;

            if (!dailyIDs.IsNullOrEmpty()) maxDailyId = dailyIDs.Max();

            goal.dailyID = maxDailyId + 1;

            await _dbContext.Goals.AddAsync(goal);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int dailyID)
        {
            var username = GetCurrentUsername();
            if (string.IsNullOrEmpty(username))
                throw new UnauthorizedAccessException("User is not authenticated");

            var today = DateOnly.FromDateTime(DateTime.Today);
            var goalToDelete = await _dbContext.Goals
                .FirstOrDefaultAsync(g => g.dailyID == dailyID &&
                                   g.Date == today &&
                                   g.GoalUsername == username);

            if (goalToDelete != null)
            {
                _dbContext.Goals.Remove(goalToDelete);
                await _dbContext.SaveChangesAsync();
            }
            else
                throw new ArgumentNullException("No goal to delete!");
        }

        public async Task<List<DailyGoalsDTO>> GetAllDailyGoals()
        {
            var username = GetCurrentUsername();
            if (string.IsNullOrEmpty(username))
                return new List<DailyGoalsDTO>();

            var today = DateOnly.FromDateTime(DateTime.Today);
            var allGoals = await _dbContext.Goals
                .Where(g => g.Date == today && g.GoalUsername == username)
                .Select(g => new DailyGoalsDTO(g.dailyID, g.Text, g.IsDone))
                .ToListAsync();

            return allGoals;
        }

        public async Task<List<DailyGoalsDTO>?> GetGoalsFromDay(DateOnly day)
        {
            var username = GetCurrentUsername();
            if (string.IsNullOrEmpty(username))
                return new List<DailyGoalsDTO>();

            var goalsFromDay = await _dbContext.Goals
                .Where(g => g.Date == day && g.GoalUsername == username)
                .Select(g => new DailyGoalsDTO(g.dailyID, g.Text, g.IsDone))
                .ToListAsync();

            return goalsFromDay;
        }

        #region Helpers
        public string? GetCurrentUsername()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        }
        #endregion
    }
}