using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalTracker.Domain.ValueObjects
{
    public record GoalUsername
    {
        public string Value { get; }

        public GoalUsername(string username)
        {
            if (username.Trim().Length > 16)
                throw new ArgumentException("Username cannot exceed 16 characters", nameof(username));

            if (username.Trim().Length < 3)
                throw new ArgumentException("Username must be at least 3 characters long", nameof(username));

            Value = username;
        }

        public override string ToString() => Value;

        public bool Equals(string val) => Value.Equals(val);
    }
}
