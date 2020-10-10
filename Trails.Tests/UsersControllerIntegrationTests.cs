
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Trails.Tests
{
  public class UsersControllerIntegrationTests : BaseIntegrationTests {


    public UsersControllerIntegrationTests(WebApplicationFactory<Trails.Startup> factory) : base(factory) {}

    [Fact]
    public async Task Post_CreateUserReturnsRegistrationRequest()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var registration = new RegistrationRequest() {
          Password = "test123",
          ConfirmPassword = "test123",
          Email = "karla123@gmail.com",
          ConfirmEmail = "karla123@gmail.com",
          Username = "karl123"
        };

        var response = await SendPost(client, "/api/users/register", registration);

        // Assert
        var responseBody = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
        var definition = new { token = "" };
        var responseObject = JsonConvert.DeserializeAnonymousType(responseBody, definition);
        Assert.NotEmpty(responseObject.token);
    }
  }
}