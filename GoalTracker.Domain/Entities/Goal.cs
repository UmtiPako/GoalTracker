using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalTracker.Domain.Models
{
    public class Goal 
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public int dailyID { get; set; }
        public string GoalUsername { get; set; }
        public string Text { get; private set; }
        public bool IsDone { get; private set; }
        public DateOnly Date { get; private set; }

        public Goal(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException("text required");
            Text = text.Trim();
            Date = DateOnly.FromDateTime(DateTime.Now); 
            IsDone = false;
        }
    }
}
