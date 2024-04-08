using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PM.BL;
using PM.BL.Domain;
using PM.DAL;
using PM.UI.Web.MVC.Controllers;
using PM.UI.Web.MVC.Controllers.Api;
using Tests.IntegrationTests.Config;

namespace Tests.IntegrationTests;

public class EpisodesControllerTests : IClassFixture<ExtendedWebApplicationFactoryWithMockAuth<Program>>
{
    private readonly ExtendedWebApplicationFactoryWithMockAuth<Program> _factory;

    public EpisodesControllerTests(ExtendedWebApplicationFactoryWithMockAuth<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void Edit_Authorized_GivenValidData_ShouldReturnNoContent()
    {
        // user1 is owner of episode with Id 1
        var id = 1;
        
        var client = _factory.SetAuthenticatedUser(
                new Claim(ClaimTypes.NameIdentifier, "User1Id"),
                new Claim(ClaimTypes.Name, "user1@app.com"),
                new Claim(ClaimTypes.Email, "user1@app.com")
            )
            .CreateClient(new WebApplicationFactoryClientOptions()
            {
                AllowAutoRedirect = false
            });
        
        var url = $"/api/Episodes/{id}";
        var httpContent = new StringContent("{\"listeners\": 1000}", Encoding.UTF8, "application/json"); 
        
        var response = client.PutAsync(url, httpContent).Result;
        
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        
        
    }

    [Fact]
    public void Edit_Unauthorized_GivenValidData_ShouldReturnForbidden()
    {
        // user1 is owner of episode with Id 1
        var id = 1;

        var client = _factory.SetAuthenticatedUser(
                new Claim(ClaimTypes.NameIdentifier, "User2Id"),
                new Claim(ClaimTypes.Name, "user2@app.com"),
                new Claim(ClaimTypes.Email, "user2@app.com")
            )
            .CreateClient(new WebApplicationFactoryClientOptions()
            {
                AllowAutoRedirect = false
            });
        
        var url = $"/api/Episodes/{id}";
        var httpContent = new StringContent("{\"listeners\": 1000}", Encoding.UTF8, "application/json");
        
        var response = client.PutAsync(url, httpContent).Result;
        
        Assert.Equal(System.Net.HttpStatusCode.Forbidden, response.StatusCode);
        
    }

    [Fact]
    public void Edit_Authorized_GivenInvalidData_ShouldReturnNotFound()
    {
        // user1 is owner of episode with Id 1
        var id = -1;

        var client = _factory.SetAuthenticatedUser(
                new Claim(ClaimTypes.NameIdentifier, "User2Id"),
                new Claim(ClaimTypes.Name, "user2@app.com"),
                new Claim(ClaimTypes.Email, "user2@app.com")
            )
            .CreateClient(new WebApplicationFactoryClientOptions()
            {
                AllowAutoRedirect = false
            });

        var url = $"/api/Episodes/{id}";
        var httpContent = new StringContent("{\"listeners\": 1000}", Encoding.UTF8, "application/json");

        var response = client.PutAsync(url, httpContent).Result;
    }
}