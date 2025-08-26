using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalTracker.Application.DTOs
{
    public class DailyGoalsDTO
    {
        public Guid guid { get; }
        public string GoalText { get; }

        public DailyGoalsDTO(Guid guid, string goalText)
        {
            this.guid = guid;
            this.GoalText = goalText;
        }
    }
}
