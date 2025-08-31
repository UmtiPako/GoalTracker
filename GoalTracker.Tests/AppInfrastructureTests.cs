using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using System.Security.Principal;
using GoalTracker.Infrastructure.Repositories;
using GoalTracker.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using GoalTracker.Infrastructure.Persistence;
using GoalTracker.Domain.Models;

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

            Assert.Equal("Umti", result);
        }
    }
}