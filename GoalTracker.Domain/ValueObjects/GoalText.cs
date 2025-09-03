using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GoalTracker.Domain.ValueObjects
{
    public record GoalText
    {
        public string Value { get; }

        private GoalText() { }
        public GoalText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Goal text cannot be null or empty", nameof(text));

            if (text.Trim().Length < 3)
                throw new ArgumentException("Goal text must be at least 3 characters long", nameof(text));

            if (text.Trim().Length > 40)
                throw new ArgumentException("Goal text cannot exceed 40 characters", nameof(text));

            Value = text;
        }

        public override string ToString() => Value;
        public bool Equals(string text) => Value.Equals(text);

    }
}
