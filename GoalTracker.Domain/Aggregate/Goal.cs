using GoalTracker.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalTracker.Domain.Models
{
    public class Goal
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public int dailyID { get; private set; }
        public GoalUsername GoalUsername { get; private set; }
        public GoalText Text { get; private set; }
        public bool IsDone { get; private set; }
        public DateOnly Date { get; private set; }

        private Goal() { }

        public Goal(string text, string username, int dailyID)
        {

            if (dailyID <= 0)
                throw new ArgumentException("DailyID should be positive!", nameof(dailyID));

            Text = new GoalText(text);
            GoalUsername = new GoalUsername(username);

            this.dailyID = dailyID;

            Date = DateOnly.FromDateTime(DateTime.Now).AddDays(1);
            IsDone = false;
        }


        public void MarkIsDone(bool val)
        {
            IsDone = val;
        }
    } 
}
