using GoalTracker.Application.DTOs;
using GoalTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalTracker.Application.Interfaces
{
    public interface IGoalRepository
    {
        Task<List<DailyGoalsDTO>?> GetAllDailyGoals();
        Task<List<Goal>?> GetGoalsFromDay(DateOnly day);
        Task AddGoalAsync(Goal goal);
        Task DeleteAsync(int dailyID);

    }
}
