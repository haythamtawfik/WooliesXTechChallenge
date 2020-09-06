using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WooliesXChallenge.Api;
using WooliesXChallenge.Api.Features.Common;
using Xunit;

namespace WooliesXChallenge.Tests
{
    public class TestContext : IAsyncLifetime
    {
        public HttpClient WooliesXApiClient { get; set; }
        public IHost Host { get; set; }

        public TestContext()
        {
            SetupContext().GetAwaiter().GetResult();
        }

        private async Task SetupContext()
        {


            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webhost =>
                {
                    webhost.UseTestServer();
                    webhost.UseStartup<Startup>();
                })
                .ConfigureServices(sc =>
                {
                    sc.ConfigureAll<ApiSettings>(options=>
                    {
                        options.Name = "Haytham Tawfik";
                        options.Token = "45ab025d-e470-4a0b-9d98-2b7da52fd137";
                    });
                    sc.ConfigureAll<ResourceApiSettings>(options =>
                        {
                            options.Url = "http://dev-wooliesx-recruitment.azurewebsites.net/api/resource";
                        });
                });
            Host = await hostBuilder.StartAsync();
            WooliesXApiClient = Host.GetTestClient();

        }
        public async Task InitializeAsync()
        {
        }

        public async Task DisposeAsync()
        {
        }
    }
}
