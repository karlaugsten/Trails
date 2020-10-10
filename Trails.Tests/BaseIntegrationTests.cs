using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Newtonsoft.Json.Serialization;
using System.IO;
using System;

namespace Trails.Tests
{
  public class BaseIntegrationTests : IClassFixture<WebApplicationFactory<Trails.Startup>> {
    protected readonly WebApplicationFactory<Trails.Startup> _factory;

    public BaseIntegrationTests(WebApplicationFactory<Trails.Startup> factory)
    {
        _factory = factory;

        var testAssemblyPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        // Remove the "\\bin\\Debug\\netcoreapp2.1"
        var testPath = testAssemblyPath.Substring(0, testAssemblyPath.LastIndexOf("/bin/", StringComparison.Ordinal));
        var solutionPath = Directory.GetParent(testPath).FullName;
        var clientReactPath = Path.Join(solutionPath, "Trails");

        // Important to ensure that npm loads and is pointing to correct directory
        Directory.SetCurrentDirectory(clientReactPath);
    }

    

    protected async Task<HttpResponseMessage> SendPost<TContent>(HttpClient client, string url, TContent content) {
      DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };

        string stringContent = JsonConvert.SerializeObject(content, new JsonSerializerSettings
        {
            ContractResolver = contractResolver,
            Formatting = Formatting.Indented
        });
        StringContent httpContent = new StringContent(stringContent);
        httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        return await client.PostAsync(url, httpContent);
    }

  }
}