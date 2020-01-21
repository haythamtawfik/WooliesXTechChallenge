using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WooliesXChallenge.Api.Features.Common;
using WooliesXChallenge.Api.Features.Sorting;
using WooliesXChallenge.Api.Features.Users;

namespace WooliesXChallenge.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHealthChecks();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "WooliesX Tech Challenge API", Version = "v1"});
            });

            services.TryAddSingleton<IUserService, UserService>();
            services.TryAddSingleton<ISortService, SortService>();

            services.AddHttpClient<IResourcesApiClient, ResourcesApiClient>();

            services.Configure<ApiSettings>(Configuration.GetSection("ApiSettings"));
            services.Configure<ResourceApiSettings>(Configuration.GetSection("ResourceApiSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "WooliesX Tech Challenge API"); });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHealthChecks("/health");
        }
    }
}
