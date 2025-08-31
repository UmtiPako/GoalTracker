using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using System.Security.Principal;
using GoalTracker.Infrastructure.Repositories;
using GoalTracker.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using GoalTracker.Infrastructure.Persistence;
using GoalTracker.Domain.Models;
using Microsoft.Identity.Client;
using GoalTracker.Application.DTOs;

namespace GoalTracker.Infrastructure.Tests
{
    public class AppInfrastructureTests 
    {
        private readonly AppDbContext _context;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly GoalRepository _repository; 

        public AppInfrastructureTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            _repository = new GoalRepository(_context, _mockHttpContextAccessor.Object);
        }

        [Fact]
        public void GetCurrentUsername_ShouldReturnUsername_IfAuthenticated()
        {
            var mockIdentity = new Mock<IIdentity>();
            var mockUser = new Mock<ClaimsPrincipal>();
            var mockContext = new Mock<HttpContext>();

            mockIdentity.Setup(x => x.Name).Returns("Umti");
            mockUser.Setup(x => x.Identity).Returns(mockIdentity.Object); 
            mockContext.Setup(x => x.User).Returns(mockUser.Object); 
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockContext.Object); 

            var result = _repository.GetCurrentUsername(); 

            Assert.Equal("Umti", result);
        }
    

        [Fact]
        public void GetCurrentUsername_ShouldReturnNull_IfNotAuthenticated()
        {
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns((HttpContext?)null);

            var result = _repository.GetCurrentUsername();

            Assert.Null(result);
        }

        [Fact]
        public async Task AddGoalAsync_AddedGoalDateMustBeTomorrow()
        {
            // Arrange
            var goal = new Goal("Goal created for addition test.");

            // Act
            await _repository.AddGoalAsync(goal);
            await _context.SaveChangesAsync();
            var tomorrowsGoals = await _context.Goals
                .Where(g => g.Date == DateOnly.FromDateTime(DateTime.Today.AddDays(1)))
                .ToListAsync();

            // Assert
            Assert.Contains<Goal>(goal, tomorrowsGoals);
        }

        [Fact]
        public async Task AddGoalAsync_ShouldFail_IfGoalTextIsEmpty()
        {
            // Arrange
            var goal = new Goal("");

            // Act & Assert
            await Assert.ThrowsAnyAsync<Exception>( async () => await _repository.AddGoalAsync(goal) );
        }

        [Fact]
        public async Task AddGoalAsync_SetsDailyIdsAccordingly_WhenNewGoalsAreAdded()
        {
            // Arrange
            var goal1 = new Goal("Goal 1 created for daily Id test");
            var goal2 = new Goal("Goal 2 created for daily Id test");
            var goal3 = new Goal("Goal 3 created for daily Id test");
            int i = 1;

            // Act
            await _repository.AddGoalAsync(goal1);
            await _repository.AddGoalAsync(goal2);
            await _repository.AddGoalAsync(goal3);

            var goalsFromDay = await _repository
                .GetGoalsFromDay(DateOnly.FromDateTime(DateTime.Today.AddDays(1)));

            // Assert
            foreach (var goal in goalsFromDay)
            {
                Assert.Equal(i, goal.dailyID);
                i++;
            }
        }
    }
}