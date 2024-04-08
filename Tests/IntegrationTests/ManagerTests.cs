using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using PM.BL;
using PM.BL.Domain;
using PM.DAL;
using Tests.IntegrationTests.Config;

namespace Tests.IntegrationTests;

public class ManagerTests : IClassFixture<ExtendedWebApplicationFactoryWithMockAuth<Program>>
{
    private readonly ExtendedWebApplicationFactoryWithMockAuth<Program> _factory;

    public ManagerTests(ExtendedWebApplicationFactoryWithMockAuth<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void AddEpisode_ValidData_ShouldAddEpisode()
    {
        using (IServiceScope scope = _factory.Services.CreateScope())
        {
            var mgr = scope.ServiceProvider.GetRequiredService<IManager>();
            var repo = scope.ServiceProvider.GetRequiredService<IRepository>();

            var title = "Test Episode";
            var duration = TimeSpan.FromMinutes(30);
            var episodeNumber = 1;
            var category = Category.UNKNOWN;
            var listeners = 1000;

            var result = mgr.AddEpisode(title, duration, episodeNumber, category, listeners);

            Assert.Equal(title, result.EpisodeTitle);
            Assert.Equal(duration, result.Duration);
            Assert.Equal(episodeNumber, result.EpisodeNumber);
            Assert.Equal(category, result.Category);
            Assert.Equal(listeners, result.Listeners);

        }
    }

    [Fact]
    public void AddEpisodewithUser_InvalidData_ShouldThrowValidationException()
    {
        using (IServiceScope scope = _factory.Services.CreateScope())
        {
            // Arrange
            var title = "Test title";
            var duration = TimeSpan.FromMinutes(30);
            var episodeNumber = 1001; // episode number should be between 1 and 1000, so exception is thrown
            var category = Category.UNKNOWN;
            var listeners = 1000;

            var mgr = scope.ServiceProvider.GetRequiredService<IManager>();
            var repo = scope.ServiceProvider.GetRequiredService<IRepository>();

            // Act & Assert
            Assert.Throws<ValidationException>(() =>
                mgr.AddEpisode(title, duration, episodeNumber, category, listeners));
        }
    }

    [Fact]
    public void AddGuest_ValidData_ShouldAddGuest()
    {
        using (IServiceScope scope = _factory.Services.CreateScope())
        {
            var mgr = scope.ServiceProvider.GetRequiredService<IManager>();
            var repo = scope.ServiceProvider.GetRequiredService<IRepository>();

            var name = "Test Guest";
            var expertise = "Expertise";
            var gender = Gender.Female;

            var result = mgr.AddGuest(name, expertise, gender);
            
            Assert.Equal(name, result.Name);
            Assert.Equal(expertise, result.Expertise);

        }
    }
    
    [Fact]
    public void AddGuest_InvalidData_ShouldNotAddGuest()
    {
        using (IServiceScope scope = _factory.Services.CreateScope())
        {
            var mgr = scope.ServiceProvider.GetRequiredService<IManager>();
            var repo = scope.ServiceProvider.GetRequiredService<IRepository>();

            var name = "Test Guest";
            var expertise = "Test Expertise"; // not correct expertise does not match regex set in validation [a-zA-Z]+
            var gender = Gender.Female;

            Assert.Throws<ValidationException>(() => mgr.AddGuest(name, expertise, gender));

        }
    }

}