using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Tests.IntegrationTests.Config;

namespace Tests.IntegrationTests;

public class HostsControllerTests : IClassFixture<ExtendedWebApplicationFactoryWithMockAuth<Program>>
{
    private readonly ExtendedWebApplicationFactoryWithMockAuth<Program> _factory;

    public HostsControllerTests(ExtendedWebApplicationFactoryWithMockAuth<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public void Index_ShouldReturnHosts()
    {
        //Arrange
        var client = _factory.CreateClient();
        var url = "/api/Hosts";
        
        //Act
        var response = client.GetAsync(url).Result;
        var responseBody = response.Content.ReadAsStringAsync().Result;
        
        //Assert
        // checking of one of the hosts names is in json response body
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);    
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
        Assert.Contains("Joe Rogan", responseBody);
    }

    [Fact]
    public void Add_GivenValidData_ShouldReturnCreated()
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
        var url = "/api/Hosts";
        var httpContent =
            new StringContent("{\"name\": \"TestHost\", \"yearFirstPublished\": 2022, \"rating\": 5, \"gender\": 1}",
                Encoding.UTF8, "application/json");

        var response = client.PostAsync(url, httpContent).Result;

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }


}
