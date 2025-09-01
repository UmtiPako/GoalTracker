using GoalTracker.Application.DTOs;
using GoalTracker.Application.Interfaces;
using GoalTracker.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalTracker.Application.Services
{
    public class GoalService : IGoalService
    {
        private readonly IGoalRepository _goalRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GoalService(IGoalRepository goalRepository, IHttpContextAccessor httpContextAccessor)
        {
            _goalRepository = goalRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<DailyGoalsDTO>> ListAsync()
        {
            var username = GetCurrentUsername();
            var today = DateOnly.FromDateTime(DateTime.Today);

            var list = await _goalRepository.GetGoalsFromDay(username, today);
            if (list != null) return list;
            else throw new ArgumentNullException("There are no goals!");
        }
        public async Task<List<DailyGoalsDTO>> ListFromDayAsync(DateOnly day)
        {
            var username = GetCurrentUsername();

            var list = await _goalRepository.GetGoalsFromDay(username, day);
            if (list != null) return list;
            else throw new ArgumentNullException("There are no goals that day!");
        }

        public async Task DeleteGoalAsync(int dailyID)
        {
            var goalDate = DateOnly.FromDateTime(DateTime.Today).AddDays(1);
            var username = GetCurrentUsername();

            await _goalRepository.DeleteAsync(dailyID, username, goalDate);
        }

        public async Task AddGoalAsync(GoalDTO goalDTO)
        {
            var username = GetCurrentUsername();
            var dailyId = await CalculateDailyID();

            Goal newGoal = new Goal(goalDTO.goalText, username, dailyId);
            
            await _goalRepository.AddGoalAsync(newGoal);
        }


        #region Helpers
        public string? GetCurrentUsername()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        }

        public async Task<int> CalculateDailyID()
        {
            var todaysGoals = await ListAsync();

            var nextDailyId = todaysGoals.Any()
            ? todaysGoals.Max(g => g.dailyID) + 1 : 1;

            return nextDailyId;
        }
        #endregion
    }
}
