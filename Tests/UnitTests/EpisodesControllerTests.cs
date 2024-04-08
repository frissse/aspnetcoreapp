using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using PM.BL;
using PM.BL.Domain;
using PM.DAL;
using PM.UI.Web.MVC.Controllers;
using PM.UI.Web.MVC.Controllers.Api;
using PM.UI.Web.MVC.Models.Dto;

namespace Tests.UnitTests;

public class EpisodesControllerTests
{
    [Fact]
    public void GetEpisode_GivenValidId_ShouldReturnOk_WithEpisodeData()
    {
        int episodeId = 1;
        var user1 = new IdentityUser()
        {
            Id = "User1Id",
            UserName = "user1@app.com",
            Email = "user1@app.com"
        };

        var episode = new Episode("The Joe Rogan Podcast #123 with Elon Musk", new TimeSpan(0, 3, 43, 20),
            Category.Technology, 123, 874320, user1);

        var mockMgr = new Mock<IManager>();
        mockMgr.Setup(m => m.GetEpisode(episodeId))
            .Returns(episode);

        var controller = new EpisodesController(mockMgr.Object);

        var result = controller.GetEpisode(episodeId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedEpisode = Assert.IsType<Episode>(okResult.Value);

        Assert.Equal(episode.Id, returnedEpisode.Id);
        Assert.Equal(episode.EpisodeTitle, returnedEpisode.EpisodeTitle);
        Assert.Equal(episode.Duration, returnedEpisode.Duration);
        Assert.Equal(episode.Category, returnedEpisode.Category);
        Assert.Equal(episode.Listeners, returnedEpisode.Listeners);

    }
    
    [Fact]
    public void GetEpisode_GivenInvalidId_ShouldReturnNoContent()
    {
        int episodeId = -1;
        var mockMgr = new Mock<IManager>();
        mockMgr.Setup(m => m.GetEpisode(episodeId)).Returns((Episode)null);
        
        var controller = new EpisodesController(mockMgr.Object);
        
        var result = controller.GetEpisode(episodeId);
        
        Assert.IsType<NoContentResult>(result);
        mockMgr.VerifyAll();
    }
    
    [Fact]
    public void UpdateEpisode_GivenValidId_WithAuthorizedUser_ShouldReturnNoContent()
    {
        int episodeId = 1;
        var episode = new Episode()
        {
            Id = 1,
            EpisodeTitle = "The Joe Rogan Podcast #123 with Elon Musk",
            Duration = new TimeSpan(0, 3, 43, 20),
            Category = Category.Technology,
            EpisodeNumber = 123,
            Listeners = 874320,
            User = new IdentityUser()
            {
                Id = "User1Id",
                UserName = "user1@app.com",
                Email = "user1@app.com"
            }
        };
        var mockMgr = new Mock<IManager>();

        mockMgr.Setup(m => m.GetEpisode(episodeId)).Returns(episode);

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, "User1Id"),
            new Claim(ClaimTypes.Name, "user1@app.com"),
            new Claim(ClaimTypes.Email, "user1@app.com")
        };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var user = new ClaimsPrincipal(identity);
        
        var controller = new EpisodesController(mockMgr.Object);
    
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        var epDto = new EditEpisodeDto()
        {
            Id = 1,
            Listeners = 90000
        };
    
        var result = controller.UpdateEpisode(episodeId, epDto);
    
        Assert.IsType<NoContentResult>(result);
        mockMgr.VerifyAll();
    }

    [Fact]
    public void Edit_AuthorizedButNotOwner_GivenValidData_ShouldReturnForbidden()
    {
        int episodeId = 1;
        var episode = new Episode()
        {
            Id = 1,
            EpisodeTitle = "The Joe Rogan Podcast #123 with Elon Musk",
            Duration = new TimeSpan(0, 3, 43, 20),
            Category = Category.Technology,
            EpisodeNumber = 123,
            Listeners = 874320,
            User = new IdentityUser()
            {
                Id = "User1Id",
                UserName = "user1@app.com",
                Email = "user1@app.com"
            }
        };
        var mockMgr = new Mock<IManager>();

        mockMgr.Setup(m => m.GetEpisode(episodeId)).Returns(episode);

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, "User2Id"),
            new Claim(ClaimTypes.Name, "user2@app.com"),
            new Claim(ClaimTypes.Email, "user2@app.com")
        };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var user = new ClaimsPrincipal(identity);

        var controller = new EpisodesController(mockMgr.Object);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        var epDto = new EditEpisodeDto()
        {
            Id = 1,
            Listeners = 90000
        };

        var result = controller.UpdateEpisode(episodeId, epDto);

        Assert.IsType<ForbidResult>(result);
        mockMgr.VerifyAll();
    }

    [Fact]
    public void Edit_Unauthorized_GivenValidData_ShouldReturnUnauthorized()
    {
        int episodeId = 1;
        var episode = new Episode()
        {
            Id = 1,
            EpisodeTitle = "The Joe Rogan Podcast #123 with Elon Musk",
            Duration = new TimeSpan(0, 3, 43, 20),
            Category = Category.Technology,
            EpisodeNumber = 123,
            Listeners = 874320,
            User = new IdentityUser()
            {
                Id = "User1Id",
                UserName = "user1@app.com",
                Email = "user1@app.com"
            }
        };
        var mockMgr = new Mock<IManager>();

        mockMgr.Setup(m => m.GetEpisode(episodeId)).Returns(episode);

        var claims = new List<Claim>();
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var user = new ClaimsPrincipal(identity);

        var controller = new EpisodesController(mockMgr.Object);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        var epDto = new EditEpisodeDto()
        {
            Id = 1,
            Listeners = 90000
        };

        var result = controller.UpdateEpisode(episodeId, epDto);

        Assert.IsType<UnauthorizedResult>(result);
        mockMgr.VerifyAll();
    }

}