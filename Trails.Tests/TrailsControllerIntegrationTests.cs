
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Trails.Tests
{
  public class TrailsControllerIntegrationTests : BaseIntegrationTests {

    public TrailsControllerIntegrationTests(WebApplicationFactory<Trails.Startup> factory) : base(factory) {}

    [Fact]
    public async Task Get_TestTrails()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await SendGet(client, "/api/trails");

        // Assert
        var responseBody = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
        var trails = JsonConvert.DeserializeObject<List<Trail>>(responseBody);
        Assert.NotEmpty(trails);
    }
  }
}