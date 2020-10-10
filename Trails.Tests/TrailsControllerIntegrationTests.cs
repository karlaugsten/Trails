
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

        // Assert
    }
  }
}