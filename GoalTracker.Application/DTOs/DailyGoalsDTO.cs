using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalTracker.Application.DTOs
{
    public class DailyGoalsDTO
    {
        public int dailyID { get; }
        public string GoalText { get; }

        public DailyGoalsDTO(int dailyID, string goalText)
        {
            this.dailyID = dailyID;
            this.GoalText = goalText;
        }
    }
}
