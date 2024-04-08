using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using PM.BL;
using PM.BL.Domain;
using PM.DAL;
using PM.UI.Web.MVC.Controllers;
using PM.UI.Web.MVC.Models;
using Tests.IntegrationTests.Config;

namespace Tests.IntegrationTests;

public class EpisodeControllerTests : IClassFixture<ExtendedWebApplicationFactoryWithMockAuth<Program>>
{
    private readonly ExtendedWebApplicationFactoryWithMockAuth<Program> _factory;

    public EpisodeControllerTests(ExtendedWebApplicationFactoryWithMockAuth<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void Details_GivenValidId_ShouldReturnDetailsPage()
    {
        var client = _factory
            .CreateClient(new WebApplicationFactoryClientOptions()
            {
                AllowAutoRedirect = false
            });
        var url = "/Episode/Details/1";
        
        var response = client.GetAsync(url).Result;
        var responseBody = response.Content.ReadAsStringAsync().Result;
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Details of Episode with Id", responseBody);
    }

    [Fact]
    public void Details_GivenInvalidId_ShouldReturnNotFound()
    {
        var client = _factory.CreateClient();
        var url = "/Episode/Details/1000";
        
        var response = client.GetAsync(url).Result;
        
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);


    }

    [Fact]
    public void Add_GivenValidData_Authorized_ShouldReturnRedirectToDetails()
    {
        var client = _factory
            .SetAuthenticatedUser(
                new Claim(ClaimTypes.NameIdentifier, "User1Id"),
                new Claim(ClaimTypes.Name, "user1@app.com"),
                new Claim(ClaimTypes.Email, "user1@app.com")
            )
            .CreateClient(new WebApplicationFactoryClientOptions()
            {
                AllowAutoRedirect = false
            });
        var url = "/Episode/Add";
        var httpContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("EpisodeTitle", "Test Episode"),
            new KeyValuePair<string, string>("Duration", "00:30:00"),
            new KeyValuePair<string, string>("EpisodeNumber", "1"),
            new KeyValuePair<string, string>("Category", "UNKNOWN"),
            new KeyValuePair<string, string>("Listeners", "1000")
        });
        
        var response = client.PostAsync(url, httpContent).Result;
        
        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        var redirectUrl = response.Headers.Location?.OriginalString;
        Assert.StartsWith("/Episode/Details", redirectUrl);
    }

    [Fact]
    public void Add_GivenInvalidData_ShouldReturnAddPage()
    {
        var client = _factory
            .SetAuthenticatedUser(
                new Claim(ClaimTypes.NameIdentifier, "User1Id"),
                new Claim(ClaimTypes.Name, "user1@app.com"),
                new Claim(ClaimTypes.Email, "user1@app.com")
            )
            .CreateClient(new WebApplicationFactoryClientOptions()
            {
                AllowAutoRedirect = false
            });
        var url = "/Episode/Add";
        var httpContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("EpisodeTitle", "Test Episode"),
            new KeyValuePair<string, string>("Duration", "00:30:00"),
            new KeyValuePair<string, string>("EpisodeNumber", "1001"), // wrong episode 
            new KeyValuePair<string, string>("Category", "UNKNOWN"),
            new KeyValuePair<string, string>("Listeners", "1000")
        });
        
        var response = client.PostAsync(url, httpContent).Result;
        var responseBody = response.Content.ReadAsStringAsync().Result;
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Add Episode", responseBody);
        
    }
}