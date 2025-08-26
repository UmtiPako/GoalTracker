using GoalTracker.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GoalTracker.Application.Services;

namespace GoalTracker.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        {

            services.AddScoped<IGoalService, GoalService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
