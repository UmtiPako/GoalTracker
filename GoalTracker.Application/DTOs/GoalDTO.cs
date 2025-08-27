using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalTracker.Application.DTOs
{
    public class GoalDTO
    {
        [Required(ErrorMessage = "Goal text is required!")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Goal text length should be 3 < x < 40!")]
        public required string goalText { get; set; }
    }
}
