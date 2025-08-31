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

        [Range(1, int.MaxValue, ErrorMessage = "Daily ID is out of bounds!!!")] 
        public int dailyID { get; set; }

        public string GoalUsername { get; set; }

        [StringLength(40, MinimumLength = 3, ErrorMessage = "Goal text length should be '3 < x < 40'!")]
        [Required(ErrorMessage = "Goal text is reqired!")] 
        public string Text { get; private set; }

        public bool IsDone { get; private set; }
        public DateOnly Date { get; private set; }

        public Goal(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Goal text cannot be null or empty", nameof(text));

            if (text.Trim().Length < 3)
                throw new ArgumentException("Goal text must be at least 3 characters long", nameof(text));

            if (text.Trim().Length > 40)
                throw new ArgumentException("Goal text cannot exceed 40 characters", nameof(text));

            Text = text.Trim();
            Date = DateOnly.FromDateTime(DateTime.Now).AddDays(1); 
            IsDone = false;
        }
    }
}
