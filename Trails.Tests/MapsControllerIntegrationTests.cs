
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Trails.Tests
{
  public class MapsControllerIntegrationTests : BaseIntegrationTests {

    public MapsControllerIntegrationTests(WebApplicationFactory<Trails.Startup> factory) : base(factory) {}

    [Fact]
    public async Task Get_TestMaps()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act

        // Assert
    }
  }
}