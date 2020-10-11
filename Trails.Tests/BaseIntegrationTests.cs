using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Newtonsoft.Json.Serialization;
using System.IO;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Trails.Tests
{
  public class BaseIntegrationTests : IClassFixture<WebApplicationFactory<Trails.Startup>>, IDisposable {
    protected readonly WebApplicationFactory<Trails.Startup> _factory;

    protected User _authenticatedUser;
    protected string _authenticatedUserPassword;

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
        
        _authenticatedUserPassword = "test123!";
        _authenticatedUser = GetOrCreateUser(Guid.NewGuid().ToString(), _authenticatedUserPassword).Result;
    }

    protected async Task<HttpResponseMessage> SendGet(HttpClient client, string url) {
      DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };
        
        return await client.GetAsync(url);
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

    protected async Task<User> GetOrCreateUser(string username, string password) {
      var client = _factory.CreateClient();
      var email = username + "@runnify.ca";
      using (var scope = _factory.Server.Host.Services.CreateScope())
      {
        UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        User existingUser = await userManager.FindByEmailAsync(email);
        if(existingUser == null) {
          var newUser = new User(){
            UserName = username,
            Email = email,
            EmailConfirmed = true
          };
          newUser.PasswordHash = userManager.PasswordHasher.HashPassword(newUser, password);
          var result = await userManager.CreateAsync(newUser);
          if (result.Succeeded) {
            return await userManager.FindByEmailAsync(email);
          }
        } else {
          return existingUser;
        }
      }
      return null;
    }

     protected async Task DeleteUser(string email) {
      var client = _factory.CreateClient();
      using (var scope = _factory.Server.Host.Services.CreateScope())
      {
        UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        User existingUser = await userManager.FindByEmailAsync(email);
        if(existingUser == null) {
         return;
        }
        await userManager.DeleteAsync(existingUser);
      }
    }

    public void Dispose()
    {
      // Delete the test user.
      DeleteUser(_authenticatedUser.Email).Wait();
    }
  }
}