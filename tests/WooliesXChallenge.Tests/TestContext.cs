using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
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
                        options.Token = "f0bd6e11-7ebf-42ee-959c-f92844ad0bbd";
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
