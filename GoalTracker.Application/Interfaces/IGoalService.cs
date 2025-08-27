using GoalTracker.Application.DTOs;
using GoalTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalTracker.Application.Interfaces
{
    public interface IGoalService
    {
        public Task<List<DailyGoalsDTO>> ListAsync();

        public Task AddGoalAsync(GoalDTO goalDTO);

        public Task DeleteGoalAsync(int dailyID);
    }
}
