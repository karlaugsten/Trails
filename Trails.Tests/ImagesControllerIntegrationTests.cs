
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Trails.Tests
{
  public class ImagesControllerIntegrationTests : BaseIntegrationTests {


    public ImagesControllerIntegrationTests(WebApplicationFactory<Trails.Startup> factory) : base(factory) {}

    [Fact]
    public async Task Get_TestImages()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act

        // Assert
    }
  }
}