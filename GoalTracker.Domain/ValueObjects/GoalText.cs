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
        public GoalText(string val)
        {
            if (string.IsNullOrWhiteSpace(val))
                throw new ArgumentException("Goal text cannot be null or empty", nameof(val));

            if (val.Trim().Length < 3)
                throw new ArgumentException("Goal text must be at least 3 characters long", nameof(val));

            if (val.Trim().Length > 40)
                throw new ArgumentException("Goal text cannot exceed 40 characters", nameof(val));

            Value = val;
        }

        public override string ToString() => Value;
        public bool Equals(string val) => Value.Equals(val);

    }
}
