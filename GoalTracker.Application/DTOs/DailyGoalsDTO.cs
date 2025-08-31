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
        public bool isDone { get; set; }

        public DailyGoalsDTO(int dailyID, string goalText, bool isDone)
        {
            this.dailyID = dailyID;
            this.GoalText = goalText;
            this.isDone = isDone;
        }
    }
}
