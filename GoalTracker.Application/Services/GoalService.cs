using GoalTracker.Application.DTOs;
using GoalTracker.Application.Interfaces;
using GoalTracker.Domain.Models;
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

        public GoalService(IGoalRepository goalRepository)
        {
            _goalRepository = goalRepository;
        }

        public async Task<List<DailyGoalsDTO>> ListAsync()
        {
            var list = await _goalRepository.GetAllDailyGoals();
            if (list != null) return list;
            else throw new ArgumentNullException("There are no goals!");
        }
        public async Task<List<Goal>> ListFromDayAsync(DateOnly day)
        {
            var list = await _goalRepository.GetGoalsFromDay(day);
            if (list != null) return list;
            else throw new ArgumentNullException("There are no goals that day!");
        }

        public async Task DeleteGoalAsync(int dailyID)
        {
            await _goalRepository.DeleteAsync(dailyID);
        }

        public async Task AddGoalAsync(GoalDTO goalDTO)
        {
            Goal newGoal = new Goal(goalDTO.goalText);
            await _goalRepository.AddGoalAsync(newGoal);
        }
    }
}
