using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Moq;
using PM.BL;
using PM.BL.Domain;
using PM.DAL;

namespace Tests.UnitTests;

public class ManagerTests
{
    [Fact]
    public void AddGuests_GivenValidData_ShouldReturnNewGuests()
    {
        // Arrange
        var mockRepo = new Mock<IRepository>();
        var mgr = new Manager(mockRepo.Object);
        mockRepo.Setup(r => r.CreateGuest(It.IsAny<Guest>())).Verifiable(Times.Once);

        var name = "John Doe";
        var expertise = "TestExpertise";
        var gender = Gender.Male;

        // Act
        var book = mgr.AddGuest(name, expertise, gender);
        // Assert
        Assert.Equal(name, book.Name);
        Assert.Equal(expertise, book.Expertise);

    }

    [Fact]
    public void AddGuests_GivenInvalidData_ShouldThrowException()
    {
        // Arrange
        var mockRepo = new Mock<IRepository>();
        var mgr = new Manager(mockRepo.Object);
        mockRepo.Setup(r => r.CreateGuest(It.IsAny<Guest>())).Throws<ValidationException>();

        // not matching regular expression
        var name = "John";
        var expertise = "TestExpertise";
        var gender = Gender.Male;

        // Act
        var action = () => { mgr.AddGuest(name, expertise, gender); };
        // Assert
        Assert.Throws<ValidationException>(action);
    }

    [Fact]
    public void AddEpisodeWithUser_GivenValidData_ShouldReturnEpisode()
    {
        var mockRepo = new Mock<IRepository>();
        var mgr = new Manager(mockRepo.Object);
        mockRepo.Setup(r => r.CreateEpisode(It.IsAny<Episode>())).Verifiable(Times.Once);
        
        var episode = new Episode
        {
           EpisodeTitle = "TestEpisode",
           Duration = new TimeSpan(0, 30, 0),
           EpisodeNumber = 101,
           Category = Category.Business,
           Listeners = 100,
           User = new IdentityUser()
           {
                Id = "User6Id",
                UserName = "user6@app.com",
                Email = "user6@app.com"
           }
        };
        
        var result = mgr.AddEpisodewithUser(episode.EpisodeTitle, episode.Duration.GetValueOrDefault(), episode.EpisodeNumber, episode.Category, episode.Listeners, episode.User);
        
        Assert.Equal(episode.EpisodeTitle, result.EpisodeTitle);
        Assert.Equal(episode.Duration, result.Duration);
        Assert.Equal(episode.EpisodeNumber, result.EpisodeNumber);
        Assert.Equal(episode.Category, result.Category);
        Assert.Equal(episode.Listeners, result.Listeners);
        Assert.Equal(episode.User, result.User);
    }

}