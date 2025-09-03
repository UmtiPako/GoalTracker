using GoalTracker.Domain.Entities;
using GoalTracker.Domain.Models;
using GoalTracker.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalTracker.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Goal> Goals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Goal>(entity =>
            {
                entity.Property(g => g.Text)
                      .HasConversion(
                          v => v.Value,              
                          v => new GoalText(v)      
                      );

                entity.Property(g => g.GoalUsername)
                      .HasConversion(
                          v => v.Value,
                          v => new GoalUsername(v)
                      );
            });
        }


    }
}
