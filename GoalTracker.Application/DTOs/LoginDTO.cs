using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalTracker.Application.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Username is required!")] public required string Username { get; set; }
        [Required(ErrorMessage = "Password is required!")] public required string Password { get; set; }

    }
}
